using FluentValidation;
using System;

namespace WebApi.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double? Sallary { get; set; }
        public int? GenderId { get; set; }
    }

    public class StudentValidator : AbstractValidator<StudentModel>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required!");
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x=>x.Sallary).InclusiveBetween(300, 3000);
            RuleFor(x=>x.GenderId).InclusiveBetween(1,2);
        }
    }
}
