using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session.Logic;
using Session.Model;
using Session.Model.ViewModels;

namespace Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SignupController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult SignUp(RegisterUserViewModel inputUser)
        {
            if(ModelState.IsValid)
            {
                // kolla unik username
                if (!uniqueUsername(inputUser.Username))
                {
                    return BadRequest(new {  signedUp = false,error = "not a unique username" });
                }

                User user = new User();
                user.Username = inputUser.Username;
                var passwordAndSalt = PasswordHashing.hashPassword(inputUser.Password);
                user.Password = passwordAndSalt.Item1;
                user.salt = passwordAndSalt.Item2;
                
                
                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok(new { signedUp = true,msg = "Signed up successfully" });
            }
            return BadRequest();
        }

        public bool uniqueUsername(string username)
        {
            List<User> usersWithSameUsername = _context.Users.Where(q => q.Username == username).ToList();
            return (usersWithSameUsername.Count() == 0);
        }

        
    }
}
