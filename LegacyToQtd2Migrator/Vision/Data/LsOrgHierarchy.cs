using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOrgHierarchy
{
    public decimal? FkParent { get; set; }

    public decimal FkChild { get; set; }

    public decimal? Sequence { get; set; }

    public decimal? FkCompany { get; set; }

    public virtual LsOrg FkChildNavigation { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual LsOrg FkParentNavigation { get; set; }
}
