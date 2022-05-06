using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly ISingletonOperation _singletonOperation;
        private readonly ITransientOperation _transientOperation;
        private readonly IScopedOperation _scopedOperation;
        private readonly StudentDbContext _studentDbContext;
        public StudentController(
            ISingletonOperation singletonOperation, 
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation,
            StudentDbContext studentDbContext)
        {
            _singletonOperation = singletonOperation;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _studentDbContext = studentDbContext;

        }
        private static List<StudentModel> _students = new List<StudentModel>();


        [HttpGet("Guid")]
        public object GetGuids()
        {
            var data = new
            {
                SingletonOperation = _singletonOperation.Id,
                TransientOperation = _transientOperation.Id,
                ScopedOperation = _scopedOperation.Id,
            };
            return data;
        }


        [HttpGet("all")]
        public async Task<List<Student>> GetAll()
        {
            var list = await _studentDbContext.Students.ToListAsync();
            return list;
        }

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentDbContext.Students.FirstOrDefaultAsync(s=>s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            await _studentDbContext.Students.AddAsync(student);
            await _studentDbContext.SaveChangesAsync();
            return Created($"/api/student/student/{student.Id}", student);
        }

        [HttpPut("update")]
        public  async Task<Student> UpdateStudent(int id, Student newStudent)
        {
            var student = await _studentDbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
            student.Name = newStudent.Name;
            student.Surname = newStudent.Surname;
            student.DateOfBirth = newStudent.DateOfBirth;
            student.Sallary = newStudent.Sallary;
            student.Gender = newStudent.Gender;

            await _studentDbContext.SaveChangesAsync();

            return newStudent;
        }

        [HttpDelete("delete")]
        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _studentDbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
            _studentDbContext.Students.Remove(student);
            await _studentDbContext.SaveChangesAsync();
            return student;
        }
    }
}
