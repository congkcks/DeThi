using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuVung.Data;
using TuVung.DTOs;
namespace TuVung.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningController : ControllerBase
{
    private readonly VocabularyDbContext _db;
    private readonly IMapper _mapper;

    public ListeningController(VocabularyDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet("lessons")]
    public async Task<IActionResult> GetLessons()
    {
        var lessons = await _db.ListeningLessons.ToListAsync();
        var result = _mapper.Map<IEnumerable<LessonDto>>(lessons);
        return Ok(result);
    }

    [HttpGet("lessons/{lessonId}/sentences")]
    public async Task<IActionResult> GetSentences(int lessonId)
    {
        var sentences = await _db.ListeningSentences
                                 .Where(s => s.LessonId == lessonId)
                                 .OrderBy(s => s.OrderNo)
                                 .ToListAsync();

        var result = _mapper.Map<IEnumerable<SentenceDto>>(sentences);
        return Ok(result);
    }

}
