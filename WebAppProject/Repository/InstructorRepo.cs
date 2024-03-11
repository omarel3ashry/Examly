using WebAppProject.Models;

namespace WebAppProject.Repository
{
    

    public class InstructorRepo 
    {
        
        public List<Instructor> GetAllByBranchId(int branchId)
        {
            
            
            return Instructor.InstructorDemo.FindAll(ins=>ins.BranchId==branchId);
        }
        public Instructor GetById(int id)
        {

            return Instructor.InstructorDemo.Find(ins=>ins.Id==id);
        }public Instructor GetByUserId(int userId)
        {

            return Instructor.InstructorDemo.Find(ins=>ins.UserId==userId);
        }

        
    }
}
