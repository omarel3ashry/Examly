using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        public bool SetManager(int departmentId, int? instructorId);
        public Task<bool> SetManagerAsync(int departmentId, int? instructorId);
        public Department? GetByManagerIdCoursesIncluded(int managerId);
        public Task<Department?> GetByManagerIdCoursesIncludedAsync(int managerId);
        public DepartmentCourse? GetDeptCourseWithIncludes(int crsId, int deptId);
        public Task<DepartmentCourse?> GetDeptCourseWithIncludesAsync(int crsId, int deptId);
        public int AddDepartmentCourses(List<DepartmentCourse> deptCourses);
        public Task<int> AddDepartmentCoursesAsync(List<DepartmentCourse> deptCourses);
        public bool UpdateDeptCourseInstructor(int deptId, int crsId, int newInstId);
        public Task<bool> UpdateDeptCourseInstructorAsync(int deptId, int crsId, int newInstId);
        public bool DeleteDeptCourse(int deptId, int crsId);
        public Task<bool> DeleteDeptCourseAsync(int deptId, int crsId);
    }
}
