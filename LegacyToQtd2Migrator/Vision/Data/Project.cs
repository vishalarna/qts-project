using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Project
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisImpl> AnalysisImpls { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisLevelImpl> AnalysisLevelImpls { get; set; } = new List<AnalysisLevelImpl>();

    public virtual ICollection<AnalysisQualImpl> AnalysisQualImpls { get; set; } = new List<AnalysisQualImpl>();

    public virtual ICollection<AnalysisQuestion> AnalysisQuestions { get; set; } = new List<AnalysisQuestion>();

    public virtual ICollection<Consolidation> Consolidations { get; set; } = new List<Consolidation>();

    public virtual ICollection<CoverSheetImpl> CoverSheetImpls { get; set; } = new List<CoverSheetImpl>();

    public virtual ICollection<DeveloperAuthorization> DeveloperAuthorizations { get; set; } = new List<DeveloperAuthorization>();

    public virtual ICollection<DirectObjective> DirectObjectives { get; set; } = new List<DirectObjective>();

    public virtual ICollection<DoclinkImpl> DoclinkImpls { get; set; } = new List<DoclinkImpl>();

    public virtual ICollection<DocumentScript> DocumentScripts { get; set; } = new List<DocumentScript>();

    public virtual ICollection<ExamImpl> ExamImpls { get; set; } = new List<ExamImpl>();

    public virtual ICollection<ExamStatusImpl> ExamStatusImpls { get; set; } = new List<ExamStatusImpl>();

    public virtual ICollection<Import> Imports { get; set; } = new List<Import>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<LsCompanyProject> LsCompanyProjects { get; set; } = new List<LsCompanyProject>();

    public virtual ICollection<LsOrgPanelsTopNode> LsOrgPanelsTopNodes { get; set; } = new List<LsOrgPanelsTopNode>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveLevelImpl> ObjectiveLevelImpls { get; set; } = new List<ObjectiveLevelImpl>();

    public virtual ICollection<ObjectiveQuestion> ObjectiveQuestions { get; set; } = new List<ObjectiveQuestion>();

    public virtual ICollection<ObjectiveStatusImpl> ObjectiveStatusImpls { get; set; } = new List<ObjectiveStatusImpl>();

    public virtual ICollection<ObjectiveTask> ObjectiveTasks { get; set; } = new List<ObjectiveTask>();

    public virtual ICollection<ProgramImpl> ProgramImpls { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramLevelImpl> ProgramLevelImpls { get; set; } = new List<ProgramLevelImpl>();

    public virtual ICollection<ProgramStatusImpl> ProgramStatusImpls { get; set; } = new List<ProgramStatusImpl>();

    public virtual ICollection<ProjectImpl> ProjectImpls { get; set; } = new List<ProjectImpl>();

    public virtual ICollection<ProjectTemplatePath> ProjectTemplatePaths { get; set; } = new List<ProjectTemplatePath>();

    public virtual ICollection<QualCardImpl> QualCardImpls { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<QuestionImpl> QuestionImpls { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionStatusImpl> QuestionStatusImpls { get; set; } = new List<QuestionStatusImpl>();

    public virtual ICollection<Recycled> Recycleds { get; set; } = new List<Recycled>();

    public virtual ICollection<RevisionTag> RevisionTags { get; set; } = new List<RevisionTag>();

    public virtual ICollection<SecurityGroupImpl> SecurityGroupImpls { get; set; } = new List<SecurityGroupImpl>();

    public virtual ICollection<Sequencing> Sequencings { get; set; } = new List<Sequencing>();

    public virtual ICollection<TaskDeselectionImpl> TaskDeselectionImpls { get; set; } = new List<TaskDeselectionImpl>();

    public virtual ICollection<TaskStatusImpl> TaskStatusImpls { get; set; } = new List<TaskStatusImpl>();

    public virtual ICollection<TimeSpanImpl> TimeSpanImpls { get; set; } = new List<TimeSpanImpl>();

    public virtual ICollection<XrefLibImpl> XrefLibImpls { get; set; } = new List<XrefLibImpl>();

    public virtual ICollection<LsOrg> FkStructures { get; set; } = new List<LsOrg>();
}
