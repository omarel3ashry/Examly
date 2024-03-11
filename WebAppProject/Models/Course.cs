namespace WebAppProject.Models
{
    public class Course
    {
        public static List<Course> CourseDemo = [
            new Course { CrsName = "c#", Id = 1 },
            new Course { CrsName = "sql", Id = 2 },
            new Course { CrsName = "network", Id = 3 },
            new Course { CrsName = "c++", Id = 4 },
            new Course { CrsName = "mvc", Id = 5 }
            ];
        public int Id { get; set; }
        public string CrsName { get; set; }

    }
}
