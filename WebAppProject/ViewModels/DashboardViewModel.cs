using WebAppProject.FakeModels;

namespace WebAppProject.ViewModels
{
    public class DashboardViewModel
    {
        public int BranchId { get; set; }
        public IEnumerable<Branch> Branches { get; set; }

        public int InstructorId { get; set; }
        public IEnumerable<Instructor> Instructors { get; set; } = new List<Instructor>();

        public int DepartmentId { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
    }
}
