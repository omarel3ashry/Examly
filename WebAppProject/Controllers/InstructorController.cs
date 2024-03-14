using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Collections.Generic;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class InstructorController : Controller
    {

        private readonly IInstructorRepository _instructorRepo;
        private readonly IQuestionRepository _questionRepo;

        public InstructorController(IInstructorRepository instructorRepo,
                                    IQuestionRepository questionRepo)
        {
            _instructorRepo = instructorRepo;
            _questionRepo = questionRepo;
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
                DepartmentName = deptCourse.Department.Name
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
                _instructorRepo.AddQuestion(questionVM.ToQuestionDTO());
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

        ////-------------------------------------------------

        ////------------------AddExam------------------
        //[HttpGet]
        //public IActionResult GenerateExam(int courseId)
        //{
        //    var course = _instructorRepository.GetCourseById(courseId);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    // Get questions for the course
        //    var questions = _instructorRepository.GetQuestionBank(courseId);

        //    // Separate true/false and multiple choice questions
        //    var trueFalseQuestions = questions.Where(q => q.Type == QuestionType.TrueFalse).ToList();
        //    var multipleChoiceQuestions = questions.Where(q => q.Type == QuestionType.MultipleChoice).ToList();

        //    // Shuffle the question
        //    var random = new Random();
        //    trueFalseQuestions = trueFalseQuestions.OrderBy(q => random.Next()).ToList();
        //    multipleChoiceQuestions = multipleChoiceQuestions.OrderBy(q => random.Next()).ToList();


        //    var viewModel = new ExamViewModel
        //    {
        //        TrueFalseQuestions = trueFalseQuestions,
        //        MultipleChoiceQuestions = multipleChoiceQuestions
        //    };

        //    return View(viewModel);
        //}



    }
}


