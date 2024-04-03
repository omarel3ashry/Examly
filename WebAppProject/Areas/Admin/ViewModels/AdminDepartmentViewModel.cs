using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class AdminDepartmentViewModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }
        public string? ManagerName { get; set; }
        public DateTime? HireDate { get; set; }
        public string? BranchName { get; set; }
    }
}
