using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public class AddCourseViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "The course name is required.")]
        //[MinLength(1, ErrorMessage = "Name must be 1 characters or more.")]
        public string Name { get; set; }
        public bool WantToAdd { get; set; }
    }
}
