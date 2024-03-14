using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using WebAppProject.ViewModels;



namespace WebAppProject.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentCourseRepository departmentCourseRepository;
        private readonly IExamTakenRepository examTakenRepository;
        private readonly IExamRepository examRepository;
       
        public StudentController(IStudentRepository studentRepository,
            IDepartmentCourseRepository departmentCourseRepository,
            IExamTakenRepository examTakenRepository,
            IExamRepository examRepository
            )
        {
            this.studentRepository=studentRepository;
            this.departmentCourseRepository = departmentCourseRepository;
            this.examTakenRepository = examTakenRepository;
            this.examRepository = examRepository;
            
        }
        public IActionResult Index()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            Student st = studentRepository.GetById(id)!;
            var model = departmentCourseRepository.GetCoursesByDeptIdWithIncludes(st.DepartmentId!.Value)!;          
            return View(model);
        }
        public IActionResult Exams()
        {        
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            Student st = studentRepository.GetById(id)!;
            var courses = departmentCourseRepository.GetCoursesByDeptIdWithIncludes(st.DepartmentId!.Value);
            var model = new StudentExamsViewModel();
            model.ExamsTaken = examTakenRepository.GetAllByStudentIdWithIncludes(id);
            List<Exam> passedExams =model.ExamsTaken.Select(e=>e.Exam).ToList();
            model.CommingExams = new List<Exam>();
            model.MissedExams = new List<Exam>();
            foreach (Course course in courses)          
                foreach (Exam exam in course.Exams)               
                    if(!passedExams.Contains(exam))
                        if (exam.ExamDate >= DateTime.Now || exam.ExamDate.AddMinutes((double)exam.DurationInMinutes) > DateTime.Now)
                            model.CommingExams.Add(exam);
                        else
                            model.MissedExams.Add(exam);
            return View(model); 
        }
        public IActionResult TakeExam(int examId)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            Student st = studentRepository.GetById(id)!;
            var model = new ExamViewModel();
            Exam exam = examRepository.GetByIdWithIncludes(examId)!;
            model.Exam = exam;
            model.DepartmentCourse = departmentCourseRepository.GetByDeptAndCrsIdWithIncludes(exam.CourseId, st.DepartmentId!.Value)!;
            model.TotalGrade = 0;
            foreach (var question in exam.Questions)
                model.TotalGrade += question.Grade;                  
            return View(model);
        }
        [HttpPost]
        public IActionResult SubmitExam(List<Choice> mcqChoices,List<Choice> tfChoices,int examId)
        {
            int grade=0;
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
            studentRepository.AddStudentAnswers(examId, id, mcqChoices.Concat(tfChoices).ToList());
            List<ExamChoices> examChoices = studentRepository.GetStudentAnswers(id, examId);
            foreach (var examChoice in examChoices)          
                if (examChoice.IsCorrect)               
                    grade += examChoice.Question.Grade;           
            ExamTaken examTaken = new ExamTaken { ExamId = examId, StudentId = id, Grade = grade };
            examTakenRepository.Add(examTaken);        
            return RedirectToAction("Answers",new {stId=id,examId=examId});
        }
        
        public IActionResult Answers(int stId, int examId)
        {
            var model = studentRepository.GetStudentAnswers(stId, examId);
            return View(model);
        }
    }
}
