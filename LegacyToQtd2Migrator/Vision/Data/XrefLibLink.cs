using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class XrefLibLink
{
    public decimal FkParent { get; set; }

    public decimal FkItem { get; set; }

    public decimal LinkToType { get; set; }

    public decimal FkLinkTo { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual XrefLib FkItemNavigation { get; set; }

    public virtual XrefLib FkParentNavigation { get; set; }
}
