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

        /*
         Returns to client if they are logged in.
         Also returns whether or not the client is a site mod.
         {isLoggedIn,username,siteModerator}
         */
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

        /*
         Logs user in.
         Assuming successful login, return {success, username}
         */
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (AuthenticateUser(user))
            {
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
                HttpContext.Session.Remove("id");
                msg = "Successfully Logged Out";
                return Ok(new { msg});
            }
            msg = "Not logged in";
            return BadRequest(new { msg });
        }
        /*
        looks up if username exists. Then checks if password is correct by hashing and comparing. 
        */
        public bool AuthenticateUser(User user)
        {
            var userList = _context.Users.Where(q => q.Username == user.Username); 
            if (userList.Count() != 1) 
                return false;

            var userInDB = userList.FirstOrDefault();
            
            string hashed = PasswordHashing.hashPassword(user.Password,userInDB.salt);

            if (hashed == userInDB.Password)
            {
                HttpContext.Session.SetString("username", userInDB.Username);
                HttpContext.Session.SetInt32("id", userInDB.Id); 
                return true;
            }
            return false;
        }
    }
}
