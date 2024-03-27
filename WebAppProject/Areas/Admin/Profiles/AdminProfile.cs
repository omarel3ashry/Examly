using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.Areas.Admin.ViewModels;


namespace WebAppProject.Areas.Admin.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Student, StudentViewModel>();

            CreateMap<Branch, BranchViewModel>().ReverseMap();

            CreateMap<Department, AdminDepartmentViewModel>()
                .ForMember(dest => dest.ManagerName, src => src.MapFrom(src => src.Manager.Name))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.Name)).ReverseMap();
            CreateMap<Department, DepartmentFormViewModel>().ReverseMap();

            CreateMap<Instructor, InstructorViewModel>()
                .ForMember(dest => dest.ManagedDepartmentName, src => src.MapFrom(src => src.ManagedDepartment.Name))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.Name)).ReverseMap();

            CreateMap<Instructor, InstructorFormViewModel>()
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Password, src => src.MapFrom(src => src.User.Password))
                .ReverseMap()
                .ForPath(dest => dest.User.Email, src => src.Ignore())
                .ForPath(dest => dest.User.Password, src => src.Ignore());

            CreateMap<ExamTaken, AdminExamTakenViewModel>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.ExamTitle, opt => opt.MapFrom(src => src.Exam.Title))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Exam.Course.Name))
                .ForMember(dest => dest.ExamDate, opt => opt.MapFrom(src => src.Exam.ExamDate.ToString()));
        }
    }
}
