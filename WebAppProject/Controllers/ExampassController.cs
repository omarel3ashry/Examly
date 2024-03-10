using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class ExampassController : Controller
    {
        public IActionResult ExamPassed()
        {
            var exams = new List<Exampass>
            {
                new Exampass { Name = "Exam 1 - Course 33", Date = DateTime.Now, Available = true },
                new Exampass { Name = "Exam 1 - Course 33", Date = new DateTime(2024, 9, 9), Available = false }
            };

            var passedExams = new List<Exampass>
            {
                new Exampass { Name = "Exam 1 - Course 44", Date = new DateTime(2024, 9, 9), Grades = "Grades" },
                new Exampass { Name = "Exam 1 - Course 44", Date = new DateTime(2024, 9, 10), Grades = "Grades" },
                new Exampass { Name = "Exam 1 - Course 44", Date = new DateTime(2024, 9, 11), Grades = "Grades" }
            };

            exams.AddRange(passedExams); // Combine both lists

            return View("ExamPassed", exams); // Pass combined list to a single view
        }
    }
}
