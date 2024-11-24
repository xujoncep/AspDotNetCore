using Microsoft.EntityFrameworkCore;
using StudentCrudWithViewModel.Models;

namespace StudentCrudWithViewModel.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions options): base(options) 
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
