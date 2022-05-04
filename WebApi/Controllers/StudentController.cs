﻿using Microsoft.AspNetCore.Mvc;
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
