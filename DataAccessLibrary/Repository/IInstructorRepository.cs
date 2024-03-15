using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public int? GetInstIdByUserId(int userId);
        public Instructor? GetByUserId(int userId);
        public Task<Instructor?> GetByUserIdAsync(int userId);
        public IQueryable<Instructor?> GetByUserIdWithCourses(int id);
        public List<Question> GetAllQuestions(int userId);
        public List<Question> GetCourseQuestions(int userId, int courseId);
        public int AddQuestion(Question entity);
        public List<Exam> GetExamsWithIncludes(int instId);
    }
}
