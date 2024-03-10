using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class ExamQuestionsController : Controller
    {
        public IActionResult ExamQuestions()
        {
            var CourseExam = new List<ExamQuestions>
            {
                new ExamQuestions
                {
                    Department = "Programming",
                    CourseName = "C#",
                    InstructorName = "Prof. Mahmoud Ouf",
                    TotalGrade = 100,
                    DurationInHours = 2,
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Text = "What is C#?",
                            Options = new List<string> { "A programming language", "A database", "A web server" },
                            CorrectAnswer = "A programming language",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "What is ASP.NET Core?",
                            Options = new List<string> { "A framework for building web apps", "A database", "A programming language" },
                            CorrectAnswer = "A framework for building web apps",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "What is HTML?",
                            Options = new List<string> { "A programming language", "A markup language", "A database" },
                            CorrectAnswer = "A markup language",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "What is CSS?",
                            Options = new List<string> { "A programming language", "A styling language", "A database" },
                            CorrectAnswer = "A styling language",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "C# is a programming language.",
                            Options = new List<string> { "True", "False" },
                            CorrectAnswer = "True",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "ASP.NET Core is a framework for building web apps.",
                            Options = new List<string> { "True", "False" },
                            CorrectAnswer = "True",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "HTML is a programming language.",
                            Options = new List<string> { "True", "False" },
                            CorrectAnswer = "False",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "CSS is a styling language.",
                            Options = new List<string> { "True", "False" },
                            CorrectAnswer = "True",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "What is AJAX?",
                            Options = new List<string> { "Asynchronous JavaScript and XML", "Asynchronous Java and XML", "Asynchronous JavaScript and XHTML" },
                            CorrectAnswer = "Asynchronous JavaScript and XML",
                            UserAnswer = ""
                        },
                        new Question
                        {
                            Text = "What is REST?",
                            Options = new List<string> { "Representational State Transfer", "Remote Service Transfer", "Representational Service Transfer" },
                            CorrectAnswer = "Representational State Transfer",
                            UserAnswer = ""
                        }
                    }
                }
            };

            return View("ExamQuestions", CourseExam);
        }
        public IActionResult ExamResults(List<Question> questions)
        {
            // Check if all questions are answered
            bool allAnswered = true;
            StringBuilder unansweredMessage = new StringBuilder();
            int unansweredCount = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (string.IsNullOrEmpty(questions[i].UserAnswer))
                {
                    allAnswered = false;
                    unansweredCount++;
                    unansweredMessage.Append($"Question {i + 1} "); 
                    if (unansweredCount < questions.Count - i)
                    {
                        unansweredMessage.Append(", ");
                    }
                }
            }

            if (!allAnswered)
            {
                TempData["ErrorMessage"] = $"Please answer all the questions. You have not answered the following {unansweredCount} question(s): {unansweredMessage}";
                return RedirectToAction("ExamQuestions");
            }

            int score = 0;
            foreach (var question in questions)
            {
                if (question.UserAnswer == question.CorrectAnswer)
                {
                    score++;
                }
            }

            int maxMarksPerQuestion = 10;
            int finalGrade = score * maxMarksPerQuestion;

            ViewBag.Score = score;
            ViewBag.FinalGrade = finalGrade;

            return View("ExamResults", questions);
        }
    }
}