using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class InstructorController : Controller
    {

        private readonly IInstructorRepository _instructorRepo;
        private readonly IExamRepository _examRepo;
        private readonly IQuestionRepository _questionRepo;
        private Random _random;

        public InstructorController(IInstructorRepository instructorRepo,
                                    IExamRepository examRepo,
                                    IQuestionRepository questionRepo)
        {
            _instructorRepo = instructorRepo;
            _examRepo = examRepo;
            _questionRepo = questionRepo;
            _random = new Random();
        }

        public IActionResult Index()
        {
            var instructor = _instructorRepo.GetByUserIdWithCourses(2).FirstOrDefault();

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
            var deptCourse = _instructorRepo.GetByUserIdWithCourses(2)
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
            var questions = _instructorRepo.GetCourseQuestions(2, id);

            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        [HttpGet]
        public IActionResult AddQuestion(int id)
        {
            var instructorId = _instructorRepo.GetInstIdByUserId(2);
            ViewBag.InstructorId = instructorId;
            ViewBag.CourseId = id;
            var model = new QuestionViewModel() { CourseId = id, InstructorId = instructorId };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddQuestion(int id, QuestionViewModel questionVM)
        {
            ViewBag.Courseid = id;
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
                model.Choices.AddRange([new ChoiceViewModel()]);
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

        ////------------------AddExam------------------

        [HttpGet]
        public IActionResult MakeExam(int id)
        {
            // TODO: remove static user id
            var instructorId = _instructorRepo.GetInstIdByUserId(2);
            if (instructorId == null || id == 0)
            {
                // TODO: user proper page
                return NotFound();
            }
            var model = new ExamViewModel() { CourseId = id };
            var questions = _questionRepo.GetInstQuestions(id, instructorId ?? 0);
            model.TotalMCQ = questions.Where(e => e.Type == QType.MCQ).Count();
            model.TotalTF = questions.Where(e => e.Type == QType.TrueFalse).Count();
            return View(model);
        }

        [HttpPost]
        public IActionResult MakeExam(ExamViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: remove static user id
                var instructorId = _instructorRepo.GetInstIdByUserId(2);

                if (instructorId == null)
                {
                    // TODO: replace this with (invalid user page)
                    return NotFound();
                }

                QDifficulty difficultyDto = (QDifficulty)(int)model.ExamDifficulty;
                var questions = _questionRepo.GetInstQuestions(model.CourseId, instructorId ?? 0, difficultyDto);

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
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult ShowExams()
        {
            List<Exam> examsDto = _instructorRepo.GetExamsWithIncludes(2);

            var model = new ExamListsViewModel(examsDto);
            return View(model);
        }
    }
}


