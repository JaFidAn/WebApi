using System.Threading.Tasks;
using WebApi.Data.Entities;
using WebApi.Repository;

namespace WebApi.UnitOFWorks
{
    public interface IUnitOfWork
    {
        public IRepository <Student, int> StudentRepository { get; set; }
        public IRepository <Course, int> CourseRepository { get; set; }
        public Task Commit();
    }
}
