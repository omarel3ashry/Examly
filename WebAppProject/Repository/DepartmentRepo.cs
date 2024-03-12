using WebAppProject.Models;

namespace WebAppProject.Repository
{
    
    public class DepartmentRepo 
    {
        
        public List<Department> GetAllByBranchId(int BranchId)
        {


            return Department.DepartmentDemo.FindAll(dept=>dept.BranchId==BranchId);
        }
        public Department GetById(int id)
        {

            return Department.DepartmentDemo.Find(i => i.Id == id);
        }
        public void Update(Department department)
        {
            var dept = GetById(department.Id);
            dept=department;
        }


    }
}
