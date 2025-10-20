using AutoMapper;
using TuVung.DTOs;
using TuVung.Models;
namespace TuVung.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ListeningLesson, LessonDto>();
        CreateMap<ListeningSentence, SentenceDto>();
    }
}
