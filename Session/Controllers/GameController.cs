using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.Model;
using Session.Model.ViewModels;
using Session.Logic;

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
        /*Returns full list of games*/
        public IActionResult Game()
        {
            List<Game> games = _context.Games.ToList<Game>();
            return Ok(games);
        }
        [Route("{id}")]
        /*Returns 1 game by Id
         Example: localhost/api/Game/1*/
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
        /*Returns 1 game by url-string
          Example: localhost/api/Game/ByString/halo_3 (this is pretty ugly)
          returns halo 3*/
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
        /*
         Only a moderator for the entire site
         can add a game.
         */
        [Route("AddGame")]
        [HttpPost]
        public IActionResult AddGame(AddGameViewModel game)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = UserLogic.GetUser(_context, HttpContext.Session.GetString("username"));
            if (user is null)
                return StatusCode(401,new {error="Not logged in"});
            
            bool isMod = UserLogic.isSiteModerator(_context, user.Id);

            if (!isMod)
                return StatusCode(401, new { error = "Not a site moderator" });

            /*Verification done*/

            /* Now add game to database.*/
            Game g = new Game();
            g.Title = game.Title;
            g.ImageName = "qmark.png";
            _context.Games.Add(g);
            _context.SaveChanges();

            GameAdmin gameAdmin = new GameAdmin();
            gameAdmin.GameId = g.Id;
            gameAdmin.UserId = user.Id;
            _context.GameAdmins.Add(gameAdmin);

            _context.SaveChanges();

            return Ok();
        }

    }
}
