using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IDepartmentCourseRepository : IRepository<DepartmentCourse>
    {
        public List<Course> GetCoursesByDeptIdWithIncludes(int deptId);
        public DepartmentCourse? GetByDeptAndCrsIdWithIncludes(int crsId, int deptId);
        public Task<DepartmentCourse?> GetByDeptAndCrsIdWithIncludesAsync(int crsId, int deptId);

    }
}
