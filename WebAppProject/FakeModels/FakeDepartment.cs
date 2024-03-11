namespace WebAppProject.FakeModels
{
    public class FakeDepartment
    {   
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public FakeBranch Branch { get; set; }
    }
}
