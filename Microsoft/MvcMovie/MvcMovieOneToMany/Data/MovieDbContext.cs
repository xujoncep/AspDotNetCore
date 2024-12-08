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
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieDetails> MoviesDetails { get; set; }

    }
}
