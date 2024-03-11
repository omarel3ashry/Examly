using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Repository
{
    public class StudentRepo
    {
        
       
        public List<Student> GetAllByDeptId(int deptId)
        {


            return Student.StudentDemo.FindAll(st=>st.DeptId==deptId);
        }
        public Student GetById(int id)
        {

            return Student.StudentDemo.Find(st => st.Id == id);
        }
        public Student GetByUserId(int userId)
        {
            return Student.StudentDemo.Find(st=>st.UserId==userId);
        }
        public void Add(Student student)
        {
            student.Id = Student.StudentDemo.Last().Id + 1;
            student.User = User.UserDemo.Find(u => u.Id == student.UserId);
            Student.StudentDemo.Add(student);
        }
        public void Update(Student student)
        {
            var st = GetById(student.Id);
            st = student;
        }
        public List<StudentExam> GetGrades(int id)
        {
            return StudentExam.studentExamDemo.FindAll(se=>se.StudentId==id);
        }

        public void Delete(int stId)
        {
            Student.StudentDemo.Remove(GetById(stId));
        }

        public List<ChoiceQuestion> GetAnswers(int stId, int examId)
        {
            List<ChoiceQuestion> result= new List<ChoiceQuestion>();// ChoiceQuestion : Question , CorrectChoices, StudentChoices
            List<Choice> ExamChoices=StudentExamChoice.studentExamChoiceDemo.FindAll(sec => sec.StudentId == stId && sec.ExamId == examId).Select(sec=>sec.Choice).ToList();
            List<Choice> QuestionCorrectChoices;
            List<Choice> StudentChoicesForQuestion;

            foreach(Choice choice in ExamChoices)
            {
                if (!result.Select(r => r.Question).Contains(choice.Question))
                {
                    Question question = choice.Question;
                    QuestionCorrectChoices = Choice.ChoiceDemo.FindAll(choice=> choice.QuestionId == question.Id && choice.IsCorrect);
                    StudentChoicesForQuestion = ExamChoices.FindAll(choice=> choice.QuestionId == question.Id);
                    result.Add(new ChoiceQuestion { Question = choice.Question, CorrectChoices = QuestionCorrectChoices, StudentChoices = StudentChoicesForQuestion });
                }
                
            }
            return result;
            
        }
    }
}
