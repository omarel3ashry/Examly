using WebAppProject.Models;

namespace WebAppProject.Repository
{
    
    public class DepartmentRepo 
    {
        public List<Department> _department = [
                new Department() { Id = 3, Name = "PD" },
            new Department() { Id = 2, Name = "OS" },
            new Department() { Id = 5, Name = "CS" },
            new Department() { Id = 1, Name = "IT" }

            ];
        public List<Department> GetAllByBranchId(int BranchId)
        {


            return _department;
        }
        public Department GetById(int id)
        {

            return _department.Find(i => i.Id == id);
        }
        public void Update(Department department)
        {
            var dept = GetById(department.Id);
            dept=department;
        }


    }
}
