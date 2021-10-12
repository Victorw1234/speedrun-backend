using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session.Logic;
using Session.Model;

namespace Session.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            string msg ="no username";
            if (HttpContext.Session.GetString("username") != null)
            {
                msg = HttpContext.Session.GetString("username");
                bool siteMod = _context.Users.Where(user => user.Username == msg).FirstOrDefault().SiteModerator; // lookup if sitemod
                return Ok( new {isLoggedIn = true,username = msg,siteModerator = siteMod});
            }
            return BadRequest( new {isLoggedIn = false, msg,siteModerator = false } );
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (AuthenticateUser(user))
            {
                HttpContext.Session.SetString("username", user.Username);
                HttpContext.Session.SetInt32("id", user.Id); // detta är broken
                return Ok( new { success = true,username = user.Username });
            }
            return BadRequest(new { success = false});
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            string msg;
            if (HttpContext.Session.GetString("username") != null)
            {
                HttpContext.Session.Remove("username");
                msg = "Successfully Logged Out";
                return Ok(new { msg});
            }
            msg = "Not logged in";
            return BadRequest(new { msg });
        }

        public bool AuthenticateUser(User user)
        {
            var userList = _context.Users.Where(q => q.Username == user.Username);
            if (userList.Count() != 1)
                return false;

            var userInDB = userList.FirstOrDefault();

            string hashed = PasswordHashing.hashPassword(user.Password,userInDB.salt);

            return (hashed == userInDB.Password);
        }
    }
}
