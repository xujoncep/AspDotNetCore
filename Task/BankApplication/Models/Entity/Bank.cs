using System.ComponentModel.DataAnnotations;

namespace BankApplication.Models.Entity
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? Logo { get; set; }
        public string? BankAddress { get; set; }


        public List<Branch>? Branches { get; set; }

    }
}
