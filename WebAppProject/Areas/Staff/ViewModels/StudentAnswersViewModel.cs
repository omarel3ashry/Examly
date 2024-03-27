using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Staff.ViewModels
{
    public class StudentAnswersViewModel
    {
        public string QuestionText { get; set; }
        public int QuestionGrade { get; set; }
        public List<ChoiceViewModel> CorrectChoices { get; set; }
        public List<ChoiceViewModel> StudentChoices { get; set; }
        public bool IsCorrect { get; set; }
    }
}
