using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppProject.ViewModels
{
    public class InstructorWithBranchesViewModel
    {
        public Instructor? Instructor { get; set; }
        public List<Branch> Branches { get; set; }

        public List<SelectListItem> GenderOptions { get;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };

    }
}
