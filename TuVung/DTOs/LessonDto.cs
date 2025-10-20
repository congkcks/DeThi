namespace TuVung.DTOs;
public class LessonDto
{
    public int LessonId { get; set; }
    public string Title { get; set; } = "";
    public string Level { get; set; } = "";
    public int TotalQuestions { get; set; }
}