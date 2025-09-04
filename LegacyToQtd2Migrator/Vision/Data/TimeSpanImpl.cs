using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class TimeSpanImpl
{
    public decimal Id { get; set; }

    public decimal FkTimeSpan { get; set; }

    public decimal FkProject { get; set; }

    public string Text { get; set; }

    public decimal FkTimeType { get; set; }

    public decimal TimeSpan { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual TimeSpan FkTimeSpanNavigation { get; set; }

    public virtual TimeType FkTimeTypeNavigation { get; set; }
}
