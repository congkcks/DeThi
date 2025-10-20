using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class Grammartopic
{
    public int TopicId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
