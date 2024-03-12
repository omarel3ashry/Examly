using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class DashboardViewModel
    {
        public int BranchId { get; set; }
        public IEnumerable<Branch> Branches { get; set; }

        public int InstructorId { get; set; }
        public IEnumerable<Instructor> Instructors { get; } =new List<Instructor>();

        public int DepartmentId { get; set; }
        public IEnumerable<Department> Departments { get; } = new List<Department>();
    }
}
