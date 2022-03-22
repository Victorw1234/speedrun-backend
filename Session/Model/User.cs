using Session.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool SiteModerator { get; set; }
        public byte[] salt { get; set; }
        public IEnumerable<Time> Times { get; set; }
        public IEnumerable<GameAdmin> Admin { get; set; }

        public bool isSiteModerator(ApplicationDbContext ctx)
        {
            var user = ctx.Users.Where(user => user.Id == Id).FirstOrDefault();
            if (user is null)
                return false;

            return user.SiteModerator;
        }

        public bool isGameAdmin(ApplicationDbContext ctx, int gameId)
        {
            return (ctx.GameAdmins.Where(ga => ga.UserId == Id && ga.GameId == gameId).Count() != 0);
        }

    }
}
