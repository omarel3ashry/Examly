using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Staff.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The department name is required.")]
        [MinLength(2, ErrorMessage = "Name must be 2 characters or more.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]

        public string Name { get; set; }

        //public IEnumerable <CourseInfoViewModel> DepartmentCourse { get; set; }
        public ICollection<CourseInfoViewModel> DepartmentCourses { get; } = new List<CourseInfoViewModel>();


    }
}
