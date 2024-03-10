using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using onlineExamInestractour.Models;
using onlineExamInestractour.Repository;

namespace onlineExamInestractour.Controllers
{
    public class InstructorController : Controller
    {
        IInstructorRepository instructorRepository;
        public InstructorController(IInstructorRepository _instructorRepository)
        {
            instructorRepository = _instructorRepository;
        }
        public IActionResult Index()
        {
            var instructor = instructorRepository.GetInstructors();

            if (instructor == null)
            {
                return NotFound();
            }
            else
            {
                var viewModel = instructorRepository.GetCoursesTaught();

                return View(viewModel);
            }
            
        }
        public IActionResult Info(int id)
        {
            var courseInfo = instructorRepository.GetCourseInfo(id);

            if (courseInfo == null)
            {
                return NotFound();
            }

            return View(courseInfo);
        }
        public IActionResult QuestionBank()
        {
            var questions = instructorRepository.GetAllQuestions();

            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }
        [HttpGet]
        public IActionResult AddQuestion()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestion(int id,Question question, List<string> choices)
        {
           
            if (ModelState.IsValid)
            {
                question.CourseId=id;
                instructorRepository.AddQuestionWithChoices(id,question, choices);
                return RedirectToAction("QuestionBank", new { id });
            }

            return View(question);
        }

        
        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            var question = instructorRepository.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }
            question.Choices = instructorRepository.GetChoicesForQuestion(id);

            return View(question);

           
        }

        [HttpPost]
        public IActionResult EditQuestion(Question question, IEnumerable<Choice> choices)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.UpdateQuestion(question, choices);
                return RedirectToAction("QuestionBank");
            }
            return View(question);
        }
        public IActionResult QuestionInfo(int id)
        {
            var QuestionInfo = instructorRepository.GetQuestionInfo(id);

            if (QuestionInfo == null)
            {
                return NotFound();
            }

            return View(QuestionInfo);
        }
        public IActionResult DeleteQuestion(int id)
        {
            instructorRepository.Deletequestion(id);
            return RedirectToAction("QuestionBank");
        }
        //-------------------------------------------------
        public IActionResult mangerindex()
        {
            var courses = instructorRepository.GetCourses();
            return View(courses);
        }
        public IActionResult Details(int id)
        {
            var course = instructorRepository.GetInfo(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpGet]
        public IActionResult AssignInstructor(int id)
        {
            var course = instructorRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            var availableInstructors = instructorRepository.GetInstructors();
            var instructorItems = availableInstructors.Select(i => new SelectListItem
            {
                Value = i.InstructorId.ToString(),
                Text = i.InstructorName
            });

            ViewBag.Instructors = instructorItems;

            return View();
        }


        [HttpPost]
        public IActionResult AssignInstructor(int Id, int instructorId)
        {
            var course = instructorRepository.GetCourseById(Id);
            if (course == null)
            {
                return NotFound();
            }

            var instructor = instructorRepository.GetInstructorById(instructorId);
            if (instructor == null)
            {
                return NotFound();
            }

            course.InstructorId = instructorId;
            instructorRepository.SaveChanges();

            return RedirectToAction("Details", new { id = Id });
        }
        [HttpGet]
        public IActionResult AddCourse(int id )
        {
          ViewBag. DepartmentId = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.AddCourse(course);
                return RedirectToAction("managerindex");
            }
            return View(course); 
        }








    }
}


