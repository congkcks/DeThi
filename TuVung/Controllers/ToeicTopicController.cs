using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuVung.Data;
using TuVung.Models;

namespace TuVung.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToeicTopicController : ControllerBase
    {
        private readonly VocabularyDbContext _context;

        public ToeicTopicController(VocabularyDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            var topics = await _context.ToeicVocabs
                .Where(v => v.Section != null && v.Section != "")
                .GroupBy(v => v.Section)
                .Select(g => new
                {
                    topicName = g.Key,
                    totalWords = g.Count(),
                    sampleWords = g.Take(5).Select(v => new
                    {
                        v.Word,
                        v.MeaningVi,
                        v.Pos
                    })
                })
                .OrderBy(t => t.topicName)
                .ToListAsync();

            return Ok(new
            {
                totalTopics = topics.Count,
                items = topics
            });
        }
        [HttpGet("{section}")]
        public async Task<IActionResult> GetWordsByTopic(string section)
        {
            var words = await _context.ToeicVocabs
                .Where(v => v.Section == section)
                .Select(v => new
                {
                    v.Id,
                    v.Word,
                    v.Phonetic,
                    v.Pos,
                    v.MeaningVi,
                    v.ExampleEn
                })
                .ToListAsync();

            if (words.Count == 0)
                return NotFound(new { message = $"Không tìm thấy từ vựng cho chủ đề '{section}'" });

            return Ok(new
            {
                topic = section,
                total = words.Count,
                items = words
            });
        }
    }
}
