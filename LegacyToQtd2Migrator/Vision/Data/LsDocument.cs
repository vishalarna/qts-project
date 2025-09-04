using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsDocument
{
    public decimal Id { get; set; }

    public decimal? FkLearner { get; set; }

    public decimal? FkLearningEvent { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal? FkObjective { get; set; }

    public decimal? FkContent { get; set; }

    public string Title { get; set; }

    public string Filename { get; set; }

    public string Comment { get; set; }

    public byte[] Data { get; set; }

    public decimal LearnerCanView { get; set; }

    public DateTime? DateCompleted { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public decimal? FkModifiedBy { get; set; }

    public decimal? FkLsQualCardRouteHistory { get; set; }

    public decimal? FkTaskQualification { get; set; }

    public decimal? EvaluationDocument { get; set; }

    public virtual Content FkContentNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual LsQualCardRouteHistory FkLsQualCardRouteHistoryNavigation { get; set; }

    public virtual Learner FkModifiedByNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual LsTaskQualification FkTaskQualificationNavigation { get; set; }
}
