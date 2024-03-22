using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.Areas.Admin.ViewModels;

namespace WebAppProject.Areas.Admin.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Branch, BranchViewModel>().ReverseMap();
        }
    }
}
