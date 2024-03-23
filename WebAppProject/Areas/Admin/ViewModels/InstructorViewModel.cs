using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class InstructorViewModel
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? BranchName { get; set; }
        public string? ManagedDepartmentName { get; set; }

    }
}
