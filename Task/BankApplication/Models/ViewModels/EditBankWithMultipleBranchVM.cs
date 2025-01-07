using BankApplication.Models.Entity;

namespace BankApplication.Models.ViewModels
{
    public class EditBankWithMultipleBranchVM
    {
        public Bank Bank { get; set; }
        public string? ExistingLogo { get; set; }
        public IFormFile? BankLogo { get; set; }
        public List<EditBranchVM>? Branches { get; set; }
        public string? DeletedBranchIds { get; set; }
    }
}
