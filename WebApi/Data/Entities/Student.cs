using System;

namespace WebApi.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double? Sallary { get; set; }
        public int? Gender { get; set; }
    }
}
