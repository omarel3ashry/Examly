using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Staff.ViewModels

{
    public class CourseInfoViewModel
    {

        public int CourseId { get; set; }
        [Required(ErrorMessage = "The cours ename is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The department name is required.")]
        [MinLength(2, ErrorMessage = "Name must be 2 characters or more.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "The instructor name is required.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string InstructorName { get; set; }
        [Required(ErrorMessage = "The branch name is required.")]
        [MinLength(3, ErrorMessage = "Name must be 3 characters or more.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "The description name is required.")]
        public string Description { get; set; }

    }
}
