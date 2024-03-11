using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public Instructor? GetByUserId(int userId);
        public Task<Instructor?> GetByUserIdAsync(int userId);
    }
}
