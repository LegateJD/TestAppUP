using TestApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestApp.Dal
{
    public class TestAppContext : DbContext
    {
        public TestAppContext(DbContextOptions<TestAppContext> options)
            : base(options)
        {
            if (Database.IsSqlServer())
            {
                Database.Migrate();
                return;
            }

            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetailses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
            .HasOne(p => p.Product)
            .WithMany(t => t.OrderDetails)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasData(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Marine",
                    Price = 50,
                    Description = "Marines are the all-purpose infantry unit produced from a Barracks. Having the quickest and cheapest production of all Terran units make the Mineral backbone that scales very well with Stimpack, Engineering Bay upgrades and Medivacs from the Starport."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tank",
                    Price = 150,
                    Description = "The long-ranged Siege Tank is a Mechanical unit built from a Factory with an attached Tech Lab and high damage versus Armored like Roaches and Stalkers. Against smaller units, the Siege Tank can switch to the stationary Siege Mode to deal splash damage from longer range. "
                });
        }
    }
}
