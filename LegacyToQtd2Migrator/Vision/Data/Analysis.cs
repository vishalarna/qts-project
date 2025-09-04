using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Analysis
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisComment> AnalysisComments { get; set; } = new List<AnalysisComment>();

    public virtual ICollection<AnalysisCondStandHtml> AnalysisCondStandHtmls { get; set; } = new List<AnalysisCondStandHtml>();

    public virtual ICollection<AnalysisCondStand> AnalysisCondStands { get; set; } = new List<AnalysisCondStand>();

    public virtual ICollection<AnalysisHierarchy> AnalysisHierarchyFkChildNavigations { get; set; } = new List<AnalysisHierarchy>();

    public virtual ICollection<AnalysisHierarchy> AnalysisHierarchyFkParentNavigations { get; set; } = new List<AnalysisHierarchy>();

    public virtual ICollection<AnalysisHtml> AnalysisHtmls { get; set; } = new List<AnalysisHtml>();

    public virtual ICollection<AnalysisImpl> AnalysisImpls { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisOjtNote> AnalysisOjtNotes { get; set; } = new List<AnalysisOjtNote>();

    public virtual ICollection<AnalysisProcedure> AnalysisProcedures { get; set; } = new List<AnalysisProcedure>();

    public virtual ICollection<AnalysisQuestion> AnalysisQuestions { get; set; } = new List<AnalysisQuestion>();

    public virtual ICollection<Consolidation> Consolidations { get; set; } = new List<Consolidation>();

    public virtual ICollection<DirectObjective> DirectObjectives { get; set; } = new List<DirectObjective>();

    public virtual ICollection<LsQualCardItem> LsQualCardItems { get; set; } = new List<LsQualCardItem>();

    public virtual ICollection<LsQualEvaluatorTrainer> LsQualEvaluatorTrainers { get; set; } = new List<LsQualEvaluatorTrainer>();

    public virtual ICollection<LsTaskQualStep> LsTaskQualSteps { get; set; } = new List<LsTaskQualStep>();

    public virtual ICollection<ObjectiveTask> ObjectiveTasks { get; set; } = new List<ObjectiveTask>();

    public virtual ICollection<QualCardItem> QualCardItems { get; set; } = new List<QualCardItem>();

    public virtual ICollection<RevisionLogAnalysis> RevisionLogAnalyses { get; set; } = new List<RevisionLogAnalysis>();

    public virtual ICollection<Analysis> FkAnalyses { get; set; } = new List<Analysis>();

    public virtual ICollection<Analysis> FkAnalysisRoots { get; set; } = new List<Analysis>();
}
