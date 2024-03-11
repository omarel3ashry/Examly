using WebAppProject.Models;

namespace WebAppProject.Repository
{
    public class BranchRepo
    {
        public List<Branch> GetAll()
        {


            return Branch.BranchDemo;
        }
        public Branch GetById(int id)
        {

            return Branch.BranchDemo.Find(b => b.Id == id);
        }
        /*public void Update(Branch department)
        {
            var dept = GetById(department.Id);
            dept = department;
        }*/

    }
}
