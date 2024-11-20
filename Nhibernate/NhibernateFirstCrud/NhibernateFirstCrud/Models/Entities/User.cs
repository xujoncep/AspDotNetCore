using System.Data;

namespace NhibernateFirstCrud.Models.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual Role Role { get; set; }
    }
}
