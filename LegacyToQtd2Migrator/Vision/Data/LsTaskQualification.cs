using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTaskQualification
{
    public decimal Id { get; set; }

    public decimal FkAnalysisObjective { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal? FkLsQualCard { get; set; }

    public decimal? FkLearningEvent { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkLsTuItemType { get; set; }

    public decimal? FkEvaluator { get; set; }

    public decimal? FkTrainer { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime? DateCompleted { get; set; }

    public decimal Status { get; set; }

    public DateTime? DateExpires { get; set; }

    public string EvaluatorComments { get; set; }

    public string TrainingComments { get; set; }

    public decimal? TrainingStatus { get; set; }

    public DateTime? DateTrainingCompleted { get; set; }

    public decimal? Exempt { get; set; }

    public string ExemptComments { get; set; }

    public decimal Archive { get; set; }

    public DateTime? OriginalDateCompleted { get; set; }

    public string Session { get; set; }

    public virtual Learner FkEvaluatorNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual LsTuItemType FkLsTuItemTypeNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Learner FkTrainerNavigation { get; set; }

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsTaskQualStep> LsTaskQualSteps { get; set; } = new List<LsTaskQualStep>();

    public virtual LsStatus StatusNavigation { get; set; }
}
