using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VisionContext : DbContext
{
    public VisionContext()
    {
    }

    public VisionContext(DbContextOptions<VisionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Analysis> Analyses { get; set; }

    public virtual DbSet<AnalysisComment> AnalysisComments { get; set; }

    public virtual DbSet<AnalysisCondStand> AnalysisCondStands { get; set; }

    public virtual DbSet<AnalysisCondStandHtml> AnalysisCondStandHtmls { get; set; }

    public virtual DbSet<AnalysisHierarchy> AnalysisHierarchies { get; set; }

    public virtual DbSet<AnalysisHtml> AnalysisHtmls { get; set; }

    public virtual DbSet<AnalysisImpl> AnalysisImpls { get; set; }

    public virtual DbSet<AnalysisLevel> AnalysisLevels { get; set; }

    public virtual DbSet<AnalysisLevelImpl> AnalysisLevelImpls { get; set; }

    public virtual DbSet<AnalysisOjtNote> AnalysisOjtNotes { get; set; }

    public virtual DbSet<AnalysisProcedure> AnalysisProcedures { get; set; }

    public virtual DbSet<AnalysisQual> AnalysisQuals { get; set; }

    public virtual DbSet<AnalysisQualImpl> AnalysisQualImpls { get; set; }

    public virtual DbSet<AnalysisQuestion> AnalysisQuestions { get; set; }

    public virtual DbSet<AppVersion> AppVersions { get; set; }

    public virtual DbSet<Cdatum> Cdata { get; set; }

    public virtual DbSet<Cglobal> Cglobals { get; set; }

    public virtual DbSet<Consolidation> Consolidations { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<ContentImpl> ContentImpls { get; set; }

    public virtual DbSet<ContentStorage> ContentStorages { get; set; }

    public virtual DbSet<ContentStorageAicc> ContentStorageAiccs { get; set; }

    public virtual DbSet<CoverSheet> CoverSheets { get; set; }

    public virtual DbSet<CoverSheetHtml> CoverSheetHtmls { get; set; }

    public virtual DbSet<CoverSheetImpl> CoverSheetImpls { get; set; }

    public virtual DbSet<DbUpdateHistory> DbUpdateHistories { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<DeveloperAuthorization> DeveloperAuthorizations { get; set; }

    public virtual DbSet<DeveloperImpl> DeveloperImpls { get; set; }

    public virtual DbSet<DirectObjective> DirectObjectives { get; set; }

    public virtual DbSet<Doclink> Doclinks { get; set; }

    public virtual DbSet<DoclinkHistory> DoclinkHistories { get; set; }

    public virtual DbSet<DoclinkImpl> DoclinkImpls { get; set; }

    public virtual DbSet<DocumentScript> DocumentScripts { get; set; }

    public virtual DbSet<EmailCategory> EmailCategories { get; set; }

    public virtual DbSet<EmailName> EmailNames { get; set; }

    public virtual DbSet<Eval> Evals { get; set; }

    public virtual DbSet<EvalEvent> EvalEvents { get; set; }

    public virtual DbSet<EvalItem> EvalItems { get; set; }

    public virtual DbSet<EvalRatingItem> EvalRatingItems { get; set; }

    public virtual DbSet<EvalResponse> EvalResponses { get; set; }

    public virtual DbSet<EvalType> EvalTypes { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamChoiceOrder> ExamChoiceOrders { get; set; }

    public virtual DbSet<ExamComment> ExamComments { get; set; }

    public virtual DbSet<ExamEvent> ExamEvents { get; set; }

    public virtual DbSet<ExamFilter> ExamFilters { get; set; }

    public virtual DbSet<ExamImpl> ExamImpls { get; set; }

    public virtual DbSet<ExamLearnerFeedback> ExamLearnerFeedbacks { get; set; }

    public virtual DbSet<ExamOnlineProfile> ExamOnlineProfiles { get; set; }

    public virtual DbSet<ExamOnlineProfileMessage> ExamOnlineProfileMessages { get; set; }

    public virtual DbSet<ExamOnlineTesting> ExamOnlineTestings { get; set; }

    public virtual DbSet<ExamOwner> ExamOwners { get; set; }

    public virtual DbSet<ExamPrintOption> ExamPrintOptions { get; set; }

    public virtual DbSet<ExamProfileByObjective> ExamProfileByObjectives { get; set; }

    public virtual DbSet<ExamQuestionEvent> ExamQuestionEvents { get; set; }

    public virtual DbSet<ExamQuestionEventFi> ExamQuestionEventFis { get; set; }

    public virtual DbSet<ExamQuestionEventMa> ExamQuestionEventMas { get; set; }

    public virtual DbSet<ExamQuestionEventMc> ExamQuestionEventMcs { get; set; }

    public virtual DbSet<ExamQuestionEventSa> ExamQuestionEventSas { get; set; }

    public virtual DbSet<ExamQuestionEventTf> ExamQuestionEventTfs { get; set; }

    public virtual DbSet<ExamStatus> ExamStatuses { get; set; }

    public virtual DbSet<ExamStatusImpl> ExamStatusImpls { get; set; }

    public virtual DbSet<ExamUnitOb> ExamUnitObs { get; set; }

    public virtual DbSet<ExamUnitPg> ExamUnitPgs { get; set; }

    public virtual DbSet<ExamUnitQq> ExamUnitQqs { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<Lock> Locks { get; set; }

    public virtual DbSet<LsAddress> LsAddresses { get; set; }

    public virtual DbSet<LsAppConfig> LsAppConfigs { get; set; }

    public virtual DbSet<LsAppMessage> LsAppMessages { get; set; }

    public virtual DbSet<LsAppMessageCategory> LsAppMessageCategories { get; set; }

    public virtual DbSet<LsAppOrgspecificparm> LsAppOrgspecificparms { get; set; }

    public virtual DbSet<LsCatalog> LsCatalogs { get; set; }

    public virtual DbSet<LsCatalogLesson> LsCatalogLessons { get; set; }

    public virtual DbSet<LsCatalogPrereq> LsCatalogPrereqs { get; set; }

    public virtual DbSet<LsCatalogRating> LsCatalogRatings { get; set; }

    public virtual DbSet<LsCatalogType> LsCatalogTypes { get; set; }

    public virtual DbSet<LsCertJob> LsCertJobs { get; set; }

    public virtual DbSet<LsCertLrnr> LsCertLrnrs { get; set; }

    public virtual DbSet<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmnts { get; set; }

    public virtual DbSet<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; }

    public virtual DbSet<LsCertLrnrRecordBycert> LsCertLrnrRecordBycerts { get; set; }

    public virtual DbSet<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; }

    public virtual DbSet<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemains { get; set; }

    public virtual DbSet<LsCertManscreCrdit> LsCertManscreCrdits { get; set; }

    public virtual DbSet<LsCertManscreCrditmain> LsCertManscreCrditmains { get; set; }

    public virtual DbSet<LsCertReqruleBasis> LsCertReqruleBases { get; set; }

    public virtual DbSet<LsCertRequirement> LsCertRequirements { get; set; }

    public virtual DbSet<LsCertRequirementsRule> LsCertRequirementsRules { get; set; }

    public virtual DbSet<LsCertTimeframeBasis> LsCertTimeframeBases { get; set; }

    public virtual DbSet<LsCertification> LsCertifications { get; set; }

    public virtual DbSet<LsCompany> LsCompanies { get; set; }

    public virtual DbSet<LsCompanyProject> LsCompanyProjects { get; set; }

    public virtual DbSet<LsCourseType> LsCourseTypes { get; set; }

    public virtual DbSet<LsDataXfer> LsDataXfers { get; set; }

    public virtual DbSet<LsDateModifier> LsDateModifiers { get; set; }

    public virtual DbSet<LsDeveloperToLearner> LsDeveloperToLearners { get; set; }

    public virtual DbSet<LsDoclinkTrack> LsDoclinkTracks { get; set; }

    public virtual DbSet<LsDocument> LsDocuments { get; set; }

    public virtual DbSet<LsEvaluatorTrainerValue> LsEvaluatorTrainerValues { get; set; }

    public virtual DbSet<LsEventAudit> LsEventAudits { get; set; }

    public virtual DbSet<LsEventsToAudit> LsEventsToAudits { get; set; }

    public virtual DbSet<LsExamEvent> LsExamEvents { get; set; }

    public virtual DbSet<LsExamGenerator> LsExamGenerators { get; set; }

    public virtual DbSet<LsExamResult> LsExamResults { get; set; }

    public virtual DbSet<LsExamType> LsExamTypes { get; set; }

    public virtual DbSet<LsExternalCompletion> LsExternalCompletions { get; set; }

    public virtual DbSet<LsExternalCourse> LsExternalCourses { get; set; }

    public virtual DbSet<LsExternalValidationLog> LsExternalValidationLogs { get; set; }

    public virtual DbSet<LsGenericValue> LsGenericValues { get; set; }

    public virtual DbSet<LsHoldRelease> LsHoldReleases { get; set; }

    public virtual DbSet<LsImportJobAssignment> LsImportJobAssignments { get; set; }

    public virtual DbSet<LsImportJobCourse> LsImportJobCourses { get; set; }

    public virtual DbSet<LsLearnerLogin> LsLearnerLogins { get; set; }

    public virtual DbSet<LsLearnerPosition> LsLearnerPositions { get; set; }

    public virtual DbSet<LsLearnerPositionHist> LsLearnerPositionHists { get; set; }

    public virtual DbSet<LsLearningEvent> LsLearningEvents { get; set; }

    public virtual DbSet<LsLearningEventAttempt> LsLearningEventAttempts { get; set; }

    public virtual DbSet<LsLearningEventLearner> LsLearningEventLearners { get; set; }

    public virtual DbSet<LsLearningEventProgram> LsLearningEventPrograms { get; set; }

    public virtual DbSet<LsLearningEventTrack> LsLearningEventTracks { get; set; }

    public virtual DbSet<LsLoginAttempt> LsLoginAttempts { get; set; }

    public virtual DbSet<LsMediaType> LsMediaTypes { get; set; }

    public virtual DbSet<LsObjExternalTrack> LsObjExternalTracks { get; set; }

    public virtual DbSet<LsObjectiveMastery> LsObjectiveMasteries { get; set; }

    public virtual DbSet<LsObjectiveTrack> LsObjectiveTracks { get; set; }

    public virtual DbSet<LsOnlineExamQuestion> LsOnlineExamQuestions { get; set; }

    public virtual DbSet<LsOnlineExamQuestionMa> LsOnlineExamQuestionMas { get; set; }

    public virtual DbSet<LsOnlineExamQuestionMc> LsOnlineExamQuestionMcs { get; set; }

    public virtual DbSet<LsOrg> LsOrgs { get; set; }

    public virtual DbSet<LsOrgHierarchy> LsOrgHierarchies { get; set; }

    public virtual DbSet<LsOrgLevel> LsOrgLevels { get; set; }

    public virtual DbSet<LsOrgPanelsTopNode> LsOrgPanelsTopNodes { get; set; }

    public virtual DbSet<LsPaEvaluatorTrainer> LsPaEvaluatorTrainers { get; set; }

    public virtual DbSet<LsPaOjeRequest> LsPaOjeRequests { get; set; }

    public virtual DbSet<LsPasswordStorage> LsPasswordStorages { get; set; }

    public virtual DbSet<LsPerformanceAssessment> LsPerformanceAssessments { get; set; }

    public virtual DbSet<LsPracticeExam> LsPracticeExams { get; set; }

    public virtual DbSet<LsPracticeExamCo> LsPracticeExamCos { get; set; }

    public virtual DbSet<LsPracticeExamMa> LsPracticeExamMas { get; set; }

    public virtual DbSet<LsPracticeExamTf> LsPracticeExamTfs { get; set; }

    public virtual DbSet<LsPreviewExam> LsPreviewExams { get; set; }

    public virtual DbSet<LsProctorSession> LsProctorSessions { get; set; }

    public virtual DbSet<LsProfileEvalType> LsProfileEvalTypes { get; set; }

    public virtual DbSet<LsProgramCompletion> LsProgramCompletions { get; set; }

    public virtual DbSet<LsQualCard> LsQualCards { get; set; }

    public virtual DbSet<LsQualCardEvent> LsQualCardEvents { get; set; }

    public virtual DbSet<LsQualCardItem> LsQualCardItems { get; set; }

    public virtual DbSet<LsQualCardPrerequisite> LsQualCardPrerequisites { get; set; }

    public virtual DbSet<LsQualCardRoute> LsQualCardRoutes { get; set; }

    public virtual DbSet<LsQualCardRouteHistory> LsQualCardRouteHistories { get; set; }

    public virtual DbSet<LsQualEvaluatorTrainer> LsQualEvaluatorTrainers { get; set; }

    public virtual DbSet<LsQualJobPosition> LsQualJobPositions { get; set; }

    public virtual DbSet<LsRule> LsRules { get; set; }

    public virtual DbSet<LsRuleEmail> LsRuleEmails { get; set; }

    public virtual DbSet<LsRuleItem> LsRuleItems { get; set; }

    public virtual DbSet<LsSecurityCode> LsSecurityCodes { get; set; }

    public virtual DbSet<LsSelectedExam> LsSelectedExams { get; set; }

    public virtual DbSet<LsStatus> LsStatuses { get; set; }

    public virtual DbSet<LsSurvey> LsSurveys { get; set; }

    public virtual DbSet<LsSurveyDifRatingscale> LsSurveyDifRatingscales { get; set; }

    public virtual DbSet<LsSurveyEvent> LsSurveyEvents { get; set; }

    public virtual DbSet<LsSurveyItem> LsSurveyItems { get; set; }

    public virtual DbSet<LsSurveyItemResultsArch> LsSurveyItemResultsArches { get; set; }

    public virtual DbSet<LsSurveyItemResultsDif> LsSurveyItemResultsDifs { get; set; }

    public virtual DbSet<LsSurveyObjectType> LsSurveyObjectTypes { get; set; }

    public virtual DbSet<LsSurveyeventItem> LsSurveyeventItems { get; set; }

    public virtual DbSet<LsSurveyeventRespondent> LsSurveyeventRespondents { get; set; }

    public virtual DbSet<LsTaskQualStep> LsTaskQualSteps { get; set; }

    public virtual DbSet<LsTaskQualification> LsTaskQualifications { get; set; }

    public virtual DbSet<LsTaskSurveyItemsArch> LsTaskSurveyItemsArches { get; set; }

    public virtual DbSet<LsTimeToComplete> LsTimeToCompletes { get; set; }

    public virtual DbSet<LsTimeToCompleteImpl> LsTimeToCompleteImpls { get; set; }

    public virtual DbSet<LsTrainingReqActionType> LsTrainingReqActionTypes { get; set; }

    public virtual DbSet<LsTrainingReqComment> LsTrainingReqComments { get; set; }

    public virtual DbSet<LsTrainingReqItem> LsTrainingReqItems { get; set; }

    public virtual DbSet<LsTrainingReqLink> LsTrainingReqLinks { get; set; }

    public virtual DbSet<LsTrainingReqSourceCatery> LsTrainingReqSourceCateries { get; set; }

    public virtual DbSet<LsTrainingReqStatus> LsTrainingReqStatuses { get; set; }

    public virtual DbSet<LsTrainingType> LsTrainingTypes { get; set; }

    public virtual DbSet<LsTuItemType> LsTuItemTypes { get; set; }

    public virtual DbSet<LsVLesson> LsVLessons { get; set; }

    public virtual DbSet<LsVLessonOb> LsVLessonObs { get; set; }

    public virtual DbSet<LsVersionInfo> LsVersionInfos { get; set; }

    public virtual DbSet<LsVisionItemType> LsVisionItemTypes { get; set; }

    public virtual DbSet<Objective> Objectives { get; set; }

    public virtual DbSet<ObjectiveClass> ObjectiveClasses { get; set; }

    public virtual DbSet<ObjectiveClassImpl> ObjectiveClassImpls { get; set; }

    public virtual DbSet<ObjectiveComment> ObjectiveComments { get; set; }

    public virtual DbSet<ObjectiveCondStand> ObjectiveCondStands { get; set; }

    public virtual DbSet<ObjectiveHierarchy> ObjectiveHierarchies { get; set; }

    public virtual DbSet<ObjectiveHtml> ObjectiveHtmls { get; set; }

    public virtual DbSet<ObjectiveImpl> ObjectiveImpls { get; set; }

    public virtual DbSet<ObjectiveLevel> ObjectiveLevels { get; set; }

    public virtual DbSet<ObjectiveLevelImpl> ObjectiveLevelImpls { get; set; }

    public virtual DbSet<ObjectiveMediaImpl> ObjectiveMediaImpls { get; set; }

    public virtual DbSet<ObjectiveMedium> ObjectiveMedia { get; set; }

    public virtual DbSet<ObjectiveQuestion> ObjectiveQuestions { get; set; }

    public virtual DbSet<ObjectiveSetting> ObjectiveSettings { get; set; }

    public virtual DbSet<ObjectiveSettingImpl> ObjectiveSettingImpls { get; set; }

    public virtual DbSet<ObjectiveStatus> ObjectiveStatuses { get; set; }

    public virtual DbSet<ObjectiveStatusImpl> ObjectiveStatusImpls { get; set; }

    public virtual DbSet<ObjectiveTask> ObjectiveTasks { get; set; }

    public virtual DbSet<OfficeMigration> OfficeMigrations { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramAlternate> ProgramAlternates { get; set; }

    public virtual DbSet<ProgramAlternateAudit> ProgramAlternateAudits { get; set; }

    public virtual DbSet<ProgramAlternateInheritance> ProgramAlternateInheritances { get; set; }

    public virtual DbSet<ProgramComment> ProgramComments { get; set; }

    public virtual DbSet<ProgramExamOnlyWeighting> ProgramExamOnlyWeightings { get; set; }

    public virtual DbSet<ProgramHierarchy> ProgramHierarchies { get; set; }

    public virtual DbSet<ProgramHtml> ProgramHtmls { get; set; }

    public virtual DbSet<ProgramImpl> ProgramImpls { get; set; }

    public virtual DbSet<ProgramLevel> ProgramLevels { get; set; }

    public virtual DbSet<ProgramLevelImpl> ProgramLevelImpls { get; set; }

    public virtual DbSet<ProgramObjectiveContent> ProgramObjectiveContents { get; set; }

    public virtual DbSet<ProgramOrganizerType> ProgramOrganizerTypes { get; set; }

    public virtual DbSet<ProgramOrganizerTypeImpl> ProgramOrganizerTypeImpls { get; set; }

    public virtual DbSet<ProgramPrerequisite> ProgramPrerequisites { get; set; }

    public virtual DbSet<ProgramStaticExam> ProgramStaticExams { get; set; }

    public virtual DbSet<ProgramStatus> ProgramStatuses { get; set; }

    public virtual DbSet<ProgramStatusImpl> ProgramStatusImpls { get; set; }

    public virtual DbSet<ProgramTuType> ProgramTuTypes { get; set; }

    public virtual DbSet<ProgramTuTypeImpl> ProgramTuTypeImpls { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectImpl> ProjectImpls { get; set; }

    public virtual DbSet<ProjectTemplatePath> ProjectTemplatePaths { get; set; }

    public virtual DbSet<QualCard> QualCards { get; set; }

    public virtual DbSet<QualCardImpl> QualCardImpls { get; set; }

    public virtual DbSet<QualCardItem> QualCardItems { get; set; }

    public virtual DbSet<QualCardPrerequisite> QualCardPrerequisites { get; set; }

    public virtual DbSet<QualCardStatus> QualCardStatuses { get; set; }

    public virtual DbSet<QualCardStatusImpl> QualCardStatusImpls { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionComment> QuestionComments { get; set; }

    public virtual DbSet<QuestionE> QuestionEs { get; set; }

    public virtual DbSet<QuestionEsHtml> QuestionEsHtmls { get; set; }

    public virtual DbSet<QuestionExplanation> QuestionExplanations { get; set; }

    public virtual DbSet<QuestionFi> QuestionFis { get; set; }

    public virtual DbSet<QuestionFiHtml> QuestionFiHtmls { get; set; }

    public virtual DbSet<QuestionHtml> QuestionHtmls { get; set; }

    public virtual DbSet<QuestionImpl> QuestionImpls { get; set; }

    public virtual DbSet<QuestionMaChoice> QuestionMaChoices { get; set; }

    public virtual DbSet<QuestionMaChoiceHtml> QuestionMaChoiceHtmls { get; set; }

    public virtual DbSet<QuestionMaItem> QuestionMaItems { get; set; }

    public virtual DbSet<QuestionMaItemHtml> QuestionMaItemHtmls { get; set; }

    public virtual DbSet<QuestionMc> QuestionMcs { get; set; }

    public virtual DbSet<QuestionMcChoice> QuestionMcChoices { get; set; }

    public virtual DbSet<QuestionMcChoiceHtml> QuestionMcChoiceHtmls { get; set; }

    public virtual DbSet<QuestionSa> QuestionSas { get; set; }

    public virtual DbSet<QuestionSaHtml> QuestionSaHtmls { get; set; }

    public virtual DbSet<QuestionSc> QuestionScs { get; set; }

    public virtual DbSet<QuestionStatus> QuestionStatuses { get; set; }

    public virtual DbSet<QuestionStatusImpl> QuestionStatusImpls { get; set; }

    public virtual DbSet<QuestionStem> QuestionStems { get; set; }

    public virtual DbSet<QuestionTf> QuestionTfs { get; set; }

    public virtual DbSet<Recycled> Recycleds { get; set; }

    public virtual DbSet<RevisionLogAnalysis> RevisionLogAnalyses { get; set; }

    public virtual DbSet<RevisionLogExam> RevisionLogExams { get; set; }

    public virtual DbSet<RevisionLogObjective> RevisionLogObjectives { get; set; }

    public virtual DbSet<RevisionLogProgram> RevisionLogPrograms { get; set; }

    public virtual DbSet<RevisionLogQualCard> RevisionLogQualCards { get; set; }

    public virtual DbSet<RevisionLogQuestion> RevisionLogQuestions { get; set; }

    public virtual DbSet<RevisionLogXref> RevisionLogXrefs { get; set; }

    public virtual DbSet<RevisionTag> RevisionTags { get; set; }

    public virtual DbSet<RtfToHtmlMigration> RtfToHtmlMigrations { get; set; }

    public virtual DbSet<RtfTrimList> RtfTrimLists { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJobs { get; set; }

    public virtual DbSet<SecurityGroup> SecurityGroups { get; set; }

    public virtual DbSet<SecurityGroupImpl> SecurityGroupImpls { get; set; }

    public virtual DbSet<Sequencing> Sequencings { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<TaskChangeImpact> TaskChangeImpacts { get; set; }

    public virtual DbSet<TaskChangeImpactImpl> TaskChangeImpactImpls { get; set; }

    public virtual DbSet<TaskDeselection> TaskDeselections { get; set; }

    public virtual DbSet<TaskDeselectionImpl> TaskDeselectionImpls { get; set; }

    public virtual DbSet<TaskStatus> TaskStatuses { get; set; }

    public virtual DbSet<TaskStatusImpl> TaskStatusImpls { get; set; }

    public virtual DbSet<TimeSpan> TimeSpans { get; set; }

    public virtual DbSet<TimeSpanImpl> TimeSpanImpls { get; set; }

    public virtual DbSet<TimeType> TimeTypes { get; set; }

    public virtual DbSet<TimeTypeImpl> TimeTypeImpls { get; set; }

    public virtual DbSet<VdevVwebSession> VdevVwebSessions { get; set; }

    public virtual DbSet<VlsBasicInferredCompletion> VlsBasicInferredCompletions { get; set; }

    public virtual DbSet<VlsExamEventLearner> VlsExamEventLearners { get; set; }

    public virtual DbSet<VlsExamObjective> VlsExamObjectives { get; set; }

    public virtual DbSet<VlsExamObjectiveQuestion> VlsExamObjectiveQuestions { get; set; }

    public virtual DbSet<VlsExamObjectiveSc> VlsExamObjectiveScs { get; set; }

    public virtual DbSet<VlsExamTestingEvent> VlsExamTestingEvents { get; set; }

    public virtual DbSet<VlsInferredCompletion> VlsInferredCompletions { get; set; }

    public virtual DbSet<VlsLearnerFeedback> VlsLearnerFeedbacks { get; set; }

    public virtual DbSet<VlsProHierarchy> VlsProHierarchies { get; set; }

    public virtual DbSet<VlsProgramAllTask> VlsProgramAllTasks { get; set; }

    public virtual DbSet<VlsProgramAssociatedTask> VlsProgramAssociatedTasks { get; set; }

    public virtual DbSet<VlsProgramConsolidTask> VlsProgramConsolidTasks { get; set; }

    public virtual DbSet<VlsProgramDirectTask> VlsProgramDirectTasks { get; set; }

    public virtual DbSet<VlsProgramPracticeQuestion> VlsProgramPracticeQuestions { get; set; }

    public virtual DbSet<VlsProgramQuestion> VlsProgramQuestions { get; set; }

    public virtual DbSet<VlsQuestionScenario> VlsQuestionScenarios { get; set; }

    public virtual DbSet<VlsQuestionSim> VlsQuestionSims { get; set; }

    public virtual DbSet<VlsQuestionType> VlsQuestionTypes { get; set; }

    public virtual DbSet<VlsSubQuestionType> VlsSubQuestionTypes { get; set; }

    public virtual DbSet<VlsXrefQuestion> VlsXrefQuestions { get; set; }

    public virtual DbSet<XrefLib> XrefLibs { get; set; }

    public virtual DbSet<XrefLibHtml> XrefLibHtmls { get; set; }

    public virtual DbSet<XrefLibImpl> XrefLibImpls { get; set; }

    public virtual DbSet<XrefLibLink> XrefLibLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WSAMZN-23VC648D;Database=VISION;Integrated Security=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analysis>(entity =>
        {
            entity.ToTable("ANALYSIS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");

            entity.HasMany(d => d.FkAnalyses).WithMany(p => p.FkAnalysisRoots)
                .UsingEntity<Dictionary<string, object>>(
                    "AnalysisFreeform",
                    r => r.HasOne<Analysis>().WithMany()
                        .HasForeignKey("FkAnalysis")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ANALYSIS_FREEFORM_ANALYSIS"),
                    l => l.HasOne<Analysis>().WithMany()
                        .HasForeignKey("FkAnalysisRoot")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ANALYSIS_FREEFORM_ANAL_ROOT"),
                    j =>
                    {
                        j.HasKey("FkAnalysisRoot", "FkAnalysis").IsClustered(false);
                        j.ToTable("ANALYSIS_FREEFORM");
                        j.IndexerProperty<decimal>("FkAnalysisRoot")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_ANALYSIS_ROOT");
                        j.IndexerProperty<decimal>("FkAnalysis")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_ANALYSIS");
                    });

            entity.HasMany(d => d.FkAnalysisRoots).WithMany(p => p.FkAnalyses)
                .UsingEntity<Dictionary<string, object>>(
                    "AnalysisFreeform",
                    r => r.HasOne<Analysis>().WithMany()
                        .HasForeignKey("FkAnalysisRoot")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ANALYSIS_FREEFORM_ANAL_ROOT"),
                    l => l.HasOne<Analysis>().WithMany()
                        .HasForeignKey("FkAnalysis")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ANALYSIS_FREEFORM_ANALYSIS"),
                    j =>
                    {
                        j.HasKey("FkAnalysisRoot", "FkAnalysis").IsClustered(false);
                        j.ToTable("ANALYSIS_FREEFORM");
                        j.IndexerProperty<decimal>("FkAnalysisRoot")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_ANALYSIS_ROOT");
                        j.IndexerProperty<decimal>("FkAnalysis")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_ANALYSIS");
                    });
        });

        modelBuilder.Entity<AnalysisComment>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("ANALYSIS_COMMENTS");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisComments)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_COMMENTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisCommentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_COMMENTS_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisCommentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_COMMENTS_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisCondStand>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.Type, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("ANALYSIS_COND_STAND");

            entity.HasIndex(e => new { e.FkAnalysis, e.Type, e.DateExpired, e.DateCreated, e.Sequence }, "NDX_ANALYSIS_COND_STAND1");

            entity.HasIndex(e => new { e.FkAnalysis, e.Type, e.Sequence, e.DateExpired }, "NDX_ANALYSIS_COND_STAND2").IsUnique();

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisCondStands)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_COND_STAND_ANALYSIS_ID");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisCondStandFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANAL_COND_STAND_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisCondStandFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANAL_COND_STAND_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisCondStandHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.Type, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("ANALYSIS_COND_STAND_HTML");

            entity.HasIndex(e => new { e.DateExpired, e.FkAnalysis, e.Type, e.Sequence, e.DateCreated }, "NDX_ANALYSIS_COND_STAND_HTML1").IsUnique();

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisCondStandHtmls)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("F_ANALYSIS_COND_STAND_HTML");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisCondStandHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANAL_COND_STD_HTM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisCondStandHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANAL_COND_STD_HTM_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisHierarchy>(entity =>
        {
            entity.HasKey(e => new { e.FkParent, e.DateExpired, e.DateCreated, e.Sequence, e.FkChild }).IsClustered(false);

            entity.ToTable("ANALYSIS_HIERARCHY");

            entity.HasIndex(e => new { e.DateExpired, e.FkChild, e.DateCreated }, "I_ANALYSIS_HIERARCHY2");

            entity.HasIndex(e => new { e.FkParent, e.DateExpired, e.Sequence, e.FkChild }, "NDX_ANALYSIS_HIERARCHY1").IsClustered();

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.FkParent, e.Sequence }, "NDX_ANALYSIS_HIERARCHY2");

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.DateCreated, e.FkParent }, "NDX_ANALYSIS_HIERARCHY3");

            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkChildNavigation).WithMany(p => p.AnalysisHierarchyFkChildNavigations)
                .HasForeignKey(d => d.FkChild)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_HIERARCHY_CHILD");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisHierarchyFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_HIERARCHYCREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisHierarchyFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_HIERARCHYEXPIREDBY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.AnalysisHierarchyFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_HIERARCHY_PARENT");
        });

        modelBuilder.Entity<AnalysisHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("ANALYSIS_HTML");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.OjtNotes).HasColumnName("OJT_NOTES");
            entity.Property(e => e.ProcedureStatement).HasColumnName("PROCEDURE_STATEMENT");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisHtmls)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("F_ANALYSIS_HTML");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_HTML_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("ANALYSIS_IMPL");

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated }, "NDX_ANALYSIS_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.FkProject }, "NDX_ANALYSIS_IMPL10");

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_ANALYSIS_IMPL11");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_ANALYSIS_IMPL12");

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired }, "NDX_ANALYSIS_IMPL13").IsUnique();

            entity.HasIndex(e => new { e.TaskIsTrained, e.DateExpired, e.TaskIsRecurring, e.FkProject }, "NDX_ANALYSIS_IMPL2");

            entity.HasIndex(e => new { e.TaskIsTrained, e.DateExpired, e.DateCreated, e.FkProject, e.TaskIsRecurring }, "NDX_ANALYSIS_IMPL3");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.FkProject }, "NDX_ANALYSIS_IMPL4");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_ANALYSIS_IMPL5");

            entity.HasIndex(e => new { e.FkAnalysisLevel, e.DateExpired, e.FkProject }, "NDX_ANALYSIS_IMPL6");

            entity.HasIndex(e => new { e.FkAnalysisLevel, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_ANALYSIS_IMPL7");

            entity.HasIndex(e => new { e.FkTaskStatus, e.DateExpired, e.FkProject }, "NDX_ANALYSIS_IMPL8");

            entity.HasIndex(e => new { e.FkTaskStatus, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_ANALYSIS_IMPL9");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CanAppearOnQualCard)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_APPEAR_ON_QUAL_CARD");
            entity.Property(e => e.ContinuousUse)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CONTINUOUS_USE");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(75)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkAnalysisLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_LEVEL");
            entity.Property(e => e.FkAnalysisQual)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_QUAL");
            entity.Property(e => e.FkAnalysisRequal)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_REQUAL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.FkTaskChangeImpact)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_CHANGE_IMPACT");
            entity.Property(e => e.FkTaskDeselection)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_DESELECTION");
            entity.Property(e => e.FkTaskStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_STATUS");
            entity.Property(e => e.FkTaskTrainingTime)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_TRAINING_TIME");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.TaskDifficulty)
                .HasColumnType("numeric(3, 2)")
                .HasColumnName("TASK_DIFFICULTY");
            entity.Property(e => e.TaskFrequency)
                .HasColumnType("numeric(3, 2)")
                .HasColumnName("TASK_FREQUENCY");
            entity.Property(e => e.TaskImportance)
                .HasColumnType("numeric(3, 2)")
                .HasColumnName("TASK_IMPORTANCE");
            entity.Property(e => e.TaskIsCritical)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TASK_IS_CRITICAL");
            entity.Property(e => e.TaskIsRecurring)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TASK_IS_RECURRING");
            entity.Property(e => e.TaskIsTrained)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TASK_IS_TRAINED");
            entity.Property(e => e.TaskMustPerform)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TASK_MUST_PERFORM");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_IMPL");

            entity.HasOne(d => d.FkAnalysisLevelNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkAnalysisLevel)
                .HasConstraintName("F_ANALYSIS_IMPL_LEVELS");

            entity.HasOne(d => d.FkAnalysisQualNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkAnalysisQual)
                .HasConstraintName("F_ANALYSIS_IMPL_QUAL");

            entity.HasOne(d => d.FkAnalysisRequalNavigation).WithMany(p => p.AnalysisImplFkAnalysisRequalNavigations)
                .HasForeignKey(d => d.FkAnalysisRequal)
                .HasConstraintName("F_ANALYSIS_IMPL_REQUAL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_IMPL_PROJECT");

            entity.HasOne(d => d.FkTaskChangeImpactNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkTaskChangeImpact)
                .HasConstraintName("FK_ANALYSIS_TASK_CHANGE_IMPACT");

            entity.HasOne(d => d.FkTaskDeselectionNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkTaskDeselection)
                .HasConstraintName("F_ANALYSIS_IMPL_DESELECTION");

            entity.HasOne(d => d.FkTaskStatusNavigation).WithMany(p => p.AnalysisImpls)
                .HasForeignKey(d => d.FkTaskStatus)
                .HasConstraintName("F_ANALYSIS_IMPL_STATUS");

            entity.HasOne(d => d.FkTaskTrainingTimeNavigation).WithMany(p => p.AnalysisImplFkTaskTrainingTimeNavigations)
                .HasForeignKey(d => d.FkTaskTrainingTime)
                .HasConstraintName("F_ANALYSIS_IMPL_TASK_TIME");
        });

        modelBuilder.Entity<AnalysisLevel>(entity =>
        {
            entity.ToTable("ANALYSIS_LEVEL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<AnalysisLevelImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.Sequence, e.FkAnalysisLevel }).IsClustered(false);

            entity.ToTable("ANALYSIS_LEVEL_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkAnalysisLevel }, "NDX_ANALYSIS_LEVEL_IMPL1");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkAnalysisLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_LEVEL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkAnalysisLevelNavigation).WithMany(p => p.AnalysisLevelImpls)
                .HasForeignKey(d => d.FkAnalysisLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANAL_LEVEL_IMPL_FK_LEVEL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisLevelImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANAL_LEVEL_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisLevelImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANAL_LEVEL_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.AnalysisLevelImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_LEVEL_IMPL_PROJECT");
        });

        modelBuilder.Entity<AnalysisOjtNote>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("ANALYSIS_OJT_NOTES");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Notes).HasColumnName("NOTES");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisOjtNotes)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_OJT_NOTES");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisOjtNoteFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_OJT_NOTE_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisOjtNoteFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_OJT_NOTE_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisProcedure>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("ANALYSIS_PROCEDURE");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisProcedures)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_PROCEDURE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisProcedureFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_PROCEDUR_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisProcedureFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_PROCEDUR_EXPIREDBY");
        });

        modelBuilder.Entity<AnalysisQual>(entity =>
        {
            entity.ToTable("ANALYSIS_QUAL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<AnalysisQualImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.Sequence, e.FkAnalysisQual }).IsClustered(false);

            entity.ToTable("ANALYSIS_QUAL_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkAnalysisQual }, "NDX_ANALYSIS_QUAL_IMPL1");

            entity.HasIndex(e => e.FkAnalysisQual, "NDX_ANALYSIS_QUAL_IMPL2");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkAnalysisQual)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_QUAL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkAnalysisQualNavigation).WithMany(p => p.AnalysisQualImpls)
                .HasForeignKey(d => d.FkAnalysisQual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_QUAL_IMP_ANA_QUAL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisQualImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_QUAL_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisQualImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_QUAL_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.AnalysisQualImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_QUAL_IMPL_PROJECT");
        });

        modelBuilder.Entity<AnalysisQuestion>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated, e.FkAnalysis }).IsClustered(false);

            entity.ToTable("ANALYSIS_QUESTION");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.FkAnalysis }, "NDX_QUESTION_ANALYSIS1");

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.Sequence, e.FkQuestion }, "NDX_QUESTION_ANALYSIS2").IsClustered();

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.FkAnalysis }, "NDX_QUESTION_ANALYSIS3");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.AnalysisQuestions)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_QUESTION");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.AnalysisQuestionFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANALYSIS_QUESTION_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.AnalysisQuestionFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_ANALYSIS_QUESTION_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.AnalysisQuestions)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_ANALYSIS_QUESTION_PROJECT");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.AnalysisQuestions)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_ANALYSIS");
        });

        modelBuilder.Entity<AppVersion>(entity =>
        {
            entity.HasKey(e => e.Application).IsClustered(false);

            entity.ToTable("APP_VERSION");

            entity.Property(e => e.Application)
                .HasMaxLength(5)
                .HasColumnName("APPLICATION");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.IsInstalled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_INSTALLED");
            entity.Property(e => e.LicenseKey)
                .HasMaxLength(1024)
                .HasColumnName("LICENSE_KEY");
            entity.Property(e => e.PwdEncryption)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PWD_ENCRYPTION");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .HasColumnName("VERSION");
        });

        modelBuilder.Entity<Cdatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CDATA");

            entity.Property(e => e.App)
                .HasMaxLength(64)
                .HasColumnName("APP");
            entity.Property(e => e.Cfid)
                .HasMaxLength(64)
                .HasColumnName("CFID");
            entity.Property(e => e.Data).HasColumnName("DATA");
        });

        modelBuilder.Entity<Cglobal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CGLOBAL");

            entity.HasIndex(e => e.Cfid, "NDX_CGLOBAL1");

            entity.Property(e => e.Cfid)
                .HasMaxLength(64)
                .HasColumnName("CFID");
            entity.Property(e => e.Data).HasColumnName("DATA");
            entity.Property(e => e.Lvisit)
                .HasColumnType("datetime")
                .HasColumnName("LVISIT");
        });

        modelBuilder.Entity<Consolidation>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.Sequence, e.DateCreated, e.FkObjective }).IsClustered(false);

            entity.ToTable("CONSOLIDATION");

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated, e.FkObjective }, "NDX_CONSOLIDATION1");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.Sequence, e.FkAnalysis }, "NDX_CONSOLIDATION2").IsClustered();

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.FkObjective }, "NDX_CONSOLIDATION3");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.Consolidations)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_CONSOLIDATION_ANA");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ConsolidationFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONSOLIDATION_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ConsolidationFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_CONSOLIDATION_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.Consolidations)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_CONSOLIDATION_OBJ");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.Consolidations)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_CONSLIDATION_PROJECT");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.ToTable("CONTENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ContentImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("CONTENT_IMPL");

            entity.HasIndex(e => new { e.OwnerType, e.FkOwner, e.MimeType, e.FieldType, e.DateExpired, e.DateCreated }, "NDX_CONTENT_IMPL1").IsClustered();

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }, "NDX_CONTENT_IMPL2");

            entity.HasIndex(e => new { e.FkContent, e.DateExpired, e.DateCreated }, "NDX_CONTENT_IMPL3");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ByteSize)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BYTE_SIZE");
            entity.Property(e => e.ContentWebDisplay)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("CONTENT_WEB_DISPLAY");
            entity.Property(e => e.Crc)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("CRC");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DefaultOrder)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("DEFAULT_ORDER");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ExternalContentApp)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("EXTERNAL_CONTENT_APP");
            entity.Property(e => e.FieldType)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("FIELD_TYPE");
            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkExternalContentId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXTERNAL_CONTENT_ID");
            entity.Property(e => e.FkOwner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OWNER");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.IsApproved)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_APPROVED");
            entity.Property(e => e.IsDefault)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_DEFAULT");
            entity.Property(e => e.IsPublished)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_PUBLISHED");
            entity.Property(e => e.MimeType)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("MIME_TYPE");
            entity.Property(e => e.OwnerType)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("OWNER_TYPE");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.ContentImpls)
                .HasForeignKey(d => d.FkContent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTENT_IMPL_CONTENT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ContentImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTENT_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ContentImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_CONTENT_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ContentImpls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_CONTENT_IMPL_QUESTION");
        });

        modelBuilder.Entity<ContentStorage>(entity =>
        {
            entity.HasKey(e => new { e.FkContent, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("CONTENT_STORAGE");

            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Data)
                .IsRequired()
                .HasColumnName("DATA");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.VersionComments)
                .HasMaxLength(4000)
                .HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.ContentStorages)
                .HasForeignKey(d => d.FkContent)
                .HasConstraintName("FK_CONTENT_STORAGE_FK_CONTENT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ContentStorageFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTENT_STORAGE_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ContentStorageFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_CONTENT_STORAGE_EXPIREDBY");
        });

        modelBuilder.Entity<ContentStorageAicc>(entity =>
        {
            entity.HasKey(e => new { e.FkContent, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("CONTENT_STORAGE_AICC");

            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Data)
                .IsRequired()
                .HasColumnName("DATA");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.VersionComments)
                .HasMaxLength(4000)
                .HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.ContentStorageAiccs)
                .HasForeignKey(d => d.FkContent)
                .HasConstraintName("FK_CONTENT_STORAGE_A_CONTENT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ContentStorageAiccFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CONTENT_STORAGE_A_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ContentStorageAiccFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_CONTENT_STORAGE_A_EXPIREDBY");
        });

        modelBuilder.Entity<CoverSheet>(entity =>
        {
            entity.ToTable("COVER_SHEET");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<CoverSheetHtml>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkCoverSheet, e.DateCreated }).IsClustered(false);

            entity.ToTable("COVER_SHEET_HTML");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkCoverSheet)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COVER_SHEET");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCoverSheetNavigation).WithMany(p => p.CoverSheetHtmls)
                .HasForeignKey(d => d.FkCoverSheet)
                .HasConstraintName("F_COVER_SHEET_HTML");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.CoverSheetHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COVER_SHEET_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.CoverSheetHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_COVER_SHEET_HTML_EXPIREDBY");
        });

        modelBuilder.Entity<CoverSheetImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkCoverSheet, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("COVER_SHEET_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_COVER_SHEET_IMPL1");

            entity.Property(e => e.FkCoverSheet)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COVER_SHEET");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text).HasColumnName("TEXT");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCoverSheetNavigation).WithMany(p => p.CoverSheetImpls)
                .HasForeignKey(d => d.FkCoverSheet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_COVER_SHEET_IMPL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.CoverSheetImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_COVERSHEET_CREATE_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.CoverSheetImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_COVER_SHEET_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.CoverSheetImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_COVER_SHEET_IMPL_PROJECT");
        });

        modelBuilder.Entity<DbUpdateHistory>(entity =>
        {
            entity.HasKey(e => new { e.ScriptName, e.ScriptVersion }).IsClustered(false);

            entity.ToTable("DB_UPDATE_HISTORY");

            entity.Property(e => e.ScriptName)
                .HasMaxLength(50)
                .HasColumnName("SCRIPT_NAME");
            entity.Property(e => e.ScriptVersion)
                .HasMaxLength(50)
                .HasColumnName("SCRIPT_VERSION");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_UPDATED");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.ToTable("DEVELOPER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<DeveloperAuthorization>(entity =>
        {
            entity.HasKey(e => new { e.FkDeveloper, e.FkProject, e.FkSecurityGroup, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("DEVELOPER_AUTHORIZATION");

            entity.HasIndex(e => new { e.FkDeveloper, e.DateExpired, e.DateCreated }, "NDX_DEVELOPER_AUTHORIZATION1");

            entity.HasIndex(e => new { e.FkDeveloper, e.FkProject, e.DateExpired }, "NDX_DEVELOPER_AUTHORIZATION2").IsUnique();

            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.FkSecurityGroup)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SECURITY_GROUP");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DeveloperAuthorizationFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVELOPER_AUTH_CREATEDBY");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.DeveloperAuthorizationFkDeveloperNavigations)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_AUTHORIZATION_ID");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DeveloperAuthorizationFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DEVELOPER_AUTH_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.DeveloperAuthorizations)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_AUTHORIZATION_PROJECT");

            entity.HasOne(d => d.FkSecurityGroupNavigation).WithMany(p => p.DeveloperAuthorizations)
                .HasForeignKey(d => d.FkSecurityGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_AUTHORIZATION_SGROUP");
        });

        modelBuilder.Entity<DeveloperImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkDeveloper, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("DEVELOPER_IMPL");

            entity.HasIndex(e => new { e.LoginName, e.DateExpired, e.DateCreated }, "NDX_DEVELOPER_IMPL1");

            entity.HasIndex(e => new { e.LastName, e.FirstName, e.DateExpired, e.DateCreated }, "NDX_DEVELOPER_IMPL2");

            entity.HasIndex(e => new { e.Archived, e.DateExpired, e.DateCreated }, "NDX_DEVELOPER_IMPL3").IsClustered();

            entity.HasIndex(e => new { e.Enabled, e.DateExpired, e.DateCreated }, "NDX_DEVELOPER_IMPL4");

            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Archived)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVED");
            entity.Property(e => e.CanAlterSharedTables)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_ALTER_SHARED_TABLES");
            entity.Property(e => e.CanChangeAlternates)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_ALTERNATES");
            entity.Property(e => e.CanChangeTaskQuals)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_TASK_QUALS");
            entity.Property(e => e.CanEditLists)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_EDIT_LISTS");
            entity.Property(e => e.CanRunContent)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_RUN_CONTENT");
            entity.Property(e => e.CanRunImportExport)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_RUN_IMPORT_EXPORT");
            entity.Property(e => e.CanRunSecurity)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_RUN_SECURITY");
            entity.Property(e => e.CanRunTableUtility)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_RUN_TABLE_UTILITY");
            entity.Property(e => e.CanRunVision)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_RUN_VISION");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("DEPARTMENT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENABLED");
            entity.Property(e => e.Fax)
                .HasMaxLength(30)
                .HasColumnName("FAX");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.IdpId)
                .HasMaxLength(50)
                .HasColumnName("IDP_ID");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_ADMIN");
            entity.Property(e => e.IsLicensed)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_LICENSED");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.LoginName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LOGIN_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(25)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(35)
                .HasColumnName("PHONE");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DeveloperImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVELOPER_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.DeveloperImplFkDeveloperNavigations)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVELOPER_IMPL_FK_DEVELOPER");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DeveloperImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DEVELOPER_IMPL_EXPIREDBY");
        });

        modelBuilder.Entity<DirectObjective>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.DateExpired, e.Sequence, e.FkObjective, e.DateCreated }).IsClustered(false);

            entity.ToTable("DIRECT_OBJECTIVE");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.DateCreated, e.FkAnalysis }, "NDX_DIRECT_OBJECTIVE1").IsClustered();

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated, e.Sequence, e.FkObjective }, "NDX_DIRECT_OBJECTIVE2");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.FkAnalysis }, "NDX_DIRECT_OBJECTIVE3");

            entity.HasIndex(e => new { e.FkAnalysis, e.FkObjective, e.DateExpired }, "NDX_DIRECT_OBJECTIVE4").IsUnique();

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.DirectObjectives)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DIRECT_OBJECTIVE_ANA");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DirectObjectiveFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIRECT_OBJECTIVE_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DirectObjectiveFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DIRECT_OBJECTIVE_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.DirectObjectives)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DIRECT_OBJECTIVE_OBJ");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.DirectObjectives)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DIRECT_OBJECTIVE_PROJECT");
        });

        modelBuilder.Entity<Doclink>(entity =>
        {
            entity.ToTable("DOCLINK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<DoclinkHistory>(entity =>
        {
            entity.HasKey(e => new { e.FkDoclink, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("DOCLINK_HISTORY");

            entity.HasIndex(e => new { e.FkDoclink, e.DateExpired, e.Sequence }, "NDX_DOCLINK_HISTORY1");

            entity.Property(e => e.FkDoclink)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DOCLINK");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.HistoryAction)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("HISTORY_ACTION");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("NOTES");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DoclinkHistoryFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DOCLINK_DEVELOPER");

            entity.HasOne(d => d.FkDoclinkNavigation).WithMany(p => p.DoclinkHistories)
                .HasForeignKey(d => d.FkDoclink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DOCLINK_HISTORY_ID");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DoclinkHistoryFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DOCLINK_HISTORY_EXPIREDBY");
        });

        modelBuilder.Entity<DoclinkImpl>(entity =>
        {
            entity.HasKey(e => new { e.LinkedToType, e.FkLinkedTo, e.DateExpired, e.Sequence, e.FkDoclink, e.DateCreated }).IsClustered(false);

            entity.ToTable("DOCLINK_IMPL");

            entity.HasIndex(e => new { e.LinkedToType, e.FkLinkedTo, e.DateExpired, e.DateCreated, e.Sequence }, "NDX_DOCLINK_IMPL1");

            entity.HasIndex(e => e.FkDoclink, "NDX_DOCLINK_IMPL2");

            entity.HasIndex(e => new { e.FkDoclink, e.DateExpired }, "NDX_DOCLINK_IMPL3").IsUnique();

            entity.Property(e => e.LinkedToType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("LINKED_TO_TYPE");
            entity.Property(e => e.FkLinkedTo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LINKED_TO");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkDoclink)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DOCLINK");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateApproved)
                .HasColumnType("datetime")
                .HasColumnName("DATE_APPROVED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("NOTES");
            entity.Property(e => e.Params)
                .HasMaxLength(300)
                .HasColumnName("PARAMS");
            entity.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("PATH");
            entity.Property(e => e.Scope)
                .HasMaxLength(100)
                .HasColumnName("SCOPE");
            entity.Property(e => e.ShowInLs)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SHOW_IN_LS");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");
            entity.Property(e => e.Tracked)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TRACKED");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DoclinkImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DOCLINK_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkDoclinkNavigation).WithMany(p => p.DoclinkImpls)
                .HasForeignKey(d => d.FkDoclink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCLINK_IMPL_FK_DOCLINK");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DoclinkImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DOCLINK_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.DoclinkImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DOCLINK_IMPL_PROJECT");
        });

        modelBuilder.Entity<DocumentScript>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("DOCUMENT_SCRIPT");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FileName)
                .HasMaxLength(1700)
                .HasColumnName("FILE_NAME");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.DocumentScriptFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCUMENT_SCRIPT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.DocumentScriptFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_DOCUMENT_SCRIPT_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.DocumentScripts)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_DOC_SCRIPT_PROJECT");
        });

        modelBuilder.Entity<EmailCategory>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("EMAIL_CATEGORY");

            entity.HasIndex(e => e.EmailCategory1, "NDX_EMAIL_CATEGORY1");

            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.EmailCategory1)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("EMAIL_CATEGORY");
        });

        modelBuilder.Entity<EmailName>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("EMAIL_NAME");

            entity.HasIndex(e => e.EmailName1, "NDX_EMAIL_NAME1");

            entity.HasIndex(e => new { e.EmailName1, e.FkEmailCategory }, "NDX_EMAIL_NAME2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.EmailName1)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("EMAIL_NAME");
            entity.Property(e => e.Enabled)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENABLED");
            entity.Property(e => e.FkEmailCategory)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EMAIL_CATEGORY");

            entity.HasOne(d => d.FkEmailCategoryNavigation).WithMany(p => p.EmailNames)
                .HasForeignKey(d => d.FkEmailCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMAIL_NAME_EMAIL_CATEGORY");
        });

        modelBuilder.Entity<Eval>(entity =>
        {
            entity.ToTable("EVAL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<EvalEvent>(entity =>
        {
            entity.HasKey(e => e.Evalid);

            entity.ToTable("EVAL_EVENT");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkEvent, e.FkProgram, e.FkLearner }, "I_EVAL_EVENT1");

            entity.Property(e => e.Evalid)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("EVALID");
            entity.Property(e => e.CompDate)
                .HasColumnType("datetime")
                .HasColumnName("COMP_DATE");
            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.FkEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVENT");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.EvalEvents)
                .HasForeignKey(d => d.FkEval)
                .HasConstraintName("FK_EVAL_EVENT_FK_EVAL");

            entity.HasOne(d => d.FkEventNavigation).WithMany(p => p.EvalEvents)
                .HasForeignKey(d => d.FkEvent)
                .HasConstraintName("FK_EVAL_EVENT_FK_EVENT");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.EvalEvents)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_EVAL_EVENT_FK_LEARNER");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.EvalEvents)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_EVAL_EVENT_FK_PROGRAM");
        });

        modelBuilder.Entity<EvalItem>(entity =>
        {
            entity.HasKey(e => e.StemId);

            entity.ToTable("EVAL_ITEMS");

            entity.Property(e => e.StemId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("STEM_ID");
            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.ResponseType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("RESPONSE_TYPE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Stem)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("STEM");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.EvalItems)
                .HasForeignKey(d => d.FkEval)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_ITEMS_FK_EVAL");
        });

        modelBuilder.Entity<EvalRatingItem>(entity =>
        {
            entity.HasKey(e => e.Itemid);

            entity.ToTable("EVAL_RATING_ITEMS");

            entity.Property(e => e.Itemid)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ITEMID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Fail)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("FAIL");
            entity.Property(e => e.FkEvalTypes)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL_TYPES");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.PointValue)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINT_VALUE");

            entity.HasOne(d => d.FkEvalTypesNavigation).WithMany(p => p.EvalRatingItems)
                .HasForeignKey(d => d.FkEvalTypes)
                .HasConstraintName("FK_EVAL_RATING_ITEMS_EVAL_TYPE");
        });

        modelBuilder.Entity<EvalResponse>(entity =>
        {
            entity.HasKey(e => new { e.FkEval, e.FkItem, e.FkLearner, e.FkEvalEvent, e.FkLearningEvent });

            entity.ToTable("EVAL_RESPONSE");

            entity.HasIndex(e => e.FkEval, "IX_EVAL_RESPONSE").HasFillFactor(90);

            entity.HasIndex(e => e.FkLearner, "IX_EVAL_RESPONSE_1").HasFillFactor(90);

            entity.HasIndex(e => e.FkProgram, "IX_EVAL_RESPONSE_2").HasFillFactor(90);

            entity.HasIndex(e => e.FkEvalEvent, "IX_EVAL_RESPONSE_3").HasFillFactor(90);

            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.FkItem)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ITEM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkEvalEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL_EVENT");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.Feedback)
                .HasMaxLength(500)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Response)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("RESPONSE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkEval)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_RESPONSE_FK_EVAL");

            entity.HasOne(d => d.FkEvalEventNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkEvalEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_RESPONSE_FK_EVAL_EVENT");

            entity.HasOne(d => d.FkItemNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_RESPONSE_FK_EVAL_ITEMS");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_RESPONSE_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVAL_RESPONSE_LEARNING_EVET");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.EvalResponses)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_EVAL_RESPONSE_FK_PROGRAM");
        });

        modelBuilder.Entity<EvalType>(entity =>
        {
            entity.ToTable("EVAL_TYPES");

            entity.Property(e => e.EvalTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("EVAL_TYPE_ID");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkRatingStyleType)
                .HasDefaultValue(3m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RATING_STYLE_TYPE");
            entity.Property(e => e.FkRatingUseType)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RATING_USE_TYPE");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ItemLabel)
                .HasMaxLength(50)
                .HasColumnName("ITEM_LABEL");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.ToTable("EXAM");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ExamChoiceOrder>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated })
                .HasName("PK_EXAM_DISTRACTOR_ORDER")
                .IsClustered(false);

            entity.ToTable("EXAM_CHOICE_ORDER");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }, "NDX_EXAM_DISTRACTOR_ORDER1");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestion, e.Sequence, e.DateExpired }, "NDX_EXAM_DISTRACTOR_ORDER2").IsUnique();

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.QuestionPosition)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_POSITION");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamChoiceOrderFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_DISTOR_ORDER_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamChoiceOrders)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_DISTRACTOR_EXAM_ID");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamChoiceOrderFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_DISTOR_ORDER_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamChoiceOrders)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_DISTRACTOR_QUESTION_ID");
        });

        modelBuilder.Entity<ExamComment>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_COMMENTS");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamCommentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_COMMENTS_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamComments)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_COMMENTS_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamCommentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_COMMENTS_EXPIREDBY");
        });

        modelBuilder.Entity<ExamEvent>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkLearner }).IsClustered(false);

            entity.ToTable("EXAM_EVENT");

            entity.HasIndex(e => new { e.FkExam, e.FkProctor }, "NDX_EXAM_EVENT1");

            entity.HasIndex(e => new { e.FkProctor, e.FkExam }, "NDX_EXAM_EVENT2");

            entity.HasIndex(e => new { e.FkLearner, e.FkExam }, "NDX_EXAM_EVENT3");

            entity.HasIndex(e => new { e.DateCompleted, e.FkExam }, "NDX_EXAM_EVENT4").IsClustered();

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateStarted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_STARTED");
            entity.Property(e => e.FkProctor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROCTOR");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamEvents)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_EVENT_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamEventFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_EVENT_LEARNER1");

            entity.HasOne(d => d.FkProctorNavigation).WithMany(p => p.ExamEventFkProctorNavigations)
                .HasForeignKey(d => d.FkProctor)
                .HasConstraintName("FK_EXAM_EVENT_LEARNER");
        });

        modelBuilder.Entity<ExamFilter>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.DateCreated, e.FilterType }).IsClustered(false);

            entity.ToTable("EXAM_FILTER");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FilterType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("FILTER_TYPE");
            entity.Property(e => e.Filter)
                .HasMaxLength(1800)
                .HasColumnName("FILTER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamFilterFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_FILTER_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamFilters)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_FILTER_FK_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamFilterFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_FILTER_EXPIREDBY");
        });

        modelBuilder.Entity<ExamImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("EXAM_IMPL");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL2");

            entity.HasIndex(e => new { e.FkProject, e.Archived, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL3");

            entity.HasIndex(e => new { e.FkProject, e.Archived, e.IsVdmExam, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL4");

            entity.HasIndex(e => new { e.FkProject, e.IsVdmExam, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL5");

            entity.HasIndex(e => new { e.FkCreatedBy, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL6");

            entity.HasIndex(e => new { e.DateExpired, e.DateCreated, e.FkCreatedBy }, "NDX_EXAM_IMPL7");

            entity.HasIndex(e => new { e.FkProject, e.Text, e.DateExpired, e.DateCreated }, "NDX_EXAM_IMPL8");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archived)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVED");
            entity.Property(e => e.CanEditWithPword)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_EDIT_WITH_PWORD");
            entity.Property(e => e.CanEditWithoutPword)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_EDIT_WITHOUT_PWORD");
            entity.Property(e => e.CanViewWithPword)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_VIEW_WITH_PWORD");
            entity.Property(e => e.CanViewWithoutPword)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_VIEW_WITHOUT_PWORD");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Disqualified)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DISQUALIFIED");
            entity.Property(e => e.ExamUnitType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXAM_UNIT_TYPE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkExamStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_STATUS");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.IsBuilt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_BUILT");
            entity.Property(e => e.IsVdmExam)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_VDM_EXAM");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.ProfileApplied)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PROFILE_APPLIED");
            entity.Property(e => e.ProfileType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PROFILE_TYPE");
            entity.Property(e => e.ProfileValue)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("PROFILE_VALUE");
            entity.Property(e => e.Salt)
                .HasMaxLength(50)
                .HasColumnName("SALT");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamImpls)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_IMPL");

            entity.HasOne(d => d.FkExamStatusNavigation).WithMany(p => p.ExamImpls)
                .HasForeignKey(d => d.FkExamStatus)
                .HasConstraintName("FK_EXAM_IMPL_FK_EXAM_STATUS");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ExamImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_PROJECT");
        });

        modelBuilder.Entity<ExamLearnerFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("EXAM_LEARNER_FEEDBACK");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.HasIndex(e => e.FkQuestion, "NDX_EXAM_LEARNER_FEEDBACK").IsClustered();

            entity.HasIndex(e => new { e.FkResolvedByD, e.Archive, e.DateCreated }, "NDX_EXAM_LEARNER_FEEDBACK2");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestion }, "NDX_EXAM_LEARNER_FEEDBACK3");

            entity.HasIndex(e => e.FkLearner, "NDX_EXAM_LEARNER_FEEDBACK4");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateResolved)
                .HasColumnType("datetime")
                .HasColumnName("DATE_RESOLVED");
            entity.Property(e => e.Feedback)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.FkAssignedTo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ASSIGNED_TO");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkProctor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROCTOR");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkResolvedByD)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RESOLVED_BY_D");
            entity.Property(e => e.FkResolvedByL)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RESOLVED_BY_L");
            entity.Property(e => e.Resolution)
                .HasMaxLength(1000)
                .HasColumnName("RESOLUTION");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Viewed)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("VIEWED");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamLearnerFeedbacks)
                .HasForeignKey(d => d.FkExam)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBACK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamLearnerFeedbackFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBK_LEARN");

            entity.HasOne(d => d.FkProctorNavigation).WithMany(p => p.ExamLearnerFeedbackFkProctorNavigations)
                .HasForeignKey(d => d.FkProctor)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBK_PROCTOR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamLearnerFeedbacks)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBACK_QUEST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamLearnerFeedbacks)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBK_QQ_IMPL");

            entity.HasOne(d => d.FkResolvedByDNavigation).WithMany(p => p.ExamLearnerFeedbacks)
                .HasForeignKey(d => d.FkResolvedByD)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBACK_DEV");

            entity.HasOne(d => d.FkResolvedByLNavigation).WithMany(p => p.ExamLearnerFeedbackFkResolvedByLNavigations)
                .HasForeignKey(d => d.FkResolvedByL)
                .HasConstraintName("FK_EXAM_LEARNER_FEEDBK_RESO_L");
        });

        modelBuilder.Entity<ExamOnlineProfile>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("EXAM_ONLINE_PROFILE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AllMustAppear)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ALL_MUST_APPEAR");
            entity.Property(e => e.Alt1EmailText)
                .HasMaxLength(4000)
                .HasColumnName("ALT1_EMAIL_TEXT");
            entity.Property(e => e.Alt2EmailText)
                .HasMaxLength(4000)
                .HasColumnName("ALT2_EMAIL_TEXT");
            entity.Property(e => e.Approved)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("APPROVED");
            entity.Property(e => e.Archived)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DigitalSignature)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DIGITAL_SIGNATURE");
            entity.Property(e => e.Email1)
                .HasMaxLength(50)
                .HasColumnName("EMAIL1");
            entity.Property(e => e.Email2)
                .HasMaxLength(50)
                .HasColumnName("EMAIL2");
            entity.Property(e => e.Email3)
                .HasMaxLength(50)
                .HasColumnName("EMAIL3");
            entity.Property(e => e.EmailResponse)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EMAIL_RESPONSE");
            entity.Property(e => e.FailedRemediation)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("FAILED_REMEDIATION");
            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.FkOrigExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORIG_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.LearnerEmailText)
                .HasMaxLength(4000)
                .HasColumnName("LEARNER_EMAIL_TEXT");
            entity.Property(e => e.PassedRemediation)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PASSED_REMEDIATION");
            entity.Property(e => e.PassingScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("PASSING_SCORE");
            entity.Property(e => e.Proctored)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PROCTORED");
            entity.Property(e => e.QuestionSampleMin)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("QUESTION_SAMPLE_MIN");
            entity.Property(e => e.QuestionSampleType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_SAMPLE_TYPE");
            entity.Property(e => e.QuestionSampleValue)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("QUESTION_SAMPLE_VALUE");
            entity.Property(e => e.Remediate)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REMEDIATE");
            entity.Property(e => e.Scoring)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SCORING");
            entity.Property(e => e.SendmailAdmin)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_ADMIN");
            entity.Property(e => e.SendmailEmail1)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_EMAIL1");
            entity.Property(e => e.SendmailEmail2)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_EMAIL2");
            entity.Property(e => e.SendmailEmail3)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_EMAIL3");
            entity.Property(e => e.SendmailInstructor)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_INSTRUCTOR");
            entity.Property(e => e.SendmailLearner)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_LEARNER");
            entity.Property(e => e.SendmailSiteadmin)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_SITEADMIN");
            entity.Property(e => e.SendmailSupervisor)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SENDMAIL_SUPERVISOR");
            entity.Property(e => e.SupervisorEmailText)
                .HasMaxLength(4000)
                .HasColumnName("SUPERVISOR_EMAIL_TEXT");
            entity.Property(e => e.TestLock)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TEST_LOCK");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.ExamOnlineProfiles)
                .HasForeignKey(d => d.FkEval)
                .HasConstraintName("FK_EXAM_ONLINE_PROFILE_FK_EVAL");

            entity.HasOne(d => d.FkOrigExamOnlineProfileNavigation).WithMany(p => p.InverseFkOrigExamOnlineProfileNavigation)
                .HasForeignKey(d => d.FkOrigExamOnlineProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_ONLINE_PROFILE_ORG");
        });

        modelBuilder.Entity<ExamOnlineProfileMessage>(entity =>
        {
            entity.HasKey(e => new { e.FkExamOnlineProfile, e.FkMessage, e.FkMessageCategory }).IsClustered(false);

            entity.ToTable("EXAM_ONLINE_PROFILE_MESSAGE");

            entity.Property(e => e.FkExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.FkMessage)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MESSAGE");
            entity.Property(e => e.FkMessageCategory)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MESSAGE_CATEGORY");

            entity.HasOne(d => d.FkExamOnlineProfileNavigation).WithMany(p => p.ExamOnlineProfileMessages)
                .HasForeignKey(d => d.FkExamOnlineProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXM_OLN_PRO_MSG_EXM_OLN_PROF");

            entity.HasOne(d => d.FkMessageNavigation).WithMany(p => p.ExamOnlineProfileMessages)
                .HasForeignKey(d => d.FkMessage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXM_OLN_PRO_MSG_FK_MESSAGE");

            entity.HasOne(d => d.FkMessageCategoryNavigation).WithMany(p => p.ExamOnlineProfileMessages)
                .HasForeignKey(d => d.FkMessageCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXM_OLN_PRO_MSG_FK_MESSG_PRO");
        });

        modelBuilder.Entity<ExamOnlineTesting>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkLearner, e.FkQuestion }).IsClustered(false);

            entity.ToTable("EXAM_ONLINE_TESTING");

            entity.HasIndex(e => e.FkLearner, "IX_FK_LEARNER").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkExam, e.FkLearner, e.FkQuestion, e.SelectionOrder }, "NDX_EXAM_ONLINE_TESTING");

            entity.HasIndex(e => new { e.FkLearner, e.FkExam, e.SelectionOrder, e.FkQuestion, e.Sequence }, "NDX_EXAM_ONLINE_TESTING_2");

            entity.HasIndex(e => e.FkLearner, "NDX_EXAM_ONLINE_TESTING_LEARNR");

            entity.HasIndex(e => new { e.FkLearner, e.FkExam, e.SelectionOrder }, "_dta_index_EXAM_ONLINE_TESTING_10_1079674894__K2_K1_K5_3_4_6_7_8_9");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateViewed)
                .HasColumnType("datetime")
                .HasColumnName("DATE_VIEWED");
            entity.Property(e => e.Flag)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("FLAG");
            entity.Property(e => e.IsSubq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQ");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.SelectionOrder)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SELECTION_ORDER");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamOnlineTestings)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_ONLINE_TESTING_FK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamOnlineTestings)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_ONLINE_TESTING_LEARNER");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamOnlineTestings)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_ONLINE_TESTING_QUESTIN");
        });

        modelBuilder.Entity<ExamOwner>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.FkDeveloper, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_OWNER");

            entity.HasIndex(e => new { e.FkDeveloper, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_OWNER1");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamOwnerFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_OWNER_CREATEDBY");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.ExamOwnerFkDeveloperNavigations)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_OWNER_FK_DEVELOPER");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamOwners)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_OWNER_FK_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamOwnerFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_OWNER_EXPIREDBY");
        });

        modelBuilder.Entity<ExamPrintOption>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_PRINT_OPTIONS");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FirstQuesNum)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("FIRST_QUES_NUM");
            entity.Property(e => e.FkAnskeyCoversheet)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANSKEY_COVERSHEET");
            entity.Property(e => e.FkAnssheetCoversheet)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANSSHEET_COVERSHEET");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkTestCoversheet)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TEST_COVERSHEET");
            entity.Property(e => e.IgnoreWhiteSpace)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IGNORE_WHITE_SPACE");
            entity.Property(e => e.ObjsOnAnskey)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJS_ON_ANSKEY");
            entity.Property(e => e.ObjsOnAnssheet)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJS_ON_ANSSHEET");
            entity.Property(e => e.ObjsOnTest)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJS_ON_TEST");
            entity.Property(e => e.OneQuesPerPage)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ONE_QUES_PER_PAGE");

            entity.HasOne(d => d.FkAnskeyCoversheetNavigation).WithMany(p => p.ExamPrintOptionFkAnskeyCoversheetNavigations)
                .HasForeignKey(d => d.FkAnskeyCoversheet)
                .HasConstraintName("F_PRINT_ANSKEY_COVERSHEET");

            entity.HasOne(d => d.FkAnssheetCoversheetNavigation).WithMany(p => p.ExamPrintOptionFkAnssheetCoversheetNavigations)
                .HasForeignKey(d => d.FkAnssheetCoversheet)
                .HasConstraintName("F_PRINT_ANSSHEET_COVERSHEET");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamPrintOptionFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_PRINT_OPTION_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamPrintOptions)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PRINT_OPTIONS_FK_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamPrintOptionFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_PRINT_OPTION_EXPIREDBY");

            entity.HasOne(d => d.FkTestCoversheetNavigation).WithMany(p => p.ExamPrintOptionFkTestCoversheetNavigations)
                .HasForeignKey(d => d.FkTestCoversheet)
                .HasConstraintName("F_PRINT_TEST_COVERSHEET");
        });

        modelBuilder.Entity<ExamProfileByObjective>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkExam, e.FkObjectiveLevel, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_PROFILE_BY_OBJECTIVE");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkObjectiveLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_LEVEL");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.PercentToTest)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PERCENT_TO_TEST");
            entity.Property(e => e.QuestionsToTest)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("QUESTIONS_TO_TEST");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamProfileByObjectiveFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_PROFIL_BY_OB_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamProfileByObjectives)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROFILE_OB_FK_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamProfileByObjectiveFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_PROFIL_BY_OB_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveLevelNavigation).WithMany(p => p.ExamProfileByObjectives)
                .HasForeignKey(d => d.FkObjectiveLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROFILE_OB_LEVEL");
        });

        modelBuilder.Entity<ExamQuestionEvent>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestionImpl, e.Sequence, e.FkLearner }, "NDX_EXAM_QUESTION_EVENT1");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestion, e.FkLearner }, "NDX_EXAM_QUESTION_EVENT2");

            entity.HasIndex(e => new { e.FkExam, e.FkLearner, e.FkQuestion, e.Sequence }, "NDX_EXAM_QUESTION_EVENT3");

            entity.HasIndex(e => new { e.FkQuestionImpl, e.ResponseType, e.Score, e.FkLearner, e.FkQuestion }, "NDX_EXAM_QUESTION_EVENT4");

            entity.HasIndex(e => new { e.FkLearner, e.FkExam, e.FkQuestionImpl, e.Sequence }, "NDX_EXAM_QUESTION_EVENT5");

            entity.HasIndex(e => new { e.FkQuestion, e.ResponseType, e.Score }, "NDX_EXAM_QUESTION_EVENT6").IsClustered();

            entity.HasIndex(e => new { e.FkExam, e.FkLearner, e.Sequence }, "NDX_EXAM_QUESTION_EVENT8");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.DateAdministered)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ADMINISTERED");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.InstructorComment)
                .HasMaxLength(2000)
                .HasColumnName("INSTRUCTOR_COMMENT");
            entity.Property(e => e.IsSubquestion)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQUESTION");
            entity.Property(e => e.Points)
                .HasDefaultValue(1.0m)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionType)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("QUESTION_TYPE");
            entity.Property(e => e.ResponseType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE_TYPE");
            entity.Property(e => e.ReviewedQuestion)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REVIEWED_QUESTION");
            entity.Property(e => e.Score)
                .HasDefaultValue(0.0m)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("SCORE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEvents)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEvents)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_LEARNER");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEvents)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_QUESTN");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEvents)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_IMPL_ID");
        });

        modelBuilder.Entity<ExamQuestionEventFi>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner, e.Sequence }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT_FI");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Response)
                .IsRequired()
                .HasColumnName("RESPONSE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEventFis)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_FI_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEventFis)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_FI_LRNR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEventFis)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_FI_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEventFis)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTIN_EVT_FI_QQ_IMPL");
        });

        modelBuilder.Entity<ExamQuestionEventMa>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner, e.ItemSequence }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT_MA");

            entity.HasIndex(e => new { e.FkQuestionImpl, e.ItemSequence, e.Points, e.Response1, e.Response2 }, "NDX_EXAM_QUESTION_EVENT_MA").IsClustered();

            entity.HasIndex(e => e.FkQuestion, "NDX_EXAM_QUESTION_EVENT_MA1");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.ItemSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ITEM_SEQUENCE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Points)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Response1)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE1");
            entity.Property(e => e.Response2)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE2");
            entity.Property(e => e.Response3)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE3");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEventMas)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MA_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEventMas)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MA_LRNR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEventMas)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MA_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEventMas)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MA_IMPL");
        });

        modelBuilder.Entity<ExamQuestionEventMc>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner, e.Sequence }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT_MC");

            entity.HasIndex(e => new { e.FkQuestionImpl, e.Sequence, e.QuestionPosition }, "NDX_EXAM_QUESTION_EVENT_MC").IsClustered();

            entity.HasIndex(e => e.FkQuestion, "NDX_EXAM_QUESTION_EVENT_MC1");

            entity.HasIndex(e => new { e.FkQuestion, e.Sequence, e.QuestionPosition }, "NDX_EXAM_QUESTION_EVENT_MC2");

            entity.HasIndex(e => new { e.FkExam, e.FkQuestion, e.FkLearner, e.Selected }, "NDX_EXAM_QUESTION_EVENT_MC3");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.QuestionPosition)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_POSITION");
            entity.Property(e => e.Selected)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SELECTED");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEventMcs)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MC_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEventMcs)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MC_LRNR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEventMcs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MC_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEventMcs)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_MC_IMPL");
        });

        modelBuilder.Entity<ExamQuestionEventSa>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT_SA");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Response)
                .IsRequired()
                .HasColumnName("RESPONSE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEventSas)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_SA_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEventSas)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_SA_LRNR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEventSas)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_SA_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEventSas)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTIN_EVT_SA_QQ_IMPL");
        });

        modelBuilder.Entity<ExamQuestionEventTf>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkQuestionImpl, e.FkLearner }).IsClustered(false);

            entity.ToTable("EXAM_QUESTION_EVENT_TF");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Response)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamQuestionEventTfs)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_TF_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.ExamQuestionEventTfs)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_TF_LRNR");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ExamQuestionEventTfs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTION_EVENT_TF_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.ExamQuestionEventTfs)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_QUESTIN_EVT_TF_QQ_IMPL");
        });

        modelBuilder.Entity<ExamStatus>(entity =>
        {
            entity.ToTable("EXAM_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ExamStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkExamStatus, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("EXAM_STATUS_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.DateCreated }, "NDX_EXAM_STATUS_IMPL1");

            entity.Property(e => e.FkExamStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_STATUS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_STATUS_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExamStatusNavigation).WithMany(p => p.ExamStatusImpls)
                .HasForeignKey(d => d.FkExamStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_STATUS_IMPL_EXAM_STATS");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_STATUS_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ExamStatusImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_STATUS_IMPL_FK_PROJECT");
        });

        modelBuilder.Entity<ExamUnitOb>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.FkUnit, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_UNIT_OB");

            entity.HasIndex(e => new { e.DateExpired, e.FkExam, e.FkParentUnit, e.Sequence, e.DateCreated, e.FkUnit }, "IX_EXAM_UNIT_OB");

            entity.HasIndex(e => new { e.DateExpired, e.FkExam, e.Sequence, e.DateCreated, e.FkUnit }, "IX_EXAM_UNIT_OB2");

            entity.HasIndex(e => new { e.DateExpired, e.FkUnit, e.DateCreated, e.FkExam }, "IX_EXAM_UNIT_OB3");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.DateCreated, e.Sequence }, "NDX_EXAM_UNIT_OB1");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.Sequence, e.FkUnit }, "NDX_EXAM_UNIT_OB2").IsClustered();

            entity.HasIndex(e => new { e.FkUnit, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_OB3");

            entity.HasIndex(e => new { e.FkExamUnit, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_OB4");

            entity.HasIndex(e => new { e.FkExam, e.FkUnit, e.DateExpired }, "NDX_EXAM_UNIT_OB5").IsUnique();

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExamUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_UNIT");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParentUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT_UNIT");
            entity.Property(e => e.FkUnitImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT_IMPL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamUnitObFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_UNIT_OB_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamUnitObs)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_OB_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamUnitObFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_UNIT_OB_EXPIREDBY");

            entity.HasOne(d => d.FkParentUnitNavigation).WithMany(p => p.ExamUnitObs)
                .HasForeignKey(d => d.FkParentUnit)
                .HasConstraintName("F_EXAM_UNIT_OB_PARENT");

            entity.HasOne(d => d.FkUnitNavigation).WithMany(p => p.ExamUnitObs)
                .HasForeignKey(d => d.FkUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_OB");

            entity.HasOne(d => d.FkUnitImplNavigation).WithMany(p => p.ExamUnitObs)
                .HasForeignKey(d => d.FkUnitImpl)
                .HasConstraintName("FK_EXAM_UNIT_OB_FK_UNIT_IMPL");
        });

        modelBuilder.Entity<ExamUnitPg>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.DateExpired, e.FkUnit, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("EXAM_UNIT_PG");

            entity.HasIndex(e => new { e.DateExpired, e.FkExam, e.Sequence, e.DateCreated, e.FkUnit }, "IX_EXAM_UNIT_PG");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.DateCreated, e.Sequence }, "NDX_EXAM_UNIT_PG1");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.Sequence, e.FkUnit }, "NDX_EXAM_UNIT_PG2");

            entity.HasIndex(e => new { e.FkUnit, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_PG3");

            entity.HasIndex(e => new { e.FkExamUnit, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_PG4");

            entity.HasIndex(e => new { e.FkExam, e.FkUnit, e.DateExpired }, "NDX_EXAM_UNIT_PG5").IsUnique();

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExamUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_UNIT");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParentUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT_UNIT");
            entity.Property(e => e.FkUnitImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT_IMPL");
            entity.Property(e => e.Weighting)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("WEIGHTING");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamUnitPgFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_UNIT_PG_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamUnitPgs)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_PG_EXAM");

            entity.HasOne(d => d.FkExamUnitNavigation).WithMany(p => p.ExamUnitPgFkExamUnitNavigations)
                .HasForeignKey(d => d.FkExamUnit)
                .HasConstraintName("F_EXAM_UNIT_UNIT");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamUnitPgFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_UNIT_PG_EXPIREDBY");

            entity.HasOne(d => d.FkParentUnitNavigation).WithMany(p => p.ExamUnitPgFkParentUnitNavigations)
                .HasForeignKey(d => d.FkParentUnit)
                .HasConstraintName("F_EXAM_UNIT_PG_PARENT");

            entity.HasOne(d => d.FkUnitNavigation).WithMany(p => p.ExamUnitPgFkUnitNavigations)
                .HasForeignKey(d => d.FkUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_PG");

            entity.HasOne(d => d.FkUnitImplNavigation).WithMany(p => p.ExamUnitPgs)
                .HasForeignKey(d => d.FkUnitImpl)
                .HasConstraintName("FK_EXAM_UNIT_PG_FK_UNIT_IMPL");
        });

        modelBuilder.Entity<ExamUnitQq>(entity =>
        {
            entity.HasKey(e => new { e.FkUnit, e.DateExpired, e.Sequence, e.DateCreated, e.FkExam }).IsClustered(false);

            entity.ToTable("EXAM_UNIT_QQ");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.IsSubq, e.DateCreated }, "NDX_EXAM_UNIT_QQ1");

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkParentUnit, e.IsSubq, e.Sequence }, "NDX_EXAM_UNIT_QQ2").IsClustered();

            entity.HasIndex(e => new { e.FkExamUnit, e.DateExpired, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_QQ3");

            entity.HasIndex(e => new { e.FkUnit, e.DateExpired, e.IsSelected, e.DateCreated, e.FkExam }, "NDX_EXAM_UNIT_QQ4");

            entity.HasIndex(e => new { e.FkExamUnit, e.DateExpired, e.DateCreated, e.IsSelected, e.FkExam }, "NDX_EXAM_UNIT_QQ5");

            entity.HasIndex(e => new { e.FkExam, e.FkUnit, e.DateExpired }, "NDX_EXAM_UNIT_QQ6").IsUnique();

            entity.Property(e => e.FkUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExamUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_UNIT");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParentUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT_UNIT");
            entity.Property(e => e.FkUnitImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_UNIT_IMPL");
            entity.Property(e => e.IsSelected)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SELECTED");
            entity.Property(e => e.IsSubq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQ");
            entity.Property(e => e.SelectionOrder)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SELECTION_ORDER");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ExamUnitQqFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_UNIT_QQ_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ExamUnitQqs)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_QQ_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ExamUnitQqFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_EXAM_UNIT_QQ_EXPIREDBY");

            entity.HasOne(d => d.FkUnitNavigation).WithMany(p => p.ExamUnitQqs)
                .HasForeignKey(d => d.FkUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_EXAM_UNIT_QQ");

            entity.HasOne(d => d.FkUnitImplNavigation).WithMany(p => p.ExamUnitQqs)
                .HasForeignKey(d => d.FkUnitImpl)
                .HasConstraintName("FK_EXAM_UNIT_QQ_FK_UNIT_IMPL");
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.HasKey(e => new { e.DataType, e.FkData }).IsClustered(false);

            entity.ToTable("IMPORT");

            entity.HasIndex(e => new { e.DataType, e.ImportId, e.FkDataProject, e.FkDataParent }, "NDX_IMPORT_ID");

            entity.Property(e => e.DataType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("DATA_TYPE");
            entity.Property(e => e.FkData)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA");
            entity.Property(e => e.FkDataParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA_PARENT");
            entity.Property(e => e.FkDataProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA_PROJECT");
            entity.Property(e => e.ImportId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("IMPORT_ID");
            entity.Property(e => e.RevisionDate)
                .HasColumnType("datetime")
                .HasColumnName("REVISION_DATE");

            entity.HasOne(d => d.FkDataProjectNavigation).WithMany(p => p.Imports)
                .HasForeignKey(d => d.FkDataProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMPORT_PROJECT");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.ToTable("LABEL");

            entity.HasIndex(e => e.FkModifiedBy, "NDX_LABEL_MODIFIED_BY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkModifiedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MODIFIED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkModifiedByNavigation).WithMany(p => p.Labels)
                .HasForeignKey(d => d.FkModifiedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_LABEL_MODIFIED_BY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.Labels)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_LABEL_PROJECT");
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LEARNER");

            entity.HasIndex(e => new { e.Id, e.FkCompany, e.FirstName, e.LastName }, "NDX_LEARNER_2");

            entity.HasIndex(e => new { e.Id, e.FirstName, e.LastName }, "NDX_LEARNER_3").IsClustered();

            entity.HasIndex(e => new { e.Id, e.FkCompany, e.UserName, e.FirstName, e.LastName }, "NDX_LEARNER_4");

            entity.HasIndex(e => new { e.Id, e.FkCompany }, "NDX_LEARNER_5");

            entity.HasIndex(e => new { e.Id, e.UserName, e.FirstName, e.LastName }, "NDX_LEARNER_6");

            entity.HasIndex(e => e.UserName, "NDX_LEARNER_LOGINNAME").IsUnique();

            entity.HasIndex(e => e.Id, "_dta_index_LEARNER_10_333244242__K1_2_5_6");

            entity.HasIndex(e => new { e.Id, e.FkCompany }, "_dta_index_LEARNER_10_333244242__K1_K13");

            entity.HasIndex(e => new { e.Id, e.FkCompany }, "_dta_index_LEARNER_10_333244242__K1_K13_5_6");

            entity.HasIndex(e => e.Id, "_dta_index_LEARNER_31_333244242__K1_5_6");

            entity.HasIndex(e => new { e.Id, e.FkCompany }, "_dta_index_LEARNER_31_333244242__K1_K13_2_5_6_7");

            entity.HasIndex(e => new { e.Id, e.FkCompany }, "_dta_index_LEARNER_31_333244242__K1_K13_5_6");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CanBeProctor)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_BE_PROCTOR");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateLearnerExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_LEARNER_EXPIRED");
            entity.Property(e => e.DatePwordChanged)
                .HasColumnType("datetime")
                .HasColumnName("DATE_PWORD_CHANGED");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.ExpireNotify)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXPIRE_NOTIFY");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.FkAddress)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ADDRESS");
            entity.Property(e => e.FkCompany)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkSecurityCode)
                .HasDefaultValue(4m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SECURITY_CODE");
            entity.Property(e => e.IdpId)
                .HasMaxLength(50)
                .HasColumnName("IDP_ID");
            entity.Property(e => e.IsEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_EVALUATOR");
            entity.Property(e => e.IsTrainer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRAINER");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(30)
                .HasColumnName("MIDDLE_NAME");
            entity.Property(e => e.MustBeProctored)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MUST_BE_PROCTORED");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Suffix)
                .HasMaxLength(10)
                .HasColumnName("SUFFIX");
            entity.Property(e => e.Supervisor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("SUPERVISOR");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(20)
                .HasColumnName("TELEPHONE_NUMBER");
            entity.Property(e => e.TempPassword)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TEMP_PASSWORD");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.Userdefined1)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINED1");
            entity.Property(e => e.Userdefined2)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINED2");
            entity.Property(e => e.Userdefined3)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINED3");

            entity.HasOne(d => d.FkAddressNavigation).WithMany(p => p.Learners)
                .HasForeignKey(d => d.FkAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNER_FK_ADDRESS");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.Learners)
                .HasForeignKey(d => d.FkCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNER_FK_COMPANY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.InverseFkCreatedByNavigation)
                .HasForeignKey(d => d.FkCreatedBy)
                .HasConstraintName("FK_LEARNER_FK_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.InverseFkExpiredByNavigation)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_LEARNER_FK_EXPIRED_BY");

            entity.HasOne(d => d.FkSecurityCodeNavigation).WithMany(p => p.Learners)
                .HasForeignKey(d => d.FkSecurityCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNER_FK_SECURITY_CODE");

            entity.HasOne(d => d.SupervisorNavigation).WithMany(p => p.InverseSupervisorNavigation)
                .HasForeignKey(d => d.Supervisor)
                .HasConstraintName("FK_LEARNER_FK_LEARNER_SUPV");

            entity.HasMany(d => d.FkCompanies).WithMany(p => p.FkLearners)
                .UsingEntity<Dictionary<string, object>>(
                    "LsLearnerCompany",
                    r => r.HasOne<LsCompany>().WithMany()
                        .HasForeignKey("FkCompany")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LEARNER_COMPANY_FK_COMPANY"),
                    l => l.HasOne<Learner>().WithMany()
                        .HasForeignKey("FkLearner")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LEARNER_COMPANY_FK_LEARNER"),
                    j =>
                    {
                        j.HasKey("FkLearner", "FkCompany").HasName("PK_LEARNER_COMPANY");
                        j.ToTable("LS_LEARNER_COMPANY");
                        j.HasIndex(new[] { "FkLearner" }, "IX_FK_LEARNER").HasFillFactor(100);
                        j.IndexerProperty<decimal>("FkLearner")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_LEARNER");
                        j.IndexerProperty<decimal>("FkCompany")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_COMPANY");
                    });

            entity.HasMany(d => d.FkLearningEvents).WithMany(p => p.FkLearners)
                .UsingEntity<Dictionary<string, object>>(
                    "LsEventInstructor",
                    r => r.HasOne<LsLearningEvent>().WithMany()
                        .HasForeignKey("FkLearningEvent")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EVENT_INSTRUCTORS_FK_EVENT"),
                    l => l.HasOne<Learner>().WithMany()
                        .HasForeignKey("FkLearner")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EVENT_INSTRUCTORS_FK_LEARN"),
                    j =>
                    {
                        j.HasKey("FkLearner", "FkLearningEvent").IsClustered(false);
                        j.ToTable("LS_EVENT_INSTRUCTORS");
                        j.IndexerProperty<decimal>("FkLearner")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_LEARNER");
                        j.IndexerProperty<decimal>("FkLearningEvent")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_LEARNING_EVENT");
                    });
        });

        modelBuilder.Entity<Lock>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LOCKS");

            entity.HasIndex(e => new { e.DataType, e.FkDataId }, "NDX_LOCKS1").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DataType)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("DATA_TYPE");
            entity.Property(e => e.DateLocked)
                .HasColumnType("datetime")
                .HasColumnName("DATE_LOCKED");
            entity.Property(e => e.DeviceName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("DEVICE_NAME");
            entity.Property(e => e.FkDataId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA_ID");
            entity.Property(e => e.FkLockedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LOCKED_BY");

            entity.HasOne(d => d.FkLockedByNavigation).WithMany(p => p.Locks)
                .HasForeignKey(d => d.FkLockedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOCKS_FK_LOCKED_BY");
        });

        modelBuilder.Entity<LsAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ADDRESS");

            entity.ToTable("LS_ADDRESS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("CITY");
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Ext)
                .HasMaxLength(10)
                .HasColumnName("EXT");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .HasColumnName("STATE");
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasColumnName("ZIP");
        });

        modelBuilder.Entity<LsAppConfig>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("LS_APP_CONFIG");

            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("NAME");
            entity.Property(e => e.Comments)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<LsAppMessage>(entity =>
        {
            entity.HasKey(e => e.SerialNumber).HasName("PK_APP_MESSAGE");

            entity.ToTable("LS_APP_MESSAGE");

            entity.Property(e => e.SerialNumber)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("SERIAL_NUMBER");
            entity.Property(e => e.FkMessageCategory)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MESSAGE_CATEGORY");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("MESSAGE");

            entity.HasOne(d => d.FkMessageCategoryNavigation).WithMany(p => p.LsAppMessages)
                .HasForeignKey(d => d.FkMessageCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APP_MESSAGE_FK_MESSAGE_CAT");
        });

        modelBuilder.Entity<LsAppMessageCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_APP_MESSAGE_CATEGORY");

            entity.ToTable("LS_APP_MESSAGE_CATEGORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<LsAppOrgspecificparm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_APP_ORGSPECIFICPARMS");

            entity.ToTable("LS_APP_ORGSPECIFICPARMS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.ParamValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("PARAM_VALUE");
            entity.Property(e => e.ValueBit)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("VALUE_BIT");
            entity.Property(e => e.ValueNumericFloat)
                .HasColumnType("numeric(12, 3)")
                .HasColumnName("VALUE_NUMERIC_FLOAT");
            entity.Property(e => e.ValueNumericInt)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("VALUE_NUMERIC_INT");
            entity.Property(e => e.ValueText)
                .HasMaxLength(250)
                .HasColumnName("VALUE_TEXT");
            entity.Property(e => e.ValueType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("VALUE_TYPE");
        });

        modelBuilder.Entity<LsCatalog>(entity =>
        {
            entity.ToTable("LS_CATALOG");

            entity.HasIndex(e => new { e.FkCompany, e.FkStructure }, "NDX_LS_CATALOG1");

            entity.HasIndex(e => e.FkProgram, "NDX_LS_CATALOG2");

            entity.HasIndex(e => new { e.FkOriginalCatalogId, e.Id }, "NDX_LS_CATALOG3");

            entity.HasIndex(e => e.Text, "NDX_LS_CATALOG4");

            entity.HasIndex(e => new { e.FkStructure, e.Status }, "NDX_LS_CATALOG5");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Approved)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("APPROVED");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(100)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.EmergencyHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("EMERGENCY_HOURS");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkCourseType)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COURSE_TYPE");
            entity.Property(e => e.FkOriginalCatalogId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORIGINAL_CATALOG_ID");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkStructure)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_STRUCTURE");
            entity.Property(e => e.FkTrainingType)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TRAINING_TYPE");
            entity.Property(e => e.Frequency)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FREQUENCY");
            entity.Property(e => e.RequalModifier)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("REQUAL_MODIFIER");
            entity.Property(e => e.RequalPeriod)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("REQUAL_PERIOD");
            entity.Property(e => e.RequiredCourse)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REQUIRED_COURSE");
            entity.Property(e => e.SimHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SIM_HOURS");
            entity.Property(e => e.StandardHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("STANDARD_HOURS");
            entity.Property(e => e.Status)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(1100)
                .HasColumnName("TEXT");
            entity.Property(e => e.TotalHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TOTAL_HOURS");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.Userdefined1)
                .HasMaxLength(100)
                .HasColumnName("USERDEFINED1");
            entity.Property(e => e.Userdefined2)
                .HasMaxLength(100)
                .HasColumnName("USERDEFINED2");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_CATALOG_FK_COMPANY");

            entity.HasOne(d => d.FkCourseTypeNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.FkCourseType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_FK_COURSE_TYPE");

            entity.HasOne(d => d.FkOriginalCatalog).WithMany(p => p.InverseFkOriginalCatalog)
                .HasForeignKey(d => d.FkOriginalCatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_FK_ORIGINAL_CATALOG");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_CATALOG_FK_PROGRAM");

            entity.HasOne(d => d.FkStructureNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.FkStructure)
                .HasConstraintName("FK_CATALOG_FK_STRUCTURE");

            entity.HasOne(d => d.FkTrainingTypeNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.FkTrainingType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_CATALOG_FK_TRAINING_TYPE");

            entity.HasOne(d => d.FrequencyNavigation).WithMany(p => p.LsCatalogFrequencyNavigations)
                .HasForeignKey(d => d.Frequency)
                .HasConstraintName("FK_CATALOG_FREQUENCY");

            entity.HasOne(d => d.RequalModifierNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.RequalModifier)
                .HasConstraintName("FK_CATALOG_REQUAL_MODIFIER");

            entity.HasOne(d => d.RequalPeriodNavigation).WithMany(p => p.LsCatalogRequalPeriodNavigations)
                .HasForeignKey(d => d.RequalPeriod)
                .HasConstraintName("FK_CATALOG_REQUAL_PERIOD");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.LsCatalogs)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_TYPE");
        });

        modelBuilder.Entity<LsCatalogLesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CATALOG_LESSONS");

            entity.ToTable("LS_CATALOG_LESSONS");

            entity.HasIndex(e => e.FkCatalog, "NDX_LS_CATALOG_LESSONS1");

            entity.HasIndex(e => e.FkProgram, "NDX_LS_CATALOG_LESSONS2");

            entity.HasIndex(e => e.FkExamOnlineProfile, "NDX_LS_CATALOG_LESSONS3");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.EmergencyHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("EMERGENCY_HOURS");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkCatalogType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG_TYPE");
            entity.Property(e => e.FkExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Path)
                .HasMaxLength(1000)
                .HasColumnName("PATH");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.SimHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SIM_HOURS");
            entity.Property(e => e.StandardHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("STANDARD_HOURS");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(1100)
                .HasColumnName("TEXT");
            entity.Property(e => e.TimeToComplete)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("TIME_TO_COMPLETE");
            entity.Property(e => e.TotalHours)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TOTAL_HOURS");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCatalogNavigation).WithMany(p => p.LsCatalogLessons)
                .HasForeignKey(d => d.FkCatalog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_LESSONS_FK_CATALOG");

            entity.HasOne(d => d.FkCatalogTypeNavigation).WithMany(p => p.LsCatalogLessons)
                .HasForeignKey(d => d.FkCatalogType)
                .HasConstraintName("FK_CATALOG_LESSONS_CATALOG_TYP");

            entity.HasOne(d => d.FkExamOnlineProfileNavigation).WithMany(p => p.LsCatalogLessons)
                .HasForeignKey(d => d.FkExamOnlineProfile)
                .HasConstraintName("FK_CATALOG_LESSONS_EXM_ON_PROF");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsCatalogLessons)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_CATALOG_LESSONS_FK_PROGRAM");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.LsCatalogLessons)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_LESSONS_MEDIA_TYPE");
        });

        modelBuilder.Entity<LsCatalogPrereq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CATALOG_PREREQ");

            entity.ToTable("LS_CATALOG_PREREQ");

            entity.HasIndex(e => e.FkCatalog, "NDX_LS_CATALOG_PREREQ1");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkPrerequisite)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PREREQUISITE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Status)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Text)
                .HasMaxLength(1000)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCatalogNavigation).WithMany(p => p.LsCatalogPrereqs)
                .HasForeignKey(d => d.FkCatalog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATALOG_PREREQ_FK_CATALOG");
        });

        modelBuilder.Entity<LsCatalogRating>(entity =>
        {
            entity.HasKey(e => e.IdValue);

            entity.ToTable("LS_CATALOG_RATING");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsCatalogType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_CATALOG_TYPE");

            entity.ToTable("LS_CATALOG_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Status)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(1100)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsCertJob>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_CERT_JOBS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCompanyId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkJobId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_JOB_ID");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkOrgParentnodeId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG_PARENTNODE_ID");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertJobs)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_JOBS_CERT_ID");

            entity.HasOne(d => d.FkCompany).WithMany(p => p.LsCertJobs)
                .HasForeignKey(d => d.FkCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_JOBS_FK_COMPANY_ID");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertJobFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_JOBS_CREATEDBY");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertJobFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_JOBS_MODIFIEDBY");
        });

        modelBuilder.Entity<LsCertLrnr>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_CERT_LRNRS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrs)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_CERT_ID");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_CREATEDBY");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_MODIFIEDBY");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_FK_LEARNER");
        });

        modelBuilder.Entity<LsCertLrnrRcrdByreqmnt>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LS_CERT_LRNRREQMNT_RCRD")
                .IsClustered(false);

            entity.ToTable("LS_CERT_LRNR_RCRD_BYREQMNT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCertRequirmentsId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_REQUIRMENTS_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLrnrCertRecrdId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LRNR_CERT_RECRD_ID");
            entity.Property(e => e.GracePeriodEndDate)
                .HasColumnType("datetime")
                .HasColumnName("GRACE_PERIOD_END_DATE");
            entity.Property(e => e.GraceprdLostcredits)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("GRACEPRD_LOSTCREDITS");
            entity.Property(e => e.GraceprdRunningTotal)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("GRACEPRD_RUNNING_TOTAL");
            entity.Property(e => e.IsCurrentReqRecord)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CURRENT_REQ_RECORD");
            entity.Property(e => e.ReqLostcredits)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQ_LOSTCREDITS");
            entity.Property(e => e.ReqPriorCertCarryover)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQ_PRIOR_CERT_CARRYOVER");
            entity.Property(e => e.ReqRulelostcreditsRuntotl)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQ_RULELOSTCREDITS_RUNTOTL");
            entity.Property(e => e.ReqRunningCarryoverTotal)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQ_RUNNING_CARRYOVER_TOTAL");
            entity.Property(e => e.ReqRunningTotal)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQ_RUNNING_TOTAL");
            entity.Property(e => e.ReqStatus)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("REQ_STATUS");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrRcrdByreqmnts)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_CERT");

            entity.HasOne(d => d.FkCertRequirments).WithMany(p => p.LsCertLrnrRcrdByreqmnts)
                .HasForeignKey(d => d.FkCertRequirmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_CRET_REQ");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrRcrdByreqmntFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrRcrdByreqmntFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_MODIFID");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrRcrdByreqmntFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_LEARNER");

            entity.HasOne(d => d.FkLrnrCertRecrd).WithMany(p => p.LsCertLrnrRcrdByreqmnts)
                .HasForeignKey(d => d.FkLrnrCertRecrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_REQ_LRN_CERT");
        });

        modelBuilder.Entity<LsCertLrnrRcrdByrule>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LS_CERT_LRNRREQRULE_RCRD")
                .IsClustered(false);

            entity.ToTable("LS_CERT_LRNR_RCRD_BYRULE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCertRequirmentsId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_REQUIRMENTS_ID");
            entity.Property(e => e.FkCertRequirmentsRuleId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_REQUIRMENTS_RULE_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLrnrCertRecrdId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LRNR_CERT_RECRD_ID");
            entity.Property(e => e.FkLrnrCertReqrecrdId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LRNR_CERT_REQRECRD_ID");
            entity.Property(e => e.IsCurrentRuleRecord)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CURRENT_RULE_RECORD");
            entity.Property(e => e.ReqruleByrecdateLostcredits)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_BYRECDATE_LOSTCREDITS");
            entity.Property(e => e.ReqruleByrecdateTocarryover)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_BYRECDATE_TOCARRYOVER");
            entity.Property(e => e.ReqruleLostcredits)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_LOSTCREDITS");
            entity.Property(e => e.ReqruleRunningCrryoverTotal)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_RUNNING_CRRYOVER_TOTAL");
            entity.Property(e => e.ReqruleRunningTotal)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_RUNNING_TOTAL");
            entity.Property(e => e.ReqruleRunningTotalByrecdte)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("REQRULE_RUNNING_TOTAL_BYRECDTE");
            entity.Property(e => e.ReqruleStatus)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("REQRULE_STATUS");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrRcrdByrules)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RULE_CERT");

            entity.HasOne(d => d.FkCertRequirments).WithMany(p => p.LsCertLrnrRcrdByrules)
                .HasForeignKey(d => d.FkCertRequirmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RUL_CRET_REQ");

            entity.HasOne(d => d.FkCertRequirmentsRule).WithMany(p => p.LsCertLrnrRcrdByrules)
                .HasForeignKey(d => d.FkCertRequirmentsRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RUL_REQ_RULE");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrRcrdByruleFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RULE_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrRcrdByruleFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RULE_MODIFID");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrRcrdByruleFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RULE_LEARNER");

            entity.HasOne(d => d.FkLrnrCertRecrd).WithMany(p => p.LsCertLrnrRcrdByrules)
                .HasForeignKey(d => d.FkLrnrCertRecrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RUL_LRN_CERT");

            entity.HasOne(d => d.FkLrnrCertReqrecrd).WithMany(p => p.LsCertLrnrRcrdByrules)
                .HasForeignKey(d => d.FkLrnrCertReqrecrdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_RUL_LRN_CTRQ");
        });

        modelBuilder.Entity<LsCertLrnrRecordBycert>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LS_CERT_LRNRCERT_RCRD")
                .IsClustered(false);

            entity.ToTable("LS_CERT_LRNR_RECORD_BYCERT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AgencyCertificationId)
                .HasMaxLength(100)
                .HasColumnName("AGENCY_CERTIFICATION_ID");
            entity.Property(e => e.AgencyIssueDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("AGENCY_ISSUE_DATE");
            entity.Property(e => e.CertStatus)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("CERT_STATUS");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.GracePeriodEndDate)
                .HasColumnType("datetime")
                .HasColumnName("GRACE_PERIOD_END_DATE");
            entity.Property(e => e.IsCurrentCertRecord)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CURRENT_CERT_RECORD");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrRecordBycerts)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CERT_CERT");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrRecordBycertFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CERT_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrRecordBycertFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CERT_MODIFID");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrRecordBycertFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CERT_LEARNER");
        });

        modelBuilder.Entity<LsCertLrnrRecordBycrse>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LS_CERT_LRNRCERT_CRSERCRD")
                .IsClustered(false);

            entity.ToTable("LS_CERT_LRNR_RECORD_BYCRSE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCertRequirmentsId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_REQUIRMENTS_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasDefaultValue(-1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkLsCertLrnrrdBycrsemain)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_CERT_LRNRRD_BYCRSEMAIN");
            entity.Property(e => e.FkProgramCompletion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_COMPLETION");
            entity.Property(e => e.FkProgramid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAMID");
            entity.Property(e => e.FkXrefLibId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB_ID");
            entity.Property(e => e.Ismanuallyscored)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISMANUALLYSCORED");
            entity.Property(e => e.IsvalidForCredit)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISVALID_FOR_CREDIT");
            entity.Property(e => e.LocationWhereCrsegiven)
                .HasMaxLength(1000)
                .HasDefaultValue("VLS")
                .HasColumnName("LOCATION_WHERE_CRSEGIVEN");
            entity.Property(e => e.RdomCrseEventId)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("RDOM_CRSE_EVENT_ID");
            entity.Property(e => e.ReceivedValue)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("RECEIVED_VALUE");
            entity.Property(e => e.TotalValueOfCourse)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("TOTAL_VALUE_OF_COURSE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_CERT");

            entity.HasOne(d => d.FkCertRequirments).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkCertRequirmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_CERTREQ");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrRecordBycrseFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrRecordBycrseFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_MODIFID");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrRecordBycrseFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_LNEVENT");

            entity.HasOne(d => d.FkLsCertLrnrrdBycrsemainNavigation).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkLsCertLrnrrdBycrsemain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_MAIN");

            entity.HasOne(d => d.FkProgramCompletionNavigation).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkProgramCompletion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_PG_COMP");

            entity.HasOne(d => d.FkProgram).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkProgramid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_PROGRAM");

            entity.HasOne(d => d.FkXrefLib).WithMany(p => p.LsCertLrnrRecordBycrses)
                .HasForeignKey(d => d.FkXrefLibId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRS_REC_CRSE_XREF_LB");
        });

        modelBuilder.Entity<LsCertLrnrrdBycrsemain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CERT_LRNRRD_BYCRSEMAIN");

            entity.ToTable("LS_CERT_LRNRRD_BYCRSEMAIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.ExternalCertificateid)
                .HasMaxLength(50)
                .HasColumnName("EXTERNAL_CERTIFICATEID");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasDefaultValue(-1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgramid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAMID");
            entity.Property(e => e.Ismanuallyscored)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISMANUALLYSCORED");
            entity.Property(e => e.LocationWhereCrsegiven)
                .IsRequired()
                .HasMaxLength(1000)
                .HasDefaultValue("VLS")
                .HasColumnName("LOCATION_WHERE_CRSEGIVEN");
            entity.Property(e => e.LrnrCertificateId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("LRNR_CERTIFICATE_ID");
            entity.Property(e => e.RdomCrseEventId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("RDOM_CRSE_EVENT_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(1100)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertLrnrrdBycrsemains)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSEMAIN_CERT");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertLrnrrdBycrsemainFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSMN_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertLrnrrdBycrsemainFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSMN_MODIFED");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertLrnrrdBycrsemainFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSMN_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsCertLrnrrdBycrsemains)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSMN_LRN_EVT");

            entity.HasOne(d => d.FkProgram).WithMany(p => p.LsCertLrnrrdBycrsemains)
                .HasForeignKey(d => d.FkProgramid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_LRNRRD_BYCRSMN_PROGRAM");
        });

        modelBuilder.Entity<LsCertManscreCrdit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CERT_MANSCRE_CRDIT");

            entity.ToTable("LS_CERT_MANSCRE_CRDIT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AmountCarryover)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_CARRYOVER");
            entity.Property(e => e.AmountCarryoverByrecdate)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_CARRYOVER_BYRECDATE");
            entity.Property(e => e.AmountCredited)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_CREDITED");
            entity.Property(e => e.AmountCreditedByrecdate)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_CREDITED_BYRECDATE");
            entity.Property(e => e.AmountLost)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_LOST");
            entity.Property(e => e.AmountLostByrecdate)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_LOST_BYRECDATE");
            entity.Property(e => e.AmountReqGraceperiod)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_REQ_GRACEPERIOD");
            entity.Property(e => e.AmountReqLost)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("AMOUNT_REQ_LOST");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLsCertLrRcrdRuleorreqid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_CERT_LR_RCRD_RULEORREQID");
            entity.Property(e => e.FkLsCertLrnrRecordBycrse)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_CERT_LRNR_RECORD_BYCRSE");
            entity.Property(e => e.FkManscreCrditmain)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MANSCRE_CRDITMAIN");
            entity.Property(e => e.Isrule)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISRULE");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertManscreCrditFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDT_CREATEDBY");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertManscreCrditFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDT_MODIFIEDBY");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertManscreCrditFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDT_LEARNER");

            entity.HasOne(d => d.FkLsCertLrnrRecordBycrseNavigation).WithMany(p => p.LsCertManscreCrdits)
                .HasForeignKey(d => d.FkLsCertLrnrRecordBycrse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDT_BYCRSE");

            entity.HasOne(d => d.FkManscreCrditmainNavigation).WithMany(p => p.LsCertManscreCrdits)
                .HasForeignKey(d => d.FkManscreCrditmain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDT_CRDITMAIN");
        });

        modelBuilder.Entity<LsCertManscreCrditmain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CERT_MANSCRE_CRDITMAIN");

            entity.ToTable("LS_CERT_MANSCRE_CRDITMAIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLsCertLrnrrdBycrsemain)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_CERT_LRNRRD_BYCRSEMAIN");
            entity.Property(e => e.FkLsCertifications)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_CERTIFICATIONS");
            entity.Property(e => e.FkLsLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertManscreCrditmainFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_CREATED");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertManscreCrditmainFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_MODIFED");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsCertManscreCrditmainFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_LEARNER");

            entity.HasOne(d => d.FkLsCertLrnrrdBycrsemainNavigation).WithMany(p => p.LsCertManscreCrditmains)
                .HasForeignKey(d => d.FkLsCertLrnrrdBycrsemain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_BYCRSMN");

            entity.HasOne(d => d.FkLsCertificationsNavigation).WithMany(p => p.LsCertManscreCrditmains)
                .HasForeignKey(d => d.FkLsCertifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_CERT");

            entity.HasOne(d => d.FkLsLearningEventNavigation).WithMany(p => p.LsCertManscreCrditmains)
                .HasForeignKey(d => d.FkLsLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_LRN_EVT");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsCertManscreCrditmains)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_MANSCR_CRDITMN_PROGRAM");
        });

        modelBuilder.Entity<LsCertReqruleBasis>(entity =>
        {
            entity.HasKey(e => e.IdValue).IsClustered(false);

            entity.ToTable("LS_CERT_REQRULE_BASIS");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<LsCertRequirement>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_CERT_REQUIREMENTS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CriteriaTotal)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("CRITERIA_TOTAL");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLscertReqbasisStart)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LSCERT_REQBASIS_START");
            entity.Property(e => e.FkTimeframeBasisReq)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMEFRAME_BASIS_REQ");
            entity.Property(e => e.FkTimetocomplValidityperiod)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMETOCOMPL_VALIDITYPERIOD");
            entity.Property(e => e.FkXrefLibId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB_ID");
            entity.Property(e => e.HonorCertGracePeriod)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("HONOR_CERT_GRACE_PERIOD");
            entity.Property(e => e.IsCriteriatotalRestricted)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CRITERIATOTAL_RESTRICTED");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.IsvalidprdSameasCertrecrd)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISVALIDPRD_SAMEAS_CERTRECRD");
            entity.Property(e => e.LrnrRecordDisplayPeriod)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("LRNR_RECORD_DISPLAY_PERIOD");
            entity.Property(e => e.PeriodBreakdownForRules)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("PERIOD_BREAKDOWN_FOR_RULES");
            entity.Property(e => e.SortOrder)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("SORT_ORDER");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertRequirements)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_CERT_ID");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertRequirementFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_CREATEDBY");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertRequirementFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_MODIFIEDBY");

            entity.HasOne(d => d.FkLscertReqbasisStartNavigation).WithMany(p => p.LsCertRequirements)
                .HasForeignKey(d => d.FkLscertReqbasisStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_RQ_LSCERT_REQBASIS_STA");

            entity.HasOne(d => d.FkTimeframeBasisReqNavigation).WithMany(p => p.LsCertRequirements)
                .HasForeignKey(d => d.FkTimeframeBasisReq)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_TF_BASIS_REQ");

            entity.HasOne(d => d.FkTimetocomplValidityperiodNavigation).WithMany(p => p.LsCertRequirements)
                .HasForeignKey(d => d.FkTimetocomplValidityperiod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTREQ_TIME2COMPL_VALID_PR");

            entity.HasOne(d => d.FkXrefLib).WithMany(p => p.LsCertRequirements)
                .HasForeignKey(d => d.FkXrefLibId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_XREF_LIB");
        });

        modelBuilder.Entity<LsCertRequirementsRule>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_CERT_REQUIREMENTS_RULES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CarryoverTimeTotal)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("CARRYOVER_TIME_TOTAL");
            entity.Property(e => e.CourserepeatTimeTotal)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("COURSEREPEAT_TIME_TOTAL");
            entity.Property(e => e.CriteriaThisRule)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("CRITERIA_THIS_RULE");
            entity.Property(e => e.CrryovrApplyedAfterReqtotl)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CRRYOVR_APPLYED_AFTER_REQTOTL");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.FkCertId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_ID");
            entity.Property(e => e.FkCertRequirmentsId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CERT_REQUIRMENTS_ID");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLastModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LAST_MODIFIEDBY");
            entity.Property(e => e.FkLscertCarryovrbasisStart)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LSCERT_CARRYOVRBASIS_START");
            entity.Property(e => e.FkLscertCrserptbasisStart)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LSCERT_CRSERPTBASIS_START");
            entity.Property(e => e.FkTimetypeCarryoverBasis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMETYPE_CARRYOVER_BASIS");
            entity.Property(e => e.FkTimetypeCrserepeatBasis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMETYPE_CRSEREPEAT_BASIS");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.MaxCarryoverAllowed)
                .HasColumnType("numeric(8, 3)")
                .HasColumnName("MAX_CARRYOVER_ALLOWED");
            entity.Property(e => e.MaxCourserepeatAllowed)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("MAX_COURSEREPEAT_ALLOWED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkCert).WithMany(p => p.LsCertRequirementsRules)
                .HasForeignKey(d => d.FkCertId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CERT_ID");

            entity.HasOne(d => d.FkCertRequirments).WithMany(p => p.LsCertRequirementsRules)
                .HasForeignKey(d => d.FkCertRequirmentsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CERT_REQ");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertRequirementsRuleFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CREATEDBY");

            entity.HasOne(d => d.FkLastModifiedbyNavigation).WithMany(p => p.LsCertRequirementsRuleFkLastModifiedbyNavigations)
                .HasForeignKey(d => d.FkLastModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_MODIFIEDBY");

            entity.HasOne(d => d.FkLscertCarryovrbasisStartNavigation).WithMany(p => p.LsCertRequirementsRuleFkLscertCarryovrbasisStartNavigations)
                .HasForeignKey(d => d.FkLscertCarryovrbasisStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CO_BAS_START");

            entity.HasOne(d => d.FkLscertCrserptbasisStartNavigation).WithMany(p => p.LsCertRequirementsRuleFkLscertCrserptbasisStartNavigations)
                .HasForeignKey(d => d.FkLscertCrserptbasisStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CRS_BAS_STRT");

            entity.HasOne(d => d.FkTimetypeCarryoverBasisNavigation).WithMany(p => p.LsCertRequirementsRuleFkTimetypeCarryoverBasisNavigations)
                .HasForeignKey(d => d.FkTimetypeCarryoverBasis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_TIME_CO_BAS");

            entity.HasOne(d => d.FkTimetypeCrserepeatBasisNavigation).WithMany(p => p.LsCertRequirementsRuleFkTimetypeCrserepeatBasisNavigations)
                .HasForeignKey(d => d.FkTimetypeCrserepeatBasis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERT_REQ_RULES_CRS_REP_BAS");
        });

        modelBuilder.Entity<LsCertTimeframeBasis>(entity =>
        {
            entity.HasKey(e => e.IdValue).IsClustered(false);

            entity.ToTable("LS_CERT_TIMEFRAME_BASIS");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<LsCertification>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_CERTIFICATIONS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AutomaticRouting)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("AUTOMATIC_ROUTING");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkCreatedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATEDBY");
            entity.Property(e => e.FkLscertReqbasisStart)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LSCERT_REQBASIS_START");
            entity.Property(e => e.FkModifiedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MODIFIEDBY");
            entity.Property(e => e.FkTimeframeBasisRenewal)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMEFRAME_BASIS_RENEWAL");
            entity.Property(e => e.FkTimetocomplGraceperiod)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMETOCOMPL_GRACEPERIOD");
            entity.Property(e => e.FkTimetocomplValidityPeriod)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIMETOCOMPL_VALIDITY_PERIOD");
            entity.Property(e => e.FkXrefLibId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB_ID");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Isnerccertification)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISNERCCERTIFICATION");
            entity.Property(e => e.IsqualcardCert)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISQUALCARD_CERT");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("MODIFIED_DATE");
            entity.Property(e => e.OrgCategory)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ORG_CATEGORY");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("TITLE");
            entity.Property(e => e.Userdefinedid)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINEDID");

            entity.HasOne(d => d.FkCreatedbyNavigation).WithMany(p => p.LsCertificationFkCreatedbyNavigations)
                .HasForeignKey(d => d.FkCreatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTIFICATIONS_CREATEDBY");

            entity.HasOne(d => d.FkLscertReqbasisStartNavigation).WithMany(p => p.LsCertifications)
                .HasForeignKey(d => d.FkLscertReqbasisStart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTS_LSCERT_REQBASIS_STA");

            entity.HasOne(d => d.FkModifiedbyNavigation).WithMany(p => p.LsCertificationFkModifiedbyNavigations)
                .HasForeignKey(d => d.FkModifiedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTIFICATIONS_MODIFIEDBY");

            entity.HasOne(d => d.FkTimeframeBasisRenewalNavigation).WithMany(p => p.LsCertifications)
                .HasForeignKey(d => d.FkTimeframeBasisRenewal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTS_TIMEFRAME_BASIS_RENWL");

            entity.HasOne(d => d.FkTimetocomplGraceperiodNavigation).WithMany(p => p.LsCertificationFkTimetocomplGraceperiodNavigations)
                .HasForeignKey(d => d.FkTimetocomplGraceperiod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTS_TIME2COMPL_GRACEPERD");

            entity.HasOne(d => d.FkTimetocomplValidityPeriodNavigation).WithMany(p => p.LsCertificationFkTimetocomplValidityPeriodNavigations)
                .HasForeignKey(d => d.FkTimetocomplValidityPeriod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTS_TIME2COMPL_VALID_PER");

            entity.HasOne(d => d.FkXrefLib).WithMany(p => p.LsCertifications)
                .HasForeignKey(d => d.FkXrefLibId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CERTIFICATIONS_XREF_LIB");
        });

        modelBuilder.Entity<LsCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_COMPANY");

            entity.ToTable("LS_COMPANY");

            entity.HasIndex(e => new { e.Id, e.Company }, "IX_LS_COMPANY").HasFillFactor(90);

            entity.HasIndex(e => new { e.Id, e.Company, e.FkAddress }, "IX_LS_COMPANY_1").HasFillFactor(90);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("COMPANY");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FkAddress)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ADDRESS");
            entity.Property(e => e.SelfRegistration)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("SELF_REGISTRATION");

            entity.HasOne(d => d.FkAddressNavigation).WithMany(p => p.LsCompanies)
                .HasForeignKey(d => d.FkAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPANY_FK_ADDRESS");
        });

        modelBuilder.Entity<LsCompanyProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_COMPANY_PROJECT");

            entity.ToTable("LS_COMPANY_PROJECT");

            entity.HasIndex(e => new { e.Id, e.FkCompany, e.FkProject }, "IX_LS_COMPANY_PROJECT2").HasFillFactor(90);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateBegin)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_BEGIN");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsCompanyProjects)
                .HasForeignKey(d => d.FkCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPANY_PROJECT_FK_COMPANY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.LsCompanyProjects)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPANY_PROJECT_FK_PROJECT");
        });

        modelBuilder.Entity<LsCourseType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_COURSE_TYPE");

            entity.ToTable("LS_COURSE_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsDataXfer>(entity =>
        {
            entity.ToTable("LS_DATA_XFER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ClassDate)
                .HasColumnType("datetime")
                .HasColumnName("CLASS_DATE");
            entity.Property(e => e.ClassTime)
                .HasColumnType("datetime")
                .HasColumnName("CLASS_TIME");
            entity.Property(e => e.PassFail)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PASS_FAIL");
            entity.Property(e => e.RecUpdated)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REC_UPDATED");
            entity.Property(e => e.Ssn)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("SSN");
            entity.Property(e => e.TestMethod)
                .HasMaxLength(20)
                .HasColumnName("TEST_METHOD");
            entity.Property(e => e.TrainingType)
                .HasMaxLength(30)
                .HasColumnName("TRAINING_TYPE");
        });

        modelBuilder.Entity<LsDateModifier>(entity =>
        {
            entity.HasKey(e => e.IdValue);

            entity.ToTable("LS_DATE_MODIFIER");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.FkTimeType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_TYPE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
            entity.Property(e => e.TimeSpan)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TIME_SPAN");

            entity.HasOne(d => d.FkTimeTypeNavigation).WithMany(p => p.LsDateModifiers)
                .HasForeignKey(d => d.FkTimeType)
                .HasConstraintName("FK_DATE_MODIFIER_FK_TIME_TYPE");
        });

        modelBuilder.Entity<LsDeveloperToLearner>(entity =>
        {
            entity.HasKey(e => e.FkLearner).HasName("PK_DEVELOPER_TO_LEARNER");

            entity.ToTable("LS_DEVELOPER_TO_LEARNER");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.LsDeveloperToLearners)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DEVELOPER_2_LEARNER_DEVELOP");

            entity.HasOne(d => d.FkLearnerNavigation).WithOne(p => p.LsDeveloperToLearner)
                .HasForeignKey<LsDeveloperToLearner>(d => d.FkLearner)
                .HasConstraintName("FK_DEVELOPER_2_LEARNER_LEARNER");
        });

        modelBuilder.Entity<LsDoclinkTrack>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.FkLearner, e.FkDoclink }).HasName("PK_DOCLINK_TRACK");

            entity.ToTable("LS_DOCLINK_TRACK");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkDoclink)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DOCLINK");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");

            entity.HasOne(d => d.FkDoclinkNavigation).WithMany(p => p.LsDoclinkTracks)
                .HasForeignKey(d => d.FkDoclink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCLINK_TRACK_FK_DOCLINK");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsDoclinkTracks)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_DOCLINK_TRACK_FK_LEARNER");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsDoclinkTracks)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DOCLINK_TRACK_FK_PROGRAM");
        });

        modelBuilder.Entity<LsDocument>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_DOCUMENT");

            entity.HasIndex(e => new { e.FkLearner, e.FkLearningEvent, e.FkProgram, e.FkObjective }, "NDX_LS_DOCUMENT1");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkLearner, e.FkProgram }, "NDX_LS_DOCUMENT2");

            entity.HasIndex(e => new { e.FkLearner, e.FkLearningEvent, e.FkObjective, e.FkProgram, e.FkContent }, "NDX_LS_DOCUMENT3");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasColumnName("COMMENT");
            entity.Property(e => e.Data)
                .IsRequired()
                .HasColumnName("DATA");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.EvaluationDocument)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EVALUATION_DOCUMENT");
            entity.Property(e => e.Filename)
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnName("FILENAME");
            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkLsQualCardRouteHistory)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD_ROUTE_HISTORY");
            entity.Property(e => e.FkModifiedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MODIFIED_BY");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkTaskQualification)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_QUALIFICATION");
            entity.Property(e => e.LearnerCanView)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("LEARNER_CAN_VIEW");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkContent)
                .HasConstraintName("FK_LS_DOCUMENT_FK_CONTENT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsDocumentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_DOCUMENT_CREATED_BY");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsDocumentFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LS_DOCUMENT_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkLearningEvent)
                .HasConstraintName("FK_LS_DOCUMENT_LEARNING_EVENT");

            entity.HasOne(d => d.FkLsQualCardRouteHistoryNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkLsQualCardRouteHistory)
                .HasConstraintName("FK_DOCUMENT_FK_QUAL_CARD_RT_HY");

            entity.HasOne(d => d.FkModifiedByNavigation).WithMany(p => p.LsDocumentFkModifiedByNavigations)
                .HasForeignKey(d => d.FkModifiedBy)
                .HasConstraintName("FK_LS_DOCUMENT_FK_MODIFIED_BY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkObjective)
                .HasConstraintName("FK_LS_DOCUMENT_FK_OBJECTIVE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_LS_DOCUMENT_FK_PROGRAM");

            entity.HasOne(d => d.FkTaskQualificationNavigation).WithMany(p => p.LsDocuments)
                .HasForeignKey(d => d.FkTaskQualification)
                .HasConstraintName("FK_LS_DOCUMENT_FK_TASK_QUAL");
        });

        modelBuilder.Entity<LsEvaluatorTrainerValue>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_EVALUATOR_TRAINER_VALUES");

            entity.ToTable("LS_EVALUATOR_TRAINER_VALUES");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.IsEvaluatorType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_EVALUATOR_TYPE");
            entity.Property(e => e.IsTrainerType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRAINER_TYPE");
            entity.Property(e => e.Optvalue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("OPTVALUE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsEventAudit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EVENT_AUDIT");

            entity.ToTable("LS_EVENT_AUDIT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.EntityIdModified)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ENTITY_ID_MODIFIED");
            entity.Property(e => e.EventDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("EVENT_DATE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkEventsToAudit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVENTS_TO_AUDIT");
            entity.Property(e => e.Ip)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.Processed)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PROCESSED");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsEventAudits)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENT_AUDIT_FK_LEARNER_TRIG");

            entity.HasOne(d => d.FkEventsToAuditNavigation).WithMany(p => p.LsEventAudits)
                .HasForeignKey(d => d.FkEventsToAudit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENT_AUDIT_EVENTS_2_AUDIT");
        });

        modelBuilder.Entity<LsEventsToAudit>(entity =>
        {
            entity.HasKey(e => e.AuditEventId).HasName("PK_EVENTS_TO_AUDIT");

            entity.ToTable("LS_EVENTS_TO_AUDIT");

            entity.Property(e => e.AuditEventId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("AUDIT_EVENT_ID");
            entity.Property(e => e.AuditEventParentId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("AUDIT_EVENT_PARENT_ID");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .HasColumnName("ENTITY_TYPE");
            entity.Property(e => e.EventActive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EVENT_ACTIVE");
            entity.Property(e => e.EventText)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("EVENT_TEXT");

            entity.HasOne(d => d.AuditEventParent).WithMany(p => p.InverseAuditEventParent)
                .HasForeignKey(d => d.AuditEventParentId)
                .HasConstraintName("FK_EVENTS_TO_AUDIT_TO_PARENT");
        });

        modelBuilder.Entity<LsExamEvent>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkLearner, e.FkLearningEvent }).IsClustered(false);

            entity.ToTable("LS_EXAM_EVENT");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkExam, e.FkLearner, e.FkExamOnlineProfile, e.FkLearningEvent, e.FkProgram }, "IX_LS_EXAM_EVENT");

            entity.HasIndex(e => new { e.FkExam, e.FkLearner, e.FkLearningEvent, e.FkProgram }, "IX_Learning_Event_Exam").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkProgram, e.FkLearner, e.FkLearningEvent }, "I_LS_EXAM_EVENT2");

            entity.HasIndex(e => new { e.FkLearner, e.FkLearningEvent, e.FkProgram }, "I_LS_EXAM_EVENT3");

            entity.HasIndex(e => new { e.FkLearner, e.FkLearningEvent, e.FkExamResults }, "I_LS_EXAM_EVENT4");

            entity.HasIndex(e => new { e.FkLearner, e.FkLearningEvent, e.FkProgram, e.DateCompleted }, "LS_EXAM_EVENT2");

            entity.HasIndex(e => new { e.FkProgram, e.FkLearner, e.FkLearningEvent }, "_dta_index_LS_EXAM_EVENT_10_292196091__K10_K3_K9_1_2_4_5_6_7_8_11");

            entity.HasIndex(e => new { e.FkProgram, e.FkLearningEvent, e.FkLearner, e.FkExam }, "_dta_index_LS_EXAM_EVENT_31_292196091__K10_K9_K3_K2_6");

            entity.HasIndex(e => new { e.FkProgram, e.FkLearningEvent, e.FkLearner, e.FkExam }, "_dta_index_LS_EXAM_EVENT_31_292196091__K10_K9_K3_K2_6_8");

            entity.HasIndex(e => new { e.FkLearner, e.FkExam, e.FkExamResults }, "_dta_index_LS_EXAM_EVENT_31_292196091__K3_K2_K8_1_4_5_6_7_9_10_11");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkProgram, e.FkExamResults, e.FkLearner }, "_dta_index_LS_EXAM_EVENT_31_292196091__K9_K10_K8_K3_1_2_4_5_6_7_11");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkLearner }, "_dta_index_LS_EXAM_EVENT_c_31_292196091__K9_K3").IsClustered();

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.Acknowledged)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ACKNOWLEDGED");
            entity.Property(e => e.AcknowledgedDateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("ACKNOWLEDGED_DATE_COMPLETED");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateStarted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_STARTED");
            entity.Property(e => e.ExamAutoReentryAttempts)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("EXAM_AUTO_REENTRY_ATTEMPTS");
            entity.Property(e => e.ExamSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("EXAM_SEQUENCE");
            entity.Property(e => e.FkExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.FkExamResults)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_RESULTS");
            entity.Property(e => e.FkExamType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_TYPE");
            entity.Property(e => e.FkLearningEventOriginal)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT_ORIGINAL");
            entity.Property(e => e.FkProctor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROCTOR");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.ManualReentryEnabled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MANUAL_REENTRY_ENABLED");
            entity.Property(e => e.Reviewed)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REVIEWED");
            entity.Property(e => e.ReviewedDateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("REVIEWED_DATE_COMPLETED");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.LsExamEvents)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_EXAM_EVENT_FK_EXAM");

            entity.HasOne(d => d.FkExamOnlineProfileNavigation).WithMany(p => p.LsExamEvents)
                .HasForeignKey(d => d.FkExamOnlineProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_EXAM_EVENT_EXM_OL_PROFIL");

            entity.HasOne(d => d.FkExamResultsNavigation).WithMany(p => p.LsExamEvents)
                .HasForeignKey(d => d.FkExamResults)
                .HasConstraintName("FK_LS_EXAM_EVENT_EXAM_RESULTS");

            entity.HasOne(d => d.FkExamTypeNavigation).WithMany(p => p.LsExamEvents)
                .HasForeignKey(d => d.FkExamType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_EXAM_EVENT_FK_EXAM_TYPE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsExamEventFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_LS_EXAM_EVENT_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsExamEventFkLearningEventNavigations)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_EXAM_EVENT_LEARNING_EVNT");

            entity.HasOne(d => d.FkLearningEventOriginalNavigation).WithMany(p => p.LsExamEventFkLearningEventOriginalNavigations)
                .HasForeignKey(d => d.FkLearningEventOriginal)
                .HasConstraintName("FK_LS_EXAM_EVENT_LRNG_EVNT_ORG");

            entity.HasOne(d => d.FkProctorNavigation).WithMany(p => p.LsExamEventFkProctorNavigations)
                .HasForeignKey(d => d.FkProctor)
                .HasConstraintName("FK_LS_EXAM_EVENT_FK_PROCTOR");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsExamEvents)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_EXAM_EVENT_FK_PROGRAM");
        });

        modelBuilder.Entity<LsExamGenerator>(entity =>
        {
            entity.HasKey(e => new { e.Vlskey, e.Question, e.ParentUnit }).IsClustered(false);

            entity.ToTable("LS_EXAM_GENERATOR");

            entity.HasIndex(e => e.Vlskey, "_dta_index_LS_EXAM_GENERATOR_c_31_1044198770__K1").IsClustered();

            entity.Property(e => e.Vlskey)
                .HasMaxLength(50)
                .HasColumnName("VLSKEY");
            entity.Property(e => e.Question)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("QUESTION");
            entity.Property(e => e.ParentUnit)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("PARENT_UNIT");
            entity.Property(e => e.FkQuestionImplId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL_ID");
            entity.Property(e => e.OltSel)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("OLT_SEL");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Subq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SUBQ");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.VisionSel)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("VISION_SEL");

            entity.HasOne(d => d.FkQuestionImpl).WithMany(p => p.LsExamGenerators)
                .HasForeignKey(d => d.FkQuestionImplId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_GENERATOR_FK_QUES_IMPL");

            entity.HasOne(d => d.QuestionNavigation).WithMany(p => p.LsExamGenerators)
                .HasForeignKey(d => d.Question)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EXAM_GENERATOR_FK_QUESTION");
        });

        modelBuilder.Entity<LsExamResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EXAM_RESULTS");

            entity.ToTable("LS_EXAM_RESULTS");

            entity.HasIndex(e => e.Id, "_dta_index_LS_EXAM_RESULTS_31_1271675578__K1_2_7");

            entity.HasIndex(e => new { e.Id, e.ExamScore }, "_dta_index_LS_EXAM_RESULTS_31_1271675578__K1_K2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AdjScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ADJ_SCORE");
            entity.Property(e => e.Comments)
                .HasMaxLength(100)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateAdj)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ADJ");
            entity.Property(e => e.ExamScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("EXAM_SCORE");
            entity.Property(e => e.FkAdjuster)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ADJUSTER");
            entity.Property(e => e.PassingScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("PASSING_SCORE");

            entity.HasOne(d => d.FkAdjusterNavigation).WithMany(p => p.LsExamResults)
                .HasForeignKey(d => d.FkAdjuster)
                .HasConstraintName("FK_EXAM_RESULTS_FK_ADJUSTER");
        });

        modelBuilder.Entity<LsExamType>(entity =>
        {
            entity.HasKey(e => e.IdValue);

            entity.ToTable("LS_EXAM_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.DateClosed)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CLOSED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<LsExternalCompletion>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_EXTERNAL_COMPLETION");

            entity.HasIndex(e => new { e.Pin, e.CourseCode, e.CompletionDate, e.Xfer, e.DateXfer }, "NDX_LS_EXTERNAL_COMPLETION1");

            entity.HasIndex(e => new { e.Xfer, e.DateXfer, e.DateCreated }, "NDX_LS_EXTERNAL_COMPLETION2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CompletionDate)
                .HasColumnType("datetime")
                .HasColumnName("COMPLETION_DATE");
            entity.Property(e => e.CourseCode)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("COURSE_CODE");
            entity.Property(e => e.Credit)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("CREDIT");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateXfer)
                .HasColumnType("datetime")
                .HasColumnName("DATE_XFER");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Pin)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("PIN");
            entity.Property(e => e.Source)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SOURCE");
            entity.Property(e => e.Xfer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("XFER");
            entity.Property(e => e.XferMessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("XFER_MESSAGE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsExternalCompletions)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_EXTERNAL_COMPLETION_LEARNER");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsExternalCompletions)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_EXTERNAL_COMPLETION_FK_PROG");
        });

        modelBuilder.Entity<LsExternalCourse>(entity =>
        {
            entity.ToTable("LS_EXTERNAL_COURSE");

            entity.HasIndex(e => e.CourseCode, "NDX_LS_EXTERNAL_COURSE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CourseCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("COURSE_CODE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<LsExternalValidationLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LS_EXTERNAL_VALIDATION_LOG");

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.OperationComment)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("OPERATION_COMMENT");
            entity.Property(e => e.RecCount)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("REC_COUNT");
        });

        modelBuilder.Entity<LsGenericValue>(entity =>
        {
            entity.HasKey(e => e.Id1);

            entity.ToTable("LS_GENERIC_VALUE");

            entity.Property(e => e.Id1)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_1");
            entity.Property(e => e.Data).HasColumnName("DATA");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Enabled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENABLED");
            entity.Property(e => e.Id2)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_2");
            entity.Property(e => e.Id3)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_3");
            entity.Property(e => e.Id4)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_4");
            entity.Property(e => e.IdString).HasColumnName("ID_STRING");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.Value).HasColumnName("VALUE");
        });

        modelBuilder.Entity<LsHoldRelease>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkProgram, e.FkObjective, e.FkContent, e.FkLearner }).HasName("PK_HOLD_RELEASE");

            entity.ToTable("LS_HOLD_RELEASE");

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.ReleaseHold)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RELEASE_HOLD");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.LsHoldReleases)
                .HasForeignKey(d => d.FkContent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOLD_RELEASE_FK_CONTENT");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsHoldReleases)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_HOLD_RELEASE_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsHoldReleases)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOLD_RELEASE_LEARNING_EVET");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsHoldReleases)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOLD_RELEASE_FK_OBJECTIVE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsHoldReleases)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HOLD_RELEASE_FK_PROGRAM");
        });

        modelBuilder.Entity<LsImportJobAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_IMPORT_JOB_ASSIGNMENTS");

            entity.HasIndex(e => new { e.Xfer, e.JobCode, e.Action, e.Pin }, "NDX_LS_IMPORT_JOB_ASSIGNMENTS1");

            entity.HasIndex(e => new { e.Xfer, e.Action, e.JobCode, e.Pin }, "NDX_LS_IMPORT_JOB_ASSIGNMENTS2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Action)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("ACTION");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateXfer)
                .HasColumnType("datetime")
                .HasColumnName("DATE_XFER");
            entity.Property(e => e.JobCode)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("JOB_CODE");
            entity.Property(e => e.Pin)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("PIN");
            entity.Property(e => e.Xfer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("XFER");
            entity.Property(e => e.XferMessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("XFER_MESSAGE");
        });

        modelBuilder.Entity<LsImportJobCourse>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_IMPORT_JOB_COURSES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CourseCode)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("COURSE_CODE");
            entity.Property(e => e.CourseInterval)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("COURSE_INTERVAL");
            entity.Property(e => e.DateXfer)
                .HasColumnType("datetime")
                .HasColumnName("DATE_XFER");
            entity.Property(e => e.Grace)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("GRACE");
            entity.Property(e => e.JobDutyTask)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("JOB_DUTY_TASK");
            entity.Property(e => e.RequalModifier)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("REQUAL_MODIFIER");
            entity.Property(e => e.Xfer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("XFER");
            entity.Property(e => e.XferMessage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("XFER_MESSAGE");

            entity.HasOne(d => d.RequalModifierNavigation).WithMany(p => p.LsImportJobCourses)
                .HasForeignKey(d => d.RequalModifier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_IMPORT_JOB_COURSES_MODIF");
        });

        modelBuilder.Entity<LsLearnerLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LEARNER_LOGIN");

            entity.ToTable("LS_LEARNER_LOGIN");

            entity.HasIndex(e => e.FkLearner, "IX_LS_LEARNER_LOGIN_FK_LEARNER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPADDRESS");
            entity.Property(e => e.LoginDate)
                .HasColumnType("datetime")
                .HasColumnName("LOGIN_DATE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsLearnerLogins)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_LEARNER_LOGIN_FK_LEARNER");
        });

        modelBuilder.Entity<LsLearnerPosition>(entity =>
        {
            entity.HasKey(e => new { e.FkLearner, e.FkOrgPosition }).HasName("PK_LEARNER_POSITION");

            entity.ToTable("LS_LEARNER_POSITION");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkOrgPosition)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG_POSITION");
            entity.Property(e => e.ExpireDate)
                .HasColumnType("datetime")
                .HasColumnName("EXPIRE_DATE");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.Injob)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INJOB");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsLearnerPositions)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_LEARNER_POSITION_FK_COMPANY");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsLearnerPositions)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNER_POSITION_FK_LEARNER");

            entity.HasOne(d => d.FkOrgPositionNavigation).WithMany(p => p.LsLearnerPositions)
                .HasForeignKey(d => d.FkOrgPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNER_POSITION_FK_ORG_POS");
        });

        modelBuilder.Entity<LsLearnerPositionHist>(entity =>
        {
            entity.ToTable("LS_LEARNER_POSITION_HIST");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("ACTION_DATE");
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .HasColumnName("COMMENT");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkOrgPosition)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG_POSITION");
            entity.Property(e => e.Injob)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INJOB");
            entity.Property(e => e.Injobpool)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INJOBPOOL");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsLearnerPositionHists)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_LEARN_POSIT_HIST_FK_COMPANY");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsLearnerPositionHists)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARN_POSIT_HIST_FK_LEARNER");

            entity.HasOne(d => d.FkOrgPositionNavigation).WithMany(p => p.LsLearnerPositionHists)
                .HasForeignKey(d => d.FkOrgPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARN_POSIT_HIST_FK_ORG_POS");
        });

        modelBuilder.Entity<LsLearningEvent>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LEARNING_EVENT")
                .IsClustered(false);

            entity.ToTable("LS_LEARNING_EVENT");

            entity.HasIndex(e => new { e.Id, e.Title, e.DateCreated }, "IX_LS_LEARNING_EVENT").HasFillFactor(90);

            entity.HasIndex(e => new { e.Id, e.StartDate, e.EndDate, e.FkInstructor }, "IX_LS_LEARNING_EVENT_2");

            entity.HasIndex(e => e.FkCatalog, "IX_LS_LEARNING_EVENT_3");

            entity.HasIndex(e => new { e.Id, e.FkInstructor, e.StartDate, e.EndDate }, "IX_LS_LEARNING_EVENT_4");

            entity.HasIndex(e => new { e.Id, e.EndDate, e.StartDate, e.FkInstructor }, "IX_LS_LEARNING_EVENT_5");

            entity.HasIndex(e => new { e.StartDate, e.FkCatalog }, "IX_LS_LEARNING_EVENT_6");

            entity.HasIndex(e => new { e.Title, e.Id }, "NDX_LS_LEARNING_EVENT1");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AllowSelfWithdrawal)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ALLOW_SELF_WITHDRAWAL");
            entity.Property(e => e.Approved)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("APPROVED");
            entity.Property(e => e.CurrentCtrlDate)
                .HasColumnType("datetime")
                .HasColumnName("CURRENT_CTRL_DATE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DaysNotify)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("DAYS_NOTIFY");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.EnforcePrereqs)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENFORCE_PREREQS");
            entity.Property(e => e.EvalRequired)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EVAL_REQUIRED");
            entity.Property(e => e.EventType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("EVENT_TYPE");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.FkInstructor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_INSTRUCTOR");
            entity.Property(e => e.NextCtrlDate)
                .HasColumnType("datetime")
                .HasColumnName("NEXT_CTRL_DATE");
            entity.Property(e => e.PassingScore)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("PASSING_SCORE");
            entity.Property(e => e.QuestionPoolType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("QUESTION_POOL_TYPE");
            entity.Property(e => e.RegType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("REG_TYPE");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.TestType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TEST_TYPE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(1100)
                .HasColumnName("TITLE");
            entity.Property(e => e.Urgent)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("URGENT");
            entity.Property(e => e.WeightedCourse)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("WEIGHTED_COURSE");

            entity.HasOne(d => d.FkCatalogNavigation).WithMany(p => p.LsLearningEvents)
                .HasForeignKey(d => d.FkCatalog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVENT_FK_CATALOG");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsLearningEventFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .HasConstraintName("FK_LEARNING_EVENT_CREATED_BY");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.LsLearningEvents)
                .HasForeignKey(d => d.FkEval)
                .HasConstraintName("FK_LEARNING_EVENT_FK_EVAL");

            entity.HasOne(d => d.FkInstructorNavigation).WithMany(p => p.LsLearningEventFkInstructorNavigations)
                .HasForeignKey(d => d.FkInstructor)
                .HasConstraintName("FK_LEARNING_EVENT_FK_INSTRUCTR");

            entity.HasOne(d => d.TestTypeNavigation).WithMany(p => p.LsLearningEvents)
                .HasForeignKey(d => d.TestType)
                .HasConstraintName("FK_LEARNING_EVENT_TEST_TYPE");

            entity.HasMany(d => d.FkOrgs).WithMany(p => p.FkLearningEvents)
                .UsingEntity<Dictionary<string, object>>(
                    "LsEventPublicOrg",
                    r => r.HasOne<LsOrg>().WithMany()
                        .HasForeignKey("FkOrg")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EVENT_PUBLIC_ORG_FK_ORG"),
                    l => l.HasOne<LsLearningEvent>().WithMany()
                        .HasForeignKey("FkLearningEvent")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EVENT_PUBLIC_ORG_LRNG_EVENT"),
                    j =>
                    {
                        j.HasKey("FkLearningEvent", "FkOrg").IsClustered(false);
                        j.ToTable("LS_EVENT_PUBLIC_ORG");
                        j.IndexerProperty<decimal>("FkLearningEvent")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_LEARNING_EVENT");
                        j.IndexerProperty<decimal>("FkOrg")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_ORG");
                    });
        });

        modelBuilder.Entity<LsLearningEventAttempt>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkProgram, e.FkLearner })
                .HasName("PK_LEARNING_EVENT_ATTEMPTS")
                .IsClustered(false);

            entity.ToTable("LS_LEARNING_EVENT_ATTEMPTS");

            entity.HasIndex(e => e.FkLearner, "IX_LS_LEARNING_EVENT_ATTEMPTS").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkProgram, e.FkLearner, e.FkLearningEvent }, "IX_LS_LEARNING_EVENT_ATTEMPT_2").IsClustered();

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkProgram, e.FkLearner }, "IX_LS_LE_ATTEMPTS");

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Attempts)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ATTEMPTS");
            entity.Property(e => e.TrysToComplete)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TRYS_TO_COMPLETE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsLearningEventAttempts)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_LEARNING_EVT_ATT_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsLearningEventAttempts)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_ATT_FK_L_EVENT");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsLearningEventAttempts)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_ATT_FK_PROGRAM");
        });

        modelBuilder.Entity<LsLearningEventLearner>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkLearnerValue }).HasName("PK_LEARNING_EVENT_LEARNER");

            entity.ToTable("LS_LEARNING_EVENT_LEARNER");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkLearnerType, e.FkLearnerValue }, "IX_LS_LEARNING_EVENT_LEARNER").HasFillFactor(90);

            entity.HasIndex(e => new { e.FkLearnerValue, e.Status, e.FkLearningEvent }, "IX_LS_LEARNING_EVENT_LEARNER_2");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkLearnerValue }, "IX_LS_LEARNING_EVENT_LEARNER_4");

            entity.HasIndex(e => new { e.FkLearnerValue, e.FkLearningEvent, e.Status }, "IX_LS_LEARNING_EVENT_LEARNER_5");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkLearnerValue }, "IX_LS_LEARNING_EVENT_LEARNER_6");

            entity.HasIndex(e => new { e.FkLearnerValue, e.FkLearningEvent, e.Status }, "IX_LS_LEARNING_EVENT_LEARNER_7");

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkLearnerValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER_VALUE");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.EventPassingScore)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("EVENT_PASSING_SCORE");
            entity.Property(e => e.EventScore)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("EVENT_SCORE");
            entity.Property(e => e.EventWeighted)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EVENT_WEIGHTED");
            entity.Property(e => e.FkLearnerType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER_TYPE");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkLearnerTypeNavigation).WithMany(p => p.LsLearningEventLearners)
                .HasForeignKey(d => d.FkLearnerType)
                .HasConstraintName("FK_LEARNING_EVT_LRN_FK_LRN_TY");

            entity.HasOne(d => d.FkLearnerValueNavigation).WithMany(p => p.LsLearningEventLearners)
                .HasForeignKey(d => d.FkLearnerValue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_LRN_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsLearningEventLearners)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_LRN_FK_L_EVENT");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.LsLearningEventLearners)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_LRN_STATUS");
        });

        modelBuilder.Entity<LsLearningEventProgram>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkProgram })
                .HasName("PK_LEARNING_EVENT_PROGRAM")
                .IsClustered(false);

            entity.ToTable("LS_LEARNING_EVENT_PROGRAM");

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkProgram, e.Sequence, e.DateBegin, e.DateExpire }, "IX_LS_LEARNING_EVENT_PROGRAM").HasFillFactor(90);

            entity.HasIndex(e => e.FkLearningEvent, "IX_LS_LEARNING_EVENT_PROGRAM_1").HasFillFactor(90);

            entity.HasIndex(e => e.FkLearningEvent, "IX_LS_LEARNING_EVENT_PROGRAM_2").HasFillFactor(90);

            entity.HasIndex(e => new { e.FkProgram, e.FkLearningEvent }, "IX_LS_LEARNING_EVENT_PROGRAM_3").IsClustered();

            entity.HasIndex(e => new { e.Sequence, e.FkLearningEvent }, "IX_LS_LEARNING_EVENT_PROGRAM_4");

            entity.HasIndex(e => e.FkProgram, "IX_LS_LEARNING_EVENT_PROGRAM_5");

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.DateBegin)
                .HasColumnType("datetime")
                .HasColumnName("DATE_BEGIN");
            entity.Property(e => e.DateExpire)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRE");
            entity.Property(e => e.DateFinish)
                .HasColumnType("datetime")
                .HasColumnName("DATE_FINISH");
            entity.Property(e => e.FkCatalogRating)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG_RATING");
            entity.Property(e => e.FkEval)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVAL");
            entity.Property(e => e.FkInstructor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_INSTRUCTOR");
            entity.Property(e => e.ObjectiveOrder)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("OBJECTIVE_ORDER");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.PreTest)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PRE_TEST");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.TestType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TEST_TYPE");
            entity.Property(e => e.TrysToComplete)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TRYS_TO_COMPLETE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.Weight)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("WEIGHT");

            entity.HasOne(d => d.FkCatalogRatingNavigation).WithMany(p => p.LsLearningEventPrograms)
                .HasForeignKey(d => d.FkCatalogRating)
                .HasConstraintName("FK_LEARNING_EVT_PG_FK_CAT_RATI");

            entity.HasOne(d => d.FkEvalNavigation).WithMany(p => p.LsLearningEventPrograms)
                .HasForeignKey(d => d.FkEval)
                .HasConstraintName("FK_LEARNING_EVT_PG_FK_EVAL");

            entity.HasOne(d => d.FkInstructorNavigation).WithMany(p => p.LsLearningEventPrograms)
                .HasForeignKey(d => d.FkInstructor)
                .HasConstraintName("FK_LEARNING_EVT_PG_FK_INSTRCTR");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsLearningEventPrograms)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_PG_FK_L_EVENT");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsLearningEventPrograms)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_PG_FK_PROGRAM");
        });

        modelBuilder.Entity<LsLearningEventTrack>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkProgram, e.FkLearner })
                .HasName("PK_LEARNING_EVENT_TRACK")
                .IsClustered(false);

            entity.ToTable("LS_LEARNING_EVENT_TRACK");

            entity.HasIndex(e => e.FkLearner, "IX_LS_LEARNING_EVENT_TRACK_1").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkLearningEvent, e.FkProgram, e.FkLearner }, "IX_LS_LEARNING_EVENT_TRACK_2").IsClustered();

            entity.HasIndex(e => e.FkInstructor, "NDX_LS_LEARNING_EVENT_TRACK1");

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkCatalogRating)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG_RATING");
            entity.Property(e => e.FkExamResults)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_RESULTS");
            entity.Property(e => e.FkInstructor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_INSTRUCTOR");
            entity.Property(e => e.ManualScoreComments).HasColumnName("MANUAL_SCORE_COMMENTS");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("SCORE");
            entity.Property(e => e.TuPassingScore)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("TU_PASSING_SCORE");
            entity.Property(e => e.TuStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TU_STATUS");
            entity.Property(e => e.TuWeight)
                .HasColumnType("numeric(6, 3)")
                .HasColumnName("TU_WEIGHT");

            entity.HasOne(d => d.FkCatalogRatingNavigation).WithMany(p => p.LsLearningEventTracks)
                .HasForeignKey(d => d.FkCatalogRating)
                .HasConstraintName("FK_LEARNING_EVT_TK_FK_CAT_RATI");

            entity.HasOne(d => d.FkExamResultsNavigation).WithMany(p => p.LsLearningEventTracks)
                .HasForeignKey(d => d.FkExamResults)
                .HasConstraintName("FK_LEARNING_EVT_TK_FK_EXAM_RES");

            entity.HasOne(d => d.FkInstructorNavigation).WithMany(p => p.LsLearningEventTrackFkInstructorNavigations)
                .HasForeignKey(d => d.FkInstructor)
                .HasConstraintName("FK_LS_LEARNING_EVENT_TRK_INST");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsLearningEventTrackFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_TK_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsLearningEventTracks)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_TK_FK_L_EVENT");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsLearningEventTracks)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_TK_FK_PROGRAM");

            entity.HasOne(d => d.TuStatusNavigation).WithMany(p => p.LsLearningEventTracks)
                .HasForeignKey(d => d.TuStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LEARNING_EVT_TK_TU_STATUS");
        });

        modelBuilder.Entity<LsLoginAttempt>(entity =>
        {
            entity.HasKey(e => e.FkLearner).HasName("PK_LOGIN_ATTEMPTS");

            entity.ToTable("LS_LOGIN_ATTEMPTS");

            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Attempt)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("ATTEMPT");
            entity.Property(e => e.LastAttempt)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("LAST_ATTEMPT");

            entity.HasOne(d => d.FkLearnerNavigation).WithOne(p => p.LsLoginAttempt)
                .HasForeignKey<LsLoginAttempt>(d => d.FkLearner)
                .HasConstraintName("FK_LOGIN_ATTEMPTS_FK_LEARNER");
        });

        modelBuilder.Entity<LsMediaType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_MEDIA_TYPE");

            entity.ToTable("LS_MEDIA_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Status)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsObjExternalTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OBJ_EXTERNAL_TRACK");

            entity.ToTable("LS_OBJ_EXTERNAL_TRACK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ApiData).HasColumnName("API_DATA");
            entity.Property(e => e.CommentsFromLms)
                .HasMaxLength(250)
                .HasColumnName("COMMENTS_FROM_LMS");
            entity.Property(e => e.Datelastmodified)
                .HasColumnType("datetime")
                .HasColumnName("DATELASTMODIFIED");
            entity.Property(e => e.FkEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVENT");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");

            entity.HasOne(d => d.FkEventNavigation).WithMany(p => p.LsObjExternalTracks)
                .HasForeignKey(d => d.FkEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_EXTERNAL_TK_FK_EVENT");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsObjExternalTracks)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_EXTERNAL_TK_FK_LEARNER");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsObjExternalTracks)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_EXTERNAL_TK_FK_OBJECTIV");
        });

        modelBuilder.Entity<LsObjectiveMastery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OBJECTIVE_MASTERY");

            entity.ToTable("LS_OBJECTIVE_MASTERY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.CognitiveCorrectQuestions)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("COGNITIVE_CORRECT_QUESTIONS");
            entity.Property(e => e.CognitiveUniqueQuestions)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("COGNITIVE_UNIQUE_QUESTIONS");
            entity.Property(e => e.ContentViewed)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("CONTENT_VIEWED");
            entity.Property(e => e.DateLastUpdated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_LAST_UPDATED");
            entity.Property(e => e.Exempt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXEMPT");
            entity.Property(e => e.ExemptComments)
                .HasMaxLength(500)
                .HasColumnName("EXEMPT_COMMENTS");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.PerformanceVerified)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PERFORMANCE_VERIFIED");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsObjectiveMasteries)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIV_MASTERY_FK_LEARNER");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsObjectiveMasteries)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIV_MASTERY_FK_OBJECTI");
        });

        modelBuilder.Entity<LsObjectiveTrack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OBJECTIVE_TRACK");

            entity.ToTable("LS_OBJECTIVE_TRACK");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.HasIndex(e => new { e.FkObjective, e.FkEvent, e.FkLearner, e.FkProgram }, "IX_LS_OBJECTIVE_TRACK");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateLastViewed)
                .HasColumnType("datetime")
                .HasColumnName("DATE_LAST_VIEWED");
            entity.Property(e => e.DateMastered)
                .HasColumnType("datetime")
                .HasColumnName("DATE_MASTERED");
            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.FkEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVENT");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkProgramImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_IMPL");
            entity.Property(e => e.LessonType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("LESSON_TYPE");
            entity.Property(e => e.Marked)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MARKED");
            entity.Property(e => e.Mastered)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MASTERED");
            entity.Property(e => e.ObjectiveDoclinks)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJECTIVE_DOCLINKS");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.TuDoclinks)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TU_DOCLINKS");
            entity.Property(e => e.Viewed)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("VIEWED");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkContent)
                .HasConstraintName("FK_OBJECTIVE_TRACK_FK_CONTENT");

            entity.HasOne(d => d.FkEventNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TRACK_FK_EVENT");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TRACK_FK_LEARNER");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TRACK_FK_OBJECTIV");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TRACK_FK_PROGRAM");

            entity.HasOne(d => d.FkProgramImplNavigation).WithMany(p => p.LsObjectiveTracks)
                .HasForeignKey(d => d.FkProgramImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_TRACK_FK_PROGRAM_IMPL");
        });

        modelBuilder.Entity<LsOnlineExamQuestion>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.FkProgram, e.FkLearner }).HasName("PK_ONLINE_EXAM_QUESTION");

            entity.ToTable("LS_ONLINE_EXAM_QUESTION");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkObjectiveParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_PARENT");
            entity.Property(e => e.FkQuestionParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_PARENT");
            entity.Property(e => e.Flag)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("FLAG");
            entity.Property(e => e.IsSubquestion)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQUESTION");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_TYPE");
            entity.Property(e => e.Response)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE");
            entity.Property(e => e.ResponseType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE_TYPE");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("SCORE");
            entity.Property(e => e.VdmSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("VDM_SEQUENCE");
            entity.Property(e => e.VlsSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("VLS_SEQUENCE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.LsOnlineExamQuestions)
                .HasForeignKey(d => d.FkExam)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsOnlineExamQuestions)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_LEARNR");

            entity.HasOne(d => d.FkObjectiveParentNavigation).WithMany(p => p.LsOnlineExamQuestions)
                .HasForeignKey(d => d.FkObjectiveParent)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_OBJ");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsOnlineExamQuestions)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_PROGRM");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsOnlineExamQuestionFkQuestionNavigations)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_QQ");

            entity.HasOne(d => d.FkQuestionParentNavigation).WithMany(p => p.LsOnlineExamQuestionFkQuestionParentNavigations)
                .HasForeignKey(d => d.FkQuestionParent)
                .HasConstraintName("FK_ONLINE_EXAM_QUEST_FK_SC_QQ");
        });

        modelBuilder.Entity<LsOnlineExamQuestionMa>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.FkProgram, e.FkLearner, e.ItemSequence }).HasName("PK_ONLINE_EXAM_QUESTION_MA");

            entity.ToTable("LS_ONLINE_EXAM_QUESTION_MA");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.ItemSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ITEM_SEQUENCE");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Response)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.LsOnlineExamQuestionMas)
                .HasForeignKey(d => d.FkExam)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MA_FK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsOnlineExamQuestionMas)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MA_FK_LEARNR");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsOnlineExamQuestionMas)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MA_FK_PROGRM");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsOnlineExamQuestionMas)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MA_FK_QQ");
        });

        modelBuilder.Entity<LsOnlineExamQuestionMc>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.FkProgram, e.FkLearner, e.PositionOnExam }).HasName("PK_ONLINE_EXAM_QUESTION_MC");

            entity.ToTable("LS_ONLINE_EXAM_QUESTION_MC");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.PositionOnExam)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("POSITION_ON_EXAM");
            entity.Property(e => e.SequenceOnQuestionMc)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE_ON_QUESTION_MC");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsOnlineExamQuestionMcs)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MC_FK_LEARNR");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsOnlineExamQuestionMcs)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MC_FK_PROGRM");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsOnlineExamQuestionMcs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ONLINE_EXAM_QQ_MC_FK_QQ");
        });

        modelBuilder.Entity<LsOrg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ORG");

            entity.ToTable("LS_ORG");

            entity.HasIndex(e => e.FkCompany, "IX_LS_ORG_COMPANY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(30)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkOrgLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG_LEVEL");
            entity.Property(e => e.FkOrgStatus)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG_STATUS");
            entity.Property(e => e.IsJob)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_JOB");
            entity.Property(e => e.PersonnelRequired)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PERSONNEL_REQUIRED");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(100)
                .HasColumnName("USER_DEFINED_ID");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsOrgs)
                .HasForeignKey(d => d.FkCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_FK_COMPANY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsOrgs)
                .HasForeignKey(d => d.FkCreatedBy)
                .HasConstraintName("FK_ORG_FK_CREATED_BY");

            entity.HasOne(d => d.FkOrgLevelNavigation).WithMany(p => p.LsOrgs)
                .HasForeignKey(d => d.FkOrgLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_FK_ORG_LEVEL");

            entity.HasMany(d => d.FkProjects).WithMany(p => p.FkStructures)
                .UsingEntity<Dictionary<string, object>>(
                    "LsOrgToPanel",
                    r => r.HasOne<Project>().WithMany()
                        .HasForeignKey("FkProject")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ORG_TO_PANELS_FK_PROJECT"),
                    l => l.HasOne<LsOrg>().WithMany()
                        .HasForeignKey("FkStructure")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ORG_TO_PANELS_FK_ORG"),
                    j =>
                    {
                        j.HasKey("FkStructure", "FkProject").HasName("PK_ORG_TO_PANELS");
                        j.ToTable("LS_ORG_TO_PANELS");
                        j.IndexerProperty<decimal>("FkStructure")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_STRUCTURE");
                        j.IndexerProperty<decimal>("FkProject")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_PROJECT");
                    });
        });

        modelBuilder.Entity<LsOrgHierarchy>(entity =>
        {
            entity.HasKey(e => e.FkChild).HasName("PK_ORG_HIERARCHY");

            entity.ToTable("LS_ORG_HIERARCHY");

            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkChildNavigation).WithOne(p => p.LsOrgHierarchyFkChildNavigation)
                .HasForeignKey<LsOrgHierarchy>(d => d.FkChild)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_HIERARCHY_FK_CHILD");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsOrgHierarchies)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_ORG_HIERARCHY_FK_COMPANY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.LsOrgHierarchyFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .HasConstraintName("FK_ORG_HIERARCHY_FK_PARENT");
        });

        modelBuilder.Entity<LsOrgLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ORG_LEVEL");

            entity.ToTable("LS_ORG_LEVEL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.Icon).HasColumnName("ICON");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsOrgLevels)
                .HasForeignKey(d => d.FkCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_LEVEL_FK_COMPANY");
        });

        modelBuilder.Entity<LsOrgPanelsTopNode>(entity =>
        {
            entity.HasKey(e => new { e.FkStructure, e.FkHierarchy, e.FkProject, e.Type }).HasName("PK_ORG_PANELS_TOP_NODE");

            entity.ToTable("LS_ORG_PANELS_TOP_NODE");

            entity.Property(e => e.FkStructure)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_STRUCTURE");
            entity.Property(e => e.FkHierarchy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_HIERARCHY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.LsOrgPanelsTopNodes)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_PANELS_TOP_NODE_PROJECT");

            entity.HasOne(d => d.FkStructureNavigation).WithMany(p => p.LsOrgPanelsTopNodes)
                .HasForeignKey(d => d.FkStructure)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORG_PANELS_TOP_NODE_FK_ORG");
        });

        modelBuilder.Entity<LsPaEvaluatorTrainer>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.FkAnalysisObjective, e.FkLearner, e.FkLsTuItemType }).HasName("PK_PA_EVALUATOR_TRAINER");

            entity.ToTable("LS_PA_EVALUATOR_TRAINER");

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkAnalysisObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_OBJECTIVE");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLsTuItemType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TU_ITEM_TYPE");
            entity.Property(e => e.IsEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_EVALUATOR");
            entity.Property(e => e.IsTrainer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRAINER");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsPaEvaluatorTrainers)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_EVALUATOR_TRN_FK_LEARNER");

            entity.HasOne(d => d.FkLsTuItemTypeNavigation).WithMany(p => p.LsPaEvaluatorTrainers)
                .HasForeignKey(d => d.FkLsTuItemType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_EVALUATOR_TRN_ITEM_TYPE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsPaEvaluatorTrainers)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_EVALUATOR_TRN_FK_PROGRAM");
        });

        modelBuilder.Entity<LsPaOjeRequest>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysisObjective, e.FkLearner, e.FkEvaluator, e.FkLsTuItemType }).HasName("PK_PA_OJE_REQUEST");

            entity.ToTable("LS_PA_OJE_REQUEST");

            entity.Property(e => e.FkAnalysisObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_OBJECTIVE");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkEvaluator)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVALUATOR");
            entity.Property(e => e.FkLsTuItemType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TU_ITEM_TYPE");
            entity.Property(e => e.FkEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVENT");
            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.IsEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_EVALUATOR");
            entity.Property(e => e.IsTrainer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRAINER");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("REQUEST_DATE");
            entity.Property(e => e.Session)
                .HasMaxLength(10)
                .HasColumnName("SESSION");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkEvaluatorNavigation).WithMany(p => p.LsPaOjeRequestFkEvaluatorNavigations)
                .HasForeignKey(d => d.FkEvaluator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_OJE_REQUEST_FK_EVALUATOR");

            entity.HasOne(d => d.FkEventNavigation).WithMany(p => p.LsPaOjeRequests)
                .HasForeignKey(d => d.FkEvent)
                .HasConstraintName("FK_PA_OJE_REQUEST_FK_EVENT");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsPaOjeRequestFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_OJE_REQUEST_FK_LEARNER");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsPaOjeRequests)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_PA_OJE_REQUEST_FK_QUAL_CARD");

            entity.HasOne(d => d.FkLsTuItemTypeNavigation).WithMany(p => p.LsPaOjeRequests)
                .HasForeignKey(d => d.FkLsTuItemType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PA_OJE_REQUEST_TU_ITEM_TYPE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsPaOjeRequests)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_PA_OJE_REQUEST_FK_PROGRAM");
        });

        modelBuilder.Entity<LsPasswordStorage>(entity =>
        {
            entity.HasKey(e => new { e.FkLearner, e.OldPassword });

            entity.ToTable("LS_PASSWORD_STORAGE");

            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.OldPassword)
                .HasMaxLength(100)
                .HasColumnName("OLD_PASSWORD");
            entity.Property(e => e.ArchiveDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("ARCHIVE_DATE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsPasswordStorages)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_PASSWORD_STORAGE_FK_LEARNER");
        });

        modelBuilder.Entity<LsPerformanceAssessment>(entity =>
        {
            entity.HasKey(e => e.FkCatalogLessons).HasName("PK_PERFORMANCE_ASSESSMENT");

            entity.ToTable("LS_PERFORMANCE_ASSESSMENT");

            entity.Property(e => e.FkCatalogLessons)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG_LESSONS");
            entity.Property(e => e.EvaluatorType)
                .HasDefaultValue(4m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("EVALUATOR_TYPE");
            entity.Property(e => e.SameTrainerEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SAME_TRAINER_EVALUATOR");
            entity.Property(e => e.SupervisorEvalException)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SUPERVISOR_EVAL_EXCEPTION");
            entity.Property(e => e.TrainerType)
                .HasDefaultValue(200m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TRAINER_TYPE");

            entity.HasOne(d => d.EvaluatorTypeNavigation).WithMany(p => p.LsPerformanceAssessmentEvaluatorTypeNavigations)
                .HasForeignKey(d => d.EvaluatorType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERF_ASSMT_EVALUATOR_TYPE");

            entity.HasOne(d => d.FkCatalogLessonsNavigation).WithOne(p => p.LsPerformanceAssessment)
                .HasForeignKey<LsPerformanceAssessment>(d => d.FkCatalogLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERF_ASSMT_FK_CAT_LESSONS");

            entity.HasOne(d => d.TrainerTypeNavigation).WithMany(p => p.LsPerformanceAssessmentTrainerTypeNavigations)
                .HasForeignKey(d => d.TrainerType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERF_ASSMT_TRAINER_TYPE");
        });

        modelBuilder.Entity<LsPracticeExam>(entity =>
        {
            entity.HasKey(e => new { e.Vlskey, e.FkQuestion, e.SelectionOrder }).HasName("PK_PRACTICE_EXAM");

            entity.ToTable("LS_PRACTICE_EXAM");

            entity.Property(e => e.Vlskey)
                .HasMaxLength(50)
                .HasColumnName("VLSKEY");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.SelectionOrder)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SELECTION_ORDER");
            entity.Property(e => e.DateCached)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CACHED");
            entity.Property(e => e.IsSubq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQ");
            entity.Property(e => e.QuestionPoints)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("QUESTION_POINTS");
            entity.Property(e => e.QuestionScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("QUESTION_SCORE");
            entity.Property(e => e.QuestionType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_TYPE");
            entity.Property(e => e.ResponseType)
                .HasDefaultValue(4m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE_TYPE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsPracticeExams)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRACTICE_EXAM_FK_QUESTION");
        });

        modelBuilder.Entity<LsPracticeExamCo>(entity =>
        {
            entity.HasKey(e => new { e.Vlskey, e.FkQuestion, e.Sequence }).HasName("PK_PRACTICE_EXAM_DO");

            entity.ToTable("LS_PRACTICE_EXAM_CO");

            entity.Property(e => e.Vlskey)
                .HasMaxLength(50)
                .HasColumnName("VLSKEY");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCached)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CACHED");
            entity.Property(e => e.QuestionPosition)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("QUESTION_POSITION");
            entity.Property(e => e.Selected)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SELECTED");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsPracticeExamCos)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRACTICE_EXAM_DO_FK_QUESTIN");
        });

        modelBuilder.Entity<LsPracticeExamMa>(entity =>
        {
            entity.HasKey(e => new { e.Vlskey, e.FkQuestion, e.ItemSequence }).HasName("PK_PRACTICE_EXAM_MA");

            entity.ToTable("LS_PRACTICE_EXAM_MA");

            entity.Property(e => e.Vlskey)
                .HasMaxLength(50)
                .HasColumnName("VLSKEY");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.ItemSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ITEM_SEQUENCE");
            entity.Property(e => e.DateCached)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CACHED");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Response1)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE1");
            entity.Property(e => e.Response2)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE2");
            entity.Property(e => e.Response3)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("RESPONSE3");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsPracticeExamMas)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRACTICE_EXAM_MA_FK_QUESTIN");
        });

        modelBuilder.Entity<LsPracticeExamTf>(entity =>
        {
            entity.HasKey(e => new { e.Vlskey, e.FkQuestion }).HasName("PK_PRACTICE_EXAM_TF");

            entity.ToTable("LS_PRACTICE_EXAM_TF");

            entity.Property(e => e.Vlskey)
                .HasMaxLength(50)
                .HasColumnName("VLSKEY");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateCached)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CACHED");
            entity.Property(e => e.Response)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.LsPracticeExamTfs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRACTICE_EXAM_TF_FK_QUESTIN");
        });

        modelBuilder.Entity<LsPreviewExam>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkProgram, e.FkLearner }).HasName("PK_PREVIEW_EXAM");

            entity.ToTable("LS_PREVIEW_EXAM");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.LsPreviewExams)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PREVIEW_EXAM_FK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsPreviewExams)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PREVIEW_EXAM_FK_LEARNER");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsPreviewExams)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PREVIEW_EXAM_FK_PROGRAM");
        });

        modelBuilder.Entity<LsProctorSession>(entity =>
        {
            entity.HasKey(e => new { e.FkLearner, e.SessionCode, e.SessionStart });

            entity.ToTable("LS_PROCTOR_SESSION");

            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.SessionCode)
                .HasMaxLength(12)
                .HasColumnName("SESSION_CODE");
            entity.Property(e => e.SessionStart)
                .HasColumnType("datetime")
                .HasColumnName("SESSION_START");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Duration)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("DURATION");
            entity.Property(e => e.SessionEnd)
                .HasColumnType("datetime")
                .HasColumnName("SESSION_END");
            entity.Property(e => e.SessionName)
                .HasMaxLength(200)
                .HasColumnName("SESSION_NAME");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsProctorSessions)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRCTOR_SESS_FK_LEARNER");
        });

        modelBuilder.Entity<LsProfileEvalType>(entity =>
        {
            entity.HasKey(e => e.FkExamOnlineProfile).HasName("PK_PROFILE_EVAL_TYPES");

            entity.ToTable("LS_PROFILE_EVAL_TYPES");

            entity.Property(e => e.FkExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.ClrId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("CLR_ID");
            entity.Property(e => e.RatingScaleId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("RATING_SCALE_ID");
            entity.Property(e => e.ReasonCodeId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("REASON_CODE_ID");

            entity.HasOne(d => d.Clr).WithMany(p => p.LsProfileEvalTypeClrs)
                .HasForeignKey(d => d.ClrId)
                .HasConstraintName("FK_PROFILE_EVAL_TYPE_CLR");

            entity.HasOne(d => d.FkExamOnlineProfileNavigation).WithOne(p => p.LsProfileEvalType)
                .HasForeignKey<LsProfileEvalType>(d => d.FkExamOnlineProfile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROFILE_EVAL_TYPE_EXM_OL_PR");

            entity.HasOne(d => d.RatingScale).WithMany(p => p.LsProfileEvalTypeRatingScales)
                .HasForeignKey(d => d.RatingScaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROFILE_EVAL_TYPE_RESULT_RS");

            entity.HasOne(d => d.ReasonCode).WithMany(p => p.LsProfileEvalTypeReasonCodes)
                .HasForeignKey(d => d.ReasonCodeId)
                .HasConstraintName("FK_PROFILE_EVAL_TYPE_RESULT_RC");
        });

        modelBuilder.Entity<LsProgramCompletion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROGRAM_COMPLETION");

            entity.ToTable("LS_PROGRAM_COMPLETION");

            entity.HasIndex(e => e.FkProgram, "NDX_LS_PROGRAM_COMPLETION4");

            entity.HasIndex(e => new { e.FkProgram, e.DateCompleted, e.Archive, e.Status }, "NDX_LS_PROGRAM_COMPLETION_1");

            entity.HasIndex(e => new { e.FkLearner, e.DateCompleted, e.Archive, e.Status }, "NDX_LS_PROGRAM_COMPLETION_2");

            entity.HasIndex(e => new { e.FkLearningEvent, e.DateCompleted, e.Archive, e.Status }, "NDX_LS_PROGRAM_COMPLETION_3");

            entity.HasIndex(e => new { e.FkLearner, e.FkProgram }, "NDX_LS_PROGRAM_COMPLETION_4");

            entity.HasIndex(e => new { e.FkLearner, e.Status, e.DateCompleted }, "NDX_LS_PROGRAM_COMPLETION_5");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.Exempt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXEMPT");
            entity.Property(e => e.ExemptComments)
                .HasMaxLength(500)
                .HasColumnName("EXEMPT_COMMENTS");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQualEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_EVENT");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkCatalogNavigation).WithMany(p => p.LsProgramCompletions)
                .HasForeignKey(d => d.FkCatalog)
                .HasConstraintName("FK_PROGRAM_COMP_FK_CATALOG");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsProgramCompletions)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_COMP_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsProgramCompletions)
                .HasForeignKey(d => d.FkLearningEvent)
                .HasConstraintName("FK_PROGRAM_COMP_FK_LEARNING_EV");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsProgramCompletions)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_PROGRAM_COMP_FK_PROGRAM");

            entity.HasOne(d => d.FkQualEventNavigation).WithMany(p => p.LsProgramCompletions)
                .HasForeignKey(d => d.FkQualEvent)
                .HasConstraintName("FK_PROGRAM_COMP_FK_QUAL_EVENT");
        });

        modelBuilder.Entity<LsQualCard>(entity =>
        {
            entity.ToTable("LS_QUAL_CARD");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Approved)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("APPROVED");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.AutomaticRouting)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("AUTOMATIC_ROUTING");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(30)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.EvaluatorType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("EVALUATOR_TYPE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkLsTimeToComplete)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TIME_TO_COMPLETE");
            entity.Property(e => e.FkModifiedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_MODIFIED_BY");
            entity.Property(e => e.FkOrg)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORG");
            entity.Property(e => e.FkOriginalLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ORIGINAL_LS_QUAL_CARD");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQualCardImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD_IMPL");
            entity.Property(e => e.FkRequal)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_REQUAL");
            entity.Property(e => e.GPeriod)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("G_PERIOD");
            entity.Property(e => e.RouteSupervisor)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ROUTE_SUPERVISOR");
            entity.Property(e => e.SameTrainerEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SAME_TRAINER_EVALUATOR");
            entity.Property(e => e.SupervisorEvalException)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SUPERVISOR_EVAL_EXCEPTION");
            entity.Property(e => e.Taskevent)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TASKEVENT");
            entity.Property(e => e.Text)
                .HasMaxLength(125)
                .HasColumnName("TEXT");
            entity.Property(e => e.TextPrefix)
                .HasMaxLength(125)
                .HasColumnName("TEXT_PREFIX");
            entity.Property(e => e.TrainerType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TRAINER_TYPE");

            entity.HasOne(d => d.EvaluatorTypeNavigation).WithMany(p => p.LsQualCardEvaluatorTypeNavigations)
                .HasForeignKey(d => d.EvaluatorType)
                .HasConstraintName("FK_LS_QUAL_CARD_EVALUATOR_TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsQualCardFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_CREATED_BY");

            entity.HasOne(d => d.FkLsTimeToCompleteNavigation).WithMany(p => p.LsQualCardFkLsTimeToCompleteNavigations)
                .HasForeignKey(d => d.FkLsTimeToComplete)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_TIME_2_COMP");

            entity.HasOne(d => d.FkModifiedByNavigation).WithMany(p => p.LsQualCardFkModifiedByNavigations)
                .HasForeignKey(d => d.FkModifiedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_MODIFIED_BY");

            entity.HasOne(d => d.FkOrgNavigation).WithMany(p => p.LsQualCards)
                .HasForeignKey(d => d.FkOrg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_ORG");

            entity.HasOne(d => d.FkOriginalLsQualCardNavigation).WithMany(p => p.InverseFkOriginalLsQualCardNavigation)
                .HasForeignKey(d => d.FkOriginalLsQualCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_QUAL_CARD_ORG_QUAL_CARD");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsQualCards)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_PROGRAM");

            entity.HasOne(d => d.FkQualCardImplNavigation).WithMany(p => p.LsQualCards)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.FkQualCardImpl)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_QUAL_CARD");

            entity.HasOne(d => d.FkRequalNavigation).WithMany(p => p.LsQualCardFkRequalNavigations)
                .HasForeignKey(d => d.FkRequal)
                .HasConstraintName("FK_LS_QUAL_CARD_FK_REQUAL");

            entity.HasOne(d => d.TrainerTypeNavigation).WithMany(p => p.LsQualCardTrainerTypeNavigations)
                .HasForeignKey(d => d.TrainerType)
                .HasConstraintName("FK_LS_QUAL_CARD_TRAINER_TYPE");
        });

        modelBuilder.Entity<LsQualCardEvent>(entity =>
        {
            entity.ToTable("LS_QUAL_CARD_EVENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AiccSessionid)
                .HasMaxLength(50)
                .HasColumnName("AICC_SESSIONID");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.CompletedTrainingDate)
                .HasColumnType("datetime")
                .HasColumnName("COMPLETED_TRAINING_DATE");
            entity.Property(e => e.DateClose)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CLOSE");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.DateOpen)
                .HasColumnType("datetime")
                .HasColumnName("DATE_OPEN");
            entity.Property(e => e.DateToBeCompleteBy)
                .HasColumnType("datetime")
                .HasColumnName("DATE_TO_BE_COMPLETE_BY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkQualEventScore)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_EVENT_SCORE");
            entity.Property(e => e.FkTrainingEventScore)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TRAINING_EVENT_SCORE");
            entity.Property(e => e.OriginalDateClose)
                .HasColumnType("datetime")
                .HasColumnName("ORIGINAL_DATE_CLOSE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsQualCardEvents)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_QUAL_CARD_EVENT_FK_LEARNER");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualCardEvents)
                .HasForeignKey(d => d.FkLsQualCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_EVENT_LS_QUAL_CRD");

            entity.HasOne(d => d.FkQualEventScoreNavigation).WithMany(p => p.LsQualCardEventFkQualEventScoreNavigations)
                .HasForeignKey(d => d.FkQualEventScore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_EVENT_EVAL_SCORE");

            entity.HasOne(d => d.FkTrainingEventScoreNavigation).WithMany(p => p.LsQualCardEventFkTrainingEventScoreNavigations)
                .HasForeignKey(d => d.FkTrainingEventScore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_EVENT_TRNG_SCORE");
        });

        modelBuilder.Entity<LsQualCardItem>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.Sequence, e.FkAnalysis });

            entity.ToTable("LS_QUAL_CARD_ITEMS");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.LsQualCardItems)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("FK_LS_QUAL_CARD_ITEMS_ANALYSIS");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualCardItems)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_LS_QUAL_CARD_ITEMS_QUAL_CRD");
        });

        modelBuilder.Entity<LsQualCardPrerequisite>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.FkPrerequisite, e.Type });

            entity.ToTable("LS_QUAL_CARD_PREREQUISITE");

            entity.HasIndex(e => e.FkLsQualCard, "IX_LS_QUAL_CARD_PREREQUISITE");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkPrerequisite)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PREREQUISITE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.FkJobPosition)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_JOB_POSITION");

            entity.HasOne(d => d.FkJobPositionNavigation).WithMany(p => p.LsQualCardPrerequisites)
                .HasForeignKey(d => d.FkJobPosition)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LS_QUAL_CARD_PRE_FK_JOB_POS");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualCardPrerequisites)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_LS_QUAL_CARD_PRE_QUAL_CARD");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.LsQualCardPrerequisites)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("FK_LS_QUAL_CARD_PRE_TYPE");
        });

        modelBuilder.Entity<LsQualCardRoute>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.FkLearner });

            entity.ToTable("LS_QUAL_CARD_ROUTE");

            entity.HasIndex(e => e.FkLearner, "IX_LS_QUAL_CARD_ROUTE");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkJobPosition)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_JOB_POSITION");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkJobPositionNavigation).WithMany(p => p.LsQualCardRoutes)
                .HasForeignKey(d => d.FkJobPosition)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QUAL_CARD_ROUTE_JOB_POSITIN");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsQualCardRoutes)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_QUAL_CARD_ROUTE_FK_LEARNER");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualCardRoutes)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_QUAL_CARD_ROUTE_FK_QUAL_CRD");
        });

        modelBuilder.Entity<LsQualCardRouteHistory>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("LS_QUAL_CARD_ROUTE_HISTORY");

            entity.HasIndex(e => new { e.FkLsQualCard, e.FkLearner, e.Sequence, e.Archive }, "IX_LS_QUAL_CARD_ROUTE_HISTORY").IsClustered();

            entity.HasIndex(e => new { e.FkLearner, e.FkLsQualCard, e.Sequence, e.Archive }, "IX_LS_QUAL_CARD_ROUTE_HISTORY2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkSupervisor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SUPERVISOR");
            entity.Property(e => e.InRoutePath)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IN_ROUTE_PATH");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SCORE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsQualCardRouteHistoryFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_RT_HY_FK_LEARNER");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualCardRouteHistories)
                .HasForeignKey(d => d.FkLsQualCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_RT_HY_FK_QUAL_CRD");

            entity.HasOne(d => d.FkSupervisorNavigation).WithMany(p => p.LsQualCardRouteHistoryFkSupervisorNavigations)
                .HasForeignKey(d => d.FkSupervisor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_RT_HY_SUPERVISOR");
        });

        modelBuilder.Entity<LsQualEvaluatorTrainer>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.FkAnalysis, e.FkLearner });

            entity.ToTable("LS_QUAL_EVALUATOR_TRAINER");

            entity.HasIndex(e => e.FkLearner, "IX_LS_QUAL_EVALUATOR_TRAINER");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.IsEvaluator)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_EVALUATOR");
            entity.Property(e => e.IsTrainer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRAINER");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.LsQualEvaluatorTrainers)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("FK_QUAL_EVAL_TRN_FK_ANALYSIS");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsQualEvaluatorTrainers)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_EVAL_TRN_FK_LEARNER");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualEvaluatorTrainers)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_QUAL_EVAL_TRN_FK_QUAL_CARD");
        });

        modelBuilder.Entity<LsQualJobPosition>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.FkJobPosition });

            entity.ToTable("LS_QUAL_JOB_POSITIONS");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkJobPosition)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_JOB_POSITION");
            entity.Property(e => e.DateChanged)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CHANGED");
            entity.Property(e => e.LastModifiedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("LAST_MODIFIED_BY");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("STATUS");

            entity.HasOne(d => d.FkJobPositionNavigation).WithMany(p => p.LsQualJobPositions)
                .HasForeignKey(d => d.FkJobPosition)
                .HasConstraintName("FK_QUAL_JOB_POSIT_FK_JOB_POSIT");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsQualJobPositions)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_QUAL_JOB_POSIT_LS_QUAL_CARD");

            entity.HasOne(d => d.LastModifiedByNavigation).WithMany(p => p.LsQualJobPositions)
                .HasForeignKey(d => d.LastModifiedBy)
                .HasConstraintName("FK_QUAL_JOB_POSIT_MODIFIED_BY");
        });

        modelBuilder.Entity<LsRule>(entity =>
        {
            entity.HasKey(e => new { e.FkLsQualCard, e.FkRule });

            entity.ToTable("LS_RULE");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkRule)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RULE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsRules)
                .HasForeignKey(d => d.FkLsQualCard)
                .HasConstraintName("FK_LS_RULE_FK_QUAL_CARD");

            entity.HasOne(d => d.FkRuleNavigation).WithMany(p => p.LsRules)
                .HasForeignKey(d => d.FkRule)
                .HasConstraintName("FK_LS_RULE_FK_RULE");
        });

        modelBuilder.Entity<LsRuleEmail>(entity =>
        {
            entity.HasKey(e => e.FkLsQualCard);

            entity.ToTable("LS_RULE_EMAIL");

            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("EMAIL");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithOne(p => p.LsRuleEmail)
                .HasForeignKey<LsRuleEmail>(d => d.FkLsQualCard)
                .HasConstraintName("FK_LS_RULE_EMAIL_FK_QUAL_CARD");
        });

        modelBuilder.Entity<LsRuleItem>(entity =>
        {
            entity.ToTable("LS_RULE_ITEMS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Itemvalue)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("ITEMVALUE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<LsSecurityCode>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_SECURITY_CODE");

            entity.ToTable("LS_SECURITY_CODE");

            entity.HasIndex(e => new { e.IdValue, e.SecurityAccess }, "IX_LS_SECURITY_CODE").HasFillFactor(90);

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Comments)
                .HasMaxLength(50)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateBegin)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_BEGIN");
            entity.Property(e => e.DateExpire)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRE");
            entity.Property(e => e.SecurityAccess)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SECURITY_ACCESS");
        });

        modelBuilder.Entity<LsSelectedExam>(entity =>
        {
            entity.HasKey(e => new { e.FkLearningEvent, e.FkProgram, e.FkLearner });

            entity.ToTable("LS_SELECTED_EXAM");

            entity.HasIndex(e => e.FkLearner, "IX_FK_Learner").HasFillFactor(100);

            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.LsSelectedExams)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SELECTED_EXAM_FK_EXAM");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsSelectedExams)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_SELECTED_EXAM_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsSelectedExams)
                .HasForeignKey(d => d.FkLearningEvent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SELECTED_EXAM_LEARNING_EVNT");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsSelectedExams)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SELECTED_EXAM_FK_PROGRAM");
        });

        modelBuilder.Entity<LsStatus>(entity =>
        {
            entity.HasKey(e => e.IdValue);

            entity.ToTable("LS_STATUS");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsSurvey>(entity =>
        {
            entity.ToTable("LS_SURVEYS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkChangedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHANGED_BY");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkCourseType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COURSE_TYPE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkDifrangescale)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DIFRANGESCALE");
            entity.Property(e => e.FkJob)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_JOB");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("TITLE");
            entity.Property(e => e.Usercode)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("USERCODE");

            entity.HasOne(d => d.FkChangedByNavigation).WithMany(p => p.LsSurveyFkChangedByNavigations)
                .HasForeignKey(d => d.FkChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVEYS_FK_CHANGED_BY");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsSurveys)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_SURVEYS_FK_COMPANY");

            entity.HasOne(d => d.FkCourseTypeNavigation).WithMany(p => p.LsSurveys)
                .HasForeignKey(d => d.FkCourseType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVEYS_FK_COURSE_TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsSurveyFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVEYS_FK_CREATED_BY");

            entity.HasOne(d => d.FkDifrangescaleNavigation).WithMany(p => p.LsSurveys)
                .HasForeignKey(d => d.FkDifrangescale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVEYS_FK_DIFRANGESCALE");

            entity.HasOne(d => d.FkJobNavigation).WithMany(p => p.LsSurveys)
                .HasForeignKey(d => d.FkJob)
                .HasConstraintName("FK_SURVEYS_FK_JOB");
        });

        modelBuilder.Entity<LsSurveyDifRatingscale>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_SURVEY_DIF_RATINGSCALE");

            entity.ToTable("LS_SURVEY_DIF_RATINGSCALE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsSurveyEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SURVEY_EVENTS");

            entity.ToTable("LS_SURVEY_EVENTS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.AdminEndDate)
                .HasColumnType("datetime")
                .HasColumnName("ADMIN_END_DATE");
            entity.Property(e => e.ChangedDate)
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.FkChangedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHANGED_BY");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkSurveys)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SURVEYS");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkChangedByNavigation).WithMany(p => p.LsSurveyEventFkChangedByNavigations)
                .HasForeignKey(d => d.FkChangedBy)
                .HasConstraintName("FK_SENT_CHBYLEARNER");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsSurveyEventFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SENT_LEARNER");

            entity.HasOne(d => d.FkSurveysNavigation).WithMany(p => p.LsSurveyEvents)
                .HasForeignKey(d => d.FkSurveys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SENT_SRVYID");
        });

        modelBuilder.Entity<LsSurveyItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SURVEY_ITEMS");

            entity.ToTable("LS_SURVEY_ITEMS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.FkLearnerChangedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER_CHANGED_BY");
            entity.Property(e => e.FkObjectId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECT_ID");
            entity.Property(e => e.FkSurveyObjType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SURVEY_OBJ_TYPE");
            entity.Property(e => e.FkSurveys)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SURVEYS");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.UserdefinedId)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINED_ID");

            entity.HasOne(d => d.FkLearnerChangedByNavigation).WithMany(p => p.LsSurveyItems)
                .HasForeignKey(d => d.FkLearnerChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SI_LEARNER");

            entity.HasOne(d => d.FkSurveyObjTypeNavigation).WithMany(p => p.LsSurveyItems)
                .HasForeignKey(d => d.FkSurveyObjType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SI_OBJTYPE");

            entity.HasOne(d => d.FkSurveysNavigation).WithMany(p => p.LsSurveyItems)
                .HasForeignKey(d => d.FkSurveys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SI_SURVEYID");
        });

        modelBuilder.Entity<LsSurveyItemResultsArch>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("LS_SURVEY_ITEM_RESULTS_ARCH");

            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.Diff)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("DIFF");
            entity.Property(e => e.FkEvaluator)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("FK_EVALUATOR");
            entity.Property(e => e.FkSurveyitemid)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("FK_SURVEYITEMID");
            entity.Property(e => e.Freq)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("FREQ");
            entity.Property(e => e.Imp)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("IMP");
        });

        modelBuilder.Entity<LsSurveyItemResultsDif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SURVEY_ITEM_RESULTS_DIF");

            entity.ToTable("LS_SURVEY_ITEM_RESULTS_DIF");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearnerChangedby)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER_CHANGEDBY");
            entity.Property(e => e.FkSeventid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SEVENTID");
            entity.Property(e => e.FkSevntItemid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SEVNT_ITEMID");
            entity.Property(e => e.FkSevntRespondent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SEVNT_RESPONDENT");
            entity.Property(e => e.FkSurvyitemId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SURVYITEM_ID");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.ItemComments)
                .HasMaxLength(500)
                .HasColumnName("ITEM_COMMENTS");
            entity.Property(e => e.ValueDiff)
                .HasColumnType("numeric(12, 1)")
                .HasColumnName("VALUE_DIFF");
            entity.Property(e => e.ValueFreq)
                .HasColumnType("numeric(12, 1)")
                .HasColumnName("VALUE_FREQ");
            entity.Property(e => e.ValueImp)
                .HasColumnType("numeric(12, 1)")
                .HasColumnName("VALUE_IMP");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsSurveyItemResultsDifFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_SURVY_ITEM_DIF_FK_LEARNER");

            entity.HasOne(d => d.FkLearnerChangedbyNavigation).WithMany(p => p.LsSurveyItemResultsDifFkLearnerChangedbyNavigations)
                .HasForeignKey(d => d.FkLearnerChangedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVY_ITEM_DIF_FK_LRNR_CHNG");

            entity.HasOne(d => d.FkSevent).WithMany(p => p.LsSurveyItemResultsDifs)
                .HasForeignKey(d => d.FkSeventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVY_ITEM_DIF_FK_SEVENTID");

            entity.HasOne(d => d.FkSevntItem).WithMany(p => p.LsSurveyItemResultsDifs)
                .HasForeignKey(d => d.FkSevntItemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVY_ITEM_DIF_SEVNT_ITEMID");

            entity.HasOne(d => d.FkSevntRespondentNavigation).WithMany(p => p.LsSurveyItemResultsDifs)
                .HasForeignKey(d => d.FkSevntRespondent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVY_ITEM_DIF_FK_SEVNT_RSP");

            entity.HasOne(d => d.FkSurvyitem).WithMany(p => p.LsSurveyItemResultsDifs)
                .HasForeignKey(d => d.FkSurvyitemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SURVY_ITEM_DIF_SURVYITEM_ID");
        });

        modelBuilder.Entity<LsSurveyObjectType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_SURVEY_OBJECT_TYPE");

            entity.ToTable("LS_SURVEY_OBJECT_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.ObjTable)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("OBJ_TABLE");
            entity.Property(e => e.ObjTableColumn)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("OBJ_TABLE_COLUMN");
            entity.Property(e => e.Object)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("OBJECT");
        });

        modelBuilder.Entity<LsSurveyeventItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SURVEYEVENT_ITEMS");

            entity.ToTable("LS_SURVEYEVENT_ITEMS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.FkLearnerChangedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER_CHANGED_BY");
            entity.Property(e => e.FkObjectId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECT_ID");
            entity.Property(e => e.FkSeventid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SEVENTID");
            entity.Property(e => e.FkSurveyObjType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SURVEY_OBJ_TYPE");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.UserdefinedId)
                .HasMaxLength(50)
                .HasColumnName("USERDEFINED_ID");

            entity.HasOne(d => d.FkLearnerChangedByNavigation).WithMany(p => p.LsSurveyeventItems)
                .HasForeignKey(d => d.FkLearnerChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVNT_LEARNER");

            entity.HasOne(d => d.FkSevent).WithMany(p => p.LsSurveyeventItems)
                .HasForeignKey(d => d.FkSeventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVNT_SURVEYID");

            entity.HasOne(d => d.FkSurveyObjTypeNavigation).WithMany(p => p.LsSurveyeventItems)
                .HasForeignKey(d => d.FkSurveyObjType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVNT_OBJTYPE");
        });

        modelBuilder.Entity<LsSurveyeventRespondent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SURVEYEVENT_RESPONDENTS");

            entity.ToTable("LS_SURVEYEVENT_RESPONDENTS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangedDate)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("CHANGED_DATE");
            entity.Property(e => e.Eventcompleted)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EVENTCOMPLETED");
            entity.Property(e => e.FkChangedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHANGED_BY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkSeventid)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SEVENTID");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.OverallEvntComments)
                .HasMaxLength(500)
                .HasColumnName("OVERALL_EVNT_COMMENTS");

            entity.HasOne(d => d.FkChangedByNavigation).WithMany(p => p.LsSurveyeventRespondentFkChangedByNavigations)
                .HasForeignKey(d => d.FkChangedBy)
                .HasConstraintName("FK_SER_CHANGEDBY_LEARNER");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsSurveyeventRespondentFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .HasConstraintName("FK_SER_LEARNER");

            entity.HasOne(d => d.FkSevent).WithMany(p => p.LsSurveyeventRespondents)
                .HasForeignKey(d => d.FkSeventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SER_EVNT");
        });

        modelBuilder.Entity<LsTaskQualStep>(entity =>
        {
            entity.ToTable("LS_TASK_QUAL_STEPS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.EvaluatorComments)
                .HasMaxLength(500)
                .HasColumnName("EVALUATOR_COMMENTS");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkEvaluator)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVALUATOR");
            entity.Property(e => e.FkTaskQualification)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_QUALIFICATION");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.LsTaskQualSteps)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("FK_TASK_QUAL_STEPS_FK_ANALYSIS");

            entity.HasOne(d => d.FkEvaluatorNavigation).WithMany(p => p.LsTaskQualSteps)
                .HasForeignKey(d => d.FkEvaluator)
                .HasConstraintName("FK_TASK_QUAL_STEPS_EVALUATOR");

            entity.HasOne(d => d.FkTaskQualificationNavigation).WithMany(p => p.LsTaskQualSteps)
                .HasForeignKey(d => d.FkTaskQualification)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_QUAL_STEPS_TASK_QUAL");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.LsTaskQualSteps)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_QUAL_STEPS_STATUS");
        });

        modelBuilder.Entity<LsTaskQualification>(entity =>
        {
            entity.ToTable("LS_TASK_QUALIFICATION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.DateStarted)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_STARTED");
            entity.Property(e => e.DateTrainingCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_TRAINING_COMPLETED");
            entity.Property(e => e.EvaluatorComments)
                .HasMaxLength(500)
                .HasColumnName("EVALUATOR_COMMENTS");
            entity.Property(e => e.Exempt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXEMPT");
            entity.Property(e => e.ExemptComments)
                .HasMaxLength(500)
                .HasColumnName("EXEMPT_COMMENTS");
            entity.Property(e => e.FkAnalysisObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_OBJECTIVE");
            entity.Property(e => e.FkEvaluator)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EVALUATOR");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkLsQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_QUAL_CARD");
            entity.Property(e => e.FkLsTuItemType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TU_ITEM_TYPE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkTrainer)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TRAINER");
            entity.Property(e => e.OriginalDateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("ORIGINAL_DATE_COMPLETED");
            entity.Property(e => e.Session)
                .HasMaxLength(10)
                .HasColumnName("SESSION");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("STATUS");
            entity.Property(e => e.TrainingComments)
                .HasMaxLength(500)
                .HasColumnName("TRAINING_COMMENTS");
            entity.Property(e => e.TrainingStatus)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("TRAINING_STATUS");

            entity.HasOne(d => d.FkEvaluatorNavigation).WithMany(p => p.LsTaskQualificationFkEvaluatorNavigations)
                .HasForeignKey(d => d.FkEvaluator)
                .HasConstraintName("FK_LS_TASK_QUAL_LEARNER_EVAL");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsTaskQualificationFkLearnerNavigations)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_TASK_QUAL_FK_LEARNER");

            entity.HasOne(d => d.FkLearningEventNavigation).WithMany(p => p.LsTaskQualifications)
                .HasForeignKey(d => d.FkLearningEvent)
                .HasConstraintName("FK_TASK_QUAL_FK_LEARNING_EVENT");

            entity.HasOne(d => d.FkLsQualCardNavigation).WithMany(p => p.LsTaskQualifications)
                .HasForeignKey(d => d.FkLsQualCard)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LS_TASK_QUAL_FK_QUAL_CARD");

            entity.HasOne(d => d.FkLsTuItemTypeNavigation).WithMany(p => p.LsTaskQualifications)
                .HasForeignKey(d => d.FkLsTuItemType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_QUAL_FK_TU_ITEM_TYPE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.LsTaskQualifications)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LS_TASK_QUAL_FK_PROGRAM");

            entity.HasOne(d => d.FkTrainerNavigation).WithMany(p => p.LsTaskQualificationFkTrainerNavigations)
                .HasForeignKey(d => d.FkTrainer)
                .HasConstraintName("FK_LS_TASK_QUAL_LEARNER_TRAINR");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.LsTaskQualifications)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_QUAL_STATUS");
        });

        modelBuilder.Entity<LsTaskSurveyItemsArch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LS_TASK___3214EC27028A8F61");

            entity.ToTable("LS_TASK_SURVEY_ITEMS_ARCH");

            entity.HasIndex(e => e.FkAnalysisItem, "IX_LS_TASK_SURVEY_ITEMS").HasFillFactor(90);

            entity.HasIndex(e => e.Project, "IX_LS_TASK_SURVEY_ITEMS_1").HasFillFactor(90);

            entity.HasIndex(e => e.Id, "IX_Table1").HasFillFactor(90);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkAnalysisItem)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("FK_ANALYSIS_ITEM");
            entity.Property(e => e.Project)
                .HasColumnType("numeric(28, 0)")
                .HasColumnName("PROJECT");
        });

        modelBuilder.Entity<LsTimeToComplete>(entity =>
        {
            entity.ToTable("LS_TIME_TO_COMPLETE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<LsTimeToCompleteImpl>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkLsTimeToComplete, e.DateCreated });

            entity.ToTable("LS_TIME_TO_COMPLETE_IMPL");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkLsTimeToComplete)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TIME_TO_COMPLETE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkCreatedByL)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY_L");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkTimeType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_TYPE");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TEXT");
            entity.Property(e => e.TimeSpan)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TIME_SPAN");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.LsTimeToCompleteImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_TO_COMP_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkCreatedByLNavigation).WithMany(p => p.LsTimeToCompleteImpls)
                .HasForeignKey(d => d.FkCreatedByL)
                .HasConstraintName("FK_TIME_TO_COMP_IMPL_CREATBY_L");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.LsTimeToCompleteImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TIME_TO_COMP_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkLsTimeToCompleteNavigation).WithMany(p => p.LsTimeToCompleteImpls)
                .HasForeignKey(d => d.FkLsTimeToComplete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_TO_COMP_IMPL_TIM_2_CMP");

            entity.HasOne(d => d.FkTimeTypeNavigation).WithMany(p => p.LsTimeToCompleteImpls)
                .HasForeignKey(d => d.FkTimeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_TO_COMP_IMPL_TIME_TYPE");
        });

        modelBuilder.Entity<LsTrainingReqActionType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_TRAINING_REQ_ACTION_TYPE");

            entity.ToTable("LS_TRAINING_REQ_ACTION_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<LsTrainingReqComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TRAINING_REQ_COMMENT");

            entity.ToTable("LS_TRAINING_REQ_COMMENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Comments)
                .IsRequired()
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");
            entity.Property(e => e.FkLsTrainingReqItem)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TRAINING_REQ_ITEM");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.LsTrainingReqComments)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRNG_REQ_COM_FK_DEVELOPER");

            entity.HasOne(d => d.FkLsTrainingReqItemNavigation).WithMany(p => p.LsTrainingReqComments)
                .HasForeignKey(d => d.FkLsTrainingReqItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRNG_REQ_COM_TRNG_REQ_ITEM");
        });

        modelBuilder.Entity<LsTrainingReqItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TRAINING_REQ_ITEM");

            entity.ToTable("LS_TRAINING_REQ_ITEM");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateActionDue)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ACTION_DUE");
            entity.Property(e => e.DateAssigned)
                .HasColumnType("datetime")
                .HasColumnName("DATE_ASSIGNED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateDue)
                .HasColumnType("datetime")
                .HasColumnName("DATE_DUE");
            entity.Property(e => e.DateResolved)
                .HasColumnType("datetime")
                .HasColumnName("DATE_RESOLVED");
            entity.Property(e => e.DateSourceDue)
                .HasColumnType("datetime")
                .HasColumnName("DATE_SOURCE_DUE");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkActionType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ACTION_TYPE");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkDeveloperAssignedTo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER_ASSIGNED_TO");
            entity.Property(e => e.FkDeveloperResolvedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER_RESOLVED_BY");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkSourceCatery)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SOURCE_CATERY");
            entity.Property(e => e.FkStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_STATUS");
            entity.Property(e => e.ResolutionAction).HasColumnName("RESOLUTION_ACTION");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasColumnName("SOURCE");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");

            entity.HasOne(d => d.FkActionTypeNavigation).WithMany(p => p.LsTrainingReqItems)
                .HasForeignKey(d => d.FkActionType)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_ACTION_TP");

            entity.HasOne(d => d.FkCompanyNavigation).WithMany(p => p.LsTrainingReqItems)
                .HasForeignKey(d => d.FkCompany)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_COMPANY");

            entity.HasOne(d => d.FkDeveloperAssignedToNavigation).WithMany(p => p.LsTrainingReqItemFkDeveloperAssignedToNavigations)
                .HasForeignKey(d => d.FkDeveloperAssignedTo)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_DEV_ASGN2");

            entity.HasOne(d => d.FkDeveloperResolvedByNavigation).WithMany(p => p.LsTrainingReqItemFkDeveloperResolvedByNavigations)
                .HasForeignKey(d => d.FkDeveloperResolvedBy)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_DEV_RESLV");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsTrainingReqItems)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_LEARNER");

            entity.HasOne(d => d.FkSourceCateryNavigation).WithMany(p => p.LsTrainingReqItems)
                .HasForeignKey(d => d.FkSourceCatery)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_SOURC_CAT");

            entity.HasOne(d => d.FkStatusNavigation).WithMany(p => p.LsTrainingReqItems)
                .HasForeignKey(d => d.FkStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRAINING_REQ_ITEM_STATUS");
        });

        modelBuilder.Entity<LsTrainingReqLink>(entity =>
        {
            entity.HasKey(e => new { e.FkLsTrainingReqItem, e.FkNode, e.Type }).HasName("PK_TRAINING_REQ_LINK");

            entity.ToTable("LS_TRAINING_REQ_LINK");

            entity.Property(e => e.FkLsTrainingReqItem)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LS_TRAINING_REQ_ITEM");
            entity.Property(e => e.FkNode)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_NODE");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_IMPL");

            entity.HasOne(d => d.FkLsTrainingReqItemNavigation).WithMany(p => p.LsTrainingReqLinks)
                .HasForeignKey(d => d.FkLsTrainingReqItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LS_TRAINING_REQ_ITEM");
        });

        modelBuilder.Entity<LsTrainingReqSourceCatery>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_TRAINING_REQ_SOURCE_CATERY");

            entity.ToTable("LS_TRAINING_REQ_SOURCE_CATERY");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("CATEGORY");
        });

        modelBuilder.Entity<LsTrainingReqStatus>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_TRAINING_REQ_STATUS");

            entity.ToTable("LS_TRAINING_REQ_STATUS");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<LsTrainingType>(entity =>
        {
            entity.HasKey(e => e.IdValue).IsClustered(false);

            entity.ToTable("LS_TRAINING_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<LsTuItemType>(entity =>
        {
            entity.HasKey(e => e.IdValue).HasName("PK_TU_ITEM_TYPE");

            entity.ToTable("LS_TU_ITEM_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<LsVLesson>(entity =>
        {
            entity.HasKey(e => e.Programid);

            entity.ToTable("LS_V_LESSON");

            entity.HasIndex(e => e.FkLearner, "IX_LS_V_LESSON").HasFillFactor(100);

            entity.Property(e => e.Programid)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("PROGRAMID");
            entity.Property(e => e.Active)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ACTIVE");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .HasColumnName("TITLE");

            entity.HasOne(d => d.FkLearnerNavigation).WithMany(p => p.LsVLessons)
                .HasForeignKey(d => d.FkLearner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_V_LESSON_FK_LEARNER");
        });

        modelBuilder.Entity<LsVLessonOb>(entity =>
        {
            entity.HasKey(e => new { e.FkVLessonId, e.FkObjective });

            entity.ToTable("LS_V_LESSON_OB");

            entity.Property(e => e.FkVLessonId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_V_LESSON_ID");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.CompleteDate)
                .HasColumnType("datetime")
                .HasColumnName("COMPLETE_DATE");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.LsVLessonObs)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_V_LESSON_OB_FK_OBJECTIVE");

            entity.HasOne(d => d.FkVLesson).WithMany(p => p.LsVLessonObs)
                .HasForeignKey(d => d.FkVLessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_V_LESSON_OB_FK_V_LESSON_ID");
        });

        modelBuilder.Entity<LsVersionInfo>(entity =>
        {
            entity.ToTable("LS_VERSION_INFO");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Data).HasColumnName("DATA");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Id2)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID2");
            entity.Property(e => e.Id3)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID3");
            entity.Property(e => e.VersionSubtype)
                .HasMaxLength(100)
                .HasColumnName("VERSION_SUBTYPE");
            entity.Property(e => e.VersionType)
                .HasMaxLength(100)
                .HasColumnName("VERSION_TYPE");
        });

        modelBuilder.Entity<LsVisionItemType>(entity =>
        {
            entity.HasKey(e => e.IdValue);

            entity.ToTable("LS_VISION_ITEM_TYPE");

            entity.Property(e => e.IdValue)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID_VALUE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<Objective>(entity =>
        {
            entity.ToTable("OBJECTIVE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveClass>(entity =>
        {
            entity.ToTable("OBJECTIVE_CLASS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveClassImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkObjectiveClass, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_CLASS_IMPL");

            entity.Property(e => e.FkObjectiveClass)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_CLASS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveClassImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_CLASS_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveClassImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJ_CLASS_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveClassNavigation).WithMany(p => p.ObjectiveClassImpls)
                .HasForeignKey(d => d.FkObjectiveClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJ_CLASS_IMPL_OBJ_CLASS");
        });

        modelBuilder.Entity<ObjectiveComment>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkObjective, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_COMMENTS");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveCommentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_COMMENT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveCommentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_COMMENT_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveComments)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_COMMENTS");
        });

        modelBuilder.Entity<ObjectiveCondStand>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkObjective, e.IsCondition, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_COND_STAND");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.IsCondition)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CONDITION");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveCondStandFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_COND_ST_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveCondStandFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_COND_ST_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveCondStands)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_COND_STAND");
        });

        modelBuilder.Entity<ObjectiveHierarchy>(entity =>
        {
            entity.HasKey(e => new { e.FkParent, e.DateExpired, e.DateCreated, e.Sequence, e.FkChild }).IsClustered(false);

            entity.ToTable("OBJECTIVE_HIERARCHY");

            entity.HasIndex(e => new { e.FkParent, e.DateExpired, e.Sequence, e.FkChild }, "NDX_OBJECTIVE_HIERARCHY1").IsClustered();

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.FkParent, e.Sequence }, "NDX_OBJECTIVE_HIERARCHY2");

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.DateCreated, e.FkParent }, "NDX_OBJECTIVE_HIERARCHY3");

            entity.HasIndex(e => new { e.FkParent, e.FkChild, e.DateExpired }, "NDX_OBJECTIVE_HIERARCHY4").IsUnique();

            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkChildNavigation).WithMany(p => p.ObjectiveHierarchyFkChildNavigations)
                .HasForeignKey(d => d.FkChild)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_HIERARCHY_CHILD");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveHierarchyFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_HIERARY_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveHierarchyFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_HIERARY_EXPIREDBY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.ObjectiveHierarchyFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_HIERARCHY_PARENT");
        });

        modelBuilder.Entity<ObjectiveHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkObjective, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_HTML");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.Condition).HasColumnName("CONDITION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Standard).HasColumnName("STANDARD");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveHtmls)
                .HasForeignKey(d => d.FkObjective)
                .HasConstraintName("F_OBJECTIVE_HTML");
        });

        modelBuilder.Entity<ObjectiveImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("OBJECTIVE_IMPL");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.DateCreated }, "NDX_OBJECTIVE_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkObjectiveLevel, e.DateExpired, e.FkProject }, "NDX_OBJECTIVE_IMPL2");

            entity.HasIndex(e => new { e.FkObjectiveLevel, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_OBJECTIVE_IMPL3");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.FkProject }, "NDX_OBJECTIVE_IMPL4");

            entity.HasIndex(e => new { e.FkObjectiveStatus, e.DateExpired, e.FkProject }, "NDX_OBJECTIVE_IMPL6");

            entity.HasIndex(e => new { e.FkObjectiveStatus, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_OBJECTIVE_IMPL7");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_OBJECTIVE_IMPL8");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired }, "NDX_OBJECTIVE_IMPL9").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkObjectiveClass)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_CLASS");
            entity.Property(e => e.FkObjectiveLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_LEVEL");
            entity.Property(e => e.FkObjectiveMedia)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_MEDIA");
            entity.Property(e => e.FkObjectiveSetting)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_SETTING");
            entity.Property(e => e.FkObjectiveStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_STATUS");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.PracticeQuestionsOnWeb)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PRACTICE_QUESTIONS_ON_WEB");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
            entity.Property(e => e.Topic)
                .HasMaxLength(100)
                .HasColumnName("TOPIC");
            entity.Property(e => e.TrainingTime)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TRAINING_TIME");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_IMPL");

            entity.HasOne(d => d.FkObjectiveClassNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjectiveClass)
                .HasConstraintName("F_OBJECTIVE_CLASSIFICATION");

            entity.HasOne(d => d.FkObjectiveLevelNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjectiveLevel)
                .HasConstraintName("F_OBJECTIVE_LEVEL");

            entity.HasOne(d => d.FkObjectiveMediaNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjectiveMedia)
                .HasConstraintName("F_OBJECTIVE_MEDIA");

            entity.HasOne(d => d.FkObjectiveSettingNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjectiveSetting)
                .HasConstraintName("F_OBJECTIVE_SETTING");

            entity.HasOne(d => d.FkObjectiveStatusNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkObjectiveStatus)
                .HasConstraintName("F_OBJECTIVE_STATUS");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ObjectiveImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_PROJECT");
        });

        modelBuilder.Entity<ObjectiveLevel>(entity =>
        {
            entity.ToTable("OBJECTIVE_LEVEL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveLevelImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.Sequence, e.FkObjectiveLevel }).IsClustered(false);

            entity.ToTable("OBJECTIVE_LEVEL_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkObjectiveLevel }, "NDX_OBJECTIVE_LEVEL_IMPL1");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkObjectiveLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_LEVEL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveLevelImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_LVL_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveLevelImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_LVL_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveLevelNavigation).WithMany(p => p.ObjectiveLevelImpls)
                .HasForeignKey(d => d.FkObjectiveLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_LVL_IMP_OBJ_LEVEL");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ObjectiveLevelImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_LVL_IMP_PROJECT");
        });

        modelBuilder.Entity<ObjectiveMediaImpl>(entity =>
        {
            entity.HasKey(e => new { e.Sequence, e.DateExpired, e.FkObjectiveMedia, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_MEDIA_IMPL");

            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkObjectiveMedia)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_MEDIA");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveMediaImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_MED_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveMediaImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_MED_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveMediaNavigation).WithMany(p => p.ObjectiveMediaImpls)
                .HasForeignKey(d => d.FkObjectiveMedia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_MED_IMP_OBJ_MEDIA");
        });

        modelBuilder.Entity<ObjectiveMedium>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("OBJECTIVE_MEDIA");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveQuestion>(entity =>
        {
            entity.HasKey(e => new { e.FkObjective, e.DateExpired, e.DateCreated, e.Sequence, e.FkQuestion }).IsClustered(false);

            entity.ToTable("OBJECTIVE_QUESTION");

            entity.HasIndex(e => new { e.FkObjective, e.FkQuestion }, "IX_OBJECTIVE_QUESTION");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.Sequence, e.FkQuestion }, "NDX_QUESTION_OBJECTIVE1").IsClustered();

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.FkObjective }, "NDX_QUESTION_OBJECTIVE2");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.FkObjective }, "NDX_QUESTION_OBJECTIVE3");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveQuestionFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_QUESTIN_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveQuestionFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_QUESTIN_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveQuestions)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_QUESTION");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ObjectiveQuestions)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_QUESTION_PROJECT");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.ObjectiveQuestions)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_OBJECTIVE");
        });

        modelBuilder.Entity<ObjectiveSetting>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("OBJECTIVE_SETTING");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveSettingImpl>(entity =>
        {
            entity.HasKey(e => new { e.Sequence, e.DateExpired, e.FkObjectiveSetting, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_SETTING_IMPL");

            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkObjectiveSetting)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_SETTING");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveSettingImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_SET_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveSettingImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_SET_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveSettingNavigation).WithMany(p => p.ObjectiveSettingImpls)
                .HasForeignKey(d => d.FkObjectiveSetting)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_SET_IMP_OBJ_SETTG");
        });

        modelBuilder.Entity<ObjectiveStatus>(entity =>
        {
            entity.ToTable("OBJECTIVE_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ObjectiveStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkObjectiveStatus, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_STATUS_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence }, "NDX_OBJECTIVE_STATUS_IMPL1");

            entity.Property(e => e.FkObjectiveStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_STATUS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_STAT_IM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_STAT_IM_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveStatusNavigation).WithMany(p => p.ObjectiveStatusImpls)
                .HasForeignKey(d => d.FkObjectiveStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_STAT_IM_OBJ_STAT");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ObjectiveStatusImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_STAT_IMPL_PROJECT");
        });

        modelBuilder.Entity<ObjectiveTask>(entity =>
        {
            entity.HasKey(e => new { e.FkObjective, e.FkAnalysis, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("OBJECTIVE_TASK");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.DateCreated }, "NDX_OBJECTIVE_TASK1").IsClustered();

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.AnalysisDescent)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ANALYSIS_DESCENT");
            entity.Property(e => e.Consolidated)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CONSOLIDATED");
            entity.Property(e => e.Direct)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DIRECT");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Grade)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("GRADE");
            entity.Property(e => e.Manual)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MANUAL");
            entity.Property(e => e.ObjectiveDescent)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJECTIVE_DESCENT");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.ObjectiveTasks)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TASK_ANA");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ObjectiveTaskFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TASK_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ObjectiveTaskFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_OBJECTIVE_TASK_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ObjectiveTasks)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TASK_OBJ");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ObjectiveTasks)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OBJECTIVE_TASK_PROJECT");
        });

        modelBuilder.Entity<OfficeMigration>(entity =>
        {
            entity.HasKey(e => e.FkContent).IsClustered(false);

            entity.ToTable("OFFICE_MIGRATION");

            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");

            entity.HasOne(d => d.FkContentNavigation).WithOne(p => p.OfficeMigration)
                .HasForeignKey<OfficeMigration>(d => d.FkContent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_CONTENT_MIGRATED");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("PROGRAM");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");

            entity.HasMany(d => d.FkExamOnlineProfiles).WithMany(p => p.FkPrograms)
                .UsingEntity<Dictionary<string, object>>(
                    "ProgramExamProfile",
                    r => r.HasOne<ExamOnlineProfile>().WithMany()
                        .HasForeignKey("FkExamOnlineProfile")
                        .HasConstraintName("FK_PRO_EX_PROFILES_EX_OL_PROF"),
                    l => l.HasOne<Program>().WithMany()
                        .HasForeignKey("FkProgram")
                        .HasConstraintName("FK_PROGRAM_EXAM_PROFILES_PROG"),
                    j =>
                    {
                        j.HasKey("FkProgram", "FkExamOnlineProfile")
                            .HasName("PK_PROGRAM_EXAM_PROFILE")
                            .IsClustered(false);
                        j.ToTable("PROGRAM_EXAM_PROFILES");
                        j.IndexerProperty<decimal>("FkProgram")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_PROGRAM");
                        j.IndexerProperty<decimal>("FkExamOnlineProfile")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_EXAM_ONLINE_PROFILE");
                    });
        });

        modelBuilder.Entity<ProgramAlternate>(entity =>
        {
            entity.ToTable("PROGRAM_ALTERNATE");

            entity.HasIndex(e => new { e.FkProgram, e.UserDefinedId }, "NDX_PROGRAM_ALTERNATE1");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramAlternates)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_PROGRAM_ALTERNATE_FK_PROG");
        });

        modelBuilder.Entity<ProgramAlternateAudit>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("PROGRAM_ALTERNATE_AUDIT");

            entity.HasIndex(e => new { e.FkPrimary, e.FkProgram, e.UserDefinedId, e.Action, e.DateCreated }, "NDX_PROGRAM_ALTERNATE_AUDIT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Action)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ACTION");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkDeveloper)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DEVELOPER");
            entity.Property(e => e.FkPrimary)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PRIMARY");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");

            entity.HasOne(d => d.FkDeveloperNavigation).WithMany(p => p.ProgramAlternateAudits)
                .HasForeignKey(d => d.FkDeveloper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_ALT_AUDIT_FK_DEVELO");

            entity.HasOne(d => d.FkPrimaryNavigation).WithMany(p => p.ProgramAlternateAuditFkPrimaryNavigations)
                .HasForeignKey(d => d.FkPrimary)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_ALT_AUDIT_FK_PRIMAR");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramAlternateAuditFkProgramNavigations)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_PROGRAM_ALT_AUDIT_PROGRAM");
        });

        modelBuilder.Entity<ProgramAlternateInheritance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PROGRAM_ALTERNATE_INHERITAN");

            entity.ToTable("PROGRAM_ALTERNATE_INHERITANCE");

            entity.HasIndex(e => new { e.FkAncestor, e.FkDescendent, e.Distance }, "NDX_PROGRAM_ALT_INHERITANCE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Distance)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("DISTANCE");
            entity.Property(e => e.FkAncestor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANCESTOR");
            entity.Property(e => e.FkDescendent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DESCENDENT");

            entity.HasOne(d => d.FkAncestorNavigation).WithMany(p => p.ProgramAlternateInheritanceFkAncestorNavigations)
                .HasForeignKey(d => d.FkAncestor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_ALT_INHERT_ANCESTOR");

            entity.HasOne(d => d.FkDescendentNavigation).WithMany(p => p.ProgramAlternateInheritanceFkDescendentNavigations)
                .HasForeignKey(d => d.FkDescendent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_ALT_INHERT_DESCENDN");
        });

        modelBuilder.Entity<ProgramComment>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROGRAM_COMMENTS");

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramCommentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_COMMENTS_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramCommentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_COMMENTS_EXPIREDBY");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramComments)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_COMMENTS");
        });

        modelBuilder.Entity<ProgramExamOnlyWeighting>(entity =>
        {
            entity.HasKey(e => new { e.FkProgramSource, e.DateExpired, e.DateCreated, e.Sequence, e.FkProgramWeighted }).IsClustered(false);

            entity.ToTable("PROGRAM_EXAM_ONLY_WEIGHTING");

            entity.HasIndex(e => new { e.FkProgramSource, e.DateExpired, e.Sequence, e.FkProgramWeighted }, "NDX_PROGRAM_EXAM_ONLY_WEIGHT1").IsClustered();

            entity.HasIndex(e => new { e.FkProgramWeighted, e.DateExpired, e.DateCreated, e.FkProgramSource }, "NDX_PROGRAM_EXAM_ONLY_WEIGHT2");

            entity.HasIndex(e => new { e.FkProgramWeighted, e.DateExpired, e.FkProgramSource }, "NDX_PROGRAM_EXAM_ONLY_WEIGHT3");

            entity.Property(e => e.FkProgramSource)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_SOURCE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkProgramWeighted)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_WEIGHTED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Weighting)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("WEIGHTING");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramExamOnlyWeightingFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROG_EXAM_ONL_WGT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramExamOnlyWeightingFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROG_EXAM_ONL_WGT_EXPIREDBY");

            entity.HasOne(d => d.FkProgramSourceNavigation).WithMany(p => p.ProgramExamOnlyWeightingFkProgramSourceNavigations)
                .HasForeignKey(d => d.FkProgramSource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_SOURCE");

            entity.HasOne(d => d.FkProgramWeightedNavigation).WithMany(p => p.ProgramExamOnlyWeightingFkProgramWeightedNavigations)
                .HasForeignKey(d => d.FkProgramWeighted)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_WEIGHTED");
        });

        modelBuilder.Entity<ProgramHierarchy>(entity =>
        {
            entity.HasKey(e => new { e.FkParent, e.DateExpired, e.DateCreated, e.Sequence, e.FkChild }).IsClustered(false);

            entity.ToTable("PROGRAM_HIERARCHY");

            entity.HasIndex(e => new { e.FkParent, e.DateExpired, e.Sequence, e.FkChild }, "NDX_PROGRAM_HIERARCHY1").IsClustered();

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.FkParent, e.Sequence }, "NDX_PROGRAM_HIERARCHY2");

            entity.HasIndex(e => new { e.FkChild, e.DateExpired, e.DateCreated, e.FkParent }, "NDX_PROGRAM_HIERARCHY3");

            entity.HasIndex(e => new { e.FkParent, e.FkChild, e.DateExpired }, "NDX_PROGRAM_HIERARCHY4").IsUnique();

            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkChildNavigation).WithMany(p => p.ProgramHierarchyFkChildNavigations)
                .HasForeignKey(d => d.FkChild)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_HIERARCHY_CHILD");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramHierarchyFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_HIERARCHY_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramHierarchyFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_HIERARCHY_EXPIREDBY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.ProgramHierarchyFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_HIERARCHY_PARENT");
        });

        modelBuilder.Entity<ProgramHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROGRAM_HTML");

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramHtmls)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("F_PROGRAM_HTML");
        });

        modelBuilder.Entity<ProgramImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("PROGRAM_IMPL");

            entity.HasIndex(e => new { e.FkProgram, e.DateExpired, e.DateCreated }, "NDX_PROGRAM_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_PROGRAM_IMPL10");

            entity.HasIndex(e => new { e.FkProgram, e.DateExpired }, "NDX_PROGRAM_IMPL11").IsUnique();

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.FkProject }, "NDX_PROGRAM_IMPL2");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_PROGRAM_IMPL3");

            entity.HasIndex(e => new { e.FkProgramLevel, e.DateExpired, e.FkProject }, "NDX_PROGRAM_IMPL4");

            entity.HasIndex(e => new { e.FkProgramLevel, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_PROGRAM_IMPL5");

            entity.HasIndex(e => new { e.IsBucket, e.DateExpired, e.FkProject }, "NDX_PROGRAM_IMPL6");

            entity.HasIndex(e => new { e.IsBucket, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_PROGRAM_IMPL7");

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.FkProject }, "NDX_PROGRAM_IMPL8");

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_PROGRAM_IMPL9");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(75)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkProgramLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_LEVEL");
            entity.Property(e => e.FkProgramOrganizerType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_ORGANIZER_TYPE");
            entity.Property(e => e.FkProgramStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_STATUS");
            entity.Property(e => e.FkProgramTuType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_TU_TYPE");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.IntroWebDisplay)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("INTRO_WEB_DISPLAY");
            entity.Property(e => e.IsBucket)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_BUCKET");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.SummaryWebDisplay)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SUMMARY_WEB_DISPLAY");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(260)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");
            entity.Property(e => e.WeightingType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("WEIGHTING_TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_IMPL");

            entity.HasOne(d => d.FkProgramLevelNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProgramLevel)
                .HasConstraintName("F_PROGRAM_LEVEL");

            entity.HasOne(d => d.FkProgramOrganizerTypeNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProgramOrganizerType)
                .HasConstraintName("F_PROGRAM_ORGANIZER_TYPE");

            entity.HasOne(d => d.FkProgramStatusNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProgramStatus)
                .HasConstraintName("F_PROGRAM_STATUS");

            entity.HasOne(d => d.FkProgramTuTypeNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProgramTuType)
                .HasConstraintName("F_PROGRAM_TU_TYPE");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ProgramImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_PROJECT");
        });

        modelBuilder.Entity<ProgramLevel>(entity =>
        {
            entity.ToTable("PROGRAM_LEVEL");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ProgramLevelImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.FkProgramLevel }).IsClustered(false);

            entity.ToTable("PROGRAM_LEVEL_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkProgramLevel }, "NDX_PROGRAM_LEVEL_IMPL1").IsClustered();

            entity.HasIndex(e => new { e.FkProgramLevel, e.DateExpired, e.DateCreated }, "NDX_PROGRAM_LEVEL_IMPL2");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkProgramLevel)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_LEVEL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramLevelImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_LEVEL_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramLevelImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_LEVEL_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkProgramLevelNavigation).WithMany(p => p.ProgramLevelImpls)
                .HasForeignKey(d => d.FkProgramLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_LEVEL_IMP_PROG_LEVL");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ProgramLevelImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_LEVEL_IMPL_PROJECT");
        });

        modelBuilder.Entity<ProgramObjectiveContent>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.DateExpired, e.FkObjective, e.FkContent, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROGRAM_OBJECTIVE_CONTENT");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.DateCreated }, "NDX_PROGRAM_OBJECTIVE_CONTENT1");

            entity.HasIndex(e => new { e.FkContent, e.DateExpired, e.DateCreated }, "NDX_PROGRAM_OBJECTIVE_CONTENT2").IsClustered();

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkContent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CONTENT");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Hold)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("HOLD");
            entity.Property(e => e.HoldDescription)
                .HasMaxLength(2000)
                .HasColumnName("HOLD_DESCRIPTION");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Transition)
                .HasMaxLength(2000)
                .HasColumnName("TRANSITION");

            entity.HasOne(d => d.FkContentNavigation).WithMany(p => p.ProgramObjectiveContents)
                .HasForeignKey(d => d.FkContent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_CONTENT_FK_CONTENT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramObjectiveContentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_CONTENT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramObjectiveContentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_CONTENT_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.ProgramObjectiveContents)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_CONTENT_OBJECTIVE");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramObjectiveContents)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_CONTENT_FK_PROGRAM");
        });

        modelBuilder.Entity<ProgramOrganizerType>(entity =>
        {
            entity.ToTable("PROGRAM_ORGANIZER_TYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ProgramOrganizerTypeImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProgramOrganizerType, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROGRAM_ORGANIZER_TYPE_IMPL");

            entity.Property(e => e.FkProgramOrganizerType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_ORGANIZER_TYPE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramOrganizerTypeImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROG_ORG_TYP_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramOrganizerTypeImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROG_ORG_TYP_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProgramOrganizerTypeNavigation).WithMany(p => p.ProgramOrganizerTypeImpls)
                .HasForeignKey(d => d.FkProgramOrganizerType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROG_ORG_TYP_IMPL_PG_OG_TYP");
        });

        modelBuilder.Entity<ProgramPrerequisite>(entity =>
        {
            entity.HasKey(e => new { e.FkProgramSource, e.DateExpired, e.DateCreated, e.Sequence, e.FkProgramPrerequisite }).IsClustered(false);

            entity.ToTable("PROGRAM_PREREQUISITE");

            entity.HasIndex(e => new { e.FkProgramSource, e.DateExpired, e.Sequence, e.FkProgramPrerequisite }, "NDX_PROGRAM_PREREQUISITE1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkProgramPrerequisite, e.DateExpired, e.FkProgramSource }, "NDX_PROGRAM_PREREQUISITE2");

            entity.HasIndex(e => new { e.FkProgramPrerequisite, e.DateExpired, e.DateCreated, e.FkProgramSource }, "NDX_PROGRAM_PREREQUISITE3");

            entity.Property(e => e.FkProgramSource)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_SOURCE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkProgramPrerequisite)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_PREREQUISITE");
            entity.Property(e => e.AndOr)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("AND_OR");
            entity.Property(e => e.Enrolled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENROLLED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.ParenType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PAREN_TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramPrerequisiteFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_PREREQUIS_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramPrerequisiteFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_PREREQUIS_EXPIREDBY");

            entity.HasOne(d => d.FkProgramPrerequisiteNavigation).WithMany(p => p.ProgramPrerequisiteFkProgramPrerequisiteNavigations)
                .HasForeignKey(d => d.FkProgramPrerequisite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_PREREQUISITE2");

            entity.HasOne(d => d.FkProgramSourceNavigation).WithMany(p => p.ProgramPrerequisiteFkProgramSourceNavigations)
                .HasForeignKey(d => d.FkProgramSource)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_PROGRAM_PREREQUISITE1");
        });

        modelBuilder.Entity<ProgramStaticExam>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.DateExpired, e.DateCreated, e.FkExam, e.UsedInLs }).IsClustered(false);

            entity.ToTable("PROGRAM_STATIC_EXAM");

            entity.HasIndex(e => new { e.FkProgram, e.DateExpired, e.FkExam, e.UsedInLs }, "NDX_PROGRAM_STATIC_EXAM1").IsClustered();

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.FkProgram, e.UsedInLs }, "NDX_PROGRAM_STATIC_EXAM2").IsUnique();

            entity.HasIndex(e => new { e.FkExam, e.DateExpired, e.DateCreated, e.FkProgram, e.UsedInLs }, "NDX_PROGRAM_STATIC_EXAM3").IsUnique();

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.UsedInLs)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("USED_IN_LS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramStaticExamFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_STATIC_EX_CREATEDBY");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.ProgramStaticExams)
                .HasForeignKey(d => d.FkExam)
                .HasConstraintName("F_STATIC_EXAM_PROGRAM_EXAM");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramStaticExamFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_STATIC_EX_EXPIREDBY");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.ProgramStaticExams)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("F_STATIC_EXAM_PROGRAM_PROGRAM");
        });

        modelBuilder.Entity<ProgramStatus>(entity =>
        {
            entity.ToTable("PROGRAM_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ProgramStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProgramStatus, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("PROGRAM_STATUS_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkProgramStatus }, "NDX_TASK_STATUS_IMPL1");

            entity.Property(e => e.FkProgramStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_STATUS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_STATUS_IM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROGRAM_STATUS_IM_EXPIREDBY");

            entity.HasOne(d => d.FkProgramStatusNavigation).WithMany(p => p.ProgramStatusImpls)
                .HasForeignKey(d => d.FkProgramStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_STATUS_IM_PG_STATUS");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ProgramStatusImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGRAM_STATUS_IMPL_PROJECT");
        });

        modelBuilder.Entity<ProgramTuType>(entity =>
        {
            entity.ToTable("PROGRAM_TU_TYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ProgramTuTypeImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProgramTuType, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROGRAM_TU_TYPE_IMPL");

            entity.Property(e => e.FkProgramTuType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_TU_TYPE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(75)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProgramTuTypeImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROG_TU_TYPE_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProgramTuTypeImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROG_TU_TYPE_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProgramTuTypeNavigation).WithMany(p => p.ProgramTuTypeImpls)
                .HasForeignKey(d => d.FkProgramTuType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROG_TU_TYPE_IMPL_PG_TU_TYP");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("PROJECT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<ProjectImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("PROJECT_IMPL");

            entity.HasIndex(e => new { e.Name, e.DateExpired, e.DateCreated }, "NDX_PROJECT_IMPL1").IsUnique();

            entity.HasIndex(e => new { e.Enabled, e.DateExpired, e.DateCreated }, "NDX_PROJECT_IMPL2").IsClustered();

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.AllowTaskChangeImpact)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ALLOW_TASK_CHANGE_IMPACT");
            entity.Property(e => e.AnalysisExam)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ANALYSIS_EXAM");
            entity.Property(e => e.AutoStats)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("AUTO_STATS");
            entity.Property(e => e.CustomHelpDoc)
                .HasMaxLength(260)
                .HasColumnName("CUSTOM_HELP_DOC");
            entity.Property(e => e.DateModified)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_MODIFIED");
            entity.Property(e => e.DefaultFontName)
                .HasMaxLength(60)
                .HasColumnName("DEFAULT_FONT_NAME");
            entity.Property(e => e.DefaultFontSize)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("DEFAULT_FONT_SIZE");
            entity.Property(e => e.DefaultFontStyle)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("DEFAULT_FONT_STYLE");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Enabled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ENABLED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.MultiSelect)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MULTI_SELECT");
            entity.Property(e => e.MultipleConsolidation)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MULTIPLE_CONSOLIDATION");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.ObToMultipleCm)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OB_TO_MULTIPLE_CM");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.QAnalysis)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("Q_ANALYSIS");
            entity.Property(e => e.Salt)
                .HasMaxLength(50)
                .HasColumnName("SALT");
            entity.Property(e => e.ShowAlternatesTab)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SHOW_ALTERNATES_TAB");
            entity.Property(e => e.UserSpellDict)
                .HasMaxLength(260)
                .HasColumnName("USER_SPELL_DICT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.ProjectImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.ProjectImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_PROJECT_IMPL_EXPIRED_BY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ProjectImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_PROJECT_IMPL");
        });

        modelBuilder.Entity<ProjectTemplatePath>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("PROJECT_TEMPLATE_PATH");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DataField)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("DATA_FIELD");
            entity.Property(e => e.FilePath)
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnName("FILE_PATH");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.ProjectTemplatePaths)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROJECT_TMP_PATH_FK_PROJECT");
        });

        modelBuilder.Entity<QualCard>(entity =>
        {
            entity.ToTable("QUAL_CARD");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<QualCardImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.FkQualCard }).IsClustered(false);

            entity.ToTable("QUAL_CARD_IMPL");

            entity.HasIndex(e => e.Id, "NDX_LS_QUAL_CARD_IMPL1").IsUnique();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.FkQualCard }, "NDX_QUAL_CARD_IMPL1").IsClustered();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.FkQualCardStatus }, "NDX_QUAL_CARD_IMPL2");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.FkQualCardStatus }, "NDX_QUAL_CARD_IMPL3");

            entity.HasIndex(e => new { e.FkProgram, e.DateExpired, e.DateCreated }, "NDX_QUAL_CARD_IMPL4");

            entity.HasIndex(e => new { e.FkQualCard, e.DateExpired }, "NDX_QUAL_CARD_IMPL5").IsUnique();

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQualCardStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD_STATUS");
            entity.Property(e => e.FkTimeToComplete)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_TO_COMPLETE");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .HasMaxLength(125)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QualCardImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QualCardImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUAL_CARD_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.QualCardImpls)
                .HasForeignKey(d => d.FkProgram)
                .HasConstraintName("FK_QUAL_CARD_IMPL_FK_PROGRAM");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.QualCardImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_IMPL_PROJECT");

            entity.HasOne(d => d.FkQualCardNavigation).WithMany(p => p.QualCardImpls)
                .HasForeignKey(d => d.FkQualCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_IMPL_QUAL_CARD");

            entity.HasOne(d => d.FkQualCardStatusNavigation).WithMany(p => p.QualCardImpls)
                .HasForeignKey(d => d.FkQualCardStatus)
                .HasConstraintName("FK_QUAL_CARD_IMPL_QUL_CRD_STAS");

            entity.HasOne(d => d.FkTimeToCompleteNavigation).WithMany(p => p.QualCardImpls)
                .HasForeignKey(d => d.FkTimeToComplete)
                .HasConstraintName("FK_QUAL_CARD_IMPL_TIME_TO_COMP");
        });

        modelBuilder.Entity<QualCardItem>(entity =>
        {
            entity.HasKey(e => new { e.FkQualCard, e.DateExpired, e.DateCreated, e.Sequence, e.FkAnalysis }).IsClustered(false);

            entity.ToTable("QUAL_CARD_ITEMS");

            entity.HasIndex(e => new { e.FkQualCard, e.DateExpired, e.Sequence, e.FkAnalysis }, "NDX_QUAL_CARD_ITEMS1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.DateCreated, e.FkQualCard, e.Sequence }, "NDX_QUAL_CARD_ITEMS2").IsUnique();

            entity.HasIndex(e => new { e.FkAnalysis, e.DateExpired, e.FkQualCard, e.Sequence }, "NDX_QUAL_CARD_ITEMS3").IsUnique();

            entity.Property(e => e.FkQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkAnalysisImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_IMPL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.QualCardItems)
                .HasForeignKey(d => d.FkAnalysis)
                .HasConstraintName("FK_QUAL_CARD_ITEMS_ANALYSIS");

            entity.HasOne(d => d.FkAnalysisImplNavigation).WithMany(p => p.QualCardItems)
                .HasForeignKey(d => d.FkAnalysisImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_ITEMS_FK_ANL_IMPL");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QualCardItemFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_ITEMS_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QualCardItemFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUAL_CARD_ITEMS_EXPIREDBY");

            entity.HasOne(d => d.FkQualCardNavigation).WithMany(p => p.QualCardItems)
                .HasForeignKey(d => d.FkQualCard)
                .HasConstraintName("FK_QUAL_CARD_ITEMS_QUAL_CARD");
        });

        modelBuilder.Entity<QualCardPrerequisite>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkQualCard, e.DateCreated, e.PrerequisiteType, e.FkPrerequisite });

            entity.ToTable("QUAL_CARD_PREREQUISITE");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.PrerequisiteType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PREREQUISITE_TYPE");
            entity.Property(e => e.FkPrerequisite)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PREREQUISITE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QualCardPrerequisiteFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_PREREQ_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QualCardPrerequisiteFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUAL_CARD_PREREQ_EXPIRED_BY");

            entity.HasOne(d => d.FkQualCardNavigation).WithMany(p => p.QualCardPrerequisites)
                .HasForeignKey(d => d.FkQualCard)
                .HasConstraintName("FK_QUAL_CARD_PREREQ_QUAL_CARD");
        });

        modelBuilder.Entity<QualCardStatus>(entity =>
        {
            entity.ToTable("QUAL_CARD_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<QualCardStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkQualCardStatus, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUAL_CARD_STATUS_IMPL");

            entity.Property(e => e.FkQualCardStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD_STATUS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QualCardStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_STA_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QualCardStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUAL_CARD_STA_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkQualCardStatusNavigation).WithMany(p => p.QualCardStatusImpls)
                .HasForeignKey(d => d.FkQualCardStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUAL_CARD_STA_IMP_QL_CD_STA");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("QUESTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<QuestionComment>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_COMMENTS");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionCommentFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_COMMENTS_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionCommentFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_COMMENTS_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionComments)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_COMMENTS");
        });

        modelBuilder.Entity<QuestionE>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_ES");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_ES1");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_ES2");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.WhiteSpace)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("WHITE_SPACE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionEFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_ES_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionEFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_ES_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionEs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_ES");
        });

        modelBuilder.Entity<QuestionEsHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_ES_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionEsHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_ES_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionEsHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_ES_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionEsHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_ES_HTML");
        });

        modelBuilder.Entity<QuestionExplanation>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_EXPLANATION");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Explanation)
                .IsRequired()
                .HasColumnName("EXPLANATION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionExplanationFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_EXPLANATION_CRTDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionExplanationFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_EXPLANATION_EXPRBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionExplanations)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_EXPLANATION");
        });

        modelBuilder.Entity<QuestionFi>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_FI");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_FI1");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_FI2");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionFiFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_FI_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionFiFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_FI_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionFis)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_FI");
        });

        modelBuilder.Entity<QuestionFiHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_FI_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionFiHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_FI_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionFiHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_FI_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionFiHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_FI_HTML");
        });

        modelBuilder.Entity<QuestionHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Comments).HasColumnName("COMMENTS");
            entity.Property(e => e.Explanation).HasColumnName("EXPLANATION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Stem).HasColumnName("STEM");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_HTML");
        });

        modelBuilder.Entity<QuestionImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("QUESTION_IMPL");

            entity.HasIndex(e => new { e.Type, e.FkQuestion, e.FkQuestionStatus, e.MustAppearOnTest, e.TimeToComplete }, "IX_QUESTION_IMPL");

            entity.HasIndex(e => new { e.FkQuestion, e.FkQuestionStatus, e.Type, e.MustAppearOnTest, e.TimeToComplete }, "IX_QUESTION_IMPL_2");

            entity.HasIndex(e => new { e.FkQuestion, e.Type, e.FkQuestionStatus, e.MustAppearOnTest, e.TimeToComplete }, "IX_QUESTION_IMPL_3");

            entity.HasIndex(e => new { e.FkQuestion, e.FkQuestionStatus, e.Type }, "IX_QUESTION_IMPL_4");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }, "NDX_QUESTION_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_QUESTION_IMPL10");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired }, "NDX_QUESTION_IMPL11").IsUnique();

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.FkProject }, "NDX_QUESTION_IMPL2");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_QUESTION_IMPL3");

            entity.HasIndex(e => new { e.Type, e.DateExpired, e.FkProject }, "NDX_QUESTION_IMPL4");

            entity.HasIndex(e => new { e.Type, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_QUESTION_IMPL5");

            entity.HasIndex(e => new { e.FkQuestionStatus, e.DateExpired, e.FkProject }, "NDX_QUESTION_IMPL6");

            entity.HasIndex(e => new { e.FkQuestionStatus, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_QUESTION_IMPL7");

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.FkProject }, "NDX_QUESTION_IMPL8");

            entity.HasIndex(e => new { e.CrossReference, e.DateExpired, e.DateCreated, e.FkProject }, "NDX_QUESTION_IMPL9");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.CrossReference)
                .HasMaxLength(75)
                .HasColumnName("CROSS_REFERENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Difficulty)
                .HasDefaultValue(0.0m)
                .HasColumnType("numeric(3, 2)")
                .HasColumnName("DIFFICULTY");
            entity.Property(e => e.Disqualified)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DISQUALIFIED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkLockedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LOCKED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkQuestionStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_STATUS");
            entity.Property(e => e.FkScenarioParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SCENARIO_PARENT");
            entity.Property(e => e.IsPractice)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_PRACTICE");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.MustAppearOnTest)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MUST_APPEAR_ON_TEST");
            entity.Property(e => e.TimeToComplete)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TIME_TO_COMPLETE");
            entity.Property(e => e.Topic)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TOPIC");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.UserField1)
                .HasMaxLength(30)
                .HasColumnName("USER_FIELD1");
            entity.Property(e => e.UserField2)
                .HasMaxLength(30)
                .HasColumnName("USER_FIELD2");
            entity.Property(e => e.UserField3)
                .HasMaxLength(30)
                .HasColumnName("USER_FIELD3");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkLockedByNavigation).WithMany(p => p.QuestionImplFkLockedByNavigations)
                .HasForeignKey(d => d.FkLockedBy)
                .HasConstraintName("FK_QUESTION_IMPL_LOCKED_BY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.QuestionImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_PROJECT");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionImplFkQuestionNavigations)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_IMPL");

            entity.HasOne(d => d.FkQuestionStatusNavigation).WithMany(p => p.QuestionImpls)
                .HasForeignKey(d => d.FkQuestionStatus)
                .HasConstraintName("F_QUESTION_STATUS");

            entity.HasOne(d => d.FkScenarioParentNavigation).WithMany(p => p.QuestionImplFkScenarioParentNavigations)
                .HasForeignKey(d => d.FkScenarioParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_FK_SCENARIO_PARENT");
        });

        modelBuilder.Entity<QuestionMaChoice>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_MA_CHOICE");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }, "NDX_QUESTION_MA_CHOICE1").IsUnique();

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMaChoiceFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MA_CHOIC_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMaChoiceFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MA_CHOIC_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMaChoices)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_MA_CHOICE");
        });

        modelBuilder.Entity<QuestionMaChoiceHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_MA_CHOICE_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Choice).HasColumnName("CHOICE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMaChoiceHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MA_CH_HT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMaChoiceHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MA_CH_HT_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMaChoiceHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_MA_CHOICE_HTML");
        });

        modelBuilder.Entity<QuestionMaItem>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_MA_ITEM");

            entity.HasIndex(e => new { e.FkQuestion, e.Points, e.Choice1 }, "IX_QUESTION_MA_ITEM");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Sequence }, "NDX_QUESTION_MA_ITEM1").IsClustered();

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_MA_ITEM2");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_MA_ITEM3");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Choice1)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("CHOICE1");
            entity.Property(e => e.Choice2)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("CHOICE2");
            entity.Property(e => e.Choice3)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("CHOICE3");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMaItemFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MA_ITEM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMaItemFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MA_ITEM_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMaItems)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_MA_ITEM");
        });

        modelBuilder.Entity<QuestionMaItemHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_MA_ITEM_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Item).HasColumnName("ITEM");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMaItemHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MA_IT_HT_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMaItemHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MA_IT_HT_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMaItemHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_MA_ITEM_HTML");
        });

        modelBuilder.Entity<QuestionMc>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_MC");

            entity.HasIndex(e => new { e.Points, e.FkQuestion }, "IX_QUESTION_MC");

            entity.HasIndex(e => e.FkQuestion, "I_QUESTION_MC_QUESTION");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_MC1");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_MC2").IsClustered();

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.AllowMultiAnswer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ALLOW_MULTI_ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Randomize)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RANDOMIZE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMcFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MC_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMcFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MC_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMcs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_MC");
        });

        modelBuilder.Entity<QuestionMcChoice>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_MC_CHOICE");

            entity.HasIndex(e => e.FkQuestion, "I_QUESTION_MC_CHOICE_QUESTION");

            entity.HasIndex(e => new { e.FkQuestion, e.Sequence }, "I_QUESTION_MC_CHOICE_QUEST_SEQ");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Sequence }, "NDX_QUESTION_MC_CHOICE_DISTR1");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }, "NDX_QUESTION_MC_CHOICE_DISTR2");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired }, "NDX_QUESTION_MC_CHOICE_DISTR3").IsClustered();

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Choice).HasColumnName("CHOICE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.IsCorrect)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CORRECT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMcChoiceFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MC_CHOICE_CREATEBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMcChoiceFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MC_CHOICE_EXPIREBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMcChoices)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MC_CHOICE_QUESTION");
        });

        modelBuilder.Entity<QuestionMcChoiceHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_MC_CHOICE_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Choice).HasColumnName("CHOICE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.IsCorrect)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_CORRECT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionMcChoiceHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_MC_CH_HTML_CREATBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionMcChoiceHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_MC_CH_HTML_EXPIRBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionMcChoiceHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("FK_QUESTION_MC_CHOICE_QQ_HTML");
        });

        modelBuilder.Entity<QuestionSa>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_SA");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_SA1");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_SA2").IsClustered();

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.WhiteSpace)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("WHITE_SPACE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionSaFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_SA_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionSaFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_SA_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionSas)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_SA");
        });

        modelBuilder.Entity<QuestionSaHtml>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_SA_HTML");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Answer).HasColumnName("ANSWER");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionSaHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_SA_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionSaHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_SA_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionSaHtmls)
                .HasForeignKey(d => d.FkQuestion)
                .HasConstraintName("F_QUESTION_SA_HTML");
        });

        modelBuilder.Entity<QuestionSc>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Sequence }).IsClustered(false);

            entity.ToTable("QUESTION_SC");

            entity.HasIndex(e => new { e.FkSubQuestion, e.DateExpired, e.DateCreated }, "NDX_QUESTION_SC1");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkSubQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SUB_QUESTION");
            entity.Property(e => e.SubQuestionType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("SUB_QUESTION_TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionScFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_SC_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionScFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_SC_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionScFkQuestionNavigations)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_SC_QUESTION2");

            entity.HasOne(d => d.FkSubQuestionNavigation).WithMany(p => p.QuestionScFkSubQuestionNavigations)
                .HasForeignKey(d => d.FkSubQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_SC_QUESTION1");
        });

        modelBuilder.Entity<QuestionStatus>(entity =>
        {
            entity.ToTable("QUESTION_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<QuestionStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestionStatus, e.DateExpired, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_STATUS_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.DateCreated }, "NDX_QUESTION_STATUS_IMPL1");

            entity.Property(e => e.FkQuestionStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_STATUS");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_STAT_IMP_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_STAT_IMP_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.QuestionStatusImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_STATUS_IMPL_PROJECT");

            entity.HasOne(d => d.FkQuestionStatusNavigation).WithMany(p => p.QuestionStatusImpls)
                .HasForeignKey(d => d.FkQuestionStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_STAT_IMP_QUST_STAT");
        });

        modelBuilder.Entity<QuestionStem>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_STEM");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Stem).HasColumnName("STEM");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionStemFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_STEM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionStemFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_STEM_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionStems)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_STEM");
        });

        modelBuilder.Entity<QuestionTf>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("QUESTION_TF");

            entity.HasIndex(e => e.Points, "IX_QUESTION_TF");

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.DateCreated, e.Points }, "NDX_QUESTION_TF1").IsClustered();

            entity.HasIndex(e => new { e.FkQuestion, e.DateExpired, e.Points }, "NDX_QUESTION_TF2");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.IsTrue)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_TRUE");
            entity.Property(e => e.Points)
                .HasDefaultValue(0.0m)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.QuestionTfFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QUESTION_TF_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.QuestionTfFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_QUESTION_TF_EXPIREDBY");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.QuestionTfs)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_QUESTION_TF");
        });

        modelBuilder.Entity<Recycled>(entity =>
        {
            entity.HasKey(e => new { e.Type, e.FkProject, e.DateExpired, e.FkChild, e.DateCreated }).IsClustered(false);

            entity.ToTable("RECYCLED");

            entity.HasIndex(e => new { e.Type, e.FkProject, e.DateExpired, e.DateCreated, e.FkChild }, "NDX_RECYCLED1");

            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.RecycledFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_RECYCLED_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.RecycledFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_RECYCLED_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.Recycleds)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_RECYCLED_PROJECT");
        });

        modelBuilder.Entity<RevisionLogAnalysis>(entity =>
        {
            entity.HasKey(e => new { e.FkAnalysis, e.FkAnalysisImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_ANALYSIS");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkAnalysisImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkAnalysisNavigation).WithMany(p => p.RevisionLogAnalyses)
                .HasForeignKey(d => d.FkAnalysis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_ANAL_ANAL");

            entity.HasOne(d => d.FkAnalysisImplNavigation).WithMany(p => p.RevisionLogAnalyses)
                .HasForeignKey(d => d.FkAnalysisImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_ANAL_ANAL_IMPL");
        });

        modelBuilder.Entity<RevisionLogExam>(entity =>
        {
            entity.HasKey(e => new { e.FkExam, e.FkExamImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_EXAM");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkExamImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkExamNavigation).WithMany(p => p.RevisionLogExams)
                .HasForeignKey(d => d.FkExam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_EXAM_EXAM");

            entity.HasOne(d => d.FkExamImplNavigation).WithMany(p => p.RevisionLogExams)
                .HasForeignKey(d => d.FkExamImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_EXAM_EXAM_IMPL");
        });

        modelBuilder.Entity<RevisionLogObjective>(entity =>
        {
            entity.HasKey(e => new { e.FkObjective, e.FkObjectiveImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_OBJECTIVE");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkObjectiveImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.RevisionLogObjectives)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_OBJ_ObJ");

            entity.HasOne(d => d.FkObjectiveImplNavigation).WithMany(p => p.RevisionLogObjectives)
                .HasForeignKey(d => d.FkObjectiveImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_OBJ_OBJ_IMPL");
        });

        modelBuilder.Entity<RevisionLogProgram>(entity =>
        {
            entity.HasKey(e => new { e.FkProgram, e.FkProgramImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_PROGRAM");

            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkProgramImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.RevisionLogPrograms)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_PROG_PROG");

            entity.HasOne(d => d.FkProgramImplNavigation).WithMany(p => p.RevisionLogPrograms)
                .HasForeignKey(d => d.FkProgramImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_PROG_PROG_IMPL");
        });

        modelBuilder.Entity<RevisionLogQualCard>(entity =>
        {
            entity.HasKey(e => new { e.FkQualCard, e.FkQualCardImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_QUAL_CARD");

            entity.Property(e => e.FkQualCard)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD");
            entity.Property(e => e.FkQualCardImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_CARD_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkQualCardNavigation).WithMany(p => p.RevisionLogQualCards)
                .HasForeignKey(d => d.FkQualCard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_QUAL_QUAL");

            entity.HasOne(d => d.FkQualCardImplNavigation).WithMany(p => p.RevisionLogQualCards)
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.FkQualCardImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_QUAL_QUAL_IMPL");
        });

        modelBuilder.Entity<RevisionLogQuestion>(entity =>
        {
            entity.HasKey(e => new { e.FkQuestion, e.FkQuestionImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_QUESTION");

            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkQuestionNavigation).WithMany(p => p.RevisionLogQuestions)
                .HasForeignKey(d => d.FkQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_QUST_QUST");

            entity.HasOne(d => d.FkQuestionImplNavigation).WithMany(p => p.RevisionLogQuestions)
                .HasForeignKey(d => d.FkQuestionImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_QUST_QUST_IMPL");
        });

        modelBuilder.Entity<RevisionLogXref>(entity =>
        {
            entity.HasKey(e => new { e.FkXrefLib, e.FkXrefLibImpl }).IsClustered(false);

            entity.ToTable("REVISION_LOG_XREF");

            entity.Property(e => e.FkXrefLib)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB");
            entity.Property(e => e.FkXrefLibImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB_IMPL");
            entity.Property(e => e.AttributesChanged)
                .HasMaxLength(500)
                .HasColumnName("ATTRIBUTES_CHANGED");

            entity.HasOne(d => d.FkXrefLibNavigation).WithMany(p => p.RevisionLogXrefs)
                .HasForeignKey(d => d.FkXrefLib)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_XREF_XREF");

            entity.HasOne(d => d.FkXrefLibImplNavigation).WithMany(p => p.RevisionLogXrefs)
                .HasForeignKey(d => d.FkXrefLibImpl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_LOG_XREF_XREF_IMPL");
        });

        modelBuilder.Entity<RevisionTag>(entity =>
        {
            entity.ToTable("REVISION_TAG");

            entity.HasIndex(e => new { e.Archived, e.Name }, "NDX_REVISION_TAG1");

            entity.HasIndex(e => new { e.Archived, e.TagDatetime }, "NDX_REVISION_TAG2");

            entity.HasIndex(e => new { e.Archived, e.FkProject, e.Name }, "NDX_REVISION_TAG3");

            entity.HasIndex(e => new { e.Archived, e.FkProject, e.TagDatetime }, "NDX_REVISION_TAG4");

            entity.HasIndex(e => new { e.Archived, e.FkCreatedBy }, "NDX_REVISION_TAG5");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Archived)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkProject)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(125)
                .HasColumnName("NAME");
            entity.Property(e => e.TagDatetime)
                .HasColumnType("datetime")
                .HasColumnName("TAG_DATETIME");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.RevisionTags)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REVISION_TAG_CREATED_BY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.RevisionTags)
                .HasForeignKey(d => d.FkProject)
                .HasConstraintName("FK_REVISION_TAG_FK_PROJECT");
        });

        modelBuilder.Entity<RtfToHtmlMigration>(entity =>
        {
            entity.HasKey(e => new { e.FkData, e.DataType }).IsClustered(false);

            entity.ToTable("RTF_TO_HTML_MIGRATION");

            entity.Property(e => e.FkData)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA");
            entity.Property(e => e.DataType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("DATA_TYPE");
        });

        modelBuilder.Entity<RtfTrimList>(entity =>
        {
            entity.HasKey(e => new { e.FkData, e.DataType }).IsClustered(false);

            entity.ToTable("RTF_TRIM_LIST");

            entity.Property(e => e.FkData)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_DATA");
            entity.Property(e => e.DataType)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("DATA_TYPE");
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("SCHEDULED_JOBS");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.DateLastRun)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATE_LAST_RUN");
        });

        modelBuilder.Entity<SecurityGroup>(entity =>
        {
            entity.ToTable("SECURITY_GROUP");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<SecurityGroupImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkSecurityGroup, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("SECURITY_GROUP_IMPL");

            entity.HasIndex(e => new { e.Text, e.DateExpired, e.DateCreated }, "NDX_SECURITY_GROUP_IMPL1");

            entity.Property(e => e.FkSecurityGroup)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_SECURITY_GROUP");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.AnalysisAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ANALYSIS_ACCESS_LEVEL");
            entity.Property(e => e.CanApproveExam)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_APPROVE_EXAM");
            entity.Property(e => e.CanChangeCoursecatStatus)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_COURSECAT_STATUS");
            entity.Property(e => e.CanChangeLabels)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_LABELS");
            entity.Property(e => e.CanChangeObStatus)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_OB_STATUS");
            entity.Property(e => e.CanChangeProgStatus)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_PROG_STATUS");
            entity.Property(e => e.CanChangeQualcardStatus)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_QUALCARD_STATUS");
            entity.Property(e => e.CanChangeQuesStatus)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_QUES_STATUS");
            entity.Property(e => e.CanChangeTaskStatus)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_CHANGE_TASK_STATUS");
            entity.Property(e => e.CanLockQues)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_LOCK_QUES");
            entity.Property(e => e.CanLockTest)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_LOCK_TEST");
            entity.Property(e => e.CanOverrideQuesLock)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_OVERRIDE_QUES_LOCK");
            entity.Property(e => e.CanOverrideTestLock)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("CAN_OVERRIDE_TEST_LOCK");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.DocumentAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DOCUMENT_ACCESS_LEVEL");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ObjectiveAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("OBJECTIVE_ACCESS_LEVEL");
            entity.Property(e => e.ProgramAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PROGRAM_ACCESS_LEVEL");
            entity.Property(e => e.ReportAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REPORT_ACCESS_LEVEL");
            entity.Property(e => e.TableAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TABLE_ACCESS_LEVEL");
            entity.Property(e => e.TestAccessLevel)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TEST_ACCESS_LEVEL");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.SecurityGroupImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SECURITY_GROUP_IM_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.SecurityGroupImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_SECURITY_GROUP_IM_EXPIREDBY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.SecurityGroupImplFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .HasConstraintName("FK_SECURITY_GROUP_IMPL_PARENT");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.SecurityGroupImpls)
                .HasForeignKey(d => d.FkProject)
                .HasConstraintName("FK_SECURITY_GROUP_IMPL_PROJECT");

            entity.HasOne(d => d.FkSecurityGroupNavigation).WithMany(p => p.SecurityGroupImplFkSecurityGroupNavigations)
                .HasForeignKey(d => d.FkSecurityGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SECURITY_GROUP_IMPL_SEC_GRP");
        });

        modelBuilder.Entity<Sequencing>(entity =>
        {
            entity.HasKey(e => new { e.FkObjective, e.DateExpired, e.FkProgram, e.Sequence, e.DateCreated }).IsClustered(false);

            entity.ToTable("SEQUENCING");

            entity.HasIndex(e => new { e.FkProgram, e.FkObjective }, "IX_SEQUENCING");

            entity.HasIndex(e => e.FkObjective, "IX_SEQUENCING_2");

            entity.HasIndex(e => new { e.FkObjective, e.FkProgram, e.Sequence, e.FkProject }, "IX_SEQUENCING_3");

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.DateCreated, e.FkProgram }, "NDX_SEQUENCING1");

            entity.HasIndex(e => new { e.FkProgram, e.DateExpired, e.Sequence, e.FkObjective }, "NDX_SEQUENCING2").IsClustered();

            entity.HasIndex(e => new { e.FkObjective, e.DateExpired, e.FkProgram }, "NDX_SEQUENCING3");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Questions)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("QUESTIONS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.SequencingFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SEQUENCING_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.SequencingFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_SEQUENCING_EXPIREDBY");

            entity.HasOne(d => d.FkObjectiveNavigation).WithMany(p => p.Sequencings)
                .HasForeignKey(d => d.FkObjective)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_OBJECTIVE_PROGRAMS");

            entity.HasOne(d => d.FkProgramNavigation).WithMany(p => p.Sequencings)
                .HasForeignKey(d => d.FkProgram)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_SEQUENCING");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.Sequencings)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_SEQUENCING_PROJECT");
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SYSTEM_SETTINGS");

            entity.Property(e => e.PasswordWarnDays)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("PASSWORD_WARN_DAYS");
            entity.Property(e => e.PathContentTemplates)
                .HasMaxLength(260)
                .HasDefaultValue("ContentTemplates")
                .HasColumnName("PATH_CONTENT_TEMPLATES");
            entity.Property(e => e.PathGraphics)
                .HasMaxLength(260)
                .HasDefaultValue("Graphics")
                .HasColumnName("PATH_GRAPHICS");
            entity.Property(e => e.PathLicense)
                .HasMaxLength(260)
                .HasColumnName("PATH_LICENSE");
            entity.Property(e => e.PathOfficeReports)
                .HasMaxLength(260)
                .HasDefaultValue("OfficeReportTemplates")
                .HasColumnName("PATH_OFFICE_REPORTS");
            entity.Property(e => e.PathProjectTemplates)
                .HasMaxLength(260)
                .HasDefaultValue("ProjectTemplates")
                .HasColumnName("PATH_PROJECT_TEMPLATES");
            entity.Property(e => e.PathReports)
                .HasMaxLength(260)
                .HasDefaultValue("Reports")
                .HasColumnName("PATH_REPORTS");
            entity.Property(e => e.PathSpellCheck)
                .HasMaxLength(260)
                .HasColumnName("PATH_SPELL_CHECK");
            entity.Property(e => e.PathTestFilters)
                .HasMaxLength(260)
                .HasDefaultValue("Testfltr")
                .HasColumnName("PATH_TEST_FILTERS");
        });

        modelBuilder.Entity<TaskChangeImpact>(entity =>
        {
            entity.ToTable("TASK_CHANGE_IMPACT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TaskChangeImpactImpl>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.DateCreated, e.Sequence, e.FkTaskChangeImpact });

            entity.ToTable("TASK_CHANGE_IMPACT_IMPL");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkTaskChangeImpact)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_CHANGE_IMPACT");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.TaskChangeImpactImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_CHANGE_IMPACT_CREAT_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.TaskChangeImpactImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TASK_CHANGE_IMPACT_EXPIR_BY");

            entity.HasOne(d => d.FkTaskChangeImpactNavigation).WithMany(p => p.TaskChangeImpactImpls)
                .HasForeignKey(d => d.FkTaskChangeImpact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_CHANGE_IMPACT_TASK_IMP");
        });

        modelBuilder.Entity<TaskDeselection>(entity =>
        {
            entity.ToTable("TASK_DESELECTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TaskDeselectionImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkTaskDeselection, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("TASK_DESELECTION_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_TASK_DESELECTION_IMPL1");

            entity.Property(e => e.FkTaskDeselection)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_DESELECTION");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.TaskDeselectionImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_DESELEC_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.TaskDeselectionImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TASK_DESELEC_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.TaskDeselectionImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_DESELEC_IMPL_PROJECT");

            entity.HasOne(d => d.FkTaskDeselectionNavigation).WithMany(p => p.TaskDeselectionImpls)
                .HasForeignKey(d => d.FkTaskDeselection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_DESELEC_IMPL_TASK_DESL");
        });

        modelBuilder.Entity<TaskStatus>(entity =>
        {
            entity.ToTable("TASK_STATUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TaskStatusImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkProject, e.DateExpired, e.DateCreated, e.Sequence, e.FkTaskStatus }).IsClustered(false);

            entity.ToTable("TASK_STATUS_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.Sequence, e.FkTaskStatus }, "NDX_TASK_STATUS_IMPL1").IsClustered();

            entity.HasIndex(e => new { e.FkTaskStatus, e.DateExpired, e.DateCreated }, "NDX_TASK_STATUS_IMPL2");

            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.FkTaskStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TASK_STATUS");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("TEXT");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("TYPE");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.TaskStatusImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_STATUS_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.TaskStatusImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TASK_STATUS_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.TaskStatusImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_TASK_STATUS_IMPL_PROJECT");

            entity.HasOne(d => d.FkTaskStatusNavigation).WithMany(p => p.TaskStatusImpls)
                .HasForeignKey(d => d.FkTaskStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TASK_STATUS_IMPL_TASK_STATS");
        });

        modelBuilder.Entity<TimeSpan>(entity =>
        {
            entity.ToTable("TIME_SPAN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TimeSpanImpl>(entity =>
        {
            entity.HasKey(e => new { e.FkTimeSpan, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("TIME_SPAN_IMPL");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.DateCreated }, "NDX_TIME_SPAN_IMPL1");

            entity.Property(e => e.FkTimeSpan)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_SPAN");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.FkTimeType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_TYPE");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("TEXT");
            entity.Property(e => e.TimeSpan)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TIME_SPAN");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.TimeSpanImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_SPAN_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.TimeSpanImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TIME_SPAN_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.TimeSpanImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_SPAN_IMPL_PROJECT");

            entity.HasOne(d => d.FkTimeSpanNavigation).WithMany(p => p.TimeSpanImpls)
                .HasForeignKey(d => d.FkTimeSpan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_SPAN_IMPL_TIME_SPAN");

            entity.HasOne(d => d.FkTimeTypeNavigation).WithMany(p => p.TimeSpanImpls)
                .HasForeignKey(d => d.FkTimeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_SPAN_IMPL_TYPE");
        });

        modelBuilder.Entity<TimeType>(entity =>
        {
            entity.ToTable("TIME_TYPE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<TimeTypeImpl>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkTimeType, e.DateCreated }).IsClustered(false);

            entity.ToTable("TIME_TYPE_IMPL");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkTimeType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_TIME_TYPE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.TimeTypeImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_TYPE_IMPL_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.TimeTypeImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_TIME_TYPE_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkTimeTypeNavigation).WithMany(p => p.TimeTypeImpls)
                .HasForeignKey(d => d.FkTimeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TIME_TYPE_IMPL_TIME_TYPE");
        });

        modelBuilder.Entity<VdevVwebSession>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VDEV_VWEB_SESSION");

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(CONVERT([datetime2](0),getdate()))")
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.Parameters)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("PARAMETERS");
            entity.Property(e => e.SessionId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SESSION_ID");
        });

        modelBuilder.Entity<VlsBasicInferredCompletion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_BASIC_INFERRED_COMPLETION");

            entity.Property(e => e.ActualCourse)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ACTUAL_COURSE");
            entity.Property(e => e.ActualType).HasColumnName("ACTUAL_TYPE");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.Exempt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXEMPT");
            entity.Property(e => e.ExemptComments)
                .HasMaxLength(500)
                .HasColumnName("EXEMPT_COMMENTS");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQualEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_EVENT");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<VlsExamEventLearner>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_EXAM_EVENT_LEARNER");

            entity.Property(e => e.Acknowledged)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ACKNOWLEDGED");
            entity.Property(e => e.AcknowledgedDateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("ACKNOWLEDGED_DATE_COMPLETED");
            entity.Property(e => e.AdjScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ADJ_SCORE");
            entity.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("COMPANY");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateStarted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_STARTED");
            entity.Property(e => e.ExamAutoReentryAttempts)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("EXAM_AUTO_REENTRY_ATTEMPTS");
            entity.Property(e => e.ExamImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ExamImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.ExamScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("EXAM_SCORE");
            entity.Property(e => e.ExamSequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("EXAM_SEQUENCE");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkExamOnlineProfile)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_ONLINE_PROFILE");
            entity.Property(e => e.FkExamResults)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_RESULTS");
            entity.Property(e => e.FkExamType)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_TYPE");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkLearningEventOriginal)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT_ORIGINAL");
            entity.Property(e => e.FkProctor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROCTOR");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.ManualReentryEnabled)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MANUAL_REENTRY_ENABLED");
            entity.Property(e => e.PassingScore)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("PASSING_SCORE");
            entity.Property(e => e.Reviewed)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REVIEWED");
            entity.Property(e => e.ReviewedDateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("REVIEWED_DATE_COMPLETED");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TEXT");
        });

        modelBuilder.Entity<VlsExamObjective>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_EXAM_OBJECTIVE");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Objective)
                .HasMaxLength(1000)
                .HasColumnName("OBJECTIVE");
        });

        modelBuilder.Entity<VlsExamObjectiveQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_EXAM_OBJECTIVE_QUESTION");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Objective)
                .HasMaxLength(1000)
                .HasColumnName("OBJECTIVE");
        });

        modelBuilder.Entity<VlsExamObjectiveSc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_EXAM_OBJECTIVE_SC");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Objective)
                .HasMaxLength(1000)
                .HasColumnName("OBJECTIVE");
        });

        modelBuilder.Entity<VlsExamTestingEvent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_EXAM_TESTING_EVENT");

            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.IsSubq)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_SUBQ");
            entity.Property(e => e.Num1)
                .HasMaxLength(30)
                .HasColumnName("NUM1");
            entity.Property(e => e.Num2)
                .HasMaxLength(30)
                .HasColumnName("NUM2");
            entity.Property(e => e.Num3)
                .HasMaxLength(30)
                .HasColumnName("NUM3");
            entity.Property(e => e.Objective)
                .HasMaxLength(1000)
                .HasColumnName("OBJECTIVE");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.ResponseType)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("RESPONSE_TYPE");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("SCORE");
            entity.Property(e => e.SelectionOrder)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SELECTION_ORDER");
            entity.Property(e => e.Topic)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TOPIC");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<VlsInferredCompletion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_INFERRED_COMPLETION");

            entity.Property(e => e.ActualCourse)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ACTUAL_COURSE");
            entity.Property(e => e.ActualType).HasColumnName("ACTUAL_TYPE");
            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.Comments)
                .HasMaxLength(500)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.DateCompleted)
                .HasColumnType("datetime")
                .HasColumnName("DATE_COMPLETED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpires)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRES");
            entity.Property(e => e.Exempt)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("EXEMPT");
            entity.Property(e => e.ExemptComments)
                .HasMaxLength(500)
                .HasColumnName("EXEMPT_COMMENTS");
            entity.Property(e => e.FkCatalog)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CATALOG");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkLearningEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNING_EVENT");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQualEvent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUAL_EVENT");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.Status)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<VlsLearnerFeedback>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_LEARNER_FEEDBACK");

            entity.Property(e => e.Archive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ARCHIVE");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateResolved)
                .HasColumnType("datetime")
                .HasColumnName("DATE_RESOLVED");
            entity.Property(e => e.Exam)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("EXAM");
            entity.Property(e => e.Examdate)
                .HasColumnType("datetime")
                .HasColumnName("EXAMDATE");
            entity.Property(e => e.Feedback)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnName("FEEDBACK");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.FkAssignedTo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ASSIGNED_TO");
            entity.Property(e => e.FkCompany)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_COMPANY");
            entity.Property(e => e.FkExam)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM");
            entity.Property(e => e.FkExamStatus)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXAM_STATUS");
            entity.Property(e => e.FkLearner)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LEARNER");
            entity.Property(e => e.FkProctor)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROCTOR");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.FkQuestionImpl)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION_IMPL");
            entity.Property(e => e.FkResolvedByD)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RESOLVED_BY_D");
            entity.Property(e => e.FkResolvedByL)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_RESOLVED_BY_L");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Pfirst)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("PFIRST");
            entity.Property(e => e.Plast)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("PLAST");
            entity.Property(e => e.Resolution)
                .HasMaxLength(1000)
                .HasColumnName("RESOLUTION");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
            entity.Property(e => e.Viewed)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("VIEWED");
        });

        modelBuilder.Entity<VlsProHierarchy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PRO_HIERARCHY");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkChild)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CHILD");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.Sequence)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("SEQUENCE");
        });

        modelBuilder.Entity<VlsProgramAllTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_ALL_TASKS");

            entity.Property(e => e.AnalysisImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.ConsolidationDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ConsolidationDateExpired).HasColumnType("datetime");
            entity.Property(e => e.DirectObjectiveDateCreated).HasColumnType("datetime");
            entity.Property(e => e.DirectObjectiveDateExpired).HasColumnType("datetime");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.SequencingDateCreated).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateExpired).HasColumnType("datetime");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
        });

        modelBuilder.Entity<VlsProgramAssociatedTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_ASSOCIATED_TASKS");

            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
        });

        modelBuilder.Entity<VlsProgramConsolidTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_CONSOLID_TASKS");

            entity.Property(e => e.AnalysisImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.ConsolidationDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ConsolidationDateExpired).HasColumnType("datetime");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.SequencingDateCreated).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateExpired).HasColumnType("datetime");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
        });

        modelBuilder.Entity<VlsProgramDirectTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_DIRECT_TASKS");

            entity.Property(e => e.AnalysisImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.AnalysisLevelImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.DirectObjectiveDateCreated).HasColumnType("datetime");
            entity.Property(e => e.DirectObjectiveDateExpired).HasColumnType("datetime");
            entity.Property(e => e.FkAnalysis)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ANALYSIS");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.Id)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.SequencingDateCreated).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateExpired).HasColumnType("datetime");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
        });

        modelBuilder.Entity<VlsProgramPracticeQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_PRACTICE_QUESTION");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.ObSeq)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("OB_SEQ");
            entity.Property(e => e.Objective)
                .HasMaxLength(1000)
                .HasColumnName("OBJECTIVE");
            entity.Property(e => e.ObjectiveImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveQuestionDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveQuestionDateExpired).HasColumnType("datetime");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionHtmlDateCreated).HasColumnType("datetime");
            entity.Property(e => e.QuestionHtmlDateExpired).HasColumnType("datetime");
            entity.Property(e => e.QuestionImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.QuestionImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateCreated).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateExpired).HasColumnType("datetime");
            entity.Property(e => e.Stem)
                .HasColumnType("image")
                .HasColumnName("STEM");
            entity.Property(e => e.Time)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TIME");
            entity.Property(e => e.Topic)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("TOPIC");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.VlsQuestionTypeDateCreated).HasColumnType("datetime");
            entity.Property(e => e.VlsQuestionTypeDateExpired).HasColumnType("datetime");
        });

        modelBuilder.Entity<VlsProgramQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_PROGRAM_QUESTION");

            entity.Property(e => e.FkObjective)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_OBJECTIVE");
            entity.Property(e => e.FkProgram)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROGRAM");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.IsPractice)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_PRACTICE");
            entity.Property(e => e.ItemId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ITEM_ID");
            entity.Property(e => e.MustAppear)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("MUST_APPEAR");
            entity.Property(e => e.ObjectiveImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveImplId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ObjectiveImplID");
            entity.Property(e => e.ObjectiveQuestionDateCreated).HasColumnType("datetime");
            entity.Property(e => e.ObjectiveQuestionDateExpired).HasColumnType("datetime");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.QuestionImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.QuestionImplId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("QuestionImplID");
            entity.Property(e => e.QuestionStatusImplDateCreated).HasColumnType("datetime");
            entity.Property(e => e.QuestionStatusImplDateExpired).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateCreated).HasColumnType("datetime");
            entity.Property(e => e.SequencingDateExpired).HasColumnType("datetime");
            entity.Property(e => e.Time)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("TIME");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.VlsQuestionSimDateCreated).HasColumnType("datetime");
            entity.Property(e => e.VlsQuestionSimDateExpired).HasColumnType("datetime");
            entity.Property(e => e.VlsQuestionTypeDateCreated).HasColumnType("datetime");
            entity.Property(e => e.VlsQuestionTypeDateExpired).HasColumnType("datetime");
        });

        modelBuilder.Entity<VlsQuestionScenario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_QUESTION_SCENARIO");

            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("QUESTION_ID");
            entity.Property(e => e.QuestionScDateCreated).HasColumnType("datetime");
            entity.Property(e => e.QuestionScDateExpired).HasColumnType("datetime");
            entity.Property(e => e.SubQuestionId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("SUB_QUESTION_ID");
            entity.Property(e => e.Type)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TYPE");
            entity.Property(e => e.VlsSubQuestionTypeDateCreated).HasColumnType("datetime");
            entity.Property(e => e.VlsSubQuestionTypeDateExpired).HasColumnType("datetime");
        });

        modelBuilder.Entity<VlsQuestionSim>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_QUESTION_SIM");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.ItemId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ITEM_ID");
            entity.Property(e => e.QuestionId)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("QUESTION_ID");
        });

        modelBuilder.Entity<VlsQuestionType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_QUESTION_TYPE");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_TYPE");
        });

        modelBuilder.Entity<VlsSubQuestionType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_SUB_QUESTION_TYPE");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.QuestionType).HasColumnName("QUESTION_TYPE");
        });

        modelBuilder.Entity<VlsXrefQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VLS_XREF_QUESTION");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkItem)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ITEM");
            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.FkQuestion)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_QUESTION");
            entity.Property(e => e.Points)
                .HasColumnType("numeric(38, 2)")
                .HasColumnName("POINTS");
            entity.Property(e => e.Practice)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("PRACTICE");
            entity.Property(e => e.QuestionType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("QUESTION_TYPE");
        });

        modelBuilder.Entity<XrefLib>(entity =>
        {
            entity.ToTable("XREF_LIB");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");

            entity.HasMany(d => d.FkCompanies).WithMany(p => p.FkXrefLibs)
                .UsingEntity<Dictionary<string, object>>(
                    "LsXrefDisplay",
                    r => r.HasOne<LsCompany>().WithMany()
                        .HasForeignKey("FkCompany")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_XREF_DISPLAY_FK_COMPANY"),
                    l => l.HasOne<XrefLib>().WithMany()
                        .HasForeignKey("FkXrefLib")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_XREF_DISPLAY_FK_XREF_LIB"),
                    j =>
                    {
                        j.HasKey("FkXrefLib", "FkCompany");
                        j.ToTable("LS_XREF_DISPLAY");
                        j.IndexerProperty<decimal>("FkXrefLib")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_XREF_LIB");
                        j.IndexerProperty<decimal>("FkCompany")
                            .HasColumnType("numeric(12, 0)")
                            .HasColumnName("FK_COMPANY");
                    });
        });

        modelBuilder.Entity<XrefLibHtml>(entity =>
        {
            entity.HasKey(e => new { e.DateExpired, e.FkXrefLib, e.DateCreated }).IsClustered(false);

            entity.ToTable("XREF_LIB_HTML");

            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkXrefLib)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.Text).HasColumnName("TEXT");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.XrefLibHtmlFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_XREF_LIB_HTML_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.XrefLibHtmlFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_XREF_LIB_HTML_EXPIREDBY");

            entity.HasOne(d => d.FkXrefLibNavigation).WithMany(p => p.XrefLibHtmls)
                .HasForeignKey(d => d.FkXrefLib)
                .HasConstraintName("FK_XREF_LIB_HTML");
        });

        modelBuilder.Entity<XrefLibImpl>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("XREF_LIB_IMPL");

            entity.HasIndex(e => new { e.UserDefinedId, e.IsOrganizer, e.FkXrefLib }, "IX_XREF_LIB_IMPL");

            entity.HasIndex(e => new { e.FkXrefLib, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_IMPL1")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.IsOrganizer, e.DateCreated }, "NDX_XREF_LIB_IMPL2");

            entity.HasIndex(e => new { e.FkProject, e.DateExpired, e.FkParent, e.DateCreated }, "NDX_XREF_LIB_IMPL3");

            entity.HasIndex(e => new { e.UserDefinedId, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_IMPL4");

            entity.HasIndex(e => new { e.FkParent, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_IMPL5");

            entity.HasIndex(e => new { e.FkXrefLib, e.DateExpired }, "NDX_XREF_LIB_IMPL6").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");
            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.FkProject)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PROJECT");
            entity.Property(e => e.FkXrefLib)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_XREF_LIB");
            entity.Property(e => e.IsOrganizer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("IS_ORGANIZER");
            entity.Property(e => e.MajorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MAJOR_VERSION_NUMBER");
            entity.Property(e => e.MinorVersionNumber)
                .HasMaxLength(15)
                .HasColumnName("MINOR_VERSION_NUMBER");
            entity.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("TEXT");
            entity.Property(e => e.TextAscii)
                .HasMaxLength(1000)
                .HasColumnName("TEXT_ASCII");
            entity.Property(e => e.TextSort)
                .HasMaxLength(50)
                .HasColumnName("TEXT_SORT");
            entity.Property(e => e.UserDefinedId)
                .HasMaxLength(50)
                .HasColumnName("USER_DEFINED_ID");
            entity.Property(e => e.VersionComments).HasColumnName("VERSION_COMMENTS");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.XrefLibImplFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_XREF_LIB_IMPL_CREATED_BY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.XrefLibImplFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_XREF_LIB_IMPL_EXPIREDBY");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.XrefLibImplFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .HasConstraintName("FK_XREF_LIB_IMPL_FK_PARENT");

            entity.HasOne(d => d.FkProjectNavigation).WithMany(p => p.XrefLibImpls)
                .HasForeignKey(d => d.FkProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_XREF_LIB_IMPL_PROJECT");

            entity.HasOne(d => d.FkXrefLibNavigation).WithMany(p => p.XrefLibImplFkXrefLibNavigations)
                .HasForeignKey(d => d.FkXrefLib)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_XREF_LIB_IMPL");
        });

        modelBuilder.Entity<XrefLibLink>(entity =>
        {
            entity.HasKey(e => new { e.FkParent, e.FkItem, e.LinkToType, e.FkLinkTo, e.DateExpired, e.DateCreated }).IsClustered(false);

            entity.ToTable("XREF_LIB_LINK");

            entity.HasIndex(e => e.LinkToType, "IX_XREF_LIB_LINK");

            entity.HasIndex(e => new { e.FkLinkTo, e.LinkToType, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_LINK1").IsClustered();

            entity.HasIndex(e => new { e.FkLinkTo, e.LinkToType, e.FkItem, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_LINK2");

            entity.HasIndex(e => new { e.FkItem, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_LINK3");

            entity.HasIndex(e => new { e.FkItem, e.LinkToType, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_LINK4");

            entity.HasIndex(e => new { e.FkLinkTo, e.LinkToType, e.FkParent, e.FkItem, e.DateExpired, e.DateCreated }, "NDX_XREF_LIB_LINK5");

            entity.Property(e => e.FkParent)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_PARENT");
            entity.Property(e => e.FkItem)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_ITEM");
            entity.Property(e => e.LinkToType)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("LINK_TO_TYPE");
            entity.Property(e => e.FkLinkTo)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_LINK_TO");
            entity.Property(e => e.DateExpired)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("DATE_EXPIRED");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATED");
            entity.Property(e => e.FkCreatedBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_CREATED_BY");
            entity.Property(e => e.FkExpiredBy)
                .HasColumnType("numeric(12, 0)")
                .HasColumnName("FK_EXPIRED_BY");

            entity.HasOne(d => d.FkCreatedByNavigation).WithMany(p => p.XrefLibLinkFkCreatedByNavigations)
                .HasForeignKey(d => d.FkCreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_XREF_LIB_LINK_CREATEDBY");

            entity.HasOne(d => d.FkExpiredByNavigation).WithMany(p => p.XrefLibLinkFkExpiredByNavigations)
                .HasForeignKey(d => d.FkExpiredBy)
                .HasConstraintName("FK_XREF_LIB_LINK_EXPIREDBY");

            entity.HasOne(d => d.FkItemNavigation).WithMany(p => p.XrefLibLinkFkItemNavigations)
                .HasForeignKey(d => d.FkItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_XREF_LIB_LINK_ITEM");

            entity.HasOne(d => d.FkParentNavigation).WithMany(p => p.XrefLibLinkFkParentNavigations)
                .HasForeignKey(d => d.FkParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_XREF_LIB_LINK_PARENT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
