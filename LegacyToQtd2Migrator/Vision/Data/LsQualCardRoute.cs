using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCardRoute
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkLearner { get; set; }

    public decimal Sequence { get; set; }

    public decimal? FkJobPosition { get; set; }

    public virtual LsOrg FkJobPositionNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }
}
