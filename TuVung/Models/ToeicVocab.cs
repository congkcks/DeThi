using System;
using System.Collections.Generic;

namespace TuVung.Models;

public partial class ToeicVocab
{
    public long Id { get; set; }

    public int Stt { get; set; }

    public string Word { get; set; } = null!;

    public string? Phonetic { get; set; }

    public string? Pos { get; set; }

    public string? MeaningVi { get; set; }

    public string? ExampleEn { get; set; }

    public string? Source { get; set; }

    public string? Section { get; set; }

    public int? TestYear { get; set; }

    public int? TestNo { get; set; }

    public DateTime? CreatedAt { get; set; }
}
