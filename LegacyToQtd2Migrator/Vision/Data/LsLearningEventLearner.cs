using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearningEventLearner
{
    public decimal FkLearningEvent { get; set; }

    public decimal? FkLearnerType { get; set; }

    public decimal FkLearnerValue { get; set; }

    public DateTime? DateCreated { get; set; }

    public decimal Status { get; set; }

    public DateTime? DateCompleted { get; set; }

    public DateTime? DateExpires { get; set; }

    public decimal? EventScore { get; set; }

    public decimal EventWeighted { get; set; }

    public decimal? EventPassingScore { get; set; }

    public virtual LsSecurityCode FkLearnerTypeNavigation { get; set; }

    public virtual Learner FkLearnerValueNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual LsStatus StatusNavigation { get; set; }
}
