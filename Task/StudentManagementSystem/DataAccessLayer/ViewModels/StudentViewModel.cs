using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage ="Name is Required!")]
        [StringLength(20,MinimumLength =3, ErrorMessage ="Name length Between 20 to 3 Characters.")]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Class is Required!")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "Class length Between 6 to 2 Characters.")]
        [DisplayName("Acadamic Class")]
        public string Class { get; set; }

        //[EmailAddress]
        //[Remote(action: "IsEmailAvailable", controller: "Student", ErrorMessage = "Email is already taken.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Maintain format user@example.com")]
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Email is Required!")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Name is Required!")]
        [RegularExpression(@"^(013|014|015|016|017|018|019)\d{8}$", ErrorMessage ="Enter Bangladeshi Phone Number!")]
        public string Phone { get; set; }
        
  
        [DisplayName("Your Biography")]
        public string? Bio { get; set; }
    }
}
