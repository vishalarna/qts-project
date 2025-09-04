using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveTask
{
    public decimal FkObjective { get; set; }

    public decimal FkAnalysis { get; set; }

    public decimal FkProject { get; set; }

    public decimal? Direct { get; set; }

    public decimal? Consolidated { get; set; }

    public decimal? ObjectiveDescent { get; set; }

    public decimal? AnalysisDescent { get; set; }

    public decimal? Manual { get; set; }

    public decimal? Grade { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
