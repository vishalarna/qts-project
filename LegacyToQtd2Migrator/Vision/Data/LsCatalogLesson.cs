using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCatalogLesson
{
    public decimal Id { get; set; }

    public decimal FkCatalog { get; set; }

    public string Text { get; set; }

    public decimal Type { get; set; }

    public decimal? FkProgram { get; set; }

    public string Path { get; set; }

    public decimal Sequence { get; set; }

    public decimal? TimeToComplete { get; set; }

    public string Description { get; set; }

    public decimal? FkCatalogType { get; set; }

    public decimal? FkExamOnlineProfile { get; set; }

    public decimal? SimHours { get; set; }

    public decimal? StandardHours { get; set; }

    public decimal? TotalHours { get; set; }

    public decimal? EmergencyHours { get; set; }

    public virtual LsCatalog FkCatalogNavigation { get; set; }

    public virtual LsCatalogType FkCatalogTypeNavigation { get; set; }

    public virtual ExamOnlineProfile FkExamOnlineProfileNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual LsPerformanceAssessment LsPerformanceAssessment { get; set; }

    public virtual LsMediaType TypeNavigation { get; set; }
}
