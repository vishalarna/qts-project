using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearnerPosition
{
    public decimal FkLearner { get; set; }

    public decimal FkOrgPosition { get; set; }

    public decimal? FkCompany { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public decimal? Injob { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsOrg FkOrgPositionNavigation { get; set; }
}
