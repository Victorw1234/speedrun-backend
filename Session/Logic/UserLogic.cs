using Microsoft.AspNetCore.Http;
using Session.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Logic
{
    public static class UserLogic
    {
        public static User GetUser(ApplicationDbContext ctx, string username)
        {
            try
            {
                return ctx.Users.Where(q => q.Username == username).FirstOrDefault();
            }
            catch (NullReferenceException)
            {
                return null;
            }
            
        }

        /*public static bool isSiteModerator(ApplicationDbContext ctx,int userId)
        {
            var user = ctx.Users.Where(user => user.Id == userId).FirstOrDefault();
            if (user is null)
                return false;

            return user.SiteModerator;

        }

        public static bool isGameAdmin(ApplicationDbContext ctx, int userId,int gameId)
        {
            return (ctx.GameAdmins.Where(ga => ga.UserId == userId && ga.GameId == gameId).Count() != 0);
        }*/
    }
}
