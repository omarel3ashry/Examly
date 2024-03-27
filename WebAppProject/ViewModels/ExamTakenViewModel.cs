namespace WebAppProject.ViewModels
{
    public class ExamTakenViewModel
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }
        public ExamViewModel Exam { get; set; }
    }
}
