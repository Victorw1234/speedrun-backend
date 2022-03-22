using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Session.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<CategoryExtension> CategoryExtensions { get; set; }
        public DbSet<GameAdmin> GameAdmins { get; set; }

        public async Task<User> GetUser(int? id)
        {
            var user = await Users.FindAsync(id);
            return user is null ?  user :  null;
        }

        public User GetUser(string username)
        {
            var user = Users.Where(user => user.Username == username);
            if (user.Count() == 1)
                return user.ElementAt(0);

            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id=1,
                    Username="victorw1234",
                    Password="apa123"
                },
                new User
                {
                    Id=2,
                    Username="baneinei",
                    Password="apa123"
                }
                

            );
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "Halo 3",
                    ImageName = "halo3.jpg"
                },
                new Game
                {
                    Id=2,
                    Title="Super Mario 64",
                    ImageName = "sm64.png"
                }
                );

            

            modelBuilder.Entity<CategoryExtension>().HasData(
                new CategoryExtension
                {
                    Id=1,
                    GameId = 1,
                    Title="Easy"
                },
                new CategoryExtension
                {
                    Id=2,
                    GameId=1,
                    Title="Legendary",
                    
                }
                );

            modelBuilder.Entity<Time>().HasData(
                new Time
                {
                    Id=1,
                    UserId=2,
                    Link= "https://www.youtube.com/watch?v=uhpuu6B3L8E",
                    RunTime = new DateTime(1,1,1,1,1,57),
                    CategoryExtensionId=1

                }

                );
            modelBuilder.Entity<GameAdmin>().HasData(
                new GameAdmin
                {
                    Id = 1,
                    GameId = 1,
                    UserId = 2
                }
                );
        }
    }
}
