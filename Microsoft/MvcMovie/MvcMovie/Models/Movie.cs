using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(60, MinimumLength =5)]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name =" Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required,RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string Genre { get; set; }
        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        [Required,StringLength(5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string? Rating { get; set; }

    }
}
