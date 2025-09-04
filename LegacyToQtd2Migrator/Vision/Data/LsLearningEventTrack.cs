using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearningEventTrack
{
    public decimal FkLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? Score { get; set; }

    public decimal? TuWeight { get; set; }

    public decimal? TuPassingScore { get; set; }

    public decimal? FkExamResults { get; set; }

    public decimal? FkCatalogRating { get; set; }

    public decimal TuStatus { get; set; }

    public decimal? FkInstructor { get; set; }

    public string ManualScoreComments { get; set; }

    public virtual LsCatalogRating FkCatalogRatingNavigation { get; set; }

    public virtual LsExamResult FkExamResultsNavigation { get; set; }

    public virtual Learner FkInstructorNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual LsStatus TuStatusNavigation { get; set; }
}
