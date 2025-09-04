using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class DeveloperAuthorization
{
    public decimal FkDeveloper { get; set; }

    public decimal FkProject { get; set; }

    public decimal FkSecurityGroup { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkDeveloperNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual SecurityGroup FkSecurityGroupNavigation { get; set; }
}
