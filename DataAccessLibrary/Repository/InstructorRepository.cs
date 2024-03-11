using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ESContext _context;

        public InstructorRepository(ESContext context)
        {
            _context = context;
        }

        public List<Instructor> GetAll()
        {
            return _context.Instructors.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Instructor>> GetAllAsync()
        {
            return _context.Instructors.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Instructor> GetAllWithIncludes()
        {
            return _context.Instructors
                .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Branch)
                .Include(e => e.ManagedDepartment)
                .Include(e => e.Questions)
                .Include(e => e.DepartmentCourses)
                .ToList();
        }

        public Task<List<Instructor>> GetAllWithIncludesAsync()
        {
            return _context.Instructors
            .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Branch)
                .Include(e => e.ManagedDepartment)
                .Include(e => e.Questions)
                .Include(e => e.DepartmentCourses)
            .ToListAsync();
        }

        public Instructor? GetById(int id)
        {
            return _context.Instructors.Find(id);
        }

        public async Task<Instructor?> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public Instructor? GetByIdWithIncludes(int id)
        {
            return _context.Instructors
                .Include(e => e.User)
                .Include(e => e.Branch)
                .Include(e => e.ManagedDepartment)
                .Include(e => e.Questions)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Instructor?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Instructors
                .Include(e => e.User)
                .Include(e => e.Branch)
                .Include(e => e.ManagedDepartment)
                .Include(e => e.Questions)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Instructor entity)
        {
            _context.Instructors.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Instructor entity)
        {
            _context.Instructors.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Instructor entity)
        {
            if (entity != null)
            {
                _context.Instructors.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Instructor entity)
        {
            if (entity != null)
            {
                _context.Instructors.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                instructor.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var instructor = _context.Instructors.Find(id);
            if (instructor != null)
            {
                instructor.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Instructor? Select(Expression<Func<Instructor, bool>> predicate)
        {
            return _context.Instructors.Where(predicate).FirstOrDefault();
        }

        public Task<Instructor?> SelectAsync(Expression<Func<Instructor, bool>> predicate)
        {
            return _context.Instructors.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
