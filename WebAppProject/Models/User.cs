namespace WebAppProject.Models
{
    public class User
    {
        public static List<User> UserDemo = [
            new User { Id = 1, Email = "1", Password = "1", RoleId = 1, Role = Role.RoleDemo[0] },
            new User { Id = 2, Email = "2", Password = "2", RoleId = 2, Role = Role.RoleDemo[1] },
            new User { Id = 3, Email = "3", Password = "3", RoleId = 3, Role = Role.RoleDemo[2] },
            new User { Id = 4, Email = "4", Password = "4", RoleId = 4, Role = Role.RoleDemo[3] },

        ];
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
