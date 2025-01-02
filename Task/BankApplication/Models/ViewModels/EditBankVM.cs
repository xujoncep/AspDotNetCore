namespace BankApplication.Models.ViewModels
{
    public class EditBankVM
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string? ExistingLogo { get; set; }
        public IFormFile? Logo { get; set; }
        public string BankAddress { get; set; }

    }
}
