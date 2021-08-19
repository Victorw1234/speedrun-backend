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
        }
    }
}
