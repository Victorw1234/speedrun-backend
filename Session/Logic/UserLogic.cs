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
    }
}
