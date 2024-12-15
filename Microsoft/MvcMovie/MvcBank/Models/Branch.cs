using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcBank.Models
{
    public class Branch
    {
        [Key]
        public Guid BranchId { get; set; }

        public string BranchName { get; set; }

        [ForeignKey("BankId")]
        public Guid? BankId { get; set; }

        public Bank? Bank { get; set; }


    }
}
