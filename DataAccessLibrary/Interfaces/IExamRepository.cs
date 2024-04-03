using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IExamRepository : IRepository<Exam>
    {
        public bool AddExamQuestions(List<ExamQuestion> examQuestions);
        public Task<bool> AddExamQuestionsAsync(List<ExamQuestion> examQuestions);
        public bool UpdateTotalGrade(int examId, int totalGrade);
        public Task<bool> UpdateTotalGradeAsync(int examId, int totalGrade);
        public List<ExamTaken> GetExamGradesWithIncludes(int examId);
        public Task<List<ExamTaken>> GetExamGradesWithIncludesAsync(int examId);
        public List<Exam> GetInstructorExams(int instructorId);
        public Task<List<Exam>> GetInstructorExamsAsync(int instructorId);
        public List<Exam> GetDeptExams(int deptId);
        public Task<List<Exam>> GetDeptExamsAsync(int deptId);
    }
}
