using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualEvaluatorTrainer
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkAnalysis { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? IsEvaluator { get; set; }

    public decimal? IsTrainer { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }
}
