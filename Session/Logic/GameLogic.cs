using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Logic
{
    public static class GameLogic
    {
        /*
         Checks if a game already exists in the database.
         Checking is done by title.
         */
        public static bool DuplicateGame(string Title,ApplicationDbContext ctx) 
        {
            return ctx.Games.Where(q => q.Title == Title).Count() != 0;
        }

        /*Adds game to database
          Params
          game: game to be added. This function assumes only title is set. (Could extend it later)
          user: The user that added the game. This user should be set as game mod.
          _context: database context.
         */
        public static bool AddGame(ApplicationDbContext _context,Game game,User user)
        {
            if (DuplicateGame(game.Title, _context))
                return false;
            Game g = new Game();
            g.Title = game.Title;
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
