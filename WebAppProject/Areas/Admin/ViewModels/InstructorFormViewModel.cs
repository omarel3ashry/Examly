using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebAppProject.Areas.Admin.ViewModels;

namespace WebAppProject.ViewModels
{
    public class InstructorFormViewModel
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int BranchId { get; set; }
        public List<BranchViewModel>? Branches { get; set; }
        public List<SelectListItem>? GenderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };


    }
}
