using WebAppProject.Models;

namespace WebAppProject.Repository
{
    

    public class InstructorRepo 
    {
        List <Instructor> _instructors= [
                
            new Instructor() { Id = 65, Name = "Ahmed Karim" },
            new Instructor() { Id = 52, Name = "Aly Mohamed" },
            new Instructor() { Id = 65, Name = "Mohamed Samir" },
            new Instructor() { Id = 52, Name = "Omar fares" }

            ];
        public List<Instructor> GetAllByBranchId(int BranchId)
        {
            
            
            return _instructors;
        }
        public Instructor GetById(int id)
        {

            return _instructors.Find(dept=>dept.Id==id);
        }

        
    }
}
