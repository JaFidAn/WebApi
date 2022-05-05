using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public StudentController(
            ISingletonOperation singletonOperation, 
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation)
        {
            _singletonOperation = singletonOperation;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
        }
        private static List<Student> _students = new List<Student>();


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
        public List<Student> GetAll()
        {
            return _students;
        }

        [HttpGet("student/{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _students.FirstOrDefault(student => student.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("create")]
        public IActionResult CreateStudent(Student student)
        {
            _students.Add(student);
            return Created($"/api/student/student/{student.Id}", student);
        }

        [HttpPut("update")]
        public Student UpdateStudent(int id, Student newStudent)
        {
            var student = _students.FirstOrDefault(student => student.Id == id);
            student.Name = newStudent.Name;
            return newStudent;
        }

        [HttpDelete("delete")]
        public Student DeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(student => student.Id == id);
            _students.Remove(student);
            return student;
        }
    }
}
