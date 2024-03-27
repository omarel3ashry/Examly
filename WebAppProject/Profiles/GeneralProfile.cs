using AutoMapper;
using DataAccessLibrary.Model;
using WebAppProject.ViewModels;

namespace WebAppProject.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Course, CourseViewModel>();

            CreateMap<ExamChoices, StudentAnswersViewModel>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question.Text))
                .ForMember(dest => dest.QuestionGrade, opt => opt.MapFrom(src => src.Question.Grade));
        }
    }
}
