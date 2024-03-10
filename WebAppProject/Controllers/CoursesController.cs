using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult Coursedesc()
        {
            var courses = new List<Course>
            {
           new Course { Id = 1, Title = "Introduction to Programming", Description = "Learn the basics of programming" },
           new Course { Id = 2, Title = "Web Development Fundamentals", Description = "Introduction to web development" },
           new Course { Id = 3, Title = "Data Structures and Algorithms", Description = "Essential concepts for efficient coding" },
           new Course { Id = 4, Title = "Database Management", Description = "Learn to manage databases effectively" },
           new Course { Id = 5, Title = "Mobile App Development", Description = "Create apps for iOS and Android platforms" },
           new Course { Id = 6, Title = "Machine Learning Fundamentals", Description = "Introduction to machine learning concepts" },
           new Course { Id = 7, Title = "Network Security", Description = "Protecting data from unauthorized access" },
           new Course { Id = 8, Title = "Cloud Computing", Description = "Introduction to cloud platforms and services" },
           new Course { Id = 9, Title = "Software Engineering Principles", Description = "Best practices in software development" },
           new Course { Id = 10, Title = "Game Development Basics", Description = "Introduction to game programming" }
            };

            return View("Coursedesc", courses);
        }
    }
}
