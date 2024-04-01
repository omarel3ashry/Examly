using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.ViewModels;


namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentCourseRepository departmentCourseRepository;
        private readonly IExamTakenRepository examTakenRepository;
        private readonly IExamRepository examRepository;
        private readonly IMapper mapper;
        private readonly int _stdId;

        public StudentController(IStudentRepository studentRepository,
                                 IDepartmentCourseRepository departmentCourseRepository,
                                 IExamTakenRepository examTakenRepository,
                                 IExamRepository examRepository,
                                 IHttpContextAccessor accessor,
                                 IMapper mapper
            )
        {
            this.studentRepository = studentRepository;
            this.departmentCourseRepository = departmentCourseRepository;
            this.examTakenRepository = examTakenRepository;
            this.examRepository = examRepository;
            this.mapper = mapper;
            _stdId = int.Parse(accessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
        }

        public async Task<IActionResult> Index()
        {

            Student? st = await studentRepository.GetByIdAsync(_stdId);
            var courses = await departmentCourseRepository
                                    .GetCoursesByDeptIdWithIncludesAsync(st!.DepartmentId!.Value)!;
            var model = mapper.Map<IList<CourseViewModel>>(courses);
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Exams()
        {
            Student? st = await studentRepository.GetByIdAsync(_stdId);
            var departmentExams = await examRepository.GetDeptExamsAsync(st!.DepartmentId!.Value);
            var ExamsTaken = await examTakenRepository.GetAllByStudentIdWithIncludesAsync(_stdId);
            var passedExams = ExamsTaken.Select(e => e.Exam).ToList();
            var CommingExams = new List<Exam>();
            var MissedExams = new List<Exam>();
            foreach (Exam exam in departmentExams)
                if (!passedExams.Contains(exam))
                    if (exam.ExamDate >= DateTime.Now || exam.ExamDate.AddMinutes(exam.DurationInMinutes) > DateTime.Now)
                        CommingExams.Add(exam);
                    else
                        MissedExams.Add(exam);
            var model = new StudentExamsViewModel();
            model.CommingExams = mapper.Map<IList<ExamViewModel>>(CommingExams).ToList();
            model.ExamsTaken = mapper.Map<IList<ExamTakenViewModel>>(ExamsTaken).ToList();
            model.MissedExams = mapper.Map<IList<ExamViewModel>>(MissedExams).ToList();
            return View(model);
        }

        public async Task<IActionResult> TakeExam(int examId)
        {
            Student? st = await studentRepository.GetByIdAsync(_stdId);
            var exam = await examRepository.GetByIdWithIncludesAsync(examId);
            var examTaken = await examTakenRepository.GetExamTakenWithIncludesAsync(_stdId, examId);
            if (exam != null
                && examTaken==null
                && exam.ExamDate <= DateTime.Now 
                && exam.ExamDate.AddMinutes(exam.DurationInMinutes) >= DateTime.Now
                )
            {
                var departmentCourse = await departmentCourseRepository.GetByDeptAndCrsIdWithIncludesAsync(exam.CourseId, st!.DepartmentId!.Value)!;
                var model = new TakeExamViewModel();
                model.Exam = mapper.Map<ExamViewModel>(exam);
                model.DepartmentCourse = mapper.Map<DepartmentCourseViewModel>(departmentCourse);
                return View(model);
            }
            return RedirectToAction("Exams");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitExam(List<Choice> mcqChoices, List<Choice> tfChoices, int examId)
        {
            var exam = await examRepository.GetByIdWithIncludesAsync(examId);
            if (exam != null
                && exam.ExamDate <= DateTime.Now
                && exam.ExamDate.AddMinutes(exam.DurationInMinutes).AddSeconds(30) >= DateTime.Now
                )
            {           
                int grade = 0;
                await studentRepository.AddStudentAnswersAsync(examId, _stdId, mcqChoices.Concat(tfChoices).ToList());
                List<ExamChoices> examChoices = await studentRepository.GetStudentAnswersAsync(_stdId, examId);
                foreach (var examChoice in examChoices)
                    if (examChoice.IsCorrect)
                        grade += examChoice.Question.Grade;
                ExamTaken examTaken = new ExamTaken { ExamId = examId, StudentId = _stdId, Grade = grade };
                await examTakenRepository.AddAsync(examTaken);
                return RedirectToAction("Answers", new { examId });
            }
            return RedirectToAction("Exams");
        }

        public async Task<IActionResult> Answers(int examId)
        {
            var examTaken = await examTakenRepository.GetExamTakenWithIncludesAsync(_stdId,examId);
            var studentAnswers = await studentRepository.GetStudentAnswersAsync(_stdId, examId);
            if (examTaken == null || studentAnswers == null)
            {
                return NotFound();
            }
            IEnumerable<StudentAnswersViewModel> model =
                    mapper.Map<IEnumerable<StudentAnswersViewModel>>(studentAnswers);
            ViewBag.StudentGrade = examTaken.Grade;
            ViewBag.Exam = examTaken.Exam;
            return View(model);
        }
    }
}
