using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoWebApp.Models;
using AutoWebApp.Migrations;

namespace AutoWebApp.Data
{
    public class AutoWebAppContext : DbContext
    {
        public AutoWebAppContext (DbContextOptions<AutoWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AutoWebApp.Models.SparePart> SparePart { get; set; } = default!;
        public DbSet<AutoWebApp.Models.User> User { get; set; } = default!;
        public DbSet<AutoWebApp.Models.Order> Order { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SparePart>().HasMany(e => e.Orders).WithMany(e => e.SpareParts).UsingEntity<OrderSparePart>();
            modelBuilder.Entity<User>().HasMany(e => e.Orders).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerID).IsRequired();
            modelBuilder.Entity<SparePart>().ToTable("SparePart");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<SparePart>().Property(m => m.Description).IsRequired(false);
            modelBuilder.Entity<User>().Property(m => m.NIP).IsRequired(false);
            modelBuilder.Entity<User>().HasData(new Models.User { UserID = 1, Email = "admin@admin", FirstName = "Admin", LastName = "Admin", IsAdmin = true, Password = "admin" });
        }
    }
}
