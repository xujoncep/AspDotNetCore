using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcBank.Models.ViewModels
{
    public class CreateBankVM
    {
        [Key]
        public Guid BankId { get; set; }
        [Remote(action: "IsBankExists", controller:"Bank")]
        public string BankName { get; set; }

        public IEnumerable<Branch>? Branch { get; set; }
    }
}
