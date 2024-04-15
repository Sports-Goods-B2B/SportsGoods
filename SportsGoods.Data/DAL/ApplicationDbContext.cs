using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsGoods.Core.Interfaces;
using SportsGoods.Core.Models;
using System.Reflection;

namespace SportsGoods.Data.DAL
{
    public class ApplicationDbContext : IdentityDbContext<Administrator>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public ApplicationDbContext() : base()
        { }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
