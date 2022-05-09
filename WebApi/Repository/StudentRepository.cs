using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Student> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Get(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> Update(Student entity)
        {
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
