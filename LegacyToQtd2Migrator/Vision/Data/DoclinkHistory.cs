using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class DoclinkHistory
{
    public decimal FkDoclink { get; set; }

    public decimal Sequence { get; set; }

    public decimal HistoryAction { get; set; }

    public string Notes { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Doclink FkDoclinkNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }
}
