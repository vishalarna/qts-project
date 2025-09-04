using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPaOjeRequest
{
    public decimal FkAnalysisObjective { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkEvaluator { get; set; }

    public decimal FkLsTuItemType { get; set; }

    public decimal? FkEvent { get; set; }

    public decimal? FkProgram { get; set; }

    public DateTime RequestDate { get; set; }

    public decimal Status { get; set; }

    public decimal? IsEvaluator { get; set; }

    public decimal? IsTrainer { get; set; }

    public decimal? FkLsQualCard { get; set; }

    public string Session { get; set; }

    public virtual Learner FkEvaluatorNavigation { get; set; }

    public virtual LsLearningEvent FkEventNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual LsTuItemType FkLsTuItemTypeNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
