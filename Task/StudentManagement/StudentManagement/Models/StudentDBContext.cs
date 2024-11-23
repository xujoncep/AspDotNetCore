using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models
{
   
        public class StudentDBContext : DbContext
        {
            //Constructor calling the Base DbContext Class Constructor
            public StudentDBContext() : base()
            {
            }
            //OnConfiguring() method is used to select and configure the data source
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //Configuring the Connection String
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-010\SQLEXPRESS01;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            //Adding Domain Classes as DbSet Properties
            public DbSet<Student> Students { get; set; }
        }
    
}
