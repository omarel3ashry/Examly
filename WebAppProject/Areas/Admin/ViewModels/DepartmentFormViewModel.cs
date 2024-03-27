

using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class DepartmentFormViewModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must select Branch first.")]
        public int BranchId { get; set; }
        public List<BranchViewModel>? Branches { get; set; }

    }
}
