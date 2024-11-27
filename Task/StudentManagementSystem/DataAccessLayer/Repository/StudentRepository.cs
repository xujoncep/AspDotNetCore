using DataAccessLayer.Data;
using DataAccessLayer.Entites;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public StudentRepository( ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _dbcontext.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            return await _dbcontext.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _dbcontext.Students.AddAsync(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _dbcontext.Students.Update(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _dbcontext.Students.FindAsync(id);
            if (student != null)
            {
                _dbcontext.Students.Remove(student);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _dbcontext.Students.AnyAsync(u => u.Email == email);
        }
    }
}
