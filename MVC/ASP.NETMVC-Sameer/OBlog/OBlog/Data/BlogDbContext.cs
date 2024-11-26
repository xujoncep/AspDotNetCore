using Microsoft.EntityFrameworkCore;
using OBlog.Models.Domain;

namespace OBlog.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; } 
    }
}
