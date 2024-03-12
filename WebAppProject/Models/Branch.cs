namespace WebAppProject.Models
{
    public class Branch
    {
        public static List<Branch> BranchDemo = [
            new Branch { Id = 1, Name = "Alexandria" },
            new Branch { Id = 2, Name = "Smart" },
            new Branch { Id = 3, Name = "Mansoura" },
            new Branch { Id = 4, Name = "Ismalia" }
        ];
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
