using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    
        public class Instructor
        {
            [Key]
            public int InstructorId { get; set; }
            public string InstructorName { get; set; }

            public List<Course> Courses { get; set; }
        }
    }

