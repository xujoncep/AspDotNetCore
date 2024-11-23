using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDBContext _context;
        public StudentController(StudentDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();

            if (students != null)
            {
                return Json(new { data = students });
            }
            return Json(new { success = false });
        }
       
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student != null)
            {
                return Json(new { data = student });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            var st = await _context.Students.FindAsync(student.Id);
            if (st == null)
            {
                return Json(new { success = false });
            }
            if (ModelState.IsValid)
            {
                st.Name = student.Name;
                st.Age = student.Age;
                st.Class = student.Class;
                // _context.Update(emp);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student == null)
            {
                return Json(new { success = false });
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
