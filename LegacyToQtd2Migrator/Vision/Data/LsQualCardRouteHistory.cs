using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCardRouteHistory
{
    public decimal Id { get; set; }

    public decimal FkLsQualCard { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkSupervisor { get; set; }

    public decimal? Sequence { get; set; }

    public DateTime? DateCompleted { get; set; }

    public decimal Score { get; set; }

    public string Comments { get; set; }

    public decimal Archive { get; set; }

    public decimal InRoutePath { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual Learner FkSupervisorNavigation { get; set; }

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();
}
