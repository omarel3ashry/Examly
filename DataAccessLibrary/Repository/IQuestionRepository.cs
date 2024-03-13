using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public bool AddChoice(Choice choice);
        public Task<bool> AddChoiceAsync(Choice choice);
        public int AddChoices(List<Choice> choices);
        public Task<int> AddChoicesAsync(List<Choice> choices);
        public Question? GetByIdCourseIncluded(int id);
    }
}
