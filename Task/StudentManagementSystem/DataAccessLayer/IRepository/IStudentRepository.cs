using DataAccessLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IStudentRepository
    {
        Task <IEnumerable<Student>> GetStudentsAsync ();
        Task<Student> GetStudentByIdAsync(Guid id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid id);

        Task<bool> IsEmailExistsAsync(string email);
    }
}
