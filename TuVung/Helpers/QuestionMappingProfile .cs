using AutoMapper;
using TuVung.Models;
using TuVung.DTOs;
namespace TuVung.Helpers;
public class QuestionMappingProfile : Profile
{
    public QuestionMappingProfile()
    {
        CreateMap<Question, QuestionDto>();
        CreateMap<Exercise, ExerciseWithQuestionsDto>();
    }
}
