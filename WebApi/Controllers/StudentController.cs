using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository;
using WebApi.UnitOFWorks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly ISingletonOperation _singletonOperation;
        private readonly ITransientOperation _transientOperation;
        private readonly IScopedOperation _scopedOperation;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentController> _logger;
        
        public StudentController(
            ISingletonOperation singletonOperation, 
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation,
            IUnitOfWork unitOfWork,
            ILogger<StudentController> logger)
            
        {
            _singletonOperation = singletonOperation;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
        [MyAutorize]
        public async Task<object> GetAll()
        {
            //var user = HttpContext.User;

            _logger.LogInformation("Reques accepted at {date}", DateTime.Now);
            var query = await _unitOfWork.StudentRepository.GetAllList();
            var result = query.ToList();
            _logger.LogWarning("Reques successfully comleted at {date}", DateTime.Now, JsonConvert.SerializeObject(result));

            return result;
        }

        //[HttpGet("studendCourseReport")]
        //public async Task<object> GetReport()
        //{
        //    var query = from sc in _studentDbContext.StudentCourses
        //                join s in _studentDbContext.Students on sc.StudentId equals s.Id
        //                join c in _studentDbContext.Courses on sc.CourseId equals c.Id
        //                join g in _studentDbContext.Genders on s.GenderId equals g.Id
        //                select new
        //                {
        //                    s.Name,
        //                    s.Surname,
        //                    s.DateOfBirth,
        //                    Gender = g.Name,
        //                    CourseName = c.Name,
        //                    sc.StartDate,
        //                    sc.EndDate
        //                };
        //    return await query.ToListAsync();
        //}

        //[HttpGet("genders")]
        //public async Task<object> GetGenders()
        //{
        //    var list = await _studentDbContext.Genders.Include(g => g.Students)
        //        .Select(g => new
        //        {
        //            g.Name,
        //            Students = g.Students.Select(s => new
        //            {
        //                s.Name,
        //                s.Surname,
        //                s.DateOfBirth,
        //                s.Sallary
        //            })
        //        }).ToListAsync();

        //    return list;
        //}

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _unitOfWork.StudentRepository.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent(StudentModel studentModel)
        {
            var student = new Student
            {
                Name = studentModel.Name,
                Surname = studentModel.Surname,
                DateOfBirth = studentModel.DateOfBirth,
                Sallary = studentModel.Sallary,
                GenderId = studentModel.GenderId,
            };
            await _unitOfWork.StudentRepository.Add(student);
            await _unitOfWork.Commit();

            return Created($"/api/student/student/{student.Id}", student);
        }

        [HttpPut("update")]
        public  async Task<Student> UpdateStudent(int id, Student newStudent)
        {
            var student = await _unitOfWork.StudentRepository.Find(id);
            student.Name = newStudent.Name;
            student.Surname = newStudent.Surname;
            student.DateOfBirth = newStudent.DateOfBirth;
            student.Sallary = newStudent.Sallary;
            student.Gender = newStudent.Gender;

             _unitOfWork.StudentRepository.Update(student);
            await _unitOfWork.Commit();

            return student;
        }

        [HttpDelete("delete")]
        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _unitOfWork.StudentRepository.Find(id);
             _unitOfWork.StudentRepository.Delete(student);
            await _unitOfWork.Commit();

            return student;
        }
    }
}
