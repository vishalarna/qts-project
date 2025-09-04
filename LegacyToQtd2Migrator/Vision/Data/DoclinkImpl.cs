using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class DoclinkImpl
{
    public decimal Id { get; set; }

    public decimal FkDoclink { get; set; }

    public decimal FkProject { get; set; }

    public decimal FkLinkedTo { get; set; }

    public decimal LinkedToType { get; set; }

    public string Title { get; set; }

    public string Path { get; set; }

    public decimal Tracked { get; set; }

    public string Notes { get; set; }

    public string Scope { get; set; }

    public DateTime? DateApproved { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string Params { get; set; }

    public decimal Sequence { get; set; }

    public decimal ShowInLs { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Doclink FkDoclinkNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
