using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IExamTakenRepository : IRepository<ExamTaken>
    {
        public ExamTaken? GetExamTakenWithIncludes(int studentId, int examId);
        public Task<ExamTaken?> GetExamTakenWithIncludesAsync(int studentId,int examId);
        public List<ExamTaken> GetAllByStudentIdWithIncludes(int studentId);
        public Task<List<ExamTaken>> GetAllByStudentIdWithIncludesAsync(int studentId);
    }
}
