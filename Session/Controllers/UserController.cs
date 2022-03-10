using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.Logic;
using Session.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Ok(new {hello = "hello"});
        }
        [Route("Userdata/{username}")]
        //Returns all the data used in the userpages
        public IActionResult UserData(string username)
        {
            var user = UserLogic.GetUser(_context,username);
            if (user == null)
            {
                return BadRequest(new { success = false });
            }

            var listOfTimes = _context.Times
                .Where(q => q.User == user)
                .Include("CategoryExtension.Game")
                .AsEnumerable()
                .GroupBy(q => q.CategoryExtension.Game.Title)
                .ToList();
            
            var returnList = new List<object>();
            foreach(var p in listOfTimes) // group by game
            {
                var listOfTimesForGame = new List<object>();
                foreach (var item in p)
                {
                    listOfTimesForGame.Add(new
                    {
                        category = item.CategoryExtension.Title,
                        time = item.RunTime
                    });
                }

                var obj = new { game = p.Key, times = listOfTimesForGame };
                returnList.Add(obj); 
            }


            return Ok(returnList);
        }
    }
}
