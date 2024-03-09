using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Repository
{
    public class StudentRepo
    {
        public List<Student> _students = [
                new Student() { Id = 3, Name = "hassan" },
            new Student() { Id = 2, Name = "loay" },
            new Student() { Id = 5, Name = "madgy" },
            new Student() { Id = 1, Name = "omar" }

            ];
       
        public List<Student> GetAllByDeptId(int BranchId)
        {


            return _students;
        }
        public Student GetById(int id)
        {

            return _students.Find(st => st.Id == id);
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
            _students.Remove(GetById(stId));
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
