namespace BankApplication.Models.ViewModels
{
    public class CreateBankVM
    {

        public string BankName { get; set; }
        public IFormFile Logo { get; set; }
        public string BankAddress { get; set; }

    }
}
