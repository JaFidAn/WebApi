using System;
using System.Collections.Generic;

namespace WebApi.Data.Entities
{
    public class Course : BaseEntity<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public DateTime? GreationTime { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
