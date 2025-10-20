using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? ExerciseId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string OptionA { get; set; } = null!;

    public string OptionB { get; set; } = null!;

    public string OptionC { get; set; } = null!;

    public string OptionD { get; set; } = null!;

    public char? CorrectOption { get; set; }

    public virtual Exercise? Exercise { get; set; }
}
