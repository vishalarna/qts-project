using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearnerPositionHist
{
    public decimal Id { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkOrgPosition { get; set; }

    public decimal? FkCompany { get; set; }

    public DateTime ActionDate { get; set; }

    public decimal? Injob { get; set; }

    public decimal? Injobpool { get; set; }

    public string Comment { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsOrg FkOrgPositionNavigation { get; set; }
}
