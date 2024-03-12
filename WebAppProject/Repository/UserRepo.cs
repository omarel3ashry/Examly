using WebAppProject.Models;

namespace WebAppProject.Repository
{
    public class UserRepo
    {
        
        public User CheckUser(User user)
        {
            var result=User.UserDemo.Find(User => User.Password == user.Password && User.Email == user.Email);
            return result;
       
            
        }

        public int AddStudent(User user)
        {
            user.Id = User.UserDemo.Last().Id + 1;
            user.RoleId = 4;
            user.Role = Role.RoleDemo[3];
            User.UserDemo.Add(user);
            return user.Id;
        }
    }
}

