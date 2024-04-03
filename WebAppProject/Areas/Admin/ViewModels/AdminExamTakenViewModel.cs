namespace WebAppProject.Areas.Admin.ViewModels
{
    public class AdminExamTakenViewModel
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }
        public string StudentName { get; set; }
        public string ExamTitle { get; set; }
        public string ExamDate { get; set; }
        public string CourseName { get; set; }
    }
}
