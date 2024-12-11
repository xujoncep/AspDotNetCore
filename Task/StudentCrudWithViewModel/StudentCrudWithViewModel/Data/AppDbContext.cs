using Microsoft.EntityFrameworkCore;
using StudentCrudWithViewModel.Models;
using System.Collections.Generic;
using System.Net;

namespace StudentCrudWithViewModel.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Passport> Passports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(p => p.Passport)
                .WithOne(u => u.User)
                .HasForeignKey<Passport>(p => p.UserId);
        }
    }
}
