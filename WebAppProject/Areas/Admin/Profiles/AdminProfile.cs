using AutoMapper;
using DataAccessLibrary.Model;
using System.Diagnostics.Tracing;
using WebAppProject.Areas.Admin.ViewModels;
using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Admin.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Branch, BranchViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();

            CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.ManagerName, src => src.MapFrom(src => src.Manager.Name))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.Name)).ReverseMap();
            CreateMap<Department, DepartmentFormViewModel>().ReverseMap();

            CreateMap<Instructor, InstructorViewModel>()
                .ForMember(dest => dest.ManagedDepartmentName,src => src.MapFrom(src => src.ManagedDepartment.Name))
                .ForMember(dest => dest.BranchName, src => src.MapFrom(src => src.Branch.Name)).ReverseMap();
            CreateMap<Instructor, InstructorFormViewModel>()
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Password, src => src.MapFrom(src => src.User.Password))
                .ReverseMap()
                .ForPath(dest => dest.User.Email, src => src.Ignore())
                .ForPath(dest => dest.User.Password, src => src.Ignore());
        }
    }
}
