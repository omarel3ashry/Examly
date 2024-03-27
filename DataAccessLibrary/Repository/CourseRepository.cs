using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ESContext _context;

        public CourseRepository(ESContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Course>> GetAllAsync()
        {
            return _context.Courses.Where(e => !e.IsDeleted).ToListAsync();
        }

        public List<Course> GetAllWithIncludes()
        {
            return _context.Courses
                .Where(e => !e.IsDeleted)
                .Include(e => e.Topics)
                .Include(e => e.Questions)
                .Include(e => e.Exams)
                .Include(e => e.DepartmentCourses)
                .ToList();
        }

        public Task<List<Course>> GetAllWithIncludesAsync()
        {
            return _context.Courses
                .Where(e => !e.IsDeleted)
                .Include(e => e.Topics)
                .Include(e => e.Questions)
                .Include(e => e.Exams)
                .Include(e => e.DepartmentCourses)
                .ToListAsync();
        }

        public Course? GetById(int id)
        {
            return _context.Courses.Find(id);
        }

        public ValueTask<Course?> GetByIdAsync(int id)
        {
            return _context.Courses.FindAsync(id);
        }

        public Course? GetByIdWithIncludes(int id)
        {
            return _context.Courses
                .Include(e => e.Topics)
                .Include(e => e.Questions)
                .Include(e => e.Exams)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Course?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Courses
                .Include(e => e.Topics)
                .Include(e => e.Questions)
                .Include(e => e.Exams)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Course entity)
        {
            _context.Courses.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Course entity)
        {
            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Course entity)
        {
            if (entity != null)
            {
                _context.Courses.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Course entity)
        {
            if (entity != null)
            {
                _context.Courses.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                course.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                course.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public List<Course> GetCoursesNotInDepartment(int deptId)
        {
            return _context.Courses
                            .Where(course => !course.IsDeleted &&
                                             !_context.DepartmentCourses
                                              .Any(
                                                    dc => dc.CourseId == course.Id &&
                                                    dc.DepartmentId == deptId
                                                  )
                                  )
                            .ToList();
        }

        public Task<List<Course>> GetCoursesNotInDepartmentAsync(int deptId)
        {
            return _context.Courses
                .Where(course => !course.IsDeleted &&
                                 !_context.DepartmentCourses
                                  .Any(
                                        dc => dc.CourseId == course.Id &&
                                        dc.DepartmentId == deptId
                                      )
                      )
                .ToListAsync();
        }

        public Course? Select(Expression<Func<Course, bool>> predicate)
        {
            return _context.Courses.Where(predicate).FirstOrDefault();
        }

        public Task<Course?> SelectAsync(Expression<Func<Course, bool>> predicate)
        {
            return _context.Courses.Where(predicate).FirstOrDefaultAsync();
        }

        public List<Course> SelectAll(Expression<Func<Course, bool>> predicate)
        {
            return _context.Courses.Where(predicate).ToList();
        }

        public Task<List<Course>> SelectAllAsync(Expression<Func<Course, bool>> predicate)
        {
            return _context.Courses.Where(predicate).ToListAsync();
        }
    }
}
