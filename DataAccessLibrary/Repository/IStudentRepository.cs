using DataAccessLibrary.Model;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        public Student? GetByUserId(int userId);
        public Task<Student?> GetByUserIdAsync(int userId);
        public List<ExamChoices> GetStudentAnswers(int studentId, int examId);
      //  public Task<StudentExamWithAnswers> GetStudentAnswersAsync(int studentId, int examId);
    }
}
