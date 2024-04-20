
<div align="center">
  <a href="https://github.com/omarel3ashry/Examly">
    <img src="assets/logo.svg" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Examly</h3>

  <p align="center">
    Empowering Education, One Click at a Time!
  </p>
</div>

# Examly

Examly is built with a focus on best practices in software design. We employ the Dependency Inversion Principle and Dependency Injection pattern to promote loose coupling and facilitate easier testing and maintainability. Additionally, we've separated the data access layer into a dedicated library for clearer code organization and maintenance. These architectural decisions ensure a scalable, maintainable, and flexible solution for online examinations.

Examly is an Online Examination Web Application, a comprehensive platform designed to facilitate educational institutions in conducting exams efficiently. It offers a range of features tailored to meet the needs of administrators, instructors, and students.

## Features

### For System Admin:
1. **Branch and Department Management:** Enables the creation of multiple branches, each with its own departments and instructors.
2. **Department Manager Assignment:** Allows assignment of a manager to each department for administrative purposes.
3. **Grade and Answer Viewing:** Grants access to view student grades and exam answers for monitoring purposes.

### For Instructor:
1. **Course and Instructor Assignment:** Department managers can add courses to the department and assign instructors to those courses within their branch.
2. **Question Creation:** Offers a user-friendly interface for instructors to create questions of types (true/false, multiple choice) with different difficulty levels for their assigned courses.
3. **Exam Creation:** Allows instructors to create exams by specifying the number, type, and difficulty of questions, which are randomly selected from the course's question bank.
4. **Grade and Answer Viewing:** Enables instructors to monitor student progress by viewing their grades and exam answers.

### For Student:
1. **Online Exams:** Facilitates students in taking exams online.
2. **Instant Result:** Provides students with instant exam results upon submission of their answers.
3. **Exam History:** Stores exam history for each student, allowing them to monitor their grades in different courses.

### Built With

- [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc) - A rich framework for building web apps and APIs using the Model-View-Controller design pattern.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) - A proprietary relational database management system developed by Microsoft.
- [Entity Framework (EF) Core](https://learn.microsoft.com/en-us/ef/core/) - An open-source object-relational mapping (ORM) framework developed by Microsoft.
- [AutoMapper](https://automapper.org/) - A convention-based object-object mapper. Takes out all of the fuss of mapping one object to another.

<br/>

## Contributors
The dedicated developers whose hard work and expertise brought this project to life.
<table>
  <tr>
    <td align="center" valign="top" width="15%"><a href="https://github.com/omarel3ashry" style:"border-radius:50%;"><img src="https://avatars.githubusercontent.com/u/32119955?v=4"  width="100px;" /><br /><sub><b>Omar Elashry</b></sub></a><br /></td>
    <td align="center" valign="top" width="15%"><a href="https://github.com/karimalshal666" style:"border-radius:50%;"><img src="https://avatars.githubusercontent.com/u/157370888?v=4"  width="100px;" /><br /><sub><b>Karim Alshal</b></sub></a><br /></td>
    <td align="center" valign="top" width="20%"><a href="https://github.com/Mo3bdelrahman" style:"border-radius:50%;"><img src="https://avatars.githubusercontent.com/u/61760258?v=4"  width="100px;" /><br /><sub><b>Mohamed Abdelrahman</b></sub></a><br /></td>
    <td align="center" valign="top" width="15%"><a href="https://github.com/hossameltayeb83" style:"border-radius:50%;"><img src="https://avatars.githubusercontent.com/u/96459585?v=4"  width="100px;" /><br /><sub><b>Hossam Eltayeb</b></sub></a><br /></td>
    <td align="center" valign="top" width="15%"><a href="https://github.com/nourhanbelal22" style:"border-radius:50%;"><img src="https://avatars.githubusercontent.com/u/157370503?v=4"  width="100px;" /><br /><sub><b>Nourhan Belal</b></sub></a><br /></td>
  </tr>
</table>

<br/>

## Installation

To run Examly locally, follow these steps:

1. **Clone the repository:**
   
   ```
   git clone https://github.com/omarel3ashry/Examly
   ```
   
3. **Navigate to the project directory:**
   
   ```
   cd Examly
   ```
   
4. **Install dependencies:**
   
   ```
   dotnet restore
   ```
   
5. **Create the database using Entity Framework Core:**
   
   ```
   dotnet ef database update
   ```
   
7. **Build and Run:**
   
   ```
   dotnet build
   dotnet run
   ```
   
9. **Open your web browser:**
    
   Navigate to https://localhost:7076 to access the application.
   


**Note:** Make sure you have [.NET Core SDK](https://dotnet.microsoft.com/en-us/download) installed on your system.
