
namespace WebAppProject.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        public int BranchId { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }

        public int InstructorId { get; set; }
        public IEnumerable<InstructorViewModel> Instructors { get; } = new List<InstructorViewModel>();

        public int DepartmentId { get; set; }
        public IEnumerable<AdminDepartmentViewModel> Departments { get; } = new List<AdminDepartmentViewModel>();
    }
}
