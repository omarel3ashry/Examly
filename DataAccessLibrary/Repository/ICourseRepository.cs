using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        public List<Course> GetCoursesNotInDepartment(int deptId);
    }
}
