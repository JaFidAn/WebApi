using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data.Entities;
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
        
        public StudentController(
            ISingletonOperation singletonOperation, 
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation,
            IUnitOfWork unitOfWork)
            
        {
            _singletonOperation = singletonOperation;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _unitOfWork = unitOfWork;
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
        public async Task<object> GetAll()
        {
            var list = await _unitOfWork.StudentRepository.GetAllList();

            return list;
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
        public async Task<IActionResult> CreateStudent(Student student)
        {
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
