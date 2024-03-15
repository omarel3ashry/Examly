using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class ExamListsViewModel
    {
        public List<ExamViewModel> UpcomingExams { get; set; }
        public List<ExamViewModel> PastExams { get; set; }

        public ExamListsViewModel(List<Exam> examsDto)
        {
            UpcomingExams = new List<ExamViewModel>();
            PastExams = new List<ExamViewModel>();

            foreach (Exam exam in examsDto)
                if (exam.ExamDate > DateTime.Now)
                    UpcomingExams.Add(new ExamViewModel(exam));
                else
                    PastExams.Add(new ExamViewModel(exam));
        }
    }
}
