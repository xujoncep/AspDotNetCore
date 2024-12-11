using Microsoft.EntityFrameworkCore;
using MvcMovieOneToMany.Models;

namespace MvcMovieOneToMany.Data
{
    public class MovieDbContext:DbContext
    {

        public MovieDbContext( DbContextOptions<MovieDbContext> options): base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetails> MoviesDetails { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Movie Genre relation: One gengre can have many movie
            modelBuilder.Entity<Genre>()
                .HasMany(m => m.Movies)
                .WithOne(g => g.Genre)
                .HasForeignKey(g => g.GenreId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);       
        }

    }
}
