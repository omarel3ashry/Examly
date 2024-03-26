using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.Areas.Staff.ViewModels;


namespace WebAppProject.Areas.Staff.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<DepartmentCourse, CourseInfoViewModel>()
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.Description, src => src.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.DepartmentName, src => src.MapFrom(src => src.Department.Name))
                .ForMember(dest=>dest.InstructorName,src=>src.MapFrom(src=>src.Instructor.Name))
                .ForMember(dest=>dest.BranchName,src=>src.MapFrom(src=>src.Department.Branch.Name))
                .ReverseMap();

            CreateMap<Department, DepartmentViewModel>()
            .ReverseMap();

            CreateMap<Course, AddCourseViewModel>()
            .ReverseMap();

            CreateMap<DepartmentCourse, DeptCourseViewModel>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.CourseDescription, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Choice, ChoiceViewModel>().ReverseMap();

            CreateMap<Question, QuestionViewModel>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ReverseMap()
                .ForMember(dest => dest.Course, opt => opt.Ignore());

            CreateMap<Exam, InstExamViewModel>()
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ReverseMap()
                .ForMember(dest => dest.Course, opt => opt.Ignore());

            CreateMap<ICollection<Exam>, ExamListsViewModel>()
                .ForMember(dest => dest.UpcomingExams,
                           opt => opt.MapFrom(src => src.Where(e => e.ExamDate > DateTime.Now)))
                .ForMember(dest => dest.PastExams,
                           opt => opt.MapFrom(src => src.Where(e => e.ExamDate <= DateTime.Now)));

            CreateMap<ExamTaken, ExamTakenViewModel>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Title));

            CreateMap<ExamChoices, StudentAnswersViewModel>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question.Text))
                .ForMember(dest => dest.QuestionGrade, opt => opt.MapFrom(src => src.Question.Grade));
        }
        
    }
}
