using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExamResult
{
    public decimal Id { get; set; }

    public decimal ExamScore { get; set; }

    public decimal AdjScore { get; set; }

    public decimal? FkAdjuster { get; set; }

    public string Comments { get; set; }

    public DateTime? DateAdj { get; set; }

    public decimal? PassingScore { get; set; }

    public virtual Learner FkAdjusterNavigation { get; set; }

    public virtual ICollection<LsExamEvent> LsExamEvents { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTracks { get; set; } = new List<LsLearningEventTrack>();
}
