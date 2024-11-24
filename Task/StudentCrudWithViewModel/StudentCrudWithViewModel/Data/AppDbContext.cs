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
    }
}
