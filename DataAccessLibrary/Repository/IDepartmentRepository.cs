using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        public bool SetManager(int departmentId, int? instructorId);
        public Task<bool> SetManagerAsync(int departmentId, int? instructorId);
    }
}
