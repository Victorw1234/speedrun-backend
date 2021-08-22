using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.Model;

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
        public IActionResult GameCategoryExtension(string urlGame,string urlCategoryExtension)
        {
            Game game = _context.Games.ToList<Game>()
                                      .Where(q => q.UrlTitle == urlGame)
                                      .FirstOrDefault();
            CategoryExtension categoryExtension = _context.CategoryExtensions.ToList<CategoryExtension>()
                                                                             .Where(q => q.UrlTitle == urlCategoryExtension && 
                                                                                         q.GameId == game.Id)
                                                                             .FirstOrDefault();

            IEnumerable<Time> times = _context.Times.Include("User")
                                                    .Where(q => q.CategoryExtensionId == categoryExtension.Id);


            return Ok(times.Select(x => new { x.Id, x.Link, x.RunTime, x.User.Username }));
        }
    }
}
