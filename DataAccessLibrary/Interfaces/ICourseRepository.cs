using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        public List<Course> GetCoursesNotInDepartment(int deptId);
    }
}
