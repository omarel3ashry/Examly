using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }

        [MinLength(4, ErrorMessage = "Name must be 4 characters or more.")]
        public string Location { get; set; }
    }
}
