namespace TuVung.DTOs;

public class SentenceDto
{
    public int SentenceId { get; set; }
    public int OrderNo { get; set; }
    public string Transcript { get; set; } = "";
    public string? Translation { get; set; }
    public string? AudioUrl { get; set; }
}