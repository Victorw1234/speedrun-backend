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

            var listOfTimes = _context.Times.Where(q => q.User == user).Include("CategoryExtension.Game").ToList();
            var grouped = listOfTimes.GroupBy(q => q.CategoryExtension.Game.Title).ToList();
            var returnList = new List<object>();
            foreach(var p in grouped) // group by game
            {
                Debug.WriteLine(p.Key);
                var listOfTimesForGame = new List<object>();
                for (int i = 0; i < p.Count(); i++) // loop through all the runs for this game
                {
                    listOfTimesForGame.Add(new { category = p.ElementAt(i).CategoryExtension.Title,
                                                 time = p.ElementAt(i).RunTime});
                    
                }
                var obj = new { game = p.Key, times = listOfTimesForGame };
                returnList.Add(obj); 
            }


            return Ok(returnList);
        }
    }
}
