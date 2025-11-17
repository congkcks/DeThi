using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuVung.Data;
using TuVung.Models;

namespace TuVung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToeicVocabController : ControllerBase
    {
        private readonly VocabularyDbContext _context;

        public ToeicVocabController(VocabularyDbContext context)
        {
            _context = context;
        }

       

        // 📗 API phụ: Lấy danh sách chủ đề (Section)
        [HttpGet("sections")]
        public async Task<IActionResult> GetSections()
        {
            var sections = await _context.ToeicVocabs
                .Where(v => v.Section != null)
                .Select(v => v.Section!)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            return Ok(sections);
        }

    }
}
