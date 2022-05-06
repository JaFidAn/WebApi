using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;

namespace WebApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        //Default olaraq, SQL-də Course Entity-sinə uyğun olaraq Courses table-axtaracaq, əgər biz öz istədiyimiz Table-lə əlaqə qurmaq istəyiriksə o zaman OnModelCreating metdounu aşağıdakı kimi override edirik.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("tblStudents");
            modelBuilder.Entity<Course>().ToTable("tblCourse");
            modelBuilder.Entity<Gender>().ToTable("tblGender");
            modelBuilder.Entity<StudentCourse>().ToTable("tblStudentsCourse");

            modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.StudentId, x.CourseId });
        }
    }
    
}
