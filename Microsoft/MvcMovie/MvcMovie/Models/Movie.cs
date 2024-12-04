using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; } 
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name =" Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
