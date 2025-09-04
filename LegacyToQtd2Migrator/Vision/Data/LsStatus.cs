using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsStatus
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public virtual ICollection<LsLearningEventLearner> LsLearningEventLearners { get; set; } = new List<LsLearningEventLearner>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTracks { get; set; } = new List<LsLearningEventTrack>();

    public virtual ICollection<LsQualCardEvent> LsQualCardEventFkQualEventScoreNavigations { get; set; } = new List<LsQualCardEvent>();

    public virtual ICollection<LsQualCardEvent> LsQualCardEventFkTrainingEventScoreNavigations { get; set; } = new List<LsQualCardEvent>();

    public virtual ICollection<LsTaskQualStep> LsTaskQualSteps { get; set; } = new List<LsTaskQualStep>();

    public virtual ICollection<LsTaskQualification> LsTaskQualifications { get; set; } = new List<LsTaskQualification>();
}
