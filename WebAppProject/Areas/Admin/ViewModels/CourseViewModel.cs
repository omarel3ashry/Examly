using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [RegularExpression(@"[a-zA-Z-#+]+", ErrorMessage = "Name must contain only [characters, # , + , -]")]
        public string Name { get; set; }

        [MinLength(4, ErrorMessage = "Name must be 4 characters or more.")]
        public string Description { get; set; }
    }
}
