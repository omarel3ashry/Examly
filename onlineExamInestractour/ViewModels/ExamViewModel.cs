using onlineExamInestractour.Models;

namespace onlineExamInestractour.ViewModels
{
    internal class ExamViewModel
    {
        public List<Question> TrueFalseQuestions { get; set; }
        public List<Question> MultipleChoiceQuestions { get; set; }
    }
}