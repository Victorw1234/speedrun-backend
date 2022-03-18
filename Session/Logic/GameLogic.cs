using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Logic
{
    public static class GameLogic
    {

        public static Game GetGameByUrl(ApplicationDbContext ctx, string gameUrl)
        {
            Game game = ctx.Games.ToList().Where(g => g.UrlTitle == gameUrl).FirstOrDefault();
            return game;
        }
        public static Game GetGame(ApplicationDbContext ctx, string gameTitle)
        {
            Game game = ctx.Games.Where(g => g.Title == gameTitle).FirstOrDefault();
            return game;
        }

        /*Checks if a game by the title exists in the database.*/
        public static bool GameExist(string Title, ApplicationDbContext ctx)
        {
            return ctx.Games.Where(q => q.Title == Title).Count() != 0;
        }
        /*Adds game with title gameTitle to database
          user: The user that added the game. This user should be set as game mod.
          This function does not verify that the user is a game mod.
         */

        public static bool AddGame(ApplicationDbContext _context,string gameTitle,User user)
        {
            if (GameExist(gameTitle,_context))
                return false;

            Game g = new Game();
            g.Title = gameTitle;
            g.ImageName = "qmark.png"; //default img, questionmark
            _context.Games.Add(g);
            _context.SaveChanges();

            GameAdmin gameAdmin = new GameAdmin();
            gameAdmin.GameId = g.Id;
            gameAdmin.UserId = user.Id;
            _context.GameAdmins.Add(gameAdmin);
            _context.SaveChanges();
            return true;
        }

    }
}
