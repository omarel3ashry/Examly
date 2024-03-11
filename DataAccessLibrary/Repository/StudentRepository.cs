using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ESContext _context;

        public StudentRepository(ESContext context)
        {
            _context = context;
        }
        public List<Student> GetAll()
        {
            return _context.Students.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _context.Students.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Student> GetAllWithIncludes()
        {
            return _context.Students
                .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .ToList();
        }

        public Task<List<Student>> GetAllWithIncludesAsync()
        {
            return _context.Students
                .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .ToListAsync();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public Student? GetByIdWithIncludes(int id)
        {
            return _context.Students
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Student?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Students
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Student entity)
        {
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Student entity)
        {
            if (entity != null)
            {
                _context.Students.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Student entity)
        {
            if (entity != null)
            {
                _context.Students.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                student.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                student.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Student? Select(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).FirstOrDefault();
        }
        public List<Student> SelectAll(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).ToList();
        }
        public Task<Student?> SelectAsync(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
