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
        /*public static User GetUser(ApplicationDbContext ctx, string username)
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

        public async static Task<User> GetUser(ApplicationDbContext ctx, int? id)
        {
            try
            {
                return await ctx.Users.FindAsync(id);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }*/

    }
}
