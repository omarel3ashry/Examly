using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        public Student? GetByUserId(int userId);
        public Task<Student?> GetByUserIdAsync(int userId);
        public Task<List<ExamChoices>> GetStudentAnswersAsync(int studentId, int examId);
        public void AddStudentAnswers(int examId, int studentId, List<Choice> choices);
        public Task<bool> AddStudentAnswersAsync(int examId, int studentId, List<Choice> choices);
    }
}
