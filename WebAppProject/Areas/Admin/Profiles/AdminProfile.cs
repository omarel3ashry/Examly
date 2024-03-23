using AutoMapper;
using DataAccessLibrary.Model;
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
            CreateMap<Instructor, InstructorFormViewModel>().ReverseMap();
        }
    }
}
