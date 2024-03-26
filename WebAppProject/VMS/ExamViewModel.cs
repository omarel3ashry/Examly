using DataAccessLibrary.Model;

namespace WebAppProject.VMS
{
    public class ExamViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int? TotalGrade { get; set; }
        public string CourseName { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }= new HashSet<QuestionViewModel>();
    }
}
