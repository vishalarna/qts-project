using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class AnalysisCondStand
{
    public decimal FkAnalysis { get; set; }

    public decimal Type { get; set; }

    public decimal Sequence { get; set; }

    public string Text { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }
}
