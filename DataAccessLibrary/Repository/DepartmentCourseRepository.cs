using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repository
{
    public class DepartmentCourseRepository : IDepartmentCourseRepository
    {
        private readonly ESContext _context;

        public DepartmentCourseRepository(ESContext context)
        {
            _context = context;
        }

        public DepartmentCourse? GetByIdWithIncludes(int id)
        {
            return _context.DepartmentCourses
                .Include(e => e.Course)
                .ThenInclude(e => e.Exams)
                .Where(e => e.DepartmentId == id && e.CourseId == 1)
                .FirstOrDefault();
        }

        public List<Course> GetCoursesByDeptIdWithIncludes(int deptId)
        {
            return _context.DepartmentCourses.Where(e => e.DepartmentId == deptId)
                .Include(e => e.Course)
                .ThenInclude(e => e.Exams)
                .Select(e => e.Course)
                .ToList();
        }

        public DepartmentCourse? GetByDeptAndCrsIdWithIncludes(int crsId, int deptId)
        {
            return _context.DepartmentCourses
                            .Include(e => e.Course)
                            .Include(e => e.Instructor)
                            .Include(e => e.Department)
                            .FirstOrDefault(e => e.CourseId == crsId && e.DepartmentId == deptId);
        }

        public Task<DepartmentCourse?> GetByDeptAndCrsIdWithIncludesAsync(int crsId,int deptId)
        {
            return _context.DepartmentCourses
                            .Include(e => e.Course)
                            .Include(e => e.Instructor)
                            .Include(e => e.Department)
                            .FirstOrDefaultAsync(e => e.CourseId == crsId&&e.DepartmentId==deptId);
        }
    }
}
