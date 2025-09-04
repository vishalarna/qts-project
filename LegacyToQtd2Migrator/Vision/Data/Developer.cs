using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Developer
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisComment> AnalysisCommentFkCreatedByNavigations { get; set; } = new List<AnalysisComment>();

    public virtual ICollection<AnalysisComment> AnalysisCommentFkExpiredByNavigations { get; set; } = new List<AnalysisComment>();

    public virtual ICollection<AnalysisCondStand> AnalysisCondStandFkCreatedByNavigations { get; set; } = new List<AnalysisCondStand>();

    public virtual ICollection<AnalysisCondStand> AnalysisCondStandFkExpiredByNavigations { get; set; } = new List<AnalysisCondStand>();

    public virtual ICollection<AnalysisCondStandHtml> AnalysisCondStandHtmlFkCreatedByNavigations { get; set; } = new List<AnalysisCondStandHtml>();

    public virtual ICollection<AnalysisCondStandHtml> AnalysisCondStandHtmlFkExpiredByNavigations { get; set; } = new List<AnalysisCondStandHtml>();

    public virtual ICollection<AnalysisHierarchy> AnalysisHierarchyFkCreatedByNavigations { get; set; } = new List<AnalysisHierarchy>();

    public virtual ICollection<AnalysisHierarchy> AnalysisHierarchyFkExpiredByNavigations { get; set; } = new List<AnalysisHierarchy>();

    public virtual ICollection<AnalysisHtml> AnalysisHtmlFkCreatedByNavigations { get; set; } = new List<AnalysisHtml>();

    public virtual ICollection<AnalysisHtml> AnalysisHtmlFkExpiredByNavigations { get; set; } = new List<AnalysisHtml>();

    public virtual ICollection<AnalysisImpl> AnalysisImplFkCreatedByNavigations { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisImpl> AnalysisImplFkExpiredByNavigations { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisLevelImpl> AnalysisLevelImplFkCreatedByNavigations { get; set; } = new List<AnalysisLevelImpl>();

    public virtual ICollection<AnalysisLevelImpl> AnalysisLevelImplFkExpiredByNavigations { get; set; } = new List<AnalysisLevelImpl>();

    public virtual ICollection<AnalysisOjtNote> AnalysisOjtNoteFkCreatedByNavigations { get; set; } = new List<AnalysisOjtNote>();

    public virtual ICollection<AnalysisOjtNote> AnalysisOjtNoteFkExpiredByNavigations { get; set; } = new List<AnalysisOjtNote>();

    public virtual ICollection<AnalysisProcedure> AnalysisProcedureFkCreatedByNavigations { get; set; } = new List<AnalysisProcedure>();

    public virtual ICollection<AnalysisProcedure> AnalysisProcedureFkExpiredByNavigations { get; set; } = new List<AnalysisProcedure>();

    public virtual ICollection<AnalysisQualImpl> AnalysisQualImplFkCreatedByNavigations { get; set; } = new List<AnalysisQualImpl>();

    public virtual ICollection<AnalysisQualImpl> AnalysisQualImplFkExpiredByNavigations { get; set; } = new List<AnalysisQualImpl>();

    public virtual ICollection<AnalysisQuestion> AnalysisQuestionFkCreatedByNavigations { get; set; } = new List<AnalysisQuestion>();

    public virtual ICollection<AnalysisQuestion> AnalysisQuestionFkExpiredByNavigations { get; set; } = new List<AnalysisQuestion>();

    public virtual ICollection<Consolidation> ConsolidationFkCreatedByNavigations { get; set; } = new List<Consolidation>();

    public virtual ICollection<Consolidation> ConsolidationFkExpiredByNavigations { get; set; } = new List<Consolidation>();

    public virtual ICollection<ContentImpl> ContentImplFkCreatedByNavigations { get; set; } = new List<ContentImpl>();

    public virtual ICollection<ContentImpl> ContentImplFkExpiredByNavigations { get; set; } = new List<ContentImpl>();

    public virtual ICollection<ContentStorageAicc> ContentStorageAiccFkCreatedByNavigations { get; set; } = new List<ContentStorageAicc>();

    public virtual ICollection<ContentStorageAicc> ContentStorageAiccFkExpiredByNavigations { get; set; } = new List<ContentStorageAicc>();

    public virtual ICollection<ContentStorage> ContentStorageFkCreatedByNavigations { get; set; } = new List<ContentStorage>();

    public virtual ICollection<ContentStorage> ContentStorageFkExpiredByNavigations { get; set; } = new List<ContentStorage>();

    public virtual ICollection<CoverSheetHtml> CoverSheetHtmlFkCreatedByNavigations { get; set; } = new List<CoverSheetHtml>();

    public virtual ICollection<CoverSheetHtml> CoverSheetHtmlFkExpiredByNavigations { get; set; } = new List<CoverSheetHtml>();

    public virtual ICollection<CoverSheetImpl> CoverSheetImplFkCreatedByNavigations { get; set; } = new List<CoverSheetImpl>();

    public virtual ICollection<CoverSheetImpl> CoverSheetImplFkExpiredByNavigations { get; set; } = new List<CoverSheetImpl>();

    public virtual ICollection<DeveloperAuthorization> DeveloperAuthorizationFkCreatedByNavigations { get; set; } = new List<DeveloperAuthorization>();

    public virtual ICollection<DeveloperAuthorization> DeveloperAuthorizationFkDeveloperNavigations { get; set; } = new List<DeveloperAuthorization>();

    public virtual ICollection<DeveloperAuthorization> DeveloperAuthorizationFkExpiredByNavigations { get; set; } = new List<DeveloperAuthorization>();

    public virtual ICollection<DeveloperImpl> DeveloperImplFkCreatedByNavigations { get; set; } = new List<DeveloperImpl>();

    public virtual ICollection<DeveloperImpl> DeveloperImplFkDeveloperNavigations { get; set; } = new List<DeveloperImpl>();

    public virtual ICollection<DeveloperImpl> DeveloperImplFkExpiredByNavigations { get; set; } = new List<DeveloperImpl>();

    public virtual ICollection<DirectObjective> DirectObjectiveFkCreatedByNavigations { get; set; } = new List<DirectObjective>();

    public virtual ICollection<DirectObjective> DirectObjectiveFkExpiredByNavigations { get; set; } = new List<DirectObjective>();

    public virtual ICollection<DoclinkHistory> DoclinkHistoryFkCreatedByNavigations { get; set; } = new List<DoclinkHistory>();

    public virtual ICollection<DoclinkHistory> DoclinkHistoryFkExpiredByNavigations { get; set; } = new List<DoclinkHistory>();

    public virtual ICollection<DoclinkImpl> DoclinkImplFkCreatedByNavigations { get; set; } = new List<DoclinkImpl>();

    public virtual ICollection<DoclinkImpl> DoclinkImplFkExpiredByNavigations { get; set; } = new List<DoclinkImpl>();

    public virtual ICollection<DocumentScript> DocumentScriptFkCreatedByNavigations { get; set; } = new List<DocumentScript>();

    public virtual ICollection<DocumentScript> DocumentScriptFkExpiredByNavigations { get; set; } = new List<DocumentScript>();

    public virtual ICollection<ExamChoiceOrder> ExamChoiceOrderFkCreatedByNavigations { get; set; } = new List<ExamChoiceOrder>();

    public virtual ICollection<ExamChoiceOrder> ExamChoiceOrderFkExpiredByNavigations { get; set; } = new List<ExamChoiceOrder>();

    public virtual ICollection<ExamComment> ExamCommentFkCreatedByNavigations { get; set; } = new List<ExamComment>();

    public virtual ICollection<ExamComment> ExamCommentFkExpiredByNavigations { get; set; } = new List<ExamComment>();

    public virtual ICollection<ExamFilter> ExamFilterFkCreatedByNavigations { get; set; } = new List<ExamFilter>();

    public virtual ICollection<ExamFilter> ExamFilterFkExpiredByNavigations { get; set; } = new List<ExamFilter>();

    public virtual ICollection<ExamImpl> ExamImplFkCreatedByNavigations { get; set; } = new List<ExamImpl>();

    public virtual ICollection<ExamImpl> ExamImplFkExpiredByNavigations { get; set; } = new List<ExamImpl>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbacks { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamOwner> ExamOwnerFkCreatedByNavigations { get; set; } = new List<ExamOwner>();

    public virtual ICollection<ExamOwner> ExamOwnerFkDeveloperNavigations { get; set; } = new List<ExamOwner>();

    public virtual ICollection<ExamOwner> ExamOwnerFkExpiredByNavigations { get; set; } = new List<ExamOwner>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptionFkCreatedByNavigations { get; set; } = new List<ExamPrintOption>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptionFkExpiredByNavigations { get; set; } = new List<ExamPrintOption>();

    public virtual ICollection<ExamProfileByObjective> ExamProfileByObjectiveFkCreatedByNavigations { get; set; } = new List<ExamProfileByObjective>();

    public virtual ICollection<ExamProfileByObjective> ExamProfileByObjectiveFkExpiredByNavigations { get; set; } = new List<ExamProfileByObjective>();

    public virtual ICollection<ExamStatusImpl> ExamStatusImplFkCreatedByNavigations { get; set; } = new List<ExamStatusImpl>();

    public virtual ICollection<ExamStatusImpl> ExamStatusImplFkExpiredByNavigations { get; set; } = new List<ExamStatusImpl>();

    public virtual ICollection<ExamUnitOb> ExamUnitObFkCreatedByNavigations { get; set; } = new List<ExamUnitOb>();

    public virtual ICollection<ExamUnitOb> ExamUnitObFkExpiredByNavigations { get; set; } = new List<ExamUnitOb>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgFkCreatedByNavigations { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgFkExpiredByNavigations { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<ExamUnitQq> ExamUnitQqFkCreatedByNavigations { get; set; } = new List<ExamUnitQq>();

    public virtual ICollection<ExamUnitQq> ExamUnitQqFkExpiredByNavigations { get; set; } = new List<ExamUnitQq>();

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<Lock> Locks { get; set; } = new List<Lock>();

    public virtual ICollection<LsDeveloperToLearner> LsDeveloperToLearners { get; set; } = new List<LsDeveloperToLearner>();

    public virtual ICollection<LsTimeToCompleteImpl> LsTimeToCompleteImplFkCreatedByNavigations { get; set; } = new List<LsTimeToCompleteImpl>();

    public virtual ICollection<LsTimeToCompleteImpl> LsTimeToCompleteImplFkExpiredByNavigations { get; set; } = new List<LsTimeToCompleteImpl>();

    public virtual ICollection<LsTrainingReqComment> LsTrainingReqComments { get; set; } = new List<LsTrainingReqComment>();

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItemFkDeveloperAssignedToNavigations { get; set; } = new List<LsTrainingReqItem>();

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItemFkDeveloperResolvedByNavigations { get; set; } = new List<LsTrainingReqItem>();

    public virtual ICollection<ObjectiveClassImpl> ObjectiveClassImplFkCreatedByNavigations { get; set; } = new List<ObjectiveClassImpl>();

    public virtual ICollection<ObjectiveClassImpl> ObjectiveClassImplFkExpiredByNavigations { get; set; } = new List<ObjectiveClassImpl>();

    public virtual ICollection<ObjectiveComment> ObjectiveCommentFkCreatedByNavigations { get; set; } = new List<ObjectiveComment>();

    public virtual ICollection<ObjectiveComment> ObjectiveCommentFkExpiredByNavigations { get; set; } = new List<ObjectiveComment>();

    public virtual ICollection<ObjectiveCondStand> ObjectiveCondStandFkCreatedByNavigations { get; set; } = new List<ObjectiveCondStand>();

    public virtual ICollection<ObjectiveCondStand> ObjectiveCondStandFkExpiredByNavigations { get; set; } = new List<ObjectiveCondStand>();

    public virtual ICollection<ObjectiveHierarchy> ObjectiveHierarchyFkCreatedByNavigations { get; set; } = new List<ObjectiveHierarchy>();

    public virtual ICollection<ObjectiveHierarchy> ObjectiveHierarchyFkExpiredByNavigations { get; set; } = new List<ObjectiveHierarchy>();

    public virtual ICollection<ObjectiveHtml> ObjectiveHtmlFkCreatedByNavigations { get; set; } = new List<ObjectiveHtml>();

    public virtual ICollection<ObjectiveHtml> ObjectiveHtmlFkExpiredByNavigations { get; set; } = new List<ObjectiveHtml>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImplFkCreatedByNavigations { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImplFkExpiredByNavigations { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveLevelImpl> ObjectiveLevelImplFkCreatedByNavigations { get; set; } = new List<ObjectiveLevelImpl>();

    public virtual ICollection<ObjectiveLevelImpl> ObjectiveLevelImplFkExpiredByNavigations { get; set; } = new List<ObjectiveLevelImpl>();

    public virtual ICollection<ObjectiveMediaImpl> ObjectiveMediaImplFkCreatedByNavigations { get; set; } = new List<ObjectiveMediaImpl>();

    public virtual ICollection<ObjectiveMediaImpl> ObjectiveMediaImplFkExpiredByNavigations { get; set; } = new List<ObjectiveMediaImpl>();

    public virtual ICollection<ObjectiveQuestion> ObjectiveQuestionFkCreatedByNavigations { get; set; } = new List<ObjectiveQuestion>();

    public virtual ICollection<ObjectiveQuestion> ObjectiveQuestionFkExpiredByNavigations { get; set; } = new List<ObjectiveQuestion>();

    public virtual ICollection<ObjectiveSettingImpl> ObjectiveSettingImplFkCreatedByNavigations { get; set; } = new List<ObjectiveSettingImpl>();

    public virtual ICollection<ObjectiveSettingImpl> ObjectiveSettingImplFkExpiredByNavigations { get; set; } = new List<ObjectiveSettingImpl>();

    public virtual ICollection<ObjectiveStatusImpl> ObjectiveStatusImplFkCreatedByNavigations { get; set; } = new List<ObjectiveStatusImpl>();

    public virtual ICollection<ObjectiveStatusImpl> ObjectiveStatusImplFkExpiredByNavigations { get; set; } = new List<ObjectiveStatusImpl>();

    public virtual ICollection<ObjectiveTask> ObjectiveTaskFkCreatedByNavigations { get; set; } = new List<ObjectiveTask>();

    public virtual ICollection<ObjectiveTask> ObjectiveTaskFkExpiredByNavigations { get; set; } = new List<ObjectiveTask>();

    public virtual ICollection<ProgramAlternateAudit> ProgramAlternateAudits { get; set; } = new List<ProgramAlternateAudit>();

    public virtual ICollection<ProgramComment> ProgramCommentFkCreatedByNavigations { get; set; } = new List<ProgramComment>();

    public virtual ICollection<ProgramComment> ProgramCommentFkExpiredByNavigations { get; set; } = new List<ProgramComment>();

    public virtual ICollection<ProgramExamOnlyWeighting> ProgramExamOnlyWeightingFkCreatedByNavigations { get; set; } = new List<ProgramExamOnlyWeighting>();

    public virtual ICollection<ProgramExamOnlyWeighting> ProgramExamOnlyWeightingFkExpiredByNavigations { get; set; } = new List<ProgramExamOnlyWeighting>();

    public virtual ICollection<ProgramHierarchy> ProgramHierarchyFkCreatedByNavigations { get; set; } = new List<ProgramHierarchy>();

    public virtual ICollection<ProgramHierarchy> ProgramHierarchyFkExpiredByNavigations { get; set; } = new List<ProgramHierarchy>();

    public virtual ICollection<ProgramHtml> ProgramHtmlFkCreatedByNavigations { get; set; } = new List<ProgramHtml>();

    public virtual ICollection<ProgramHtml> ProgramHtmlFkExpiredByNavigations { get; set; } = new List<ProgramHtml>();

    public virtual ICollection<ProgramImpl> ProgramImplFkCreatedByNavigations { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramImpl> ProgramImplFkExpiredByNavigations { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramLevelImpl> ProgramLevelImplFkCreatedByNavigations { get; set; } = new List<ProgramLevelImpl>();

    public virtual ICollection<ProgramLevelImpl> ProgramLevelImplFkExpiredByNavigations { get; set; } = new List<ProgramLevelImpl>();

    public virtual ICollection<ProgramObjectiveContent> ProgramObjectiveContentFkCreatedByNavigations { get; set; } = new List<ProgramObjectiveContent>();

    public virtual ICollection<ProgramObjectiveContent> ProgramObjectiveContentFkExpiredByNavigations { get; set; } = new List<ProgramObjectiveContent>();

    public virtual ICollection<ProgramOrganizerTypeImpl> ProgramOrganizerTypeImplFkCreatedByNavigations { get; set; } = new List<ProgramOrganizerTypeImpl>();

    public virtual ICollection<ProgramOrganizerTypeImpl> ProgramOrganizerTypeImplFkExpiredByNavigations { get; set; } = new List<ProgramOrganizerTypeImpl>();

    public virtual ICollection<ProgramPrerequisite> ProgramPrerequisiteFkCreatedByNavigations { get; set; } = new List<ProgramPrerequisite>();

    public virtual ICollection<ProgramPrerequisite> ProgramPrerequisiteFkExpiredByNavigations { get; set; } = new List<ProgramPrerequisite>();

    public virtual ICollection<ProgramStaticExam> ProgramStaticExamFkCreatedByNavigations { get; set; } = new List<ProgramStaticExam>();

    public virtual ICollection<ProgramStaticExam> ProgramStaticExamFkExpiredByNavigations { get; set; } = new List<ProgramStaticExam>();

    public virtual ICollection<ProgramStatusImpl> ProgramStatusImplFkCreatedByNavigations { get; set; } = new List<ProgramStatusImpl>();

    public virtual ICollection<ProgramStatusImpl> ProgramStatusImplFkExpiredByNavigations { get; set; } = new List<ProgramStatusImpl>();

    public virtual ICollection<ProgramTuTypeImpl> ProgramTuTypeImplFkCreatedByNavigations { get; set; } = new List<ProgramTuTypeImpl>();

    public virtual ICollection<ProgramTuTypeImpl> ProgramTuTypeImplFkExpiredByNavigations { get; set; } = new List<ProgramTuTypeImpl>();

    public virtual ICollection<ProjectImpl> ProjectImplFkCreatedByNavigations { get; set; } = new List<ProjectImpl>();

    public virtual ICollection<ProjectImpl> ProjectImplFkExpiredByNavigations { get; set; } = new List<ProjectImpl>();

    public virtual ICollection<QualCardImpl> QualCardImplFkCreatedByNavigations { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<QualCardImpl> QualCardImplFkExpiredByNavigations { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<QualCardItem> QualCardItemFkCreatedByNavigations { get; set; } = new List<QualCardItem>();

    public virtual ICollection<QualCardItem> QualCardItemFkExpiredByNavigations { get; set; } = new List<QualCardItem>();

    public virtual ICollection<QualCardPrerequisite> QualCardPrerequisiteFkCreatedByNavigations { get; set; } = new List<QualCardPrerequisite>();

    public virtual ICollection<QualCardPrerequisite> QualCardPrerequisiteFkExpiredByNavigations { get; set; } = new List<QualCardPrerequisite>();

    public virtual ICollection<QualCardStatusImpl> QualCardStatusImplFkCreatedByNavigations { get; set; } = new List<QualCardStatusImpl>();

    public virtual ICollection<QualCardStatusImpl> QualCardStatusImplFkExpiredByNavigations { get; set; } = new List<QualCardStatusImpl>();

    public virtual ICollection<QuestionComment> QuestionCommentFkCreatedByNavigations { get; set; } = new List<QuestionComment>();

    public virtual ICollection<QuestionComment> QuestionCommentFkExpiredByNavigations { get; set; } = new List<QuestionComment>();

    public virtual ICollection<QuestionE> QuestionEFkCreatedByNavigations { get; set; } = new List<QuestionE>();

    public virtual ICollection<QuestionE> QuestionEFkExpiredByNavigations { get; set; } = new List<QuestionE>();

    public virtual ICollection<QuestionEsHtml> QuestionEsHtmlFkCreatedByNavigations { get; set; } = new List<QuestionEsHtml>();

    public virtual ICollection<QuestionEsHtml> QuestionEsHtmlFkExpiredByNavigations { get; set; } = new List<QuestionEsHtml>();

    public virtual ICollection<QuestionExplanation> QuestionExplanationFkCreatedByNavigations { get; set; } = new List<QuestionExplanation>();

    public virtual ICollection<QuestionExplanation> QuestionExplanationFkExpiredByNavigations { get; set; } = new List<QuestionExplanation>();

    public virtual ICollection<QuestionFi> QuestionFiFkCreatedByNavigations { get; set; } = new List<QuestionFi>();

    public virtual ICollection<QuestionFi> QuestionFiFkExpiredByNavigations { get; set; } = new List<QuestionFi>();

    public virtual ICollection<QuestionFiHtml> QuestionFiHtmlFkCreatedByNavigations { get; set; } = new List<QuestionFiHtml>();

    public virtual ICollection<QuestionFiHtml> QuestionFiHtmlFkExpiredByNavigations { get; set; } = new List<QuestionFiHtml>();

    public virtual ICollection<QuestionHtml> QuestionHtmlFkCreatedByNavigations { get; set; } = new List<QuestionHtml>();

    public virtual ICollection<QuestionHtml> QuestionHtmlFkExpiredByNavigations { get; set; } = new List<QuestionHtml>();

    public virtual ICollection<QuestionImpl> QuestionImplFkCreatedByNavigations { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionImpl> QuestionImplFkExpiredByNavigations { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionImpl> QuestionImplFkLockedByNavigations { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionMaChoice> QuestionMaChoiceFkCreatedByNavigations { get; set; } = new List<QuestionMaChoice>();

    public virtual ICollection<QuestionMaChoice> QuestionMaChoiceFkExpiredByNavigations { get; set; } = new List<QuestionMaChoice>();

    public virtual ICollection<QuestionMaChoiceHtml> QuestionMaChoiceHtmlFkCreatedByNavigations { get; set; } = new List<QuestionMaChoiceHtml>();

    public virtual ICollection<QuestionMaChoiceHtml> QuestionMaChoiceHtmlFkExpiredByNavigations { get; set; } = new List<QuestionMaChoiceHtml>();

    public virtual ICollection<QuestionMaItem> QuestionMaItemFkCreatedByNavigations { get; set; } = new List<QuestionMaItem>();

    public virtual ICollection<QuestionMaItem> QuestionMaItemFkExpiredByNavigations { get; set; } = new List<QuestionMaItem>();

    public virtual ICollection<QuestionMaItemHtml> QuestionMaItemHtmlFkCreatedByNavigations { get; set; } = new List<QuestionMaItemHtml>();

    public virtual ICollection<QuestionMaItemHtml> QuestionMaItemHtmlFkExpiredByNavigations { get; set; } = new List<QuestionMaItemHtml>();

    public virtual ICollection<QuestionMcChoice> QuestionMcChoiceFkCreatedByNavigations { get; set; } = new List<QuestionMcChoice>();

    public virtual ICollection<QuestionMcChoice> QuestionMcChoiceFkExpiredByNavigations { get; set; } = new List<QuestionMcChoice>();

    public virtual ICollection<QuestionMcChoiceHtml> QuestionMcChoiceHtmlFkCreatedByNavigations { get; set; } = new List<QuestionMcChoiceHtml>();

    public virtual ICollection<QuestionMcChoiceHtml> QuestionMcChoiceHtmlFkExpiredByNavigations { get; set; } = new List<QuestionMcChoiceHtml>();

    public virtual ICollection<QuestionMc> QuestionMcFkCreatedByNavigations { get; set; } = new List<QuestionMc>();

    public virtual ICollection<QuestionMc> QuestionMcFkExpiredByNavigations { get; set; } = new List<QuestionMc>();

    public virtual ICollection<QuestionSa> QuestionSaFkCreatedByNavigations { get; set; } = new List<QuestionSa>();

    public virtual ICollection<QuestionSa> QuestionSaFkExpiredByNavigations { get; set; } = new List<QuestionSa>();

    public virtual ICollection<QuestionSaHtml> QuestionSaHtmlFkCreatedByNavigations { get; set; } = new List<QuestionSaHtml>();

    public virtual ICollection<QuestionSaHtml> QuestionSaHtmlFkExpiredByNavigations { get; set; } = new List<QuestionSaHtml>();

    public virtual ICollection<QuestionSc> QuestionScFkCreatedByNavigations { get; set; } = new List<QuestionSc>();

    public virtual ICollection<QuestionSc> QuestionScFkExpiredByNavigations { get; set; } = new List<QuestionSc>();

    public virtual ICollection<QuestionStatusImpl> QuestionStatusImplFkCreatedByNavigations { get; set; } = new List<QuestionStatusImpl>();

    public virtual ICollection<QuestionStatusImpl> QuestionStatusImplFkExpiredByNavigations { get; set; } = new List<QuestionStatusImpl>();

    public virtual ICollection<QuestionStem> QuestionStemFkCreatedByNavigations { get; set; } = new List<QuestionStem>();

    public virtual ICollection<QuestionStem> QuestionStemFkExpiredByNavigations { get; set; } = new List<QuestionStem>();

    public virtual ICollection<QuestionTf> QuestionTfFkCreatedByNavigations { get; set; } = new List<QuestionTf>();

    public virtual ICollection<QuestionTf> QuestionTfFkExpiredByNavigations { get; set; } = new List<QuestionTf>();

    public virtual ICollection<Recycled> RecycledFkCreatedByNavigations { get; set; } = new List<Recycled>();

    public virtual ICollection<Recycled> RecycledFkExpiredByNavigations { get; set; } = new List<Recycled>();

    public virtual ICollection<RevisionTag> RevisionTags { get; set; } = new List<RevisionTag>();

    public virtual ICollection<SecurityGroupImpl> SecurityGroupImplFkCreatedByNavigations { get; set; } = new List<SecurityGroupImpl>();

    public virtual ICollection<SecurityGroupImpl> SecurityGroupImplFkExpiredByNavigations { get; set; } = new List<SecurityGroupImpl>();

    public virtual ICollection<Sequencing> SequencingFkCreatedByNavigations { get; set; } = new List<Sequencing>();

    public virtual ICollection<Sequencing> SequencingFkExpiredByNavigations { get; set; } = new List<Sequencing>();

    public virtual ICollection<TaskChangeImpactImpl> TaskChangeImpactImplFkCreatedByNavigations { get; set; } = new List<TaskChangeImpactImpl>();

    public virtual ICollection<TaskChangeImpactImpl> TaskChangeImpactImplFkExpiredByNavigations { get; set; } = new List<TaskChangeImpactImpl>();

    public virtual ICollection<TaskDeselectionImpl> TaskDeselectionImplFkCreatedByNavigations { get; set; } = new List<TaskDeselectionImpl>();

    public virtual ICollection<TaskDeselectionImpl> TaskDeselectionImplFkExpiredByNavigations { get; set; } = new List<TaskDeselectionImpl>();

    public virtual ICollection<TaskStatusImpl> TaskStatusImplFkCreatedByNavigations { get; set; } = new List<TaskStatusImpl>();

    public virtual ICollection<TaskStatusImpl> TaskStatusImplFkExpiredByNavigations { get; set; } = new List<TaskStatusImpl>();

    public virtual ICollection<TimeSpanImpl> TimeSpanImplFkCreatedByNavigations { get; set; } = new List<TimeSpanImpl>();

    public virtual ICollection<TimeSpanImpl> TimeSpanImplFkExpiredByNavigations { get; set; } = new List<TimeSpanImpl>();

    public virtual ICollection<TimeTypeImpl> TimeTypeImplFkCreatedByNavigations { get; set; } = new List<TimeTypeImpl>();

    public virtual ICollection<TimeTypeImpl> TimeTypeImplFkExpiredByNavigations { get; set; } = new List<TimeTypeImpl>();

    public virtual ICollection<XrefLibHtml> XrefLibHtmlFkCreatedByNavigations { get; set; } = new List<XrefLibHtml>();

    public virtual ICollection<XrefLibHtml> XrefLibHtmlFkExpiredByNavigations { get; set; } = new List<XrefLibHtml>();

    public virtual ICollection<XrefLibImpl> XrefLibImplFkCreatedByNavigations { get; set; } = new List<XrefLibImpl>();

    public virtual ICollection<XrefLibImpl> XrefLibImplFkExpiredByNavigations { get; set; } = new List<XrefLibImpl>();

    public virtual ICollection<XrefLibLink> XrefLibLinkFkCreatedByNavigations { get; set; } = new List<XrefLibLink>();

    public virtual ICollection<XrefLibLink> XrefLibLinkFkExpiredByNavigations { get; set; } = new List<XrefLibLink>();
}
