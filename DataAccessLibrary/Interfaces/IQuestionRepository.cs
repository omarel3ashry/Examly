using DataAccessLibrary.Model;

namespace DataAccessLibrary.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public bool AddChoice(Choice choice);
        public Task<bool> AddChoiceAsync(Choice choice);
        public int AddChoices(List<Choice> choices);
        public Task<int> AddChoicesAsync(List<Choice> choices);
        public Question? GetByIdCourseIncluded(int id);
        public Task<Question?> GetByIdCourseIncludedAsync(int id);
        public List<Question> GetInstQuestions(int crsId, int instId);
        public Task<List<Question>> GetInstQuestionsAsync(int crsId, int instId);
        public List<Question> GetInstQuestions(int crsId, int instId, QDifficulty difficulty);
        public Task<List<Question>> GetInstQuestionsAsync(int crsId, int instId, QDifficulty difficulty);
    }
}
