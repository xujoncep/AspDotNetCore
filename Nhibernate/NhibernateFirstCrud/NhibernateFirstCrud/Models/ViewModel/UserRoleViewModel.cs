using NhibernateFirstCrud.Models.Entities;

namespace NhibernateFirstCrud.Models.ViewModel
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int RoleId { get; set; } 
        public List<Role> Roles { get; set; } 
    }
}
