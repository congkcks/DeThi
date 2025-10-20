using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class ListeningSentence
{
    public int SentenceId { get; set; }

    public int LessonId { get; set; }

    public short OrderNo { get; set; }

    public string Transcript { get; set; } = null!;

    public string? Translation { get; set; }

    public string? AudioUrl { get; set; }

    public virtual ListeningLesson Lesson { get; set; } = null!;
}
