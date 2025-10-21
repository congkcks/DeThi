namespace TuVung.DTOs;

public class ExerciseWithQuestionsDto
{
    public int ExerciseId { get; set; }
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new();
}
