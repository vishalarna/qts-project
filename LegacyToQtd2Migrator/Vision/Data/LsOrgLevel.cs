using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOrgLevel
{
    public decimal Id { get; set; }

    public decimal FkCompany { get; set; }

    public string Text { get; set; }

    public decimal? Sequence { get; set; }

    public byte[] Icon { get; set; }

    public decimal? Status { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual ICollection<LsOrg> LsOrgs { get; set; } = new List<LsOrg>();
}
