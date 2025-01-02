using BankApplication.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options): base(options)
        {

        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Bank>()
                .HasMany(b => b.Branches)
                .WithOne(b => b.Bank)
                .HasForeignKey(b => b.BankId);

        }
    }
}
