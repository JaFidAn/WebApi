using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data.Entities;

namespace WebApi.Repository
{
    public interface IStudentRepository
    {
        Task<Student> Get(int id);
        Task<IEnumerable<Student>> GetAll();
        Task<int> Add(Student entity);
        Task<Student> Delete(int id);
        Task<Student> Update(Student entity);
    }
}
