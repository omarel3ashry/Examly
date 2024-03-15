using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class DepartmentFormViewModel
    {
        public Department? Department { get; set; }
        public List<Branch> Branches { get; set; }

    }
}
