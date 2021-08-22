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
    public class GameController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        [Route("")]
        public IActionResult Game()
        {
            List<Game> games = _context.Games.ToList<Game>();
            return Ok(games);
        }
        [Route("{id}")]
        public IActionResult GameById(int id)
        {
            Game game = _context.Games
                                .Include("CategoryExtensions")
                                .Where(q => q.Id == id)
                                .FirstOrDefault();
            if (game == null)
            {
                return BadRequest(new {Error = "No game with that Id exist" });
            }
            return Ok(game);
        }
        [Route("[action]/{urlTitle}")]
        public IActionResult ByString(string urlTitle)
        {
            int gameId;
            try
            {
                gameId = _context.Games.ToList()
                                       .Where(q => q.UrlTitle == urlTitle)
                                       .FirstOrDefault()
                                       .Id;

            }
            catch (System.NullReferenceException e)
            {
                return BadRequest(e);
            }
            return RedirectToAction("GameById", new { id = gameId });
        }

    }
}
