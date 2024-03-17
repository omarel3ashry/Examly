using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository
{
    public class DepartmentCourseRepository : IDepartmentCourseRepository
    {
        private readonly ESContext _context;

        public DepartmentCourseRepository(ESContext context)
        {
            _context = context;
        }
        public int Add(DepartmentCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(DepartmentCourse entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentCourse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentCourse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public List<DepartmentCourse> GetAllWithIncludes()
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentCourse>> GetAllWithIncludesAsync()
        {
            throw new NotImplementedException();
        }

        public DepartmentCourse? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentCourse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public DepartmentCourse? GetByIdWithIncludes(int id)
        {
            return _context.DepartmentCourses.Include(e=>e.Course).ThenInclude(e=>e.Exams).Where(e=>e.DepartmentId==id&&e.CourseId==1).FirstOrDefault();
        }

        public Task<DepartmentCourse?> GetByIdWithIncludesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetCoursesByDeptIdWithIncludes(int deptId)
        {
            return _context.DepartmentCourses.Where(e => e.DepartmentId == deptId)
                .Include(e => e.Course)
                .ThenInclude(e=>e.Exams)
                .Select(e=>e.Course)
                .ToList();
        }
        public DepartmentCourse? GetByDeptAndCrsIdWithIncludes(int crsId,int deptId)
        {
            return _context.DepartmentCourses
                            .Include(e => e.Course)
                            .Include(e => e.Instructor)
                            .Include(e => e.Department)
                            .FirstOrDefault(e => e.CourseId == crsId&&e.DepartmentId==deptId);
        }

        public DepartmentCourse? Select(Expression<Func<DepartmentCourse, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentCourse> SelectAll(Expression<Func<DepartmentCourse, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentCourse>> SelectAllAsync(Expression<Func<DepartmentCourse, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentCourse?> SelectAsync(Expression<Func<DepartmentCourse, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(DepartmentCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(DepartmentCourse entity)
        {
            throw new NotImplementedException();
        }
    }
}
