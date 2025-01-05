using BankApplication.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models.ViewModels
{
    public class CreateBankWithBranchVM
    {
        public string? BankName { get; set; }

        public IFormFile? BankLogo { get; set; }

        [Required]
        public string? BankAddress { get; set; }

      
        [Required]
        public string? BranchName { get; set; }

        public IFormFile? BranchLogo { get; set; }

        public bool IsActive { get; set; }
        
    }
}

