using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class ListeningLesson
{
    public int LessonId { get; set; }

    public string Title { get; set; } = null!;

    public string? Level { get; set; }

    public short? TotalQuestions { get; set; }

    public virtual ICollection<ListeningSentence> ListeningSentences { get; set; } = new List<ListeningSentence>();
}
