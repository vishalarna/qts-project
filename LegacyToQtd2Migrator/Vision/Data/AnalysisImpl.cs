using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class AnalysisImpl
{
    public decimal Id { get; set; }

    public decimal FkAnalysis { get; set; }

    public decimal FkProject { get; set; }

    public decimal? FkAnalysisLevel { get; set; }

    public string Text { get; set; }

    public string UserDefinedId { get; set; }

    public string CrossReference { get; set; }

    public decimal? FkTaskStatus { get; set; }

    public decimal? TaskIsTrained { get; set; }

    public decimal? FkTaskDeselection { get; set; }

    public decimal? TaskIsRecurring { get; set; }

    public decimal? FkTaskTrainingTime { get; set; }

    public decimal? TaskDifficulty { get; set; }

    public decimal? TaskImportance { get; set; }

    public decimal? TaskFrequency { get; set; }

    public decimal? TaskIsCritical { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public string TextAscii { get; set; }

    public decimal? FkAnalysisQual { get; set; }

    public decimal? FkAnalysisRequal { get; set; }

    public string VersionComments { get; set; }

    public decimal? CanAppearOnQualCard { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal? ContinuousUse { get; set; }

    public decimal TaskMustPerform { get; set; }

    public decimal? FkTaskChangeImpact { get; set; }

    public virtual AnalysisLevel FkAnalysisLevelNavigation { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual AnalysisQual FkAnalysisQualNavigation { get; set; }

    public virtual TimeSpan FkAnalysisRequalNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual TaskChangeImpact FkTaskChangeImpactNavigation { get; set; }

    public virtual TaskDeselection FkTaskDeselectionNavigation { get; set; }

    public virtual TaskStatus FkTaskStatusNavigation { get; set; }

    public virtual TimeSpan FkTaskTrainingTimeNavigation { get; set; }

    public virtual ICollection<QualCardItem> QualCardItems { get; set; } = new List<QualCardItem>();

    public virtual ICollection<RevisionLogAnalysis> RevisionLogAnalyses { get; set; } = new List<RevisionLogAnalysis>();
}
