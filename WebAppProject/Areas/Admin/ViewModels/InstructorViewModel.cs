namespace WebAppProject.Areas.Admin.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? BranchName { get; set; }
        public string? ManagedDepartmentName { get; set; }

    }
}
