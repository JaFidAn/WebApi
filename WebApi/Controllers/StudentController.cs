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
        private static List<Student> _students = new List<Student>();

        [HttpGet("all")]
        public List<Student> GetAll()
        {
            return _students;
        }

        [HttpPost("create")]
        public Student CreateStudent(Student student)
        {
            _students.Add(student);
            return student;
        }

        [HttpPut("update")]
        public Student UpdateStudent(int id, Student newStudent)
        {
            var studend = _students.FirstOrDefault(student => student.Id == id);
            studend.Name = newStudent.Name;
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
