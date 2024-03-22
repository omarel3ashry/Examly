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
                .ReverseMap();
        }
    }
}
