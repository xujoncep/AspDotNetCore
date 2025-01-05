using BankApplication.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models.ViewModels
{
    public class CreateBankWithMultipleBranchVM
    {
        public Bank Bank { get; set; }
        public IFormFile? BankLogo { get; set; }
        public List<CreateBranchVM>? Branches { get; set; }

    }
}
