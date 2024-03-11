namespace WebAppProject.FakeModels
{
    public class Department
    {   
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
