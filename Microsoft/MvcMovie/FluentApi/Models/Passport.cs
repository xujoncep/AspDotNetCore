using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class Passport
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PassportId { get; set; }
        public int PassportNumber { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
