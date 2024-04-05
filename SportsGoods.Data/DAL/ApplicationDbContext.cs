using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Interfaces;
using SportsGoods.Data.Models;

namespace SportsGoods.Data.DAL
{
    public class ApplicationDbContext : IdentityDbContext<Administrator>, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Administrator>().ToTable("Administrators");

            modelBuilder.Entity<Administrator>().HasData(
                new Administrator
                {
                    Id = new Guid(),
                    Email = "FirstAdmin@abv.bg",
                    Password = "password1"
                },
                 new Administrator
                 {
                     Id = new Guid(),
                     Email = "SecondAdmin@abv.bg",
                     Password = "password2"
                 },
                  new Administrator
                  {
                      Id = new Guid(),
                      Email = "ThirdAdmin@abv.bg",
                      Password = "password3"
                  }
            );
        }

    }
}
