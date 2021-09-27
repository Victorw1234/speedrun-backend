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
            return ctx.Users.Where(q => q.Username == username).FirstOrDefault();
        }

        public static bool isSiteModerator(ApplicationDbContext ctx,int userId)
        {
            var user = ctx.Users.Where(user => user.Id == userId).FirstOrDefault();
            if (user is null)
                return false;

            return user.SiteModerator;

        }
    }
}
