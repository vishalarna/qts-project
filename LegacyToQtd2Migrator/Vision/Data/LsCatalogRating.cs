using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCatalogRating
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public virtual ICollection<LsLearningEventProgram> LsLearningEventPrograms { get; set; } = new List<LsLearningEventProgram>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTracks { get; set; } = new List<LsLearningEventTrack>();
}
