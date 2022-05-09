using System;

namespace WebApi.Data.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
