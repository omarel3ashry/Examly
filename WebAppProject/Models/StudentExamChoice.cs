namespace WebAppProject.Models
{
    public class StudentExamChoice
    {
        public static List<StudentExamChoice> studentExamChoiceDemo = [
            new StudentExamChoice { StudentId = 3, ExamId = 3, ChoiceId = 1, Choice = Choice.ChoiceDemo[0] },
           
            new StudentExamChoice { StudentId = 3, ExamId = 3, ChoiceId = 5, Choice = Choice.ChoiceDemo[4] }
        ];
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int ChoiceId { get; set; }
        public Choice Choice { get; set; }
    }
}
