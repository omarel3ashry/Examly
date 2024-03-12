using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using System.Collections.Generic;

namespace WebAppProject.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult Coursedesc()
        {
            var courses = new List<Course>
            {
                new Course { Id = 1, Title = "Introduction to Programming", Description = "This course provides a comprehensive introduction to the fundamental concepts of programming. Starting from the basics of variables and data types, it progresses to cover control structures, functions, and object-oriented programming principles. Through hands-on exercises and projects, students will gain practical experience in problem-solving and algorithmic thinking." },
                new Course { Id = 2, Title = "Web Development Fundamentals", Description = "Web Development Fundamentals is designed to equip students with the foundational knowledge and skills required to build dynamic and interactive websites. Topics covered include HTML, CSS, JavaScript, and basic server-side programming. By the end of the course, students will be able to create responsive web applications and deploy them on the web." },
                new Course { Id = 3, Title = "Data Structures and Algorithms", Description = "Data Structures and Algorithms is a core course for computer science students. It explores various data structures such as arrays, linked lists, stacks, queues, trees, and graphs, along with algorithms for searching, sorting, and traversing them. Emphasis is placed on understanding the efficiency and performance of different data structures and algorithms through analysis and implementation." },
                new Course { Id = 4, Title = "Database Management", Description = "Database Management introduces students to the principles and practices of database design, implementation, and management. Topics covered include relational database concepts, SQL querying, normalization, indexing, and transaction management. Students will also learn about popular database management systems like MySQL, PostgreSQL, and MongoDB." },
                new Course { Id = 5, Title = "Mobile App Development", Description = "Mobile App Development teaches students how to design and develop applications for mobile devices, focusing on platforms like iOS and Android. Topics include user interface design, event handling, data persistence, and integration with device features such as GPS, camera, and sensors. Students will gain practical experience by building their own mobile apps throughout the course." },
                new Course { Id = 6, Title = "Machine Learning Fundamentals", Description = "Machine Learning Fundamentals provides an introduction to the core concepts and techniques of machine learning. Topics covered include supervised and unsupervised learning, regression, classification, clustering, and neural networks. Through hands-on projects and case studies, students will learn how to apply machine learning algorithms to real-world problems." },
                new Course { Id = 7, Title = "Network Security", Description = "Network Security covers the principles and practices of securing computer networks against cyber threats and attacks. Topics include cryptography, authentication, access control, firewalls, intrusion detection systems, and secure communication protocols. Students will learn how to identify vulnerabilities and implement effective security measures to protect data and systems." },
                new Course { Id = 8, Title = "Cloud Computing", Description = "Cloud Computing introduces students to the concepts and technologies behind cloud computing platforms and services. Topics covered include virtualization, distributed computing, cloud deployment models (IaaS, PaaS, SaaS), and cloud service models (public, private, hybrid). Students will gain practical experience by deploying applications on cloud platforms like AWS, Azure, and Google Cloud." },
                new Course { Id = 9, Title = "Software Engineering Principles", Description = "Software Engineering Principles covers the principles, methodologies, and best practices used in the development of large-scale software systems. Topics include requirements engineering, software design, testing, maintenance, and project management. Students will learn how to apply modern software engineering techniques to build reliable, scalable, and maintainable software products." },
                new Course { Id = 10, Title = "Game Development Basics", Description = "Game Development Basics introduces students to the fundamental concepts and techniques used in game development. Topics covered include game design principles, 2D and 3D graphics, physics simulation, audio programming, and user interaction. Students will gain practical experience by developing their own simple games using game development frameworks and engines." }
            };

            return View("Coursedesc", courses);
        }
    }
}
