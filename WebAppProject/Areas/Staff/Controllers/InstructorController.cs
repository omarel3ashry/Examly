using AutoMapper;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Areas.Staff.ViewModels;
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
        private readonly IMapper _mapper;
        private Random _random;
        private readonly int _instId;

        public InstructorController(IInstructorRepository instructorRepo,
                                    IExamRepository examRepo,
                                    IQuestionRepository questionRepo,
                                    IStudentRepository studentRepo,
                                    IHttpContextAccessor accessor,
                                    IMapper mapper)
        {
            _instructorRepo = instructorRepo;
            _examRepo = examRepo;
            _questionRepo = questionRepo;
            _studentRepo = studentRepo;
            _mapper = mapper;
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

            var courseInfoViewModel = _mapper.Map<CourseInfoViewModel>(deptCourse);
            return View(courseInfoViewModel);
        }

/*        public IActionResult Info(int id)
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
        }*/

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
        public IActionResult MakeExam(int id,int deptId)
        {
            if (id == 0)
            {
                // TODO: user proper page
                return NotFound();
            }
            var questions = _questionRepo.GetInstQuestions(id, _instId);
            var mcqDifficultyG = questions.Where(e => e.Type == QType.MCQ)
                                      .GroupBy(q => q.Difficulty)
                                      .OrderBy(q => (int)q.Key)
                                      .ToList();
            var tfDifficultyG = questions.Where(e => e.Type == QType.TrueFalse)
                                      .GroupBy(q => q.Difficulty)
                                      .OrderBy(q => (int)q.Key)
                                      .ToList();

            int[] mcqDifficultyCounts = new int[3];
            for (int i = 0; i < mcqDifficultyG.Count(); i++)
            {
                int index = ((int)mcqDifficultyG[i].Key) - 1;
                mcqDifficultyCounts[index] = mcqDifficultyG[i].Count();
            }
            int[] tfDifficultyCounts = new int[3];
            for (int i = 0; i < tfDifficultyG.Count(); i++)
            {
                int index = ((int)tfDifficultyG[i].Key) - 1;
                tfDifficultyCounts[index] = tfDifficultyG[i].Count();
            }

            if (mcqDifficultyCounts.Sum() + tfDifficultyCounts.Sum() == 0)
            {
                return NoContent();
            }

            var model = new InstExamViewModel()
            {
                CourseId = id,
                TotalMCQ = mcqDifficultyCounts,
                TotalTF = tfDifficultyCounts,
                DepartmentId=deptId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult MakeExam(InstExamViewModel model)
        {
            if (ModelState.IsValid && (model.NoOfMCQ.Sum() + model.NoOfTF.Sum() > 0))
            {
                var allQuestionsDto = _questionRepo.GetInstQuestions(model.CourseId, _instId);

                // Separate true/false and multiple choice questions
                var mcqQuestionsDto = new List<Question>();
                var tfQuestionsDto = new List<Question>();

                for (int i = 0; i < 3; i++)
                {
                    if (model.NoOfMCQ[i] != 0)
                    {
                        var questions = allQuestionsDto.Where(q => q.Type == QType.MCQ && q.Difficulty == (QDifficulty)(i + 1))
                                                      .OrderBy(q => _random.Next())
                                                      .Take(model.NoOfMCQ[i])
                                                      .ToList();
                        mcqQuestionsDto.AddRange(questions);
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (model.NoOfTF[i] != 0)
                    {
                        var questions = allQuestionsDto.Where(q => q.Type == QType.TrueFalse && q.Difficulty == (QDifficulty)(i + 1))
                                                      .OrderBy(q => _random.Next())
                                                      .Take(model.NoOfTF[i])
                                                      .ToList();
                        tfQuestionsDto.AddRange(questions);
                    }
                }

                var generatedQuestions = mcqQuestionsDto.Concat(tfQuestionsDto);
                var examQuestions = new List<ExamQuestion>();
                int totalGrade = 0;

                Exam examDto = model.ToExamDTO();
                int examId = _examRepo.Add(examDto);

                if (examId == 0)
                {
                    // TODO: replace this with (invalid operation)
                    return NotFound();
                }

                foreach (var question in generatedQuestions)
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


