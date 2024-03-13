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

        public int? GetInstIdByUserId(int userId)
        {
            return _context.Instructors.FirstOrDefault(e => e.UserId == userId)?.Id;
        }

        public IQueryable<Instructor?> GetByUserIdWithCourses(int userId)
        {
            return _context.Instructors
                .Include(e => e.DepartmentCourses).ThenInclude(e => e.Course)
                .Include(e => e.DepartmentCourses).ThenInclude(e => e.Department)
                .Where(e => !e.IsDeleted && e.UserId == userId);
        }

        public List<Question> GetAllQuestions(int userId)
        {
            var question = _context.Instructors
                            .Include(e => e.Questions.Where(e => !e.IsDeleted))
                            .ThenInclude(e => e.Choices)
                            .FirstOrDefault(e => !e.IsDeleted && e.UserId == userId)?
                            .Questions;

            return question?.ToList() ?? new List<Question>();
        }

        public List<Question> GetCourseQuestions(int userId, int courseId)
        {
            var question = _context.Instructors
                .Include(e => e.Questions.Where(e => !e.IsDeleted && e.CourseId == courseId))
                .ThenInclude(e => e.Choices)
                .FirstOrDefault(e => !e.IsDeleted && e.UserId == userId)?
                .Questions;

            return question?.ToList() ?? new List<Question>();
        }

        public int AddQuestion(Question entity)
        {
            //var choices = entity.Choices;
            _context.Questions.Add(entity);
            _context.SaveChanges();
            return entity.Id;
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

        public Instructor? GetByUserId(int userId)
        {
            return _context.Instructors.FirstOrDefault(e => e.UserId == userId);
        }

        public Task<Instructor?> GetByUserIdAsync(int userId)
        {
            return _context.Instructors.FirstOrDefaultAsync(e => e.UserId == userId);
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

        public List<Instructor> SelectAll(Expression<Func<Instructor, bool>> predicate)
        {
            return _context.Instructors.Where(predicate).ToList();
        }

        public Task<List<Instructor>> SelectAllAsync(Expression<Func<Instructor, bool>> predicate)
        {
            return _context.Instructors.Where(predicate).ToListAsync();
        }
    }
}
