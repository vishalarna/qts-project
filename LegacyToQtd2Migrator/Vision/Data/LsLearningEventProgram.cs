using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearningEventProgram
{
    public decimal FkLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal? Sequence { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateExpire { get; set; }

    public DateTime? DateFinish { get; set; }

    public decimal? FkInstructor { get; set; }

    public decimal? FkEval { get; set; }

    public decimal? PreTest { get; set; }

    public decimal? TestType { get; set; }

    public decimal? ObjectiveOrder { get; set; }

    public decimal? Type { get; set; }

    public decimal? Points { get; set; }

    public decimal? Weight { get; set; }

    public decimal? FkCatalogRating { get; set; }

    public decimal? TrysToComplete { get; set; }

    public virtual LsCatalogRating FkCatalogRatingNavigation { get; set; }

    public virtual Eval FkEvalNavigation { get; set; }

    public virtual Learner FkInstructorNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
