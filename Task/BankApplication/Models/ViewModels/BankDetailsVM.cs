using BankApplication.Models.Entity;

namespace BankApplication.Models.ViewModels
{
    public class BankDetailsVM
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string Logo { get; set; }
        public string BankAddress { get; set; }


        public List<Branch>? Branches { get; set; }
    }
}
