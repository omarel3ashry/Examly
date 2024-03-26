using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.VMS;

namespace WebAppProject.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Course,CourseViewModel>();
            CreateMap<Department,DepartmentViewModel>();
            CreateMap<DepartmentCourse, DepartmentCourseViewModel>()
                .ForMember(dest => dest.DeptName, src => src.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.CourseName, src => src.MapFrom(src => src.Course.Name))
                .ForMember(dest=>dest.CourseDescription,src=>src.MapFrom(src=>src.Course.Description))
                .ForMember(dest => dest.InstructorName, src => src.MapFrom(src => src.Instructor!.Name));
            CreateMap<Choice, ChoiceViewModel>();
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Exam,ExamViewModel>()
                .ForMember(dest=>dest.CourseName,src=>src.MapFrom(src=>src.Course.Name));
            CreateMap<ExamTaken, ExamTakenViewModel>();
                
            
        }
    }
}
