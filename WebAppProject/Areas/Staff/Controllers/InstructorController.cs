using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IExamTakenRepository _examTakenRepo;
        private readonly IMapper _mapper;
        private Random _random;
        private readonly int _instId;

        public InstructorController(IInstructorRepository instructorRepo,
                                    IExamRepository examRepo,
                                    IQuestionRepository questionRepo,
                                    IStudentRepository studentRepo,
                                    IExamTakenRepository examTakenRepo,
                                    IHttpContextAccessor accessor,
                                    IMapper mapper)
        {
            _instructorRepo = instructorRepo;
            _examRepo = examRepo;
            _questionRepo = questionRepo;
            _studentRepo = studentRepo;
            _examTakenRepo = examTakenRepo;
            _mapper = mapper;
            _random = new Random();
            _instId = int.Parse(accessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);
        }

        public async Task<IActionResult> Index()
        {
            var instructor = await _instructorRepo.GetByIdWithCourses(_instId).FirstOrDefaultAsync();

            if (instructor == null)
            {
                return NotFound();
            }

            IEnumerable<DeptCourseViewModel> deptCourses =
                _mapper.Map<IEnumerable<DeptCourseViewModel>>(instructor.DepartmentCourses);

            return View(deptCourses);
        }

        public async Task<IActionResult> Info(int id)
        {
            var deptCourses = await _instructorRepo.GetByIdWithCourses(_instId)
                                .Select(e => e.DepartmentCourses.Where(e => e.CourseId == id))
                                .FirstOrDefaultAsync();
            var deptCourse = deptCourses?.ElementAt(0);

            if (deptCourse == null)
            {
                return NotFound();
            }

            var deptCourseViewModel = _mapper.Map<DeptCourseViewModel>(deptCourse);
            return View(deptCourseViewModel);
        }

        public async Task<IActionResult> QuestionBank(int id)
        {
            ViewBag.CourseId = id;
            var questions = await _instructorRepo.GetCourseQuestionsAsync(_instId, id);

            if (questions == null)
            {
                return NotFound();
            }

            IEnumerable<QuestionViewModel> model =
                _mapper.Map<IEnumerable<QuestionViewModel>>(questions);

            return View(model);
        }

        [HttpGet]
        public IActionResult AddQuestion(int courseId)
        {
            var model = new QuestionViewModel() { CourseId = courseId, InstructorId = _instId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(int courseId, QuestionViewModel questionVM)
        {
            if (ModelState.IsValid)
            {
                if (questionVM.Choices.Count == 2)
                {
                    questionVM.Choices[0].IsCorrect = true;
                    questionVM.Choices[1].Text = questionVM.Choices[0].Text.Equals("True") ? "False" : "True";
                }

                var questionDto = _mapper.Map<Question>(questionVM);
                await _instructorRepo.AddQuestionAsync(questionDto);
                return RedirectToAction("QuestionBank", new { id = courseId });
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
        public async Task<IActionResult> EditQuestion(int id)
        {
            var question = await _questionRepo.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            var questionViewModel = _mapper.Map<QuestionViewModel>(question);
            return View(questionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion(QuestionViewModel questionVM)
        {
            if (ModelState.IsValid)
            {
                var questionDto = _mapper.Map<Question>(questionVM);
                await _questionRepo.UpdateAsync(questionDto);
                return RedirectToAction("QuestionBank", new { id = questionVM.CourseId });
            }

            return View(questionVM);
        }

        public async Task<IActionResult> QuestionInfo(int id)
        {
            var question = await _questionRepo.GetByIdCourseIncludedAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var questionViewModel = _mapper.Map<QuestionViewModel>(question);

            return View(questionViewModel);
        }

        public async Task<IActionResult> DeleteQuestion(int id, int qId)
        {
            await _questionRepo.DeleteAsync(qId);
            return RedirectToAction("QuestionBank", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> MakeExam(int courseId, int deptId)
        {
            if (courseId == 0)
            {
                // TODO: user proper page
                return NotFound();
            }
            var questions = await _questionRepo.GetInstQuestionsAsync(courseId, _instId);
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
                CourseId = courseId,
                TotalMCQ = mcqDifficultyCounts,
                TotalTF = tfDifficultyCounts,
                NoOfMCQ = new int[3],
                NoOfTF = new int[3],
                DepartmentId = deptId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MakeExam(InstExamViewModel instExamVM)
        {
            if (ModelState.IsValid && (instExamVM.NoOfMCQ.Sum() + instExamVM.NoOfTF.Sum() > 0))
            {
                var allQuestionsDto = await _questionRepo.GetInstQuestionsAsync(instExamVM.CourseId, _instId);

                // Separate true/false and multiple choice questions
                var mcqQuestionsDto = new List<Question>();
                var tfQuestionsDto = new List<Question>();

                for (int i = 0; i < 3; i++)
                {
                    if (instExamVM.NoOfMCQ[i] != 0)
                    {
                        var questions = allQuestionsDto.Where(q => q.Type == QType.MCQ && q.Difficulty == (QDifficulty)(i + 1))
                                                      .OrderBy(q => _random.Next())
                                                      .Take(instExamVM.NoOfMCQ[i])
                                                      .ToList();
                        mcqQuestionsDto.AddRange(questions);
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (instExamVM.NoOfTF[i] != 0)
                    {
                        var questions = allQuestionsDto.Where(q => q.Type == QType.TrueFalse && q.Difficulty == (QDifficulty)(i + 1))
                                                      .OrderBy(q => _random.Next())
                                                      .Take(instExamVM.NoOfTF[i])
                                                      .ToList();
                        tfQuestionsDto.AddRange(questions);
                    }
                }

                var generatedQuestions = mcqQuestionsDto.Concat(tfQuestionsDto);
                var examQuestions = new List<ExamQuestion>();
                int totalGrade = 0;

                Exam examDto = _mapper.Map<Exam>(instExamVM);
                int examId = await _examRepo.AddAsync(examDto);

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

                await _examRepo.UpdateTotalGradeAsync(examId, totalGrade);

                bool questionAdded = await _examRepo.AddExamQuestionsAsync(examQuestions);

                if (!questionAdded)
                {
                    // TODO: replace this with (invalid operation)
                    return NotFound();
                }
                return RedirectToAction("ShowExams");
            }
            return View(instExamVM);
        }

        public async Task<IActionResult> ShowExams()
        {
            List<Exam> exams = await _examRepo.GetInstructorExamsAsync(_instId);

            var model = _mapper.Map<ExamListsViewModel>(exams);
            return View(model);
        }

        public async Task<IActionResult> ExamInfo(int id)
        {
            Exam? exam = await _examRepo.GetByIdWithIncludesAsync(id);
            if (exam == null)
            {
                // TODO: user proper page
                return NotFound();
            }
            var instExamViewModel = _mapper.Map<InstExamViewModel>(exam);
            return View(instExamViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExamEdit(int id)
        {
            Exam? exam = await _examRepo.GetByIdWithIncludesAsync(id);
            if (exam == null)
            {
                // TODO: user proper page
                return NotFound();
            }
            var instExamViewModel = _mapper.Map<InstExamViewModel>(exam);
            return View(instExamViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ExamEdit(InstExamViewModel model)
        {

            if (ModelState.IsValid)
            {
                Exam examDto = _mapper.Map<Exam>(model);
                bool success = await _examRepo.UpdateAsync(examDto);
                return success ? RedirectToAction("ShowExams") : View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> ExamDelete(int id)
        {
            await _examRepo.DeleteAsync(id);
            return RedirectToAction("ShowExams");
        }

        public async Task<IActionResult> ExamGrades(int id)
        {
            List<ExamTaken> examsTaken = await _examRepo.GetExamGradesWithIncludesAsync(id);

            IEnumerable<StaffExamTakenViewModel> examsTakenVM =
                _mapper.Map<IEnumerable<StaffExamTakenViewModel>>(examsTaken);

            return View(examsTakenVM);
        }

        public async Task<IActionResult> StudentAnswers(int examId, int stdId)
        {
            var examTaken = await _examTakenRepo.GetExamTakenWithIncludesAsync(stdId, examId);
            List<ExamChoices> studentAnswers = await _studentRepo.GetStudentAnswersAsync(stdId, examId);

            if (examTaken == null || studentAnswers.IsNullOrEmpty())
            {
                return NotFound();
            }

            IEnumerable<StudentAnswersViewModel> studentAnswersVM =
                _mapper.Map<IEnumerable<StudentAnswersViewModel>>(studentAnswers);

            ViewBag.StudentGrade = examTaken.Grade;
            ViewBag.ExamGrade = examTaken.Exam.TotalGrade;

            return View(studentAnswersVM);
        }
    }
}


