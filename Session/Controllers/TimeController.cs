using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.Attributes;
using Session.Logic;
using Session.Model;
using Session.Model.ViewModels;

namespace Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public TimeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("{urlGame}/{urlCategoryExtension}")]
        public IActionResult GameCategoryExtension(string urlGame, string urlCategoryExtension)
        {
            Game game = GameLogic.GetGameByUrl(_context,urlGame);
            CategoryExtension categoryExtension = CategoryExtensionLogic.GetCategoryExtension(_context,game.Id, urlCategoryExtension);

            IEnumerable<Time> times = _context.Times.OrderBy(q => q.RunTime)
                                                    .Include("User")
                                                    .Where(q => q.CategoryExtensionId == categoryExtension.Id);


            return Ok(times.Select(x => new { x.Id, x.Link,x.DateSet, x.RunTime, x.User.Username }));
        }
        [HttpPost]
        [CustomAuthorize]
        [Route("{urlGame}/{urlCategoryExtension}")]
        public async Task<IActionResult> AddTimeAsync(AddTimeViewModel time, string urlGame, string urlCategoryExtension)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { timeAdded = false });

            Game game = GameLogic.GetGameByUrl(_context,urlGame);
            CategoryExtension ce = CategoryExtensionLogic.GetCategoryExtension(_context,game.Id, urlCategoryExtension);
            string username = HttpContext.Session.GetString("username");

            // remove their earlier time, if they had any.
            // you could change this in the future if you want
            // to maintain a history of their PBs

            

            Time t = _context.Times.Where(q => q.CategoryExtensionId == ce.Id && // their old time
                                 q.User.Username == username)
                                    .FirstOrDefault();
            if (t != null)
            {
                _context.Times.Remove(t);
            }

            // new time

            Time newTime = new Time();
            newTime.CategoryExtensionId = ce.Id;
            newTime.Link = time.Link;
            newTime.RunTime = new DateTime(1,1,1,time.RunTime.Hour, time.RunTime.Minute, time.RunTime.Second, time.RunTime.Millisecond);
            newTime.DateSet = DateTime.Now;
            newTime.UserId = Session.Model.User.GetUser(_context,username).Id;

            await _context.Times.AddAsync(newTime);
            await _context.SaveChangesAsync();

            return Ok(new { timeAdded = true,msg = "added time successfully"});

        }

       

        

    }
}
