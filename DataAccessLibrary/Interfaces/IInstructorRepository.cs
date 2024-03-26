using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public int? GetInstIdByUserId(int userId);
        public Instructor? GetByUserId(int userId);
        public IQueryable<Instructor?> GetByIdWithCourses(int instId);
        public List<Question> GetAllQuestions(int instId);
        public List<Question> GetCourseQuestions(int instId, int courseId);
        public int AddQuestion(Question entity);
        public List<Exam> GetExamsWithIncludes(int instId);
    }
}
