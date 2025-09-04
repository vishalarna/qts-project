using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsObjectiveMastery
{
    public decimal Id { get; set; }

    public decimal FkObjective { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime DateLastUpdated { get; set; }

    public decimal? ContentViewed { get; set; }

    public decimal? CognitiveUniqueQuestions { get; set; }

    public decimal? CognitiveCorrectQuestions { get; set; }

    public decimal? PerformanceVerified { get; set; }

    public decimal? Exempt { get; set; }

    public string ExemptComments { get; set; }

    public decimal? Archive { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }
}
