using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Repository;

namespace WebApi.UnitOFWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _studentDbContext;

        public UnitOfWork(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;

            StudentRepository = new EfRepository<Student, int>(studentDbContext);
            CourseRepository = new EfRepository<Course, int>(studentDbContext);
        }

        public IRepository<Student, int> StudentRepository { get; set; }
        public IRepository<Course, int> CourseRepository { get; set; }

        public async Task Commit()
        {
            await _studentDbContext.SaveChangesAsync(); 
        }
    }
}
