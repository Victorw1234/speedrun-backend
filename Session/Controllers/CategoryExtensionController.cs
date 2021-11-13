using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session.Model;
using Session.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Session.Logic;
namespace Session.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryExtensionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryExtensionController(ApplicationDbContext context)
        {
            _context = context;

        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult AddCategoryExtension(AddCategoryExtensionViewModel vm)
        {
            User user = UserLogic.GetUser(_context,HttpContext.Session.GetString("username"));
            if (user is null)
            {
                return StatusCode(401, new { message = "Unauthorized, not logged in" });
            }
            Game game = _context.Games.Where(g => g.Title == vm.gameTitle).FirstOrDefault();

            bool isGameMod = UserLogic.isGameAdmin(_context, user.Id, game.Id);
            if (!isGameMod)
            {
                return StatusCode(401, new { message = "Unauthorized, not game admin" });
            }



            CategoryExtension ce = new CategoryExtension();
            
            ce.Game = game;
            ce.GameId = game.Id;
            ce.Title = vm.CategoryName;

            List<CategoryExtension> ces = _context.CategoryExtensions.Where(q => q.GameId == ce.GameId).ToList<CategoryExtension>();
            for (int i = 0; i < ces.Count(); i++)
            {
                string url = ces[i].UrlTitle;
                if (url == ce.UrlTitle) return BadRequest(new { msg = "not unique url parameter" });
            }

            _context.CategoryExtensions.Add(ce);
            _context.SaveChanges();


            return Ok(new { message="added category extension"});
        }
    }
}
