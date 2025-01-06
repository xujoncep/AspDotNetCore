using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models.Entity
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? BranchLogo { get; set; }
        public bool IsActive { get; set; }


        public int BankId { get; set; }
        public virtual Bank? Bank { get; set; }
    }
}
