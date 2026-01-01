using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rating> Ratings { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call base so Identity tables map correctly
            base.OnModelCreating(builder);

            // Configure Brand.Name unique constraint to prevent duplicate brand names
            builder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();

            // Configure Vehicle relationships
            builder.Entity<Vehicle>()
                .HasOne(v => v.Brand)
                .WithMany(b => b.vehicles)
                .HasForeignKey(v => v.BrandId)
                .OnDelete(DeleteBehavior.Restrict);// Prevent cascade delete to avoid deleting vehicles when a brand is deleted


        }

    }
}
