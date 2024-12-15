using Microsoft.EntityFrameworkCore;
using MvcBank.Models;
using System.Collections.Generic;

namespace MvcBank.Data
{
    public class BankDbContext : DbContext
    {

        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branch { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One bank can have many branch
            modelBuilder.Entity<Bank>()
                .HasMany(b => b.Branch)
                .WithOne(b => b.Bank)
                .HasForeignKey(b => b.BankId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
