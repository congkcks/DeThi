using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TuVung.Models;
using TuVung.DTOs;
using TuVung.Data;

namespace TuVung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly VocabularyDbContext _context;
        private readonly IMapper _mapper;

        public QuestionsController(VocabularyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 🟢 GET: api/Questions
        // Lấy tất cả câu hỏi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions()
        {
            var questions = await _context.Questions.ToListAsync();
            var result = _mapper.Map<List<QuestionDto>>(questions);
            return Ok(result);
        }

        // 🟢 GET: api/Questions/5
        // Lấy chi tiết 1 câu hỏi
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
                return NotFound(new { message = "Không tìm thấy câu hỏi." });

            return Ok(_mapper.Map<QuestionDto>(question));
        }

        // 🟢 GET: api/Questions/Exercise/1
        // Lấy danh sách câu hỏi thuộc 1 bài tập
        [HttpGet("Exercise/{exerciseId}")]
        public async Task<ActionResult<ExerciseWithQuestionsDto>> GetQuestionsByExercise(int exerciseId)
        {
            var exercise = await _context.Exercises
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId);

            if (exercise == null)
                return NotFound(new { message = "Không tìm thấy bài tập." });

            var result = _mapper.Map<ExerciseWithQuestionsDto>(exercise);
            return Ok(result);
        }

        // 🟢 GET: api/Questions/Topic/1
        // Lấy tất cả câu hỏi theo chủ đề ngữ pháp (topic)
        [HttpGet("Topic/{topicId}")]
        public async Task<ActionResult<IEnumerable<ExerciseWithQuestionsDto>>> GetQuestionsByTopic(int topicId)
        {
            var exercises = await _context.Exercises
                .Where(e => e.TopicId == topicId)
                .Include(e => e.Questions)
                .ToListAsync();

            if (!exercises.Any())
                return NotFound(new { message = "Không có bài tập nào trong chủ đề này." });

            var result = _mapper.Map<List<ExerciseWithQuestionsDto>>(exercises);
            return Ok(result);
        }
    }
}
