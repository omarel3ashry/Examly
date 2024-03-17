using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Staff.Controllers
{
    [Authorize(Roles = "Manager,Instructor")]
    [Area(areaName: "Staff")]
    public class InstructorController : Controller
    {

        private readonly IInstructorRepository _instructorRepo;
        private readonly IExamRepository _examRepo;
        private readonly IQuestionRepository _questionRepo;
        private readonly IStudentRepository _studentRepo;
        private Random _random;
        private readonly int _instId;

        public InstructorController(IInstructorRepository instructorRepo,
                                    IExamRepository examRepo,
                                    IQuestionRepository questionRepo,
                                    IStudentRepository studentRepo,
                                    IHttpContextAccessor accessor)
        {
            _instructorRepo = instructorRepo;
            _examRepo = examRepo;
            _questionRepo = questionRepo;
            _studentRepo = studentRepo;
            _random = new Random();
            _instId = int.Parse(accessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
        }

        public IActionResult Index()
        {
            var instructor = _instructorRepo.GetByIdWithCourses(_instId).FirstOrDefault();

            if (instructor == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = instructor.DepartmentCourses;

                return View(viewModel);
            }
        }

        public IActionResult Info(int id)
        {
            var deptCourse = _instructorRepo.GetByIdWithCourses(_instId)
                                .Select(e => e.DepartmentCourses.Where(e => e.CourseId == id))
                                .FirstOrDefault()?.ElementAt(0);

            if (deptCourse == null)
            {
                return NotFound();
            }
            var courseInfo = new CourseInfo()
            {
                CourseName = deptCourse.Course.Name,
                DepartmentName = deptCourse.Department.Name,
                CourseDescription = deptCourse.Course.Description
            };
            return View(courseInfo);
        }

        public IActionResult QuestionBank(int id)
        {
            ViewBag.CourseId = id;
            var questions = _instructorRepo.GetCourseQuestions(_instId, id);

            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        [HttpGet]
        public IActionResult AddQuestion(int id)
        {
            var model = new QuestionViewModel() { CourseId = id, InstructorId = _instId };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddQuestion(int id, QuestionViewModel questionVM)
        {
            if (ModelState.IsValid)
            {
                var questionDto = questionVM.ToQuestionDTO();
                _instructorRepo.AddQuestion(questionDto);
                return RedirectToAction("QuestionBank", new { id });
            }

            return View(questionVM);
        }

        public IActionResult AddChoices(int id)
        {
            var model = new QuestionViewModel();
            if (id == 1)
            {
                model.Choices.AddRange(
                        [
                            new ChoiceViewModel(),
                            new ChoiceViewModel(),
                            new ChoiceViewModel(),
                            new ChoiceViewModel()
                        ]
                        );
                return PartialView("MCQChoicesPartial", model);
            }
            else
            {
                model.Choices.AddRange([new ChoiceViewModel(), new ChoiceViewModel()]);
                return PartialView("TFChoicesPartial", model);
            }
        }

        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            var question = _questionRepo.GetById(id);
            if (question == null)
            {
                return NotFound();
            }

            return View(new QuestionViewModel(question));
        }

        [HttpPost]
        public IActionResult EditQuestion(QuestionViewModel questionVM)
        {
            if (ModelState.IsValid)
            {
                var questioDto = questionVM.ToQuestionDTO(isNew: false);
                _questionRepo.Update(questioDto);
                return RedirectToAction("QuestionBank", new { id = questionVM.CourseId });
            }

            return View(questionVM);
        }

        public IActionResult QuestionInfo(int id)
        {
            var questionDto = _questionRepo.GetByIdCourseIncluded(id);

            if (questionDto == null)
            {
                return NotFound();
            }
            var model = new QuestionViewModel(questionDto);
            return View(model);
        }

        public IActionResult DeleteQuestion(int id, int qId)
        {
            _questionRepo.Delete(qId);
            return RedirectToAction("QuestionBank", new { id });
        }

        [HttpGet]
        public IActionResult MakeExam(int id)
        {
            if (id == 0)
            {
                // TODO: user proper page
                return NotFound();
            }
            var model = new InstExamViewModel() { CourseId = id };
            var questions = _questionRepo.GetInstQuestions(id, _instId);
            model.TotalMCQ = questions.Where(e => e.Type == QType.MCQ).Count();
            model.TotalTF = questions.Where(e => e.Type == QType.TrueFalse).Count();
            return View(model);
        }

        [HttpPost]
        public IActionResult MakeExam(InstExamViewModel model)
        {
            if (ModelState.IsValid)
            {
                QDifficulty difficultyDto = (QDifficulty)(int)model.ExamDifficulty;
                var questions = _questionRepo.GetInstQuestions(model.CourseId, _instId, difficultyDto);

                // Separate true/false and multiple choice questions
                var mcqQuestions = questions.Where(q => q.Type == QType.MCQ)
                                                       .OrderBy(q => _random.Next())
                                                       .Take(model.NoOfMCQ)
                                                       .ToList();

                var tfQuestions = questions.Where(q => q.Type == QType.TrueFalse)
                                                        .OrderBy(q => _random.Next())
                                                        .Take(model.NoOfTF)
                                                        .ToList();

                var allQuestions = mcqQuestions.Concat(tfQuestions);
                var examQuestions = new List<ExamQuestion>();
                int totalGrade = 0;

                Exam examDto = model.ToExamDTO();
                int examId = _examRepo.Add(examDto);

                if (examId == 0)
                {
                    // TODO: replace this with (invalid operation)
                    return NotFound();
                }

                foreach (var question in allQuestions)
                {
                    examQuestions.Add(new ExamQuestion() { ExamId = examId, QuestionId = question.Id });
                    totalGrade += question.Grade;
                }

                _examRepo.UpdateTotalGrade(examId, totalGrade);

                bool questionAdded = _examRepo.AddExamQuestions(examQuestions);

                if (!questionAdded)
                {
                    // TODO: replace this with (invalid operation)
                    return NotFound();
                }
                return RedirectToAction("ShowExams");
            }
            return View(model);
        }

        public IActionResult ShowExams()
        {
            List<Exam> examsDto = _examRepo.GetInstructorExam(_instId);

            var model = new ExamListsViewModel(examsDto);
            return View(model);
        }

        public IActionResult ExamInfo(int id)
        {
            Exam? examDto = _examRepo.GetByIdWithIncludes(id);
            if (examDto == null)
            {
                // TODO: user proper page
                return NotFound();
            }
            var model = new InstExamViewModel(examDto);
            return View(model);
        }

        [HttpGet]
        public IActionResult ExamEdit(int id)
        {
            Exam? examDto = _examRepo.GetByIdWithIncludes(id);
            if (examDto == null)
            {
                // TODO: user proper page
                return NotFound();
            }
            var model = new InstExamViewModel(examDto);
            return View(model);
        }

        [HttpPost]
        public IActionResult ExamEdit(InstExamViewModel model)
        {

            if (ModelState.IsValid)
            {
                Exam examDto = model.ToExamDTO(isNew: false);
                bool success = _examRepo.Update(examDto);
                return success ? RedirectToAction("ShowExams") : View(model);
            }

            return View(model);
        }

        public IActionResult ExamDelete(int id)
        {
            _examRepo.Delete(id);
            return RedirectToAction("ShowExams");
        }

        public IActionResult ExamGrades(int id)
        {
            List<ExamTaken> examsTaken = _examRepo.GetExamGradesWithIncludes(id);
            return View(examsTaken);
        }

        public IActionResult StudentAnswers(int examId, int stdId)
        {
            var model = _studentRepo.GetStudentAnswers(stdId, examId);
            return View(model);
        }
    }
}


