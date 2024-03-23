using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.Areas.Staff.ViewModels;
using WebAppProject.ViewModels;

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

        }
        
    }
}
