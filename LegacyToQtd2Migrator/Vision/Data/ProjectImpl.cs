using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProjectImpl
{
    public decimal Id { get; set; }

    public decimal FkProject { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Enabled { get; set; }

    public decimal ObToMultipleCm { get; set; }

    public decimal MultipleConsolidation { get; set; }

    public string DefaultFontName { get; set; }

    public decimal? DefaultFontSize { get; set; }

    public decimal? DefaultFontStyle { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public decimal QAnalysis { get; set; }

    public decimal AnalysisExam { get; set; }

    public string UserSpellDict { get; set; }

    public string CustomHelpDoc { get; set; }

    public string Password { get; set; }

    public decimal AutoStats { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal AllowTaskChangeImpact { get; set; }

    public string Salt { get; set; }

    public decimal MultiSelect { get; set; }

    public decimal ShowAlternatesTab { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
