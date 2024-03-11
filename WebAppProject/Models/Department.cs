namespace WebAppProject.Models
{
    public class Department
    {
        public static List<Department> DepartmentDemo = [
                new Department() { Id = 1, Name = "PD", BranchId = 1, Branch = Branch.BranchDemo[0] },
            new Department() { Id = 2, Name = "OS", BranchId = 1, Branch = Branch.BranchDemo[0] },
            new Department() { Id = 3, Name = "CS", BranchId = 2, Branch = Branch.BranchDemo[1] },
            new Department() { Id = 4, Name = "IT", BranchId = 2, Branch = Branch.BranchDemo[1] }

            ];
        public int Id { get; set; }
        public string Name { get; set; }
        public int MgrId { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
