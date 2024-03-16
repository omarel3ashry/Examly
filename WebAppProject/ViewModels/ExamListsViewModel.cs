using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class ExamListsViewModel
    {
        public List<InstExamViewModel> UpcomingExams { get; set; }
        public List<InstExamViewModel> PastExams { get; set; }

        public ExamListsViewModel(List<Exam> examsDto)
        {
            UpcomingExams = new List<InstExamViewModel>();
            PastExams = new List<InstExamViewModel>();

            foreach (Exam exam in examsDto)
                if (exam.ExamDate > DateTime.Now)
                    UpcomingExams.Add(new InstExamViewModel(exam));
                else
                    PastExams.Add(new InstExamViewModel(exam));
        }
    }
}
