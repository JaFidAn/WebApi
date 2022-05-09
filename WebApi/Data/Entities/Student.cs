using System;
using System.Collections.Generic;

namespace WebApi.Data.Entities
{
    public class Student : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double? Sallary { get; set; }

        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
