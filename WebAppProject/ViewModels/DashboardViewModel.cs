using WebAppProject.FakeModels;

namespace WebAppProject.ViewModels
{
    public class DashboardViewModel
    {
        public int BranchId { get; set; }
        public IEnumerable<FakeBranch> Branches { get; set; }

        public int InstructorId { get; set; }
        public IEnumerable<FakeInstructor> Instructors { get; set; } = new List<FakeInstructor>();

        public int DepartmentId { get; set; }
        public IEnumerable<FakeDepartment> Departments { get; set; } = new List<FakeDepartment>();
    }
}
