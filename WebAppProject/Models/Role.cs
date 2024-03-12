namespace WebAppProject.Models
{
    public class Role
    {
        public static List<Role> RoleDemo = [
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Instructor" },
            new Role { Id=3,Name="Manager"},
            new Role { Id = 4, Name = "Student" }
        ];
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}
