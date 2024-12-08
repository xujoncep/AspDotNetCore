using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovieOneToMany.Models
{
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }

        [ Display(Name ="Genre Name")]
        public string GenreName { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}
