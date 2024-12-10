using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovieOneToMany.Models
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }

        public string Title { get; set; }


        [ForeignKey("GenreId")]
        public Guid GenreId { get; set; }

        public Genre? Genre { get; set; }

    }
}
