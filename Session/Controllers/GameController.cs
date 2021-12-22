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
            List<GameAdmin> u = _context.Games.Include("Admins").Include("Admins.User").Where(q => q.Id == id).FirstOrDefault().Admins.ToList<GameAdmin>();
            List<string> gameAdminUsernames = new List<string>();
            for (int i  = 0; i < u.Count(); i++)
            {
                gameAdminUsernames.Add(u.ElementAt(i).User.Username);
            }
            Game game = _context.Games
                                .Include("CategoryExtensions")
                                .Include("Admins").Include("Admins.User")
                                .Where(q => q.Id == id)
                                .FirstOrDefault();
            if (game == null)
            {
                return BadRequest(new {Error = "No game with that Id exist" });
            }
            return Ok(
                new
                {
                    success = true,
                    categoryExtensions = game.CategoryExtensions,
                    imageName = game.ImageName,
                    title = game.Title,
                    urlTitle = game.UrlTitle,
                    admins = gameAdminUsernames
                }
                );
        }
        [Route("[action]/{urlTitle}")]
        /*Returns one game by url-string
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
            catch (NullReferenceException e)
            {
                return BadRequest(new { success = false });
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
                return StatusCode(401,new {status="Not logged in"});
            
            bool isMod = UserLogic.isSiteModerator(_context, user.Id);

            if (!isMod)
                return StatusCode(401, new { status = "Not a site moderator" });

            /*Verification done*/

            /* Now add game to database.*/
            Game g = new Game();
            g.Title = game.Title;
            if (!GameLogic.AddGame(_context,g,user))
            {
                return StatusCode(401, new { status = "That game exist in the db already" });
            }

            return Ok(new { status="Successfully added game to database"});
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult AddGameImage(AddGameImageViewModel vm)
        {
            var user = UserLogic.GetUser(_context, HttpContext.Session.GetString("username"));
            Game game = _context.Games.Where(g => g.Title == vm.GameTitle).FirstOrDefault();
            if (user is null)
                return StatusCode(401, new { status = "Not logged in" });

            bool isGameAdmin = UserLogic.isGameAdmin(_context,user.Id,game.Id);
            if (!isGameAdmin)
                return StatusCode(401, new { status = "Not a game admin for this game" });


            string fileType;
            try
            {
                fileType = vm.Img.Split(";")[0].Split(":")[1];
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            if (fileType == "image/jpeg" || fileType == "image/jpg")
            {
                fileType = "jpg";
            }
            else if (fileType == "image/png")
            {
                fileType = "png";   
            }
            else
            {
                return BadRequest(new { msg = "Invalid data format" });
            }

            
            string filePath = $"C:\\Users\\victo\\source\\repos\\Session\\Session\\Resources\\Images\\{game.UrlTitle}.{fileType}";
            string base64withoutHeader = vm.Img.Split(",")[1];
            System.IO.File.Delete(filePath);
            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64withoutHeader));
            game.ImageName = game.UrlTitle + "." + fileType;
           
            _context.SaveChanges();

            return Ok(new { msg = "Successfully updated image" });
        }



    }
}
