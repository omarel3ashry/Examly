using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ESContext _context;

        public DepartmentRepository(ESContext context)
        {
            _context = context;
        }

        public List<Department> GetAll()
        {
            return _context.Departments.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Department>> GetAllAsync()
        {
            return _context.Departments.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Department> GetAllWithIncludes()
        {
            return _context.Departments
                .Where(e => !e.IsDeleted)
                .Include(e => e.Manager)
                .Include(e => e.Branch)
                .Include(e => e.Students)
                .Include(e => e.DepartmentCourses)
                .ToList();
        }

        public Task<List<Department>> GetAllWithIncludesAsync()
        {
            return _context.Departments
                .Where(e => !e.IsDeleted)
                .Include(e => e.Manager)
                .Include(e => e.Branch)
                .Include(e => e.Students)
                .Include(e => e.DepartmentCourses)
                .ToListAsync();
        }

        public Department? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(e => !e.IsDeleted && e.Id == id);
        }

        public Department? GetByManagerIdCoursesIncluded(int managerId)
        {
            return _context.Departments
                .Include(e => e.DepartmentCourses).ThenInclude(e => e.Course)
                .Include(e => e.DepartmentCourses).ThenInclude(e => e.Instructor)
                .FirstOrDefault(e => !e.IsDeleted && e.ManagerId == managerId);
        }

        public DepartmentCourse? GetDeptCourseWithIncludes(int crsId, int deptId)
        {
            return _context.DepartmentCourses
                            .Include(e => e.Course)
                            .Include(e => e.Instructor)
                            .Include(e => e.Department)
                            .ThenInclude(e => e.Branch)
                            .FirstOrDefault(e => e.CourseId == crsId && e.DepartmentId == deptId);
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public Department? GetByIdWithIncludes(int id)
        {
            return _context.Departments
                .Include(e => e.Manager)
                .Include(e => e.Branch)
                .Include(e => e.Students)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Department?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Departments
                .Include(e => e.Manager)
                .Include(e => e.Branch)
                .Include(e => e.Students)
                .Include(e => e.DepartmentCourses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Department entity)
        {
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Department entity)
        {
            if (entity != null)
            {
                _context.Departments.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public int AddDepartmentCourses(List<DepartmentCourse> deptCourses)
        {
            _context.DepartmentCourses.AddRange(deptCourses);
            return _context.SaveChanges();
        }

        public bool UpdateDeptCourseInstructor(int deptId, int crsId, int newInstId)
        {
            var deptCourse = _context.DepartmentCourses
                .FirstOrDefault(e => e.DepartmentId == deptId && e.CourseId == crsId);
            if (deptCourse != null)
            {
                deptCourse.InstructorId = newInstId;
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Department entity)
        {
            if (entity != null)
            {
                _context.Departments.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteDeptCourse(int deptId, int crsId)
        {
            var deptCourse = _context.DepartmentCourses
                   .FirstOrDefault(e => e.DepartmentId == deptId && e.CourseId == crsId);
            if (deptCourse != null)
            {
                _context.DepartmentCourses.Remove(deptCourse);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Department? Select(Expression<Func<Department, bool>> predicate)
        {
            return _context.Departments.Where(predicate).FirstOrDefault();
        }

        public Task<Department?> SelectAsync(Expression<Func<Department, bool>> predicate)
        {
            return _context.Departments.Where(predicate).FirstOrDefaultAsync();
        }

        public bool SetManager(int departmentId, int? instructorId)
        {

            var department = _context.Departments.Find(departmentId);
            if (department != null)
            {
                var ins = _context.Instructors.Include(e => e.User).FirstOrDefault(e => e.Id == instructorId && !e.IsDeleted);
                if (ins != null)
                    ins.User.RoleId = 2;
                if (department.ManagerId != null)
                {
                    var oldManagerUserID = _context.Instructors.FirstOrDefault(e => e.Id == department.ManagerId)?.UserId;
                    if (oldManagerUserID != null)
                    {
                        var u = _context.Users.FirstOrDefault(e => e.Id == oldManagerUserID);
                        if (u != null)
                            u.RoleId = 3;
                    }
                }
                department.ManagerId = instructorId;
                department.HireDate = DateTime.Now;
                return _context.SaveChanges() > 1;
            }
            return false;
        }

        public async Task<bool> SetManagerAsync(int departmentId, int? instructorId)
        {
            var department = _context.Departments.Find(departmentId);
            if (department != null)
            {
                department.ManagerId = instructorId;
                department.HireDate = DateTime.Now;
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public List<Department> SelectAll(Expression<Func<Department, bool>> predicate)
        {
            return _context.Departments.Where(predicate).ToList();
        }

        public Task<List<Department>> SelectAllAsync(Expression<Func<Department, bool>> predicate)
        {
            return _context.Departments.Where(predicate).ToListAsync();
        }
    }
}
