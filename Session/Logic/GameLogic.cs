using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Logic
{
    public static class GameLogic
    {
        public static bool DuplicateGame(string Title,ApplicationDbContext ctx) 
        {
            return ctx.Games.Where(q => q.Title == Title).Count() != 0;
        }
    }
}
