using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class AnalysisHtml
{
    public decimal FkAnalysis { get; set; }

    public byte[] Text { get; set; }

    public byte[] Comments { get; set; }

    public byte[] ProcedureStatement { get; set; }

    public byte[] OjtNotes { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }
}
