using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPaEvaluatorTrainer
{
    public decimal FkProgram { get; set; }

    public decimal FkAnalysisObjective { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkLsTuItemType { get; set; }

    public decimal? IsEvaluator { get; set; }

    public decimal? IsTrainer { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsTuItemType FkLsTuItemTypeNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
