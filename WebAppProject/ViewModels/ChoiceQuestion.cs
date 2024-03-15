using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class ChoiceQuestion
    {
        public Question Question { get; set; }
        public List<Choice> CorrectChoices { get; set; }
        public List<Choice> StudentChoices { get; set; }
    }
}
