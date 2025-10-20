using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public int? TopicId { get; set; }

    public string Title { get; set; } = null!;

    public int? TotalQuestions { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Grammartopic? Topic { get; set; }
}
