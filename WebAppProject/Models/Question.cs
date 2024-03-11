namespace WebAppProject.Models
{
    public class Question
    {
        public static List<Question> QuestionDemo = [
            new Question { Id = 1, Body = "what color is the sky?", CrsId = 3, Course = Course.CourseDemo[2] },
            new Question { Id = 2, Body = "what color is the grass?", CrsId = 3, Course = Course.CourseDemo[2] },
            new Question { Id = 3, Body = "what color is the sun?", CrsId = 2, Course = Course.CourseDemo[1] }
        ];
        public int Id { get; set; }
        public string Body { get; set; }
        public int CrsId { get; set; }
        public Course Course { get; set; }
    }
}
