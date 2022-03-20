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
using Microsoft.Extensions.Configuration;
using Session.Attributes;
using System.Diagnostics;

namespace Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IConfiguration configuration;
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;

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
        public async Task<IActionResult> GameById(int id)
        {

            var gameAdminUsernames = _context.GameAdmins
                                     .Where(item => item.GameId == id)
                                     .Include("User")
                                     .Select(item => item.User.Username);

            Game game = await _context.Games
                                 .Include("CategoryExtensions")
                                 .FirstOrDefaultAsync(item => item.Id == id);
             if (game == null)
             {
                 return BadRequest(new {Error = "No game with that Id exist" });
             }


            var returnObject = new
            {
                success = true,
                categoryExtensions = game.CategoryExtensions.Select(item => new {
                    id = item.Id,
                    title = item.Title,
                    urlTitle = item.UrlTitle,
                    gameId = item.UrlTitle
                }),
                 imageName = game.ImageName,
                 title = game.Title,
                 urlTitle = game.UrlTitle,
                 admins = gameAdminUsernames
             };

             return Ok(
                returnObject
                 );
        }
        [Route("[action]/{urlTitle}")]
        /*Returns one game by url-string
          Example: localhost/api/Game/ByString/halo_3 (this is pretty ugly)
          returns halo 3*/

        /*Should rewrite this in the future, as this needs two db checks as of now.*/
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
                Debug.WriteLine(e);
                return BadRequest(new { success = false });
            }
            return RedirectToAction("GameById", new { id = gameId });
        }
        /*
         Only a moderator for the entire site
         can add a game.
         */
        [Route("AddGame")]
        [CustomAuthorize("SiteModerator")]
        [HttpPost]
        public async Task<IActionResult> AddGame(AddGameViewModel game)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await Session.Model.User.GetUser(_context, HttpContext.Session.GetInt32("id"));

            if (!GameLogic.AddGame(_context,game.Title,user))
            {
                return StatusCode(401, new { status = "That game exist in the db already" });
            }

            return Ok(new { status="Successfully added game to database"});
        }

        /*
         Game mod can upload an image for the game
        */
        [Route("[action]")]
        [CustomAuthorize]
        [HttpPost]
        public IActionResult AddGameImage(AddGameImageViewModel vm)
        {
            Game game = GameLogic.GetGame(_context,vm.GameTitle);

            var user = Session.Model.User.GetUser(_context, HttpContext.Session.GetString("username"));
            bool isGameAdmin = user.isGameAdmin(_context,game.Id);
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

            
            string pathFromConfig = "";
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                pathFromConfig = configuration.GetSection("SaveImagePath")["Production"];
            else
                pathFromConfig = configuration.GetSection("SaveImagePath")["Local"];
            string filePath = $"{pathFromConfig}{game.UrlTitle}.{fileType}";
            string base64withoutHeader = vm.Img.Split(",")[1];
            //async?
            System.IO.File.Delete(filePath);
            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(base64withoutHeader));
            game.ImageName = game.UrlTitle + "." + fileType;
           
            _context.SaveChanges();

            return Ok(new { msg = "Successfully updated image" });
        }
    }
}
