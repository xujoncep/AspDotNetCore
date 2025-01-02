using BankApplication.Models.Entity;

namespace BankApplication.Models.ViewModels
{
    public class EditBranchVM
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string? ExistingBranchLogo { get; set; }
        public IFormFile? BranchLogo { get; set; }
        public bool IsActive { get; set; }
        public int BankId { get; set; }
        public Bank? Bank { get; set; }
    }
}
