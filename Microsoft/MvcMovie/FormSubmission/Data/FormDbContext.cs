using FormSubmission.Models;
using Microsoft.EntityFrameworkCore;

namespace FormSubmission.Data
{
    public class FormDbContext: DbContext
    {

        public FormDbContext(DbContextOptions<FormDbContext> options):base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
