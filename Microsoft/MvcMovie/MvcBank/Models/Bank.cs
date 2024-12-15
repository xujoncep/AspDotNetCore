using System.ComponentModel.DataAnnotations;

namespace MvcBank.Models
{
    public class Bank
    {
        [Key]
        public Guid BankId { get; set; }
        public string BankName { get; set; }

        public IEnumerable<Branch>? Branch { get; set; }

    }
}
