using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IExamTakenRepository : IRepository<ExamTaken>
    {
        public ExamTaken? GetByStudentId(int studentId);
        public List<ExamTaken> GetAllByStudentIdWithIncludes(int studentId);
        public Task<List<ExamTaken>> GetAllByStudentIdWithIncludesAsync(int studentId);
        public Task<ExamTaken?> GetByStudentIdAsync(int studentId);
        public ExamTaken? GetByStudentIdWithIncludes(int studentId);
        public Task<ExamTaken?> GetByStudentIdWithIncludesAsync(int studentId);
    }
}
