namespace WebAppProject.Models
{
    public class StudentExam
    {
        static public List<StudentExam> studentExamDemo = [
            new StudentExam() { StudentId = 3, ExamId = 3, Grade = 50, Exam = Exam.ExamDemo[0] },
            new StudentExam() { StudentId = 3, ExamId = 4, Grade = 60, Exam = Exam.ExamDemo[1] },
            new StudentExam() { StudentId = 3, ExamId = 3, Grade = 50, Exam = Exam.ExamDemo[2] },
            new StudentExam() { StudentId = 2, ExamId = 4, Grade = 60, Exam = Exam.ExamDemo[3] },

        ];
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }
        public Exam Exam { get; set; }
    }
}
