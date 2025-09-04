using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearningEventAttempt
{
    public decimal FkLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? TrysToComplete { get; set; }

    public decimal? Attempts { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
