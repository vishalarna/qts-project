using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCard
{
    public decimal Id { get; set; }

    public decimal FkQualCardImpl { get; set; }

    public decimal FkOrg { get; set; }

    public string Text { get; set; }

    public string TextPrefix { get; set; }

    public decimal? FkLsTimeToComplete { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateModified { get; set; }

    public decimal FkModifiedBy { get; set; }

    public decimal? FkRequal { get; set; }

    public decimal? GPeriod { get; set; }

    public decimal? Taskevent { get; set; }

    public decimal? EvaluatorType { get; set; }

    public decimal? TrainerType { get; set; }

    public decimal SameTrainerEvaluator { get; set; }

    public string CrossReference { get; set; }

    public decimal RouteSupervisor { get; set; }

    public decimal? AutomaticRouting { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal SupervisorEvalException { get; set; }

    public decimal FkOriginalLsQualCard { get; set; }

    public decimal Approved { get; set; }

    public decimal Archive { get; set; }

    public virtual LsEvaluatorTrainerValue EvaluatorTypeNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual LsTimeToComplete FkLsTimeToCompleteNavigation { get; set; }

    public virtual Learner FkModifiedByNavigation { get; set; }

    public virtual LsOrg FkOrgNavigation { get; set; }

    public virtual LsQualCard FkOriginalLsQualCardNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual QualCardImpl FkQualCardImplNavigation { get; set; }

    public virtual LsTimeToComplete FkRequalNavigation { get; set; }

    public virtual ICollection<LsQualCard> InverseFkOriginalLsQualCardNavigation { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequests { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsQualCardEvent> LsQualCardEvents { get; set; } = new List<LsQualCardEvent>();

    public virtual ICollection<LsQualCardItem> LsQualCardItems { get; set; } = new List<LsQualCardItem>();

    public virtual ICollection<LsQualCardPrerequisite> LsQualCardPrerequisites { get; set; } = new List<LsQualCardPrerequisite>();

    public virtual ICollection<LsQualCardRouteHistory> LsQualCardRouteHistories { get; set; } = new List<LsQualCardRouteHistory>();

    public virtual ICollection<LsQualCardRoute> LsQualCardRoutes { get; set; } = new List<LsQualCardRoute>();

    public virtual ICollection<LsQualEvaluatorTrainer> LsQualEvaluatorTrainers { get; set; } = new List<LsQualEvaluatorTrainer>();

    public virtual ICollection<LsQualJobPosition> LsQualJobPositions { get; set; } = new List<LsQualJobPosition>();

    public virtual LsRuleEmail LsRuleEmail { get; set; }

    public virtual ICollection<LsRule> LsRules { get; set; } = new List<LsRule>();

    public virtual ICollection<LsTaskQualification> LsTaskQualifications { get; set; } = new List<LsTaskQualification>();

    public virtual LsEvaluatorTrainerValue TrainerTypeNavigation { get; set; }
}
