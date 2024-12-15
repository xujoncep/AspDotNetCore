using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcBank.Models.ViewModels
{
    public class EditBranchVM
    {

        public Guid BranchId { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        [DisplayName("Select Bank")]
        public Guid? BankId { get; set; }
        public IEnumerable<SelectListItem>? Banks { get; set; }
    }
}
