using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsHoldRelease
{
    public decimal FkLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkObjective { get; set; }

    public decimal FkContent { get; set; }

    public decimal FkLearner { get; set; }

    public decimal ReleaseHold { get; set; }

    public virtual Content FkContentNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
