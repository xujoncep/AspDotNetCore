using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcBank.Models.ViewModels
{
    public class CreateBranchVM
    {
        public Guid BranchId { get; set; }

        [Required]
        [Remote(action: "IsBranchExists", controller:"Branch")]

        [DisplayName("Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [DisplayName("Select Bank")]
        public Guid? BankId {  get; set; } 
        public IEnumerable<SelectListItem>? Banks { get; set; }


    }
}
