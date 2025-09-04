using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class EMP_DemoContext : DbContext
    {
        public EMP_DemoContext()
        {
        }

        public EMP_DemoContext(DbContextOptions<EMP_DemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CentralLock> CentralLocks { get; set; }
        public virtual DbSet<Cmi5AuLaunchDatum> Cmi5AuLaunchData { get; set; }
        public virtual DbSet<Cmi5ObjectiveMap> Cmi5ObjectiveMaps { get; set; }
        public virtual DbSet<Cmi5RegistrationToAu> Cmi5RegistrationToAus { get; set; }
        public virtual DbSet<Cmi5Session> Cmi5Sessions { get; set; }
        public virtual DbSet<CoursePhase> CoursePhases { get; set; }
        public virtual DbSet<LkTblCertificationType> LkTblCertificationTypes { get; set; }
        public virtual DbSet<LkTblExternalProvider> LkTblExternalProviders { get; set; }
        public virtual DbSet<LkTblIlaevalAttendanceVerification> LkTblIlaevalAttendanceVerifications { get; set; }
        public virtual DbSet<LkTblIlamethod> LkTblIlamethods { get; set; }
        public virtual DbSet<LkTblIlaproviderStatus> LkTblIlaproviderStatuses { get; set; }
        public virtual DbSet<LktblAnnualTrainingRequirement> LktblAnnualTrainingRequirements { get; set; }
        public virtual DbSet<LktblCoversheetType> LktblCoversheetTypes { get; set; }
        public virtual DbSet<LktblFormQuestionsAnswer> LktblFormQuestionsAnswers { get; set; }
        public virtual DbSet<LktblInstructor> LktblInstructors { get; set; }
        public virtual DbSet<LktblIssuingAuthority> LktblIssuingAuthorities { get; set; }
        public virtual DbSet<LktblLocation> LktblLocations { get; set; }
        public virtual DbSet<LktblOrganization> LktblOrganizations { get; set; }
        public virtual DbSet<LktblQueryBuilder> LktblQueryBuilders { get; set; }
        public virtual DbSet<LktblRatingScale> LktblRatingScales { get; set; }
        public virtual DbSet<LktblSelfRegStatus> LktblSelfRegStatuses { get; set; }
        public virtual DbSet<LktblSupplier> LktblSuppliers { get; set; }
        public virtual DbSet<LktblTrainingEvalDriver> LktblTrainingEvalDrivers { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<PensCommand> PensCommands { get; set; }
        public virtual DbSet<PensCommandScormPackage> PensCommandScormPackages { get; set; }
        public virtual DbSet<QryCarryOverTotalCeh> QryCarryOverTotalCehs { get; set; }
        public virtual DbSet<QryCehapplicationClass> QryCehapplicationClasses { get; set; }
        public virtual DbSet<QryCehapplicationDataReport> QryCehapplicationDataReports { get; set; }
        public virtual DbSet<QryClass> QryClasses { get; set; }
        public virtual DbSet<QryClassBasedRecord> QryClassBasedRecords { get; set; }
        public virtual DbSet<QryClassEvaluationComment> QryClassEvaluationComments { get; set; }
        public virtual DbSet<QryClassEvaluationSummary> QryClassEvaluationSummaries { get; set; }
        public virtual DbSet<QryClassRoster> QryClassRosters { get; set; }
        public virtual DbSet<QryClassesOverride> QryClassesOverrides { get; set; }
        public virtual DbSet<QryClassesToUpload> QryClassesToUploads { get; set; }
        public virtual DbSet<QryCourse> QryCourses { get; set; }
        public virtual DbSet<QryCriticalTask> QryCriticalTasks { get; set; }
        public virtual DbSet<QryDistinctTasksHistory> QryDistinctTasksHistories { get; set; }
        public virtual DbSet<QryEmpTrainingTowardReCertPer002> QryEmpTrainingTowardReCertPer002s { get; set; }
        public virtual DbSet<QryEmpTrainingTowardReCertPer002Final> QryEmpTrainingTowardReCertPer002Finals { get; set; }
        public virtual DbSet<QryEmpTrainingTowardReCertSharedBasic> QryEmpTrainingTowardReCertSharedBasics { get; set; }
        public virtual DbSet<QryEmployee> QryEmployees { get; set; }
        public virtual DbSet<QryExchangeOfElectronicRecordsAdvanced> QryExchangeOfElectronicRecordsAdvanceds { get; set; }
        public virtual DbSet<QryFullRecordByyear> QryFullRecordByyears { get; set; }
        public virtual DbSet<QryFutureClass> QryFutureClasses { get; set; }
        public virtual DbSet<QryHoursRequiredPerIdp> QryHoursRequiredPerIdps { get; set; }
        public virtual DbSet<QryIdpCompletionSmud> QryIdpCompletionSmuds { get; set; }
        public virtual DbSet<QryIlaByPositionSkBasicDelete> QryIlaByPositionSkBasicDeletes { get; set; }
        public virtual DbSet<QryInitialPositionTraining> QryInitialPositionTrainings { get; set; }
        public virtual DbSet<QryJonsEmployeeCourse> QryJonsEmployeeCourses { get; set; }
        public virtual DbSet<QryLastOjtevaluation> QryLastOjtevaluations { get; set; }
        public virtual DbSet<QryLatestCompleted> QryLatestCompleteds { get; set; }
        public virtual DbSet<QryLatestCompletedGrade> QryLatestCompletedGrades { get; set; }
        public virtual DbSet<QryLatestPlanned> QryLatestPlanneds { get; set; }
        public virtual DbSet<QryLatestRecord> QryLatestRecords { get; set; }
        public virtual DbSet<QryLatestRecordDetaile> QryLatestRecordDetailes { get; set; }
        public virtual DbSet<QryLatestRecordDetailesNow> QryLatestRecordDetailesNows { get; set; }
        public virtual DbSet<QryLatestRecordDetailesSmud> QryLatestRecordDetailesSmuds { get; set; }
        public virtual DbSet<QryMainCategory> QryMainCategories { get; set; }
        public virtual DbSet<QryMainDuty> QryMainDuties { get; set; }
        public virtual DbSet<QryMetRecertification> QryMetRecertifications { get; set; }
        public virtual DbSet<QryOveralEvaluationCommentsSelfPaced> QryOveralEvaluationCommentsSelfPaceds { get; set; }
        public virtual DbSet<QryPositionTrainingI> QryPositionTrainingIs { get; set; }
        public virtual DbSet<QryRecertificationHour> QryRecertificationHours { get; set; }
        public virtual DbSet<QrySelfPacedRecord> QrySelfPacedRecords { get; set; }
        public virtual DbSet<QrySelfPasedCompletion> QrySelfPasedCompletions { get; set; }
        public virtual DbSet<QryStudent> QryStudents { get; set; }
        public virtual DbSet<QryStudentFormsClass> QryStudentFormsClasses { get; set; }
        public virtual DbSet<QryTaskSkill> QryTaskSkills { get; set; }
        public virtual DbSet<QryTasksCriticality> QryTasksCriticalities { get; set; }
        public virtual DbSet<QryTasksNoStep> QryTasksNoSteps { get; set; }
        public virtual DbSet<QryTasksSpecification> QryTasksSpecifications { get; set; }
        public virtual DbSet<QryTasksSpecificationByPosition> QryTasksSpecificationByPositions { get; set; }
        public virtual DbSet<QryTrainingSummaryByPositionExtra> QryTrainingSummaryByPositionExtras { get; set; }
        public virtual DbSet<QryTrainingSummaryByPositionExtra1> QryTrainingSummaryByPositionExtras1 { get; set; }
        public virtual DbSet<QryXmltranscript> QryXmltranscripts { get; set; }
        public virtual DbSet<RsTblClassStudent> RsTblClassStudents { get; set; }
        public virtual DbSet<RsTblCoursesProcedure> RsTblCoursesProcedures { get; set; }
        public virtual DbSet<RsTblCoursesSkillsKnowledge> RsTblCoursesSkillsKnowledges { get; set; }
        public virtual DbSet<RsTblCoursesTask> RsTblCoursesTasks { get; set; }
        public virtual DbSet<RsTblEmployeesSummaryTask> RsTblEmployeesSummaryTasks { get; set; }
        public virtual DbSet<RsTblEmployeesTask> RsTblEmployeesTasks { get; set; }
        public virtual DbSet<RsTblPositionTraining> RsTblPositionTrainings { get; set; }
        public virtual DbSet<RsTblProceduresTask> RsTblProceduresTasks { get; set; }
        public virtual DbSet<RsTblReportingMgrEmployee> RsTblReportingMgrEmployees { get; set; }
        public virtual DbSet<RsTblTasksSkillsKnowledge> RsTblTasksSkillsKnowledges { get; set; }
        public virtual DbSet<RsTblTestTestItem> RsTblTestTestItems { get; set; }
        public virtual DbSet<RstblPositionsTask> RstblPositionsTasks { get; set; }
        public virtual DbSet<ScormActivity> ScormActivities { get; set; }
        public virtual DbSet<ScormActivityObjective> ScormActivityObjectives { get; set; }
        public virtual DbSet<ScormActivityRt> ScormActivityRts { get; set; }
        public virtual DbSet<ScormActivityRtcomment> ScormActivityRtcomments { get; set; }
        public virtual DbSet<ScormActivityRtintCorrectResp> ScormActivityRtintCorrectResps { get; set; }
        public virtual DbSet<ScormActivityRtintLearnerResp> ScormActivityRtintLearnerResps { get; set; }
        public virtual DbSet<ScormActivityRtintObjective> ScormActivityRtintObjectives { get; set; }
        public virtual DbSet<ScormActivityRtinteraction> ScormActivityRtinteractions { get; set; }
        public virtual DbSet<ScormActivityRtobjective> ScormActivityRtobjectives { get; set; }
        public virtual DbSet<ScormAiccSession> ScormAiccSessions { get; set; }
        public virtual DbSet<ScormEngineDbUpdate> ScormEngineDbUpdates { get; set; }
        public virtual DbSet<ScormLaunchHistory> ScormLaunchHistories { get; set; }
        public virtual DbSet<ScormMetadatum> ScormMetadata { get; set; }
        public virtual DbSet<ScormObject> ScormObjects { get; set; }
        public virtual DbSet<ScormObjectHierarchy> ScormObjectHierarchies { get; set; }
        public virtual DbSet<ScormObjectIdentifier> ScormObjectIdentifiers { get; set; }
        public virtual DbSet<ScormObjectSeqDatum> ScormObjectSeqData { get; set; }
        public virtual DbSet<ScormObjectSeqObjective> ScormObjectSeqObjectives { get; set; }
        public virtual DbSet<ScormObjectSeqObjectiveMap> ScormObjectSeqObjectiveMaps { get; set; }
        public virtual DbSet<ScormObjectSeqRollupRule> ScormObjectSeqRollupRules { get; set; }
        public virtual DbSet<ScormObjectSeqRollupRuleCond> ScormObjectSeqRollupRuleConds { get; set; }
        public virtual DbSet<ScormObjectSeqRule> ScormObjectSeqRules { get; set; }
        public virtual DbSet<ScormObjectSeqRuleCond> ScormObjectSeqRuleConds { get; set; }
        public virtual DbSet<ScormObjectSharedDataMap> ScormObjectSharedDataMaps { get; set; }
        public virtual DbSet<ScormObjectSspbucket> ScormObjectSspbuckets { get; set; }
        public virtual DbSet<ScormObjectStore> ScormObjectStores { get; set; }
        public virtual DbSet<ScormPackage> ScormPackages { get; set; }
        public virtual DbSet<ScormPackagePropertiesPreset> ScormPackagePropertiesPresets { get; set; }
        public virtual DbSet<ScormPackageProperty> ScormPackageProperties { get; set; }
        public virtual DbSet<ScormRegistration> ScormRegistrations { get; set; }
        public virtual DbSet<ScormRegistrationGlobalObj> ScormRegistrationGlobalObjs { get; set; }
        public virtual DbSet<ScormRegistrationSharedDataVal> ScormRegistrationSharedDataVals { get; set; }
        public virtual DbSet<ScormRegistrationSharedDatum> ScormRegistrationSharedData { get; set; }
        public virtual DbSet<ScormRegistrationSspbucket> ScormRegistrationSspbuckets { get; set; }
        public virtual DbSet<ScormRegistrationStatementMap> ScormRegistrationStatementMaps { get; set; }
        public virtual DbSet<ServerPath> ServerPaths { get; set; }
        public virtual DbSet<SysTblCopyTable> SysTblCopyTables { get; set; }
        public virtual DbSet<SysTblDbscriptsRun> SysTblDbscriptsRuns { get; set; }
        public virtual DbSet<SysTblDbversionsAll> SysTblDbversionsAlls { get; set; }
        public virtual DbSet<SysTblDbversionsLoaded> SysTblDbversionsLoadeds { get; set; }
        public virtual DbSet<SysTblMainMenu> SysTblMainMenus { get; set; }
        public virtual DbSet<SysTblMenu> SysTblMenus { get; set; }
        public virtual DbSet<SysTblReport> SysTblReports { get; set; }
        public virtual DbSet<SysTblSetting> SysTblSettings { get; set; }
        public virtual DbSet<SysTblSubMenu> SysTblSubMenus { get; set; }
        public virtual DbSet<TblActionTaken> TblActionTakens { get; set; }
        public virtual DbSet<TblAddlCertsInfo> TblAddlCertsInfos { get; set; }
        public virtual DbSet<TblAlert> TblAlerts { get; set; }
        public virtual DbSet<TblAuditMenuLevel> TblAuditMenuLevels { get; set; }
        public virtual DbSet<TblAuditReportsInsert> TblAuditReportsInserts { get; set; }
        public virtual DbSet<TblCarryOverHour> TblCarryOverHours { get; set; }
        public virtual DbSet<TblCategory> TblCategories { get; set; }
        public virtual DbSet<TblCertificationHistory> TblCertificationHistories { get; set; }
        public virtual DbSet<TblClass> TblClasses { get; set; }
        public virtual DbSet<TblClassNotificationEmployee> TblClassNotificationEmployees { get; set; }
        public virtual DbSet<TblClassNotificationHistory> TblClassNotificationHistories { get; set; }
        public virtual DbSet<TblClassNotificationSetting> TblClassNotificationSettings { get; set; }
        public virtual DbSet<TblClassTest> TblClassTests { get; set; }
        public virtual DbSet<TblClassTestOverride> TblClassTestOverrides { get; set; }
        public virtual DbSet<TblClassesDropReg> TblClassesDropRegs { get; set; }
        public virtual DbSet<TblClassesSelfReg> TblClassesSelfRegs { get; set; }
        public virtual DbSet<TblCoSk> TblCoSks { get; set; }
        public virtual DbSet<TblContentObject> TblContentObjects { get; set; }
        public virtual DbSet<TblContentObjectPresentation> TblContentObjectPresentations { get; set; }
        public virtual DbSet<TblCopresentationCo> TblCopresentationCos { get; set; }
        public virtual DbSet<TblCorack> TblCoracks { get; set; }
        public virtual DbSet<TblCoroom> TblCorooms { get; set; }
        public virtual DbSet<TblCoshelf> TblCoshelves { get; set; }
        public virtual DbSet<TblCourse> TblCourses { get; set; }
        public virtual DbSet<TblCourseEvaluationTrainingIssue> TblCourseEvaluationTrainingIssues { get; set; }
        public virtual DbSet<TblCourseSegment> TblCourseSegments { get; set; }
        public virtual DbSet<TblCourseSegmentDetail> TblCourseSegmentDetails { get; set; }
        public virtual DbSet<TblCourseSegmentLearningObjective> TblCourseSegmentLearningObjectives { get; set; }
        public virtual DbSet<TblCoversheet> TblCoversheets { get; set; }
        public virtual DbSet<TblDifproject> TblDifprojects { get; set; }
        public virtual DbSet<TblDifsurveyEmplSummary> TblDifsurveyEmplSummaries { get; set; }
        public virtual DbSet<TblDifsurveyEmployee> TblDifsurveyEmployees { get; set; }
        public virtual DbSet<TblDifsurveyPosition> TblDifsurveyPositions { get; set; }
        public virtual DbSet<TblDocumentLink> TblDocumentLinks { get; set; }
        public virtual DbSet<TblDocumentLinkType> TblDocumentLinkTypes { get; set; }
        public virtual DbSet<TblDocumentType> TblDocumentTypes { get; set; }
        public virtual DbSet<TblDutyArea> TblDutyAreas { get; set; }
        public virtual DbSet<TblElecImportClmnMap> TblElecImportClmnMaps { get; set; }
        public virtual DbSet<TblEmpLogin> TblEmpLogins { get; set; }
        public virtual DbSet<TblEmpTestInstance> TblEmpTestInstances { get; set; }
        public virtual DbSet<TblEmployee> TblEmployees { get; set; }
        public virtual DbSet<TblEmployeeAdditionalPosition> TblEmployeeAdditionalPositions { get; set; }
        public virtual DbSet<TblEmployeeGroup> TblEmployeeGroups { get; set; }
        public virtual DbSet<TblEmployeeGroupEmployee> TblEmployeeGroupEmployees { get; set; }
        public virtual DbSet<TblEmployeePositionHistory> TblEmployeePositionHistories { get; set; }
        public virtual DbSet<TblEmployeeStatusRecord> TblEmployeeStatusRecords { get; set; }
        public virtual DbSet<TblEmployeeTest> TblEmployeeTests { get; set; }
        public virtual DbSet<TblEmployeeTestResponse> TblEmployeeTestResponses { get; set; }
        public virtual DbSet<TblEmpsetting> TblEmpsettings { get; set; }
        public virtual DbSet<TblEmpuser> TblEmpusers { get; set; }
        public virtual DbSet<TblErrorLog> TblErrorLogs { get; set; }
        public virtual DbSet<TblExtImportHistory> TblExtImportHistories { get; set; }
        public virtual DbSet<TblFavoriteReport> TblFavoriteReports { get; set; }
        public virtual DbSet<TblForm> TblForms { get; set; }
        public virtual DbSet<TblFormQuestion> TblFormQuestions { get; set; }
        public virtual DbSet<TblFormSection> TblFormSections { get; set; }
        public virtual DbSet<TblGapProject> TblGapProjects { get; set; }
        public virtual DbSet<TblGapRating> TblGapRatings { get; set; }
        public virtual DbSet<TblGapstatus> TblGapstatuses { get; set; }
        public virtual DbSet<TblGroupSchedule> TblGroupSchedules { get; set; }
        public virtual DbSet<TblGroupScheduleEmployee> TblGroupScheduleEmployees { get; set; }
        public virtual DbSet<TblGroupScheduleIla> TblGroupScheduleIlas { get; set; }
        public virtual DbSet<TblIdp> TblIdps { get; set; }
        public virtual DbSet<TblIdpreleaseEmpSummary> TblIdpreleaseEmpSummaries { get; set; }
        public virtual DbSet<TblIdpreview> TblIdpreviews { get; set; }
        public virtual DbSet<TblIlaContentObject> TblIlaContentObjects { get; set; }
        public virtual DbSet<TblIlaDetail> TblIlaDetails { get; set; }
        public virtual DbSet<TblIlaGroup> TblIlaGroups { get; set; }
        public virtual DbSet<TblIlaNercstandard> TblIlaNercstandards { get; set; }
        public virtual DbSet<TblIlaSimulation> TblIlaSimulations { get; set; }
        public virtual DbSet<TblIlaSimulationCriterion> TblIlaSimulationCriteria { get; set; }
        public virtual DbSet<TblIlaSimulationEventGroup> TblIlaSimulationEventGroups { get; set; }
        public virtual DbSet<TblIlaSimulationObjective> TblIlaSimulationObjectives { get; set; }
        public virtual DbSet<TblIlaSimulationPosition> TblIlaSimulationPositions { get; set; }
        public virtual DbSet<TblIlaSimulationScript> TblIlaSimulationScripts { get; set; }
        public virtual DbSet<TblIlaTest> TblIlaTests { get; set; }
        public virtual DbSet<TblIlaTrainingTopic> TblIlaTrainingTopics { get; set; }
        public virtual DbSet<TblIlagroupIla> TblIlagroupIlas { get; set; }
        public virtual DbSet<TblIlaresource> TblIlaresources { get; set; }
        public virtual DbSet<TblImage> TblImages { get; set; }
        public virtual DbSet<TblImageSize> TblImageSizes { get; set; }
        public virtual DbSet<TblInstructorsAdministrator> TblInstructorsAdministrators { get; set; }
        public virtual DbSet<TblLabelReplacementText> TblLabelReplacementTexts { get; set; }
        public virtual DbSet<TblLinkedDocument> TblLinkedDocuments { get; set; }
        public virtual DbSet<TblMenuItem> TblMenuItems { get; set; }
        public virtual DbSet<TblNercstandard> TblNercstandards { get; set; }
        public virtual DbSet<TblNercstandardsCategory> TblNercstandardsCategories { get; set; }
        public virtual DbSet<TblObjectivesUserAdd> TblObjectivesUserAdds { get; set; }
        public virtual DbSet<TblOjtevaluator> TblOjtevaluators { get; set; }
        public virtual DbSet<TblOjthistory> TblOjthistories { get; set; }
        public virtual DbSet<TblOjthistoryQuestion> TblOjthistoryQuestions { get; set; }
        public virtual DbSet<TblOjthistoryStep> TblOjthistorySteps { get; set; }
        public virtual DbSet<TblOjtinstruction> TblOjtinstructions { get; set; }
        public virtual DbSet<TblOjtreportSelectedSetting> TblOjtreportSelectedSettings { get; set; }
        public virtual DbSet<TblOjtreportSettingsDetail> TblOjtreportSettingsDetails { get; set; }
        public virtual DbSet<TblOnlineClass> TblOnlineClasses { get; set; }
        public virtual DbSet<TblPerspectiveCourse> TblPerspectiveCourses { get; set; }
        public virtual DbSet<TblPerspectiveCourseArchive> TblPerspectiveCourseArchives { get; set; }
        public virtual DbSet<TblPerspectiveCourseSnapshot> TblPerspectiveCourseSnapshots { get; set; }
        public virtual DbSet<TblPhase> TblPhases { get; set; }
        public virtual DbSet<TblPosTaskAnnualReview> TblPosTaskAnnualReviews { get; set; }
        public virtual DbSet<TblPosition> TblPositions { get; set; }
        public virtual DbSet<TblPositionTasksHistory> TblPositionTasksHistories { get; set; }
        public virtual DbSet<TblPositionTrainingProgram> TblPositionTrainingPrograms { get; set; }
        public virtual DbSet<TblProcReleaseEmpSummary> TblProcReleaseEmpSummaries { get; set; }
        public virtual DbSet<TblProcedure> TblProcedures { get; set; }
        public virtual DbSet<TblProcedureResource> TblProcedureResources { get; set; }
        public virtual DbSet<TblProceduresHistory> TblProceduresHistories { get; set; }
        public virtual DbSet<TblProceduresTasksHistory> TblProceduresTasksHistories { get; set; }
        public virtual DbSet<TblProctor> TblProctors { get; set; }
        public virtual DbSet<TblQtsmgrOrganization> TblQtsmgrOrganizations { get; set; }
        public virtual DbSet<TblReportingManager> TblReportingManagers { get; set; }
        public virtual DbSet<TblReportsField> TblReportsFields { get; set; }
        public virtual DbSet<TblRsaw> TblRsaws { get; set; }
        public virtual DbSet<TblRsaw2> TblRsaw2s { get; set; }
        public virtual DbSet<TblRsaw2Change> TblRsaw2Changes { get; set; }
        public virtual DbSet<TblRsaw2Evidence> TblRsaw2Evidences { get; set; }
        public virtual DbSet<TblRsaw2Expert> TblRsaw2Experts { get; set; }
        public virtual DbSet<TblRsaw2Modified> TblRsaw2Modifieds { get; set; }
        public virtual DbSet<TblRsawChange> TblRsawChanges { get; set; }
        public virtual DbSet<TblRsawEvidence> TblRsawEvidences { get; set; }
        public virtual DbSet<TblRsawExpert> TblRsawExperts { get; set; }
        public virtual DbSet<TblRsawModified> TblRsawModifieds { get; set; }
        public virtual DbSet<TblSafetyHazard> TblSafetyHazards { get; set; }
        public virtual DbSet<TblSafetyHazardAbatement> TblSafetyHazardAbatements { get; set; }
        public virtual DbSet<TblSafetyHazardCategory> TblSafetyHazardCategories { get; set; }
        public virtual DbSet<TblSafetyHazardControl> TblSafetyHazardControls { get; set; }
        public virtual DbSet<TblSafetyHazardEo> TblSafetyHazardEos { get; set; }
        public virtual DbSet<TblSafetyHazardIla> TblSafetyHazardIlas { get; set; }
        public virtual DbSet<TblSafetyHazardTask> TblSafetyHazardTasks { get; set; }
        public virtual DbSet<TblSaveQuery> TblSaveQueries { get; set; }
        public virtual DbSet<TblSaveQueryCriterion> TblSaveQueryCriteria { get; set; }
        public virtual DbSet<TblSaveQuerySelectedField> TblSaveQuerySelectedFields { get; set; }
        public virtual DbSet<TblSkProcedure> TblSkProcedures { get; set; }
        public virtual DbSet<TblSkillsKnowledge> TblSkillsKnowledges { get; set; }
        public virtual DbSet<TblSmudCourseDesignReviewer> TblSmudCourseDesignReviewers { get; set; }
        public virtual DbSet<TblSmudCourseDevelopReviewer> TblSmudCourseDevelopReviewers { get; set; }
        public virtual DbSet<TblSmudCourseImplementReviewer> TblSmudCourseImplementReviewers { get; set; }
        public virtual DbSet<TblSmudDesignDefaultView> TblSmudDesignDefaultViews { get; set; }
        public virtual DbSet<TblSmudTrainingTopic> TblSmudTrainingTopics { get; set; }
        public virtual DbSet<TblSmudTrainingTopicsHeading> TblSmudTrainingTopicsHeadings { get; set; }
        public virtual DbSet<TblSmudcourseDesign> TblSmudcourseDesigns { get; set; }
        public virtual DbSet<TblSmudcourseDesignDelieveryMethod> TblSmudcourseDesignDelieveryMethods { get; set; }
        public virtual DbSet<TblSmudcourseDesignEnablingObjective> TblSmudcourseDesignEnablingObjectives { get; set; }
        public virtual DbSet<TblSmudcourseDesignNerc> TblSmudcourseDesignNercs { get; set; }
        public virtual DbSet<TblSmudcourseDesignPrerequistie> TblSmudcourseDesignPrerequisties { get; set; }
        public virtual DbSet<TblSmudcourseDesignProcedure> TblSmudcourseDesignProcedures { get; set; }
        public virtual DbSet<TblSmudcourseDesignResource> TblSmudcourseDesignResources { get; set; }
        public virtual DbSet<TblSmudcourseDesignSafetyHazard> TblSmudcourseDesignSafetyHazards { get; set; }
        public virtual DbSet<TblSmudcourseDesignSegment> TblSmudcourseDesignSegments { get; set; }
        public virtual DbSet<TblSmudcourseDesignTargetAudience> TblSmudcourseDesignTargetAudiences { get; set; }
        public virtual DbSet<TblSmudcourseDesignTask> TblSmudcourseDesignTasks { get; set; }
        public virtual DbSet<TblSmudcourseDesignTrainingTopic> TblSmudcourseDesignTrainingTopics { get; set; }
        public virtual DbSet<TblSmudcourseDevelop> TblSmudcourseDevelops { get; set; }
        public virtual DbSet<TblSmudcourseEvaluation> TblSmudcourseEvaluations { get; set; }
        public virtual DbSet<TblSmudcourseEvaluationDefaultView> TblSmudcourseEvaluationDefaultViews { get; set; }
        public virtual DbSet<TblSmudcourseEvaluationTestAnalysis> TblSmudcourseEvaluationTestAnalyses { get; set; }
        public virtual DbSet<TblSmudcourseImplement> TblSmudcourseImplements { get; set; }
        public virtual DbSet<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassSchedules { get; set; }
        public virtual DbSet<TblSmudsegmentsLinkObjective> TblSmudsegmentsLinkObjectives { get; set; }
        public virtual DbSet<TblSmudsegmentsNercStandard> TblSmudsegmentsNercStandards { get; set; }
        public virtual DbSet<TblStudentEvaluationOverride> TblStudentEvaluationOverrides { get; set; }
        public virtual DbSet<TblStudentForm> TblStudentForms { get; set; }
        public virtual DbSet<TblStudentFormsAnswer> TblStudentFormsAnswers { get; set; }
        public virtual DbSet<TblSupportingDoc> TblSupportingDocs { get; set; }
        public virtual DbSet<TblTargetAudience> TblTargetAudiences { get; set; }
        public virtual DbSet<TblTask> TblTasks { get; set; }
        public virtual DbSet<TblTaskAuditChangeType> TblTaskAuditChangeTypes { get; set; }
        public virtual DbSet<TblTaskIntroductionType> TblTaskIntroductionTypes { get; set; }
        public virtual DbSet<TblTaskLinkage> TblTaskLinkages { get; set; }
        public virtual DbSet<TblTaskSubStep> TblTaskSubSteps { get; set; }
        public virtual DbSet<TblTaskToolList> TblTaskToolLists { get; set; }
        public virtual DbSet<TblTasksAuditHistory> TblTasksAuditHistories { get; set; }
        public virtual DbSet<TblTasksHistory> TblTasksHistories { get; set; }
        public virtual DbSet<TblTasksIntroduction> TblTasksIntroductions { get; set; }
        public virtual DbSet<TblTasksQuestion> TblTasksQuestions { get; set; }
        public virtual DbSet<TblTasksReleasedQual> TblTasksReleasedQuals { get; set; }
        public virtual DbSet<TblTasksSkillAssessment> TblTasksSkillAssessments { get; set; }
        public virtual DbSet<TblTaxonomy> TblTaxonomies { get; set; }
        public virtual DbSet<TblTchistory> TblTchistories { get; set; }
        public virtual DbSet<TblTdtimage> TblTdtimages { get; set; }
        public virtual DbSet<TblTdtrandomReview> TblTdtrandomReviews { get; set; }
        public virtual DbSet<TblTdtrandomReviewDetail> TblTdtrandomReviewDetails { get; set; }
        public virtual DbSet<TblTempCat> TblTempCats { get; set; }
        public virtual DbSet<TblTempDelinquency> TblTempDelinquencies { get; set; }
        public virtual DbSet<TblTempDuty> TblTempDuties { get; set; }
        public virtual DbSet<TblTempEvaluationDatum> TblTempEvaluationData { get; set; }
        public virtual DbSet<TblTempList> TblTempLists { get; set; }
        public virtual DbSet<TblTempOjthistory> TblTempOjthistories { get; set; }
        public virtual DbSet<TblTest> TblTests { get; set; }
        public virtual DbSet<TblTestItem> TblTestItems { get; set; }
        public virtual DbSet<TblTestItemType> TblTestItemTypes { get; set; }
        public virtual DbSet<TblTestRecallHistory> TblTestRecallHistories { get; set; }
        public virtual DbSet<TblTestStatus> TblTestStatuses { get; set; }
        public virtual DbSet<TblTestitemDistractor> TblTestitemDistractors { get; set; }
        public virtual DbSet<TblTmcompHistoryByEmpSelectedTempList> TblTmcompHistoryByEmpSelectedTempLists { get; set; }
        public virtual DbSet<TblTmcompHistorySelectedTempList> TblTmcompHistorySelectedTempLists { get; set; }
        public virtual DbSet<TblTmcompletionHistoryByEmpTempList> TblTmcompletionHistoryByEmpTempLists { get; set; }
        public virtual DbSet<TblTmcompletionHistoryTempList> TblTmcompletionHistoryTempLists { get; set; }
        public virtual DbSet<TblTmselectedTempList> TblTmselectedTempLists { get; set; }
        public virtual DbSet<TblTmtempList> TblTmtempLists { get; set; }
        public virtual DbSet<TblTrainingEvalIssue> TblTrainingEvalIssues { get; set; }
        public virtual DbSet<TblTrainingEvalIssueDeliverable> TblTrainingEvalIssueDeliverables { get; set; }
        public virtual DbSet<TblTrainingEvalIssueTask> TblTrainingEvalIssueTasks { get; set; }
        public virtual DbSet<TblTrainingIssueAnnualReview> TblTrainingIssueAnnualReviews { get; set; }
        public virtual DbSet<TblTrainingIssueSupportingDoc> TblTrainingIssueSupportingDocs { get; set; }
        public virtual DbSet<TblTrainingModule> TblTrainingModules { get; set; }
        public virtual DbSet<TblTrainingModuleEoObjective> TblTrainingModuleEoObjectives { get; set; }
        public virtual DbSet<TblTrainingModuleIla> TblTrainingModuleIlas { get; set; }
        public virtual DbSet<TblTrainingModuleIlaObjective> TblTrainingModuleIlaObjectives { get; set; }
        public virtual DbSet<TblTrainingModuleProcedure> TblTrainingModuleProcedures { get; set; }
        public virtual DbSet<TblTrainingModuleProcedureNote> TblTrainingModuleProcedureNotes { get; set; }
        public virtual DbSet<TblTrainingModuleResource> TblTrainingModuleResources { get; set; }
        public virtual DbSet<TblTrainingModuleSk> TblTrainingModuleSks { get; set; }
        public virtual DbSet<TblTrainingModuleTask> TblTrainingModuleTasks { get; set; }
        public virtual DbSet<TblTrainingModuleTasksObjective> TblTrainingModuleTasksObjectives { get; set; }
        public virtual DbSet<TblTrainingPhase> TblTrainingPhases { get; set; }
        public virtual DbSet<TblUserActivityLog> TblUserActivityLogs { get; set; }
        public virtual DbSet<TblWarningMsgHistory> TblWarningMsgHistories { get; set; }
        public virtual DbSet<TblWarningSetting> TblWarningSettings { get; set; }
        public virtual DbSet<TenantConnectorContent> TenantConnectorContents { get; set; }
        public virtual DbSet<TenantCredential> TenantCredentials { get; set; }
        public virtual DbSet<TenantDbUpdate> TenantDbUpdates { get; set; }
        public virtual DbSet<TenantPluginConfiguration> TenantPluginConfigurations { get; set; }
        public virtual DbSet<TenantPluginObjStore> TenantPluginObjStores { get; set; }
        public virtual DbSet<TenantProperty> TenantProperties { get; set; }
        public virtual DbSet<TenantTinCanForwardingMap> TenantTinCanForwardingMaps { get; set; }
        public virtual DbSet<TinCanActivityProvider> TinCanActivityProviders { get; set; }
        public virtual DbSet<TinCanActivityProviderMap> TinCanActivityProviderMaps { get; set; }
        public virtual DbSet<TinCanActorProperty> TinCanActorProperties { get; set; }
        public virtual DbSet<TinCanAgent> TinCanAgents { get; set; }
        public virtual DbSet<TinCanContentToken> TinCanContentTokens { get; set; }
        public virtual DbSet<TinCanContextActivity> TinCanContextActivities { get; set; }
        public virtual DbSet<TinCanDocument> TinCanDocuments { get; set; }
        public virtual DbSet<TinCanLaunchToken> TinCanLaunchTokens { get; set; }
        public virtual DbSet<TinCanObjectStore> TinCanObjectStores { get; set; }
        public virtual DbSet<TinCanPackage> TinCanPackages { get; set; }
        public virtual DbSet<TinCanPermission> TinCanPermissions { get; set; }
        public virtual DbSet<TinCanRegistration> TinCanRegistrations { get; set; }
        public virtual DbSet<TinCanRelatedActivity> TinCanRelatedActivities { get; set; }
        public virtual DbSet<TinCanRelatedAgent> TinCanRelatedAgents { get; set; }
        public virtual DbSet<TinCanStatementIndex> TinCanStatementIndices { get; set; }
        public virtual DbSet<TinCanTargetStatement> TinCanTargetStatements { get; set; }
        public virtual DbSet<VAllClassBasedRecord> VAllClassBasedRecords { get; set; }
        public virtual DbSet<VAllSelfPacedRecord> VAllSelfPacedRecords { get; set; }
        public virtual DbSet<VQtsAdditionalCertsForIla> VQtsAdditionalCertsForIlas { get; set; }
        public virtual DbSet<VQtsCertificate> VQtsCertificates { get; set; }
        public virtual DbSet<VQtsCertificateAllCeh> VQtsCertificateAllCehs { get; set; }
        public virtual DbSet<VQtsClassBasedRecord> VQtsClassBasedRecords { get; set; }
        public virtual DbSet<VQtsClassEvaluationComment> VQtsClassEvaluationComments { get; set; }
        public virtual DbSet<VQtsClassEvaluationSummary> VQtsClassEvaluationSummaries { get; set; }
        public virtual DbSet<VQtsClassesAll> VQtsClassesAlls { get; set; }
        public virtual DbSet<VQtsCoursesSkillsRelated> VQtsCoursesSkillsRelateds { get; set; }
        public virtual DbSet<VQtsCoursesTask> VQtsCoursesTasks { get; set; }
        public virtual DbSet<VQtsCriticalTask> VQtsCriticalTasks { get; set; }
        public virtual DbSet<VQtsDutyArea> VQtsDutyAreas { get; set; }
        public virtual DbSet<VQtsEmpPosChange> VQtsEmpPosChanges { get; set; }
        public virtual DbSet<VQtsEmpPosChangeDesk> VQtsEmpPosChangeDesks { get; set; }
        public virtual DbSet<VQtsEmployeePositionTraining> VQtsEmployeePositionTrainings { get; set; }
        public virtual DbSet<VQtsEmployeePositionTrainingAllDataDesk> VQtsEmployeePositionTrainingAllDataDesks { get; set; }
        public virtual DbSet<VQtsEmployeePositionTrainingAllDatum> VQtsEmployeePositionTrainingAllData { get; set; }
        public virtual DbSet<VQtsEmployeePositionTrainingPreviou> VQtsEmployeePositionTrainingPrevious { get; set; }
        public virtual DbSet<VQtsEmployeeTraining> VQtsEmployeeTrainings { get; set; }
        public virtual DbSet<VQtsEmployeeTrainingDesk> VQtsEmployeeTrainingDesks { get; set; }
        public virtual DbSet<VQtsEmployeesDum> VQtsEmployeesDa { get; set; }
        public virtual DbSet<VQtsEmployeesTask> VQtsEmployeesTasks { get; set; }
        public virtual DbSet<VQtsEnablingObjective> VQtsEnablingObjectives { get; set; }
        public virtual DbSet<VQtsEobyPosition> VQtsEobyPositions { get; set; }
        public virtual DbSet<VQtsEobyTask> VQtsEobyTasks { get; set; }
        public virtual DbSet<VQtsFullRecordByYear> VQtsFullRecordByYears { get; set; }
        public virtual DbSet<VQtsIdpcompletion> VQtsIdpcompletions { get; set; }
        public virtual DbSet<VQtsIdplatestRecordDetail> VQtsIdplatestRecordDetails { get; set; }
        public virtual DbSet<VQtsInitialPositionTraining> VQtsInitialPositionTrainings { get; set; }
        public virtual DbSet<VQtsLabelReplacement> VQtsLabelReplacements { get; set; }
        public virtual DbSet<VQtsLastOjtevaluation> VQtsLastOjtevaluations { get; set; }
        public virtual DbSet<VQtsLatestCompleted> VQtsLatestCompleteds { get; set; }
        public virtual DbSet<VQtsLatestCompletedGrade> VQtsLatestCompletedGrades { get; set; }
        public virtual DbSet<VQtsLatestPlanned> VQtsLatestPlanneds { get; set; }
        public virtual DbSet<VQtsLatestRecord> VQtsLatestRecords { get; set; }
        public virtual DbSet<VQtsLatestRecordDetail> VQtsLatestRecordDetails { get; set; }
        public virtual DbSet<VQtsLatestScheduled> VQtsLatestScheduleds { get; set; }
        public virtual DbSet<VQtsLatestWaved> VQtsLatestWaveds { get; set; }
        public virtual DbSet<VQtsMainCategory> VQtsMainCategories { get; set; }
        public virtual DbSet<VQtsMainDuty> VQtsMainDuties { get; set; }
        public virtual DbSet<VQtsMainTask> VQtsMainTasks { get; set; }
        public virtual DbSet<VQtsManagerClass> VQtsManagerClasses { get; set; }
        public virtual DbSet<VQtsManagerCourse> VQtsManagerCourses { get; set; }
        public virtual DbSet<VQtsManagerEmployee> VQtsManagerEmployees { get; set; }
        public virtual DbSet<VQtsManagerOrganization> VQtsManagerOrganizations { get; set; }
        public virtual DbSet<VQtsManagerPosition> VQtsManagerPositions { get; set; }
        public virtual DbSet<VQtsMaxCoursesTasksSequence> VQtsMaxCoursesTasksSequences { get; set; }
        public virtual DbSet<VQtsMaxEvalDate> VQtsMaxEvalDates { get; set; }
        public virtual DbSet<VQtsMaxInitialVersion> VQtsMaxInitialVersions { get; set; }
        public virtual DbSet<VQtsMaxPosTaskAddDate> VQtsMaxPosTaskAddDates { get; set; }
        public virtual DbSet<VQtsMaxPosTaskAnnualReview> VQtsMaxPosTaskAnnualReviews { get; set; }
        public virtual DbSet<VQtsMaxPosTaskAnnualReviewDate> VQtsMaxPosTaskAnnualReviewDates { get; set; }
        public virtual DbSet<VQtsMaxTaskHistory> VQtsMaxTaskHistories { get; set; }
        public virtual DbSet<VQtsMaxTaskHistoryDate> VQtsMaxTaskHistoryDates { get; set; }
        public virtual DbSet<VQtsMaxVersion> VQtsMaxVersions { get; set; }
        public virtual DbSet<VQtsMinPosTaskAfterAdd> VQtsMinPosTaskAfterAdds { get; set; }
        public virtual DbSet<VQtsNercrelatedTraining> VQtsNercrelatedTrainings { get; set; }
        public virtual DbSet<VQtsNercreportedClass> VQtsNercreportedClasses { get; set; }
        public virtual DbSet<VQtsNewOrModifiedTask> VQtsNewOrModifiedTasks { get; set; }
        public virtual DbSet<VQtsOjtsortOrder> VQtsOjtsortOrders { get; set; }
        public virtual DbSet<VQtsOjttask> VQtsOjttasks { get; set; }
        public virtual DbSet<VQtsPosTaskAfterAdd> VQtsPosTaskAfterAdds { get; set; }
        public virtual DbSet<VQtsPosTaskDate> VQtsPosTaskDates { get; set; }
        public virtual DbSet<VQtsPositionTask> VQtsPositionTasks { get; set; }
        public virtual DbSet<VQtsPositionTrainingI> VQtsPositionTrainingIs { get; set; }
        public virtual DbSet<VQtsPositionsTask> VQtsPositionsTasks { get; set; }
        public virtual DbSet<VQtsProceduresHistory> VQtsProceduresHistories { get; set; }
        public virtual DbSet<VQtsProceduresTask> VQtsProceduresTasks { get; set; }
        public virtual DbSet<VQtsRptSstrainingGuideSkillsAndTask> VQtsRptSstrainingGuideSkillsAndTasks { get; set; }
        public virtual DbSet<VQtsScormpackage> VQtsScormpackages { get; set; }
        public virtual DbSet<VQtsScrub> VQtsScrubs { get; set; }
        public virtual DbSet<VQtsShazByPosition> VQtsShazByPositions { get; set; }
        public virtual DbSet<VQtsSkbyPosition> VQtsSkbyPositions { get; set; }
        public virtual DbSet<VQtsSkbyTask> VQtsSkbyTasks { get; set; }
        public virtual DbSet<VQtsSkillsKnowledge> VQtsSkillsKnowledges { get; set; }
        public virtual DbSet<VQtsStep> VQtsSteps { get; set; }
        public virtual DbSet<VQtsStudentFormsAverage> VQtsStudentFormsAverages { get; set; }
        public virtual DbSet<VQtsStudentFormsAvgByCourse> VQtsStudentFormsAvgByCourses { get; set; }
        public virtual DbSet<VQtsSubDuty> VQtsSubDuties { get; set; }
        public virtual DbSet<VQtsTask> VQtsTasks { get; set; }
        public virtual DbSet<VQtsTaskAnnualReview> VQtsTaskAnnualReviews { get; set; }
        public virtual DbSet<VQtsTaskChangeAfterPositionAdd> VQtsTaskChangeAfterPositionAdds { get; set; }
        public virtual DbSet<VQtsTaskHistoryNotBaseline> VQtsTaskHistoryNotBaselines { get; set; }
        public virtual DbSet<VQtsTaskInactiveDate> VQtsTaskInactiveDates { get; set; }
        public virtual DbSet<VQtsTaskMaxChangeDate> VQtsTaskMaxChangeDates { get; set; }
        public virtual DbSet<VQtsTaskMaxChgDateNoRr> VQtsTaskMaxChgDateNoRrs { get; set; }
        public virtual DbSet<VQtsTaskNumberChange> VQtsTaskNumberChanges { get; set; }
        public virtual DbSet<VQtsTaskQualRecStep> VQtsTaskQualRecSteps { get; set; }
        public virtual DbSet<VQtsTaskQualification> VQtsTaskQualifications { get; set; }
        public virtual DbSet<VQtsTaskQualificationsBySk> VQtsTaskQualificationsBySks { get; set; }
        public virtual DbSet<VQtsTaskQualificationsFinal> VQtsTaskQualificationsFinals { get; set; }
        public virtual DbSet<VQtsTaskQualificationsFinalOjt> VQtsTaskQualificationsFinalOjts { get; set; }
        public virtual DbSet<VQtsTaskQualificationsFinalUnion> VQtsTaskQualificationsFinalUnions { get; set; }
        public virtual DbSet<VQtsTaskQualificationsOjt> VQtsTaskQualificationsOjts { get; set; }
        public virtual DbSet<VQtsTaskQualificationsOjtDesk> VQtsTaskQualificationsOjtDesks { get; set; }
        public virtual DbSet<VQtsTaskQualificationsSk> VQtsTaskQualificationsSks { get; set; }
        public virtual DbSet<VQtsTaskQualificationsSk1> VQtsTaskQualificationsSks1 { get; set; }
        public virtual DbSet<VQtsTaskQualificationsUnion> VQtsTaskQualificationsUnions { get; set; }
        public virtual DbSet<VQtsTaskSkill> VQtsTaskSkills { get; set; }
        public virtual DbSet<VQtsTaskStatement> VQtsTaskStatements { get; set; }
        public virtual DbSet<VQtsTaskStatementChange> VQtsTaskStatementChanges { get; set; }
        public virtual DbSet<VQtsTasksActive> VQtsTasksActives { get; set; }
        public virtual DbSet<VQtsTasksAll> VQtsTasksAlls { get; set; }
        public virtual DbSet<VQtsTasksIntroduction> VQtsTasksIntroductions { get; set; }
        public virtual DbSet<VQtsTasksOnly> VQtsTasksOnlies { get; set; }
        public virtual DbSet<VQtsTasksOnlyIncludeInactive> VQtsTasksOnlyIncludeInactives { get; set; }
        public virtual DbSet<VQtsTasksSkillsKnowledge> VQtsTasksSkillsKnowledges { get; set; }
        public virtual DbSet<VQtsTasksSkillsRelated> VQtsTasksSkillsRelateds { get; set; }
        public virtual DbSet<VQtsTestDistractor> VQtsTestDistractors { get; set; }
        public virtual DbSet<VQtsTopic> VQtsTopics { get; set; }
        public virtual DbSet<VQtsTotalCehCompleted> VQtsTotalCehCompleteds { get; set; }
        public virtual DbSet<VQtsTotalCehCompletedSum> VQtsTotalCehCompletedSums { get; set; }
        public virtual DbSet<VQtsTotalCehPlanned> VQtsTotalCehPlanneds { get; set; }
        public virtual DbSet<VQtsTotalCehPlannedSum> VQtsTotalCehPlannedSums { get; set; }
        public virtual DbSet<VQtsTotalCehScheduled> VQtsTotalCehScheduleds { get; set; }
        public virtual DbSet<VQtsTotalCehScheduledSum> VQtsTotalCehScheduledSums { get; set; }
        public virtual DbSet<VQtsTotalCehscCompleted> VQtsTotalCehscCompleteds { get; set; }
        public virtual DbSet<VQtsTotalCehscCompletedSum> VQtsTotalCehscCompletedSums { get; set; }
        public virtual DbSet<VQtsTotalCehscScheduled> VQtsTotalCehscScheduleds { get; set; }
        public virtual DbSet<VQtsTotalCehscScheduledSum> VQtsTotalCehscScheduledSums { get; set; }
        public virtual DbSet<VQtsTrainingSummaryByPosition> VQtsTrainingSummaryByPositions { get; set; }
        public virtual DbSet<VQtsTrainingSummaryByPositionExtra> VQtsTrainingSummaryByPositionExtras { get; set; }
        public virtual DbSet<ViewQtsCategory> ViewQtsCategories { get; set; }
        public virtual DbSet<ViewQtsDum> ViewQtsDa { get; set; }
        public virtual DbSet<ViewQtsDynamicQuery> ViewQtsDynamicQueries { get; set; }
        public virtual DbSet<ViewQtsDynamicQuery2> ViewQtsDynamicQuery2s { get; set; }
        public virtual DbSet<ViewQtsSk> ViewQtsSks { get; set; }
        public virtual DbSet<ViewQtsSkillNum> ViewQtsSkillNums { get; set; }
        public virtual DbSet<ViewQtsTask> ViewQtsTasks { get; set; }
        public virtual DbSet<ViewQtsTyear> ViewQtsTyears { get; set; }
        public virtual DbSet<VwCategoryTopicSk> VwCategoryTopicSks { get; set; }
        public virtual DbSet<VwClass> VwClasses { get; set; }
        public virtual DbSet<VwContentObject> VwContentObjects { get; set; }
        public virtual DbSet<VwCourseSkill> VwCourseSkills { get; set; }
        public virtual DbSet<VwDefaultTraining> VwDefaultTrainings { get; set; }
        public virtual DbSet<VwDutyAreaTask> VwDutyAreaTasks { get; set; }
        public virtual DbSet<VwIlatask> VwIlatasks { get; set; }
        public virtual DbSet<VwImportedDocument> VwImportedDocuments { get; set; }
        public virtual DbSet<VwJobAnalysisSurvey> VwJobAnalysisSurveys { get; set; }
        public virtual DbSet<VwLinkedDocument> VwLinkedDocuments { get; set; }
        public virtual DbSet<VwPositionsTask> VwPositionsTasks { get; set; }
        public virtual DbSet<VwProcedureTask> VwProcedureTasks { get; set; }
        public virtual DbSet<VwQtsMainDaSubDaTaskGap> VwQtsMainDaSubDaTaskGaps { get; set; }
        public virtual DbSet<VwQtsMainDaSubDum> VwQtsMainDaSubDa { get; set; }
        public virtual DbSet<VwStudent> VwStudents { get; set; }
        public virtual DbSet<VwTask> VwTasks { get; set; }
        public virtual DbSet<VwTaskSkill> VwTaskSkills { get; set; }
        public virtual DbSet<VwTopic> VwTopics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=WSAMZN-MO1QATJM;Initial Catalog=EMP_Demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CentralLock>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.CLockId })
                    .HasName("CentralLock_pkey");

                entity.ToTable("CentralLock");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.CLockId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("c_lock_id");

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.LockerId)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("locker_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Cmi5AuLaunchDatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("Cmi5AuLaunchData_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.EntitlementKey)
                    .IsUnicode(false)
                    .HasColumnName("entitlement_key");

                entity.Property(e => e.LaunchMethod)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("launch_method");

                entity.Property(e => e.LaunchParameters)
                    .IsUnicode(false)
                    .HasColumnName("launch_parameters");

                entity.Property(e => e.MasteryScore)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("mastery_score");

                entity.Property(e => e.MoveOn)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("move_on");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithOne(p => p.Cmi5AuLaunchDatum)
                    .HasForeignKey<Cmi5AuLaunchDatum>(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5AuLaunchData_ScormObj_1");
            });

            modelBuilder.Entity<Cmi5ObjectiveMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectObjectiveId, e.ScormObjectId })
                    .HasName("Cmi5ObjectiveMap_pkey");

                entity.ToTable("Cmi5ObjectiveMap");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_Cmi5ObjectiveMap_ScormObj");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectObjectiveId).HasColumnName("scorm_object_objective_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.Cmi5ObjectiveMapScormObjects)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5ObjectiveMap_ScormObj_2");

                entity.HasOne(d => d.ScormObjectNavigation)
                    .WithMany(p => p.Cmi5ObjectiveMapScormObjectNavigations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectObjectiveId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5ObjectiveMap_ScormObj_1");
            });

            modelBuilder.Entity<Cmi5RegistrationToAu>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormObjectId })
                    .HasName("Cmi5RegistrationToAu_pkey");

                entity.ToTable("Cmi5RegistrationToAu");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_Cmi5RegToAu_ScormObj");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.IsSatisfied).HasColumnName("is_satisfied");

                entity.Property(e => e.LaunchParameters)
                    .IsUnicode(false)
                    .HasColumnName("launch_parameters");

                entity.Property(e => e.MasteryScore)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("mastery_score");

                entity.Property(e => e.MoveOn)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("move_on");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.Cmi5RegistrationToAus)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5RegToAu_ScormObj_1");

                entity.HasOne(d => d.TinCanRegistration)
                    .WithMany(p => p.Cmi5RegistrationToAus)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5RegToAu_ScormReg_1");
            });

            modelBuilder.Entity<Cmi5Session>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.Cmi5SessionId })
                    .HasName("Cmi5Session_pkey");

                entity.ToTable("Cmi5Session");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_Cmi5Session_RegUnique")
                    .IsUnique();

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormObjectId }, "IX_Cmi5Session_ScormRegAndObj");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.Cmi5SessionId).HasColumnName("cmi5_session_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.IsFailed).HasColumnName("is_failed");

                entity.Property(e => e.IsInitialized).HasColumnName("is_initialized");

                entity.Property(e => e.IsLaunched).HasColumnName("is_launched");

                entity.Property(e => e.IsTerminated).HasColumnName("is_terminated");

                entity.Property(e => e.LastRequestTime)
                    .HasColumnType("datetime")
                    .HasColumnName("last_request_time");

                entity.Property(e => e.LaunchHistoryId).HasColumnName("launch_history_id");

                entity.Property(e => e.LaunchMode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("launch_mode");

                entity.Property(e => e.LaunchTokenFetched).HasColumnName("launch_token_fetched");

                entity.Property(e => e.LaunchTokenId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("launch_token_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ScormObjectId).HasColumnName("scorm_object_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.TimeReported).HasColumnName("time_reported");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Cmi5RegistrationToAu)
                    .WithMany(p => p.Cmi5Sessions)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cmi5Session_Cmi5RegToAu_1");
            });

            modelBuilder.Entity<CoursePhase>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursePhases)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblPerspectivrCourse_CoursePhases");

                entity.HasOne(d => d.CoursePhaseNavigation)
                    .WithMany(p => p.CoursePhases)
                    .HasForeignKey(d => d.CoursePhaseId)
                    .HasConstraintName("FK_tbPhases_CoursePhases");
            });

            modelBuilder.Entity<LkTblCertificationType>(entity =>
            {
                entity.HasKey(e => e.CertId)
                    .HasName("aaaaalkTblCertificationTypes_PK")
                    .IsClustered(false);

                entity.ToTable("lkTblCertificationTypes");

                entity.HasIndex(e => e.CertId, "CertID");

                entity.Property(e => e.CertId).HasColumnName("CertID");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.CertDesc).HasMaxLength(50);

                entity.Property(e => e.CredReqHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nercpolicy)
                    .HasColumnName("NERCPolicy")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalReqCehs)
                    .HasColumnName("TotalReqCEHs")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LkTblExternalProvider>(entity =>
            {
                entity.HasKey(e => e.Extpid)
                    .HasName("PK__lkTblExt__78D68B5B6D8D9533");

                entity.ToTable("lkTblExternalProviders");

                entity.Property(e => e.Extpid)
                    .ValueGeneratedNever()
                    .HasColumnName("EXTPID");

                entity.Property(e => e.ExtPname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ExtPName");

                entity.Property(e => e.ExtPpassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ExtPPassword")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtPurl)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ExtPURL")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ExtPuserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ExtPUserName")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<LkTblIlaevalAttendanceVerification>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("lkTblILAEvalAttendanceVerification");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LkTblIlamethod>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("lkTblILAMethods");

                entity.Property(e => e.Mid).HasColumnName("MID");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MethodType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LkTblIlaproviderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("lkTblILAProviderStatus");

                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusDesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LktblAnnualTrainingRequirement>(entity =>
            {
                entity.HasKey(e => e.TrainingType)
                    .HasName("aaaaalktblAnnualTrainingRequirements_PK")
                    .IsClustered(false);

                entity.ToTable("lktblAnnualTrainingRequirements");

                entity.Property(e => e.TrainingType).HasMaxLength(50);

                entity.Property(e => e.Acronym).HasMaxLength(50);

                entity.Property(e => e.TrainingHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrainingTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TrainingTypeID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblCoversheetType>(entity =>
            {
                entity.HasKey(e => e.CvtypeId)
                    .HasName("PK__lktblCov__DA9F384EA89575CB");

                entity.ToTable("lktblCoversheetTypes");

                entity.Property(e => e.CvtypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("CVTypeID");

                entity.Property(e => e.CustomFlag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Cvtype)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CVType");
            });

            modelBuilder.Entity<LktblFormQuestionsAnswer>(entity =>
            {
                entity.HasKey(e => e.Fqaid)
                    .HasName("aaaaalktblFormQuestionsAnswers_PK")
                    .IsClustered(false);

                entity.ToTable("lktblFormQuestionsAnswers");

                entity.HasIndex(e => e.Fqaid, "FQAID");

                entity.Property(e => e.Fqaid).HasColumnName("FQAID");

                entity.Property(e => e.Fqadesc)
                    .HasMaxLength(50)
                    .HasColumnName("FQADesc");

                entity.Property(e => e.Fqascore)
                    .HasColumnName("FQAScore")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblInstructor>(entity =>
            {
                entity.HasKey(e => e.Inid);

                entity.ToTable("lktblInstructors");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.InNote1)
                    .HasMaxLength(255)
                    .HasColumnName("IN_note1");

                entity.Property(e => e.InNote2)
                    .HasMaxLength(255)
                    .HasColumnName("IN_note2");

                entity.Property(e => e.Inemail)
                    .HasMaxLength(200)
                    .HasColumnName("INEmail");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblIssuingAuthority>(entity =>
            {
                entity.HasKey(e => e.Iaid);

                entity.ToTable("lktblIssuingAuthority");

                entity.Property(e => e.Iaid).HasColumnName("IAID");

                entity.Property(e => e.Iaaddress1)
                    .HasMaxLength(100)
                    .HasColumnName("IAAddress1");

                entity.Property(e => e.Iaaddress2)
                    .HasMaxLength(50)
                    .HasColumnName("IAAddress2");

                entity.Property(e => e.Iacity)
                    .HasMaxLength(50)
                    .HasColumnName("IACity");

                entity.Property(e => e.Iadefault).HasColumnName("IADefault");

                entity.Property(e => e.Iaemail)
                    .HasMaxLength(50)
                    .HasColumnName("IAEmail");

                entity.Property(e => e.Iafax)
                    .HasMaxLength(50)
                    .HasColumnName("IAFax");

                entity.Property(e => e.Ianum).HasColumnName("IANum");

                entity.Property(e => e.Iaperson)
                    .HasMaxLength(100)
                    .HasColumnName("IAPerson");

                entity.Property(e => e.Iaphone)
                    .HasMaxLength(50)
                    .HasColumnName("IAPhone");

                entity.Property(e => e.Iastate)
                    .HasMaxLength(50)
                    .HasColumnName("IAState");

                entity.Property(e => e.Iatitle)
                    .HasMaxLength(50)
                    .HasColumnName("IATitle");

                entity.Property(e => e.Iazip)
                    .HasMaxLength(50)
                    .HasColumnName("IAZIP");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblLocation>(entity =>
            {
                entity.HasKey(e => e.Lcid);

                entity.ToTable("lktblLocations");

                entity.Property(e => e.Lcid).HasColumnName("LCID");

                entity.Property(e => e.Lccity)
                    .HasMaxLength(50)
                    .HasColumnName("LCCity");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcemail)
                    .HasMaxLength(50)
                    .HasColumnName("LCEmail");

                entity.Property(e => e.Lcfax)
                    .HasMaxLength(50)
                    .HasColumnName("LCFax");

                entity.Property(e => e.Lcnote1)
                    .HasMaxLength(255)
                    .HasColumnName("LCNote1");

                entity.Property(e => e.Lcnote2)
                    .HasMaxLength(255)
                    .HasColumnName("LCNote2");

                entity.Property(e => e.Lcphone)
                    .HasMaxLength(50)
                    .HasColumnName("LCPhone");

                entity.Property(e => e.Lcstate)
                    .HasMaxLength(50)
                    .HasColumnName("LCState");

                entity.Property(e => e.LcwebSite)
                    .HasMaxLength(100)
                    .HasColumnName("LCWebSite");

                entity.Property(e => e.Lczip)
                    .HasMaxLength(50)
                    .HasColumnName("LCZip");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblOrganization>(entity =>
            {
                entity.HasKey(e => e.Oid);

                entity.ToTable("lktblOrganizations");

                entity.Property(e => e.Oid).HasColumnName("OID");

                entity.Property(e => e.Oaddress)
                    .HasMaxLength(150)
                    .HasColumnName("OAddress");

                entity.Property(e => e.Ocity)
                    .HasMaxLength(50)
                    .HasColumnName("OCity");

                entity.Property(e => e.Oemail)
                    .HasMaxLength(50)
                    .HasColumnName("OEmail");

                entity.Property(e => e.Ofax)
                    .HasMaxLength(50)
                    .HasColumnName("OFax");

                entity.Property(e => e.Oname)
                    .HasMaxLength(150)
                    .HasColumnName("OName");

                entity.Property(e => e.Ophone)
                    .HasMaxLength(50)
                    .HasColumnName("OPhone");

                entity.Property(e => e.Ostate)
                    .HasMaxLength(50)
                    .HasColumnName("OState");

                entity.Property(e => e.OwebSite)
                    .HasMaxLength(100)
                    .HasColumnName("OWebSite");

                entity.Property(e => e.Ozip)
                    .HasMaxLength(50)
                    .HasColumnName("OZip");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblQueryBuilder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lktblQueryBuilder");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Visible).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<LktblRatingScale>(entity =>
            {
                entity.HasKey(e => e.Rsid);

                entity.ToTable("lktblRatingScales");

                entity.Property(e => e.Rsid).HasColumnName("RSID");

                entity.Property(e => e.Rsdescription)
                    .HasMaxLength(50)
                    .HasColumnName("RSDescription");

                entity.Property(e => e.Rsinstruction)
                    .HasMaxLength(255)
                    .HasColumnName("RSInstruction");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblSelfRegStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lktblSelfRegStatus");

                entity.HasIndex(e => e.SelfRegStatus, "UQ__lktblSel__014CBA4907DC3217")
                    .IsUnique();

                entity.Property(e => e.StatusStr)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusStrEmp)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("StatusStrEMP");
            });

            modelBuilder.Entity<LktblSupplier>(entity =>
            {
                entity.HasKey(e => e.Suid);

                entity.ToTable("lktblSupplier");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.CardHolder).HasMaxLength(50);

                entity.Property(e => e.Cell)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.CpCity)
                    .HasMaxLength(50)
                    .HasColumnName("CP_City");

                entity.Property(e => e.CpEmail)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Email");

                entity.Property(e => e.CpFax)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Fax");

                entity.Property(e => e.CpPhone)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Phone");

                entity.Property(e => e.CpState)
                    .HasMaxLength(50)
                    .HasColumnName("CP_State");

                entity.Property(e => e.CpStreetAddress)
                    .HasMaxLength(100)
                    .HasColumnName("CP_StreetAddress");

                entity.Property(e => e.CpTitle)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Title");

                entity.Property(e => e.CpWebsite)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Website");

                entity.Property(e => e.CpZip)
                    .HasMaxLength(50)
                    .HasColumnName("CP_ZIP");

                entity.Property(e => e.ExpDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Ext)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Inactive).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Note1).HasMaxLength(50);

                entity.Property(e => e.Note2).HasMaxLength(50);

                entity.Property(e => e.PaymentInfoName).HasMaxLength(50);

                entity.Property(e => e.PiCity)
                    .HasMaxLength(50)
                    .HasColumnName("PI_City");

                entity.Property(e => e.PiEmail)
                    .HasMaxLength(50)
                    .HasColumnName("PI_Email");

                entity.Property(e => e.PiFax)
                    .HasMaxLength(50)
                    .HasColumnName("PI_Fax");

                entity.Property(e => e.PiOrganization)
                    .HasMaxLength(50)
                    .HasColumnName("PI_Organization");

                entity.Property(e => e.PiPhone)
                    .HasMaxLength(50)
                    .HasColumnName("PI_Phone");

                entity.Property(e => e.PiState)
                    .HasMaxLength(50)
                    .HasColumnName("PI_State");

                entity.Property(e => e.PiStreetAddress)
                    .HasMaxLength(50)
                    .HasColumnName("PI_StreetAddress");

                entity.Property(e => e.PiTitle)
                    .HasMaxLength(50)
                    .HasColumnName("PI_Title");

                entity.Property(e => e.PiZip)
                    .HasMaxLength(50)
                    .HasColumnName("PI_ZIP");

                entity.Property(e => e.SponsorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.TprEmail)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Email");

                entity.Property(e => e.TprFax)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Fax");

                entity.Property(e => e.TprName)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Name");

                entity.Property(e => e.TprPhone)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Phone");

                entity.Property(e => e.TprSignaturePath)
                    .HasMaxLength(255)
                    .HasColumnName("TPR_SignaturePath");

                entity.Property(e => e.TprTitle)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Title");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<LktblTrainingEvalDriver>(entity =>
            {
                entity.HasKey(e => e.DriverId);

                entity.ToTable("lktblTrainingEvalDrivers");

                entity.Property(e => e.DriverId)
                    .ValueGeneratedNever()
                    .HasColumnName("DriverID");

                entity.Property(e => e.DriverDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Log");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Exception)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Thread)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PensCommand>(entity =>
            {
                entity.HasKey(e => e.PensCommandId)
                    .IsClustered(false);

                entity.ToTable("PensCommand");

                entity.HasIndex(e => new { e.InternalStep, e.LockId, e.ShouldProcess }, "IX_PensCommand");

                entity.Property(e => e.PensCommandId)
                    .HasColumnName("pens_command_id")
                    .HasComment("The internal Pens command Id");

                entity.Property(e => e.FailedCount)
                    .HasColumnName("failedCount")
                    .HasComment("Number of failed attempts to process this command");

                entity.Property(e => e.InternalStep)
                    .HasColumnName("internal_step")
                    .HasComment("Pens Internal Step");

                entity.Property(e => e.LockId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lock_id")
                    .HasDefaultValueSql("('')")
                    .HasComment("ID of the lock requestor");

                entity.Property(e => e.PensClient)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("pens_client")
                    .HasComment("Pens client");

                entity.Property(e => e.PensCommand1)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("pens_command")
                    .IsFixedLength(true)
                    .HasComment("Pens command");

                entity.Property(e => e.PensPackageId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("pens_package_id")
                    .HasComment("Package Identifier of the Pens command package");

                entity.Property(e => e.PensPackageType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("pens_package_type")
                    .IsFixedLength(true)
                    .HasComment("Package type of the Pens command package");

                entity.Property(e => e.PensSerialized)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("pens_serialized")
                    .HasComment("Serialied Pens command");

                entity.Property(e => e.PensStep)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("pens_step")
                    .HasComment("The current workflow step this command is in");

                entity.Property(e => e.PensSystemUserId)
                    .HasMaxLength(100)
                    .HasColumnName("pens_system_user_id")
                    .HasComment("Pens system user id");

                entity.Property(e => e.ProcessAfter)
                    .HasColumnType("datetime")
                    .HasColumnName("processAfter")
                    .HasComment("Time to wait until before processing this command (used on retry)");

                entity.Property(e => e.ShouldProcess)
                    .HasColumnName("should_process")
                    .HasComment("Value indicating whether this Pens command should be processed");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("update_by")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasComment("The time this record was last updated");
            });

            modelBuilder.Entity<PensCommandScormPackage>(entity =>
            {
                entity.HasKey(e => new { e.PensCommandId, e.ScormPackageId })
                    .IsClustered(false);

                entity.ToTable("PensCommandScormPackage");

                entity.Property(e => e.PensCommandId)
                    .HasColumnName("pens_command_id")
                    .HasComment("The internal Pens command Id");

                entity.Property(e => e.ScormPackageId)
                    .HasColumnName("scorm_package_id")
                    .HasComment("The internal SCORM package identifier for the package");

                entity.Property(e => e.ExternalPackageId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_package_id")
                    .HasDefaultValueSql("('')")
                    .HasComment("The external SCORM package identifier for the package");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("update_by")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.PensCommand)
                    .WithMany(p => p.PensCommandScormPackages)
                    .HasForeignKey(d => d.PensCommandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PensCommandScormPackage_PensCommand");
            });

            modelBuilder.Entity<QryCarryOverTotalCeh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryCarryOverTotalCEHs");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompDate6).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");
            });

            modelBuilder.Entity<QryCehapplicationClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryCEHApplicationClasses");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lccity)
                    .HasMaxLength(50)
                    .HasColumnName("LCCity");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcstate)
                    .HasMaxLength(50)
                    .HasColumnName("LCState");

                entity.Property(e => e.Lczip)
                    .HasMaxLength(50)
                    .HasColumnName("LCZip");
            });

            modelBuilder.Entity<QryCehapplicationDataReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryCEHApplicationDataReport");

                entity.Property(e => e.ActEoptotal).HasColumnName("Act_EOPTotal");

                entity.Property(e => e.ActMayBeUsedAsPdh).HasColumnName("Act_MayBeUsedAsPDH");

                entity.Property(e => e.ActMayBeUsedByCso).HasColumnName("Act_MayBeUsedByCSO");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.ActSimulationTotal).HasColumnName("Act_SimulationTotal");

                entity.Property(e => e.AssessmentMethodId).HasColumnName("AssessmentMethodID");

                entity.Property(e => e.CatNercstandards).HasColumnName("Cat_NERCStandards");

                entity.Property(e => e.CatOperatingTopics).HasColumnName("Cat_OperatingTopics");

                entity.Property(e => e.CatProfRelated).HasColumnName("Cat_ProfRelated");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.CehApprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_ApprovalDate");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CehappDataFee).HasColumnName("CEHAppData_Fee");

                entity.Property(e => e.CehappDataType)
                    .HasMaxLength(50)
                    .HasColumnName("CEHAppData_Type");

                entity.Property(e => e.Content)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.CorexpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CORExpDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CourseProcedures)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

                entity.Property(e => e.DeliveryTeam)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EopsPer002).HasColumnName("EOPsPER002");

                entity.Property(e => e.EvaluationMethod)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Expr1001).HasColumnType("datetime");

                entity.Property(e => e.Expr1002).HasMaxLength(255);

                entity.Property(e => e.Expr1003).HasMaxLength(50);

                entity.Property(e => e.Expr1004).HasMaxLength(100);

                entity.Property(e => e.Expr1005).HasMaxLength(50);

                entity.Property(e => e.Expr1006).HasMaxLength(50);

                entity.Property(e => e.Expr1007).HasMaxLength(50);

                entity.Property(e => e.Expr1008).HasMaxLength(50);

                entity.Property(e => e.Expr1009).HasMaxLength(50);

                entity.Property(e => e.Expr1010).HasMaxLength(100);

                entity.Property(e => e.Expr1011).HasMaxLength(50);

                entity.Property(e => e.Expr1012).HasMaxLength(50);

                entity.Property(e => e.Expr1013).HasMaxLength(50);

                entity.Property(e => e.Expr1014).HasMaxLength(50);

                entity.Property(e => e.Expr1015).HasMaxLength(50);

                entity.Property(e => e.Expr1016).HasMaxLength(50);

                entity.Property(e => e.Expr1017).HasMaxLength(50);

                entity.Property(e => e.Expr1018).HasMaxLength(50);

                entity.Property(e => e.Expr1019).HasMaxLength(50);

                entity.Property(e => e.Expr1020).HasMaxLength(50);

                entity.Property(e => e.Expr1021).HasMaxLength(50);

                entity.Property(e => e.Expr1022).HasMaxLength(50);

                entity.Property(e => e.Expr1023).HasMaxLength(50);

                entity.Property(e => e.Expr1024).HasMaxLength(50);

                entity.Property(e => e.Expr1025).HasMaxLength(50);

                entity.Property(e => e.Expr1028).HasMaxLength(50);

                entity.Property(e => e.Expr1029).HasColumnType("smalldatetime");

                entity.Property(e => e.Expr1030).HasMaxLength(50);

                entity.Property(e => e.Expr1031).HasMaxLength(50);

                entity.Property(e => e.Expr1032).HasMaxLength(50);

                entity.Property(e => e.Expr1033).HasMaxLength(50);

                entity.Property(e => e.Expr1034).HasMaxLength(50);

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.LerningActivityMultiDeliv).HasColumnName("LerningActivity_MultiDeliv");

                entity.Property(e => e.LerningActivityPublic).HasColumnName("LerningActivity_Public");

                entity.Property(e => e.LerningActivitySelfStudy).HasColumnName("LerningActivity_SelfStudy");

                entity.Property(e => e.LerningActivitySelfStudyNa).HasColumnName("LerningActivity_SelfStudy_NA");

                entity.Property(e => e.NotNercrelated).HasColumnName("NotNERCRelated");

                entity.Property(e => e.NotOnNercreport).HasColumnName("NotOnNERCReport");

                entity.Property(e => e.Note1)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Note2)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Prerequisites)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.TargetAudience10).HasColumnName("TargetAudience_10");

                entity.Property(e => e.TargetAudience9).HasColumnName("TargetAudience_9");

                entity.Property(e => e.TargetAudienceBio).HasColumnName("TargetAudience_BIO");

                entity.Property(e => e.TargetAudienceCrs).HasColumnName("TargetAudience_CRS");

                entity.Property(e => e.TargetAudienceGo).HasColumnName("TargetAudience_GO");

                entity.Property(e => e.TargetAudienceMo).HasColumnName("TargetAudience_MO");

                entity.Property(e => e.TargetAudienceOpe).HasColumnName("TargetAudience_OPE");

                entity.Property(e => e.TargetAudienceOther).HasColumnName("TargetAudience_Other");

                entity.Property(e => e.TargetAudienceOtherSpecify)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TargetAudience_OtherSpecify");

                entity.Property(e => e.TargetAudienceRc).HasColumnName("TargetAudience_RC");

                entity.Property(e => e.TargetAudienceTo).HasColumnName("TargetAudience_TO");

                entity.Property(e => e.Topics11).HasColumnName("Topics_11");

                entity.Property(e => e.Topics12).HasColumnName("Topics_12");

                entity.Property(e => e.TopicsBc).HasColumnName("Topics_BC");

                entity.Property(e => e.TopicsEo).HasColumnName("Topics_EO");

                entity.Property(e => e.TopicsIpso).HasColumnName("Topics_IPSO");

                entity.Property(e => e.TopicsMo).HasColumnName("Topics_MO");

                entity.Property(e => e.TopicsOa).HasColumnName("Topics_OA");

                entity.Property(e => e.TopicsPp).HasColumnName("Topics_PP");

                entity.Property(e => e.TopicsPsr).HasColumnName("Topics_PSR");

                entity.Property(e => e.TopicsPtee).HasColumnName("Topics_PTEE");

                entity.Property(e => e.TopicsSp).HasColumnName("Topics_SP");

                entity.Property(e => e.TopicsTools).HasColumnName("Topics_Tools");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TrainingPlan)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Type10).HasColumnName("Type_10");

                entity.Property(e => e.Type11).HasColumnName("Type_11");

                entity.Property(e => e.TypeClassroom).HasColumnName("Type_Classroom");

                entity.Property(e => e.TypeComputerBased).HasColumnName("Type_ComputerBased");

                entity.Property(e => e.TypeConference).HasColumnName("Type_Conference");

                entity.Property(e => e.TypeInternetBased).HasColumnName("Type_InternetBased");

                entity.Property(e => e.TypeOjttraining).HasColumnName("Type_OJTTraining");

                entity.Property(e => e.TypeOther).HasColumnName("Type_Other");

                entity.Property(e => e.TypeOtherSpecify)
                    .HasMaxLength(50)
                    .HasColumnName("Type_OtherSpecify");

                entity.Property(e => e.TypeOtsimulation).HasColumnName("Type_OTSimulation");

                entity.Property(e => e.TypeSelfStudy).HasColumnName("Type_SelfStudy");

                entity.Property(e => e.TypeWorkshop).HasColumnName("Type_Workshop");

                entity.Property(e => e.VerifAndDocOfCehhours)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("VerifAndDocOfCEHHours");
            });

            modelBuilder.Entity<QryClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClasses");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");
            });

            modelBuilder.Entity<QryClassBasedRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassBasedRecords");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Suid).HasColumnName("SUID");
            });

            modelBuilder.Entity<QryClassEvaluationComment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassEvaluationComments");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqid).HasColumnName("FQID");

                entity.Property(e => e.Sfacomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("SFAComments");
            });

            modelBuilder.Entity<QryClassEvaluationSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassEvaluationSummary");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqid).HasColumnName("FQID");
            });

            modelBuilder.Entity<QryClassRoster>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassRoster");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Oname)
                    .HasMaxLength(150)
                    .HasColumnName("OName");

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");
            });

            modelBuilder.Entity<QryClassesOverride>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassesOverrides");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Expr1005)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");
            });

            modelBuilder.Entity<QryClassesToUpload>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryClassesToUpload");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");
            });

            modelBuilder.Entity<QryCourse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryCourses");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");
            });

            modelBuilder.Entity<QryCriticalTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryCriticalTasks");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<QryDistinctTasksHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryDistinctTasksHistory");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<QryEmpTrainingTowardReCertPer002>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qry_EmpTrainingTowardReCert_PER002");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.HoursCompleted).HasColumnName("Hours Completed");
            });

            modelBuilder.Entity<QryEmpTrainingTowardReCertPer002Final>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qry_EmpTrainingTowardReCert_PER002_Final");

                entity.Property(e => e.CertType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Cert Type");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EmpNo)
                    .HasMaxLength(50)
                    .HasColumnName("Emp No");

                entity.Property(e => e.EnotCertified).HasColumnName("ENotCertified");

                entity.Property(e => e.EwillNotBeRecertified).HasColumnName("EWillNotBeRecertified");

                entity.Property(e => e.HrsCompleted).HasColumnName("Hrs Completed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(102);
            });

            modelBuilder.Entity<QryEmpTrainingTowardReCertSharedBasic>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qry_EmpTrainingTowardReCert_Shared_Basic");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.Tyear).HasColumnName("TYear");
            });

            modelBuilder.Entity<QryEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryEmployee");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<QryExchangeOfElectronicRecordsAdvanced>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryExchangeOfElectronicRecords_Advanced");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Oid).HasColumnName("OID");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");
            });

            modelBuilder.Entity<QryFullRecordByyear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryFullRecordBYYear");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");
            });

            modelBuilder.Entity<QryFutureClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryFutureClasses");

                entity.Property(e => e.ActEoptotal).HasColumnName("Act_EOPTotal");

                entity.Property(e => e.ActMayBeUsedAsPdh).HasColumnName("Act_MayBeUsedAsPDH");

                entity.Property(e => e.ActMayBeUsedByCso).HasColumnName("Act_MayBeUsedByCSO");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.ActSimulationTotal).HasColumnName("Act_SimulationTotal");

                entity.Property(e => e.AssessmentMethodId).HasColumnName("AssessmentMethodID");

                entity.Property(e => e.CatNercstandards).HasColumnName("Cat_NERCStandards");

                entity.Property(e => e.CatOperatingTopics).HasColumnName("Cat_OperatingTopics");

                entity.Property(e => e.CatProfRelated).HasColumnName("Cat_ProfRelated");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.CehApprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_ApprovalDate");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CehappDataFee).HasColumnName("CEHAppData_Fee");

                entity.Property(e => e.CehappDataType)
                    .HasMaxLength(50)
                    .HasColumnName("CEHAppData_Type");

                entity.Property(e => e.Content)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.CorexpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CORExpDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CourseProcedures)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

                entity.Property(e => e.DeliveryTeam)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EopsPer002).HasColumnName("EOPsPER002");

                entity.Property(e => e.EvaluationMethod)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Expr1001).HasMaxLength(50);

                entity.Property(e => e.Expr1002).HasColumnType("datetime");

                entity.Property(e => e.Expr1003).HasMaxLength(255);

                entity.Property(e => e.Expr1004).HasMaxLength(50);

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.LerningActivityMultiDeliv).HasColumnName("LerningActivity_MultiDeliv");

                entity.Property(e => e.LerningActivityPublic).HasColumnName("LerningActivity_Public");

                entity.Property(e => e.LerningActivitySelfStudy).HasColumnName("LerningActivity_SelfStudy");

                entity.Property(e => e.LerningActivitySelfStudyNa).HasColumnName("LerningActivity_SelfStudy_NA");

                entity.Property(e => e.NotNercrelated).HasColumnName("NotNERCRelated");

                entity.Property(e => e.NotOnNercreport).HasColumnName("NotOnNERCReport");

                entity.Property(e => e.Note1)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Note2)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Prerequisites)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.TargetAudience10).HasColumnName("TargetAudience_10");

                entity.Property(e => e.TargetAudience9).HasColumnName("TargetAudience_9");

                entity.Property(e => e.TargetAudienceBio).HasColumnName("TargetAudience_BIO");

                entity.Property(e => e.TargetAudienceCrs).HasColumnName("TargetAudience_CRS");

                entity.Property(e => e.TargetAudienceGo).HasColumnName("TargetAudience_GO");

                entity.Property(e => e.TargetAudienceMo).HasColumnName("TargetAudience_MO");

                entity.Property(e => e.TargetAudienceOpe).HasColumnName("TargetAudience_OPE");

                entity.Property(e => e.TargetAudienceOther).HasColumnName("TargetAudience_Other");

                entity.Property(e => e.TargetAudienceOtherSpecify)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TargetAudience_OtherSpecify");

                entity.Property(e => e.TargetAudienceRc).HasColumnName("TargetAudience_RC");

                entity.Property(e => e.TargetAudienceTo).HasColumnName("TargetAudience_TO");

                entity.Property(e => e.Topics11).HasColumnName("Topics_11");

                entity.Property(e => e.Topics12).HasColumnName("Topics_12");

                entity.Property(e => e.TopicsBc).HasColumnName("Topics_BC");

                entity.Property(e => e.TopicsEo).HasColumnName("Topics_EO");

                entity.Property(e => e.TopicsIpso).HasColumnName("Topics_IPSO");

                entity.Property(e => e.TopicsMo).HasColumnName("Topics_MO");

                entity.Property(e => e.TopicsOa).HasColumnName("Topics_OA");

                entity.Property(e => e.TopicsPp).HasColumnName("Topics_PP");

                entity.Property(e => e.TopicsPsr).HasColumnName("Topics_PSR");

                entity.Property(e => e.TopicsPtee).HasColumnName("Topics_PTEE");

                entity.Property(e => e.TopicsSp).HasColumnName("Topics_SP");

                entity.Property(e => e.TopicsTools).HasColumnName("Topics_Tools");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TrainingPlan)
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Type10).HasColumnName("Type_10");

                entity.Property(e => e.Type11).HasColumnName("Type_11");

                entity.Property(e => e.TypeClassroom).HasColumnName("Type_Classroom");

                entity.Property(e => e.TypeComputerBased).HasColumnName("Type_ComputerBased");

                entity.Property(e => e.TypeConference).HasColumnName("Type_Conference");

                entity.Property(e => e.TypeInternetBased).HasColumnName("Type_InternetBased");

                entity.Property(e => e.TypeOjttraining).HasColumnName("Type_OJTTraining");

                entity.Property(e => e.TypeOther).HasColumnName("Type_Other");

                entity.Property(e => e.TypeOtherSpecify)
                    .HasMaxLength(50)
                    .HasColumnName("Type_OtherSpecify");

                entity.Property(e => e.TypeOtsimulation).HasColumnName("Type_OTSimulation");

                entity.Property(e => e.TypeSelfStudy).HasColumnName("Type_SelfStudy");

                entity.Property(e => e.TypeWorkshop).HasColumnName("Type_Workshop");

                entity.Property(e => e.VerifAndDocOfCehhours)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("VerifAndDocOfCEHHours");
            });

            modelBuilder.Entity<QryHoursRequiredPerIdp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryHoursRequiredPerIDP");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.SumOfCehReg).HasColumnName("SumOfCEH_Reg");

                entity.Property(e => e.SumOfTotalCeh).HasColumnName("SumOfTotalCEH");

                entity.Property(e => e.Tyear)
                    .HasMaxLength(50)
                    .HasColumnName("TYear");
            });

            modelBuilder.Entity<QryIdpCompletionSmud>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryIDP_CompletionSMUD");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Completed).HasColumnType("datetime");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Idpid).HasColumnName("IDPID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.InstLoc).HasMaxLength(306);

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Partial)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.Planned).HasColumnType("datetime");

                entity.Property(e => e.ProctorId).HasColumnName("ProctorID");

                entity.Property(e => e.ReqCompDate).HasColumnType("datetime");

                entity.Property(e => e.Scheduled).HasColumnType("datetime");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.Tyear)
                    .HasMaxLength(50)
                    .HasColumnName("TYear");
            });

            modelBuilder.Entity<QryIlaByPositionSkBasicDelete>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryILA_by_PositionSK_basic_DELETE");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.Skid).HasColumnName("SKID");
            });

            modelBuilder.Entity<QryInitialPositionTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryInitialPositionTraining");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);
            });

            modelBuilder.Entity<QryJonsEmployeeCourse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryJonsEmployeeCourses");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(53);

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<QryLastOjtevaluation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLastOJTEvaluation");

                entity.Property(e => e.LastEvaluation).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<QryLatestCompleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestCompleted");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastCompleted).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryLatestCompletedGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestCompletedGrade");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastCompleted).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryLatestPlanned>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestPlanned");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LatestPlanned).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryLatestRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestRecord");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryLatestRecordDetaile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestRecord_Detailes");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryLatestRecordDetailesNow>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestRecord_Detailes_Now");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");
            });

            modelBuilder.Entity<QryLatestRecordDetailesSmud>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryLatestRecord_DetailesSMUD");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ProctorId).HasColumnName("ProctorID");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryMainCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryMainCategories");

                entity.Property(e => e.Cdesc)
                    .HasMaxLength(255)
                    .HasColumnName("CDesc");

                entity.Property(e => e.Cid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");
            });

            modelBuilder.Entity<QryMainDuty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryMainDuty");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");
            });

            modelBuilder.Entity<QryMetRecertification>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryMetRecertification");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.MetRecert)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");
            });

            modelBuilder.Entity<QryOveralEvaluationCommentsSelfPaced>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryOveralEvaluationComments_SelfPaced");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FQDesc");

                entity.Property(e => e.Fqid).HasColumnName("FQID");

                entity.Property(e => e.Fqnum).HasColumnName("FQNum");

                entity.Property(e => e.SelfPacedCorid).HasColumnName("SelfPacedCORID");

                entity.Property(e => e.Sfacomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("SFAComments");
            });

            modelBuilder.Entity<QryPositionTrainingI>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryPositionTrainingI");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<QryRecertificationHour>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryRecertificationHours");

                entity.Property(e => e.CompCeh).HasColumnName("Comp_CEH");

                entity.Property(e => e.CompNerc).HasColumnName("Comp_NERC");

                entity.Property(e => e.CompSim).HasColumnName("Comp_Sim");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Nercpolicy).HasColumnName("NERCPolicy");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");
            });

            modelBuilder.Entity<QrySelfPacedRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qrySelfPacedRecords");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Suid).HasColumnName("SUID");
            });

            modelBuilder.Entity<QrySelfPasedCompletion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qrySelfPasedCompletion");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ProctorId).HasColumnName("ProctorID");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<QryStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryStudents");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(162);
            });

            modelBuilder.Entity<QryStudentFormsClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryStudentForms_Classes");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Sfcomplete).HasColumnName("SFComplete");

                entity.Property(e => e.Sfid).HasColumnName("SFID");
            });

            modelBuilder.Entity<QryTaskSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTaskSkills");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Skill)
                    .HasMaxLength(43)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<QryTasksCriticality>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTasksCriticality");

                entity.Property(e => e.CriticalFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<QryTasksNoStep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTasksNoSteps");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TCriteria");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.TfullNum)
                    .HasMaxLength(63)
                    .HasColumnName("TFullNum");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<QryTasksSpecification>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTasksSpecification");

                entity.Property(e => e.CurrentUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.MainNum).HasMaxLength(33);

                entity.Property(e => e.Num).HasMaxLength(44);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tid2).HasColumnName("TID2");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<QryTasksSpecificationByPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTasksSpecificationByPosition");

                entity.Property(e => e.CurrentUser)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.MainNum)
                    .HasMaxLength(42)
                    .IsUnicode(false);

                entity.Property(e => e.Num)
                    .HasMaxLength(53)
                    .IsUnicode(false);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tid2).HasColumnName("TID2");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<QryTrainingSummaryByPositionExtra>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qry_TrainingSummaryByPosition_Extra");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum).HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(156);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercpolicy).HasColumnName("NERCPolicy");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");

                entity.Property(e => e.Tyear).HasColumnName("TYear");
            });

            modelBuilder.Entity<QryTrainingSummaryByPositionExtra1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryTrainingSummaryByPosition_Extra");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.CoTotalCeh).HasColumnName("CO_TotalCEH");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CourseStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(156);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercpolicy).HasColumnName("NERCPolicy");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");

                entity.Property(e => e.Tyear).HasColumnName("TYear");
            });

            modelBuilder.Entity<QryXmltranscript>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("qryXMLTranscript");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<RsTblClassStudent>(entity =>
            {
                entity.HasKey(e => new { e.Clid, e.Eid })
                    .HasName("aaaaarsTblClass_Students_PK")
                    .IsClustered(false);

                entity.ToTable("rsTblClass_Students");

                entity.HasIndex(e => e.Clid, "CLID");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.Clid, "tblClassesrsTblClass_Students");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cl)
                    .WithMany(p => p.RsTblClassStudents)
                    .HasForeignKey(d => d.Clid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblClass_Students_FK00");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.RsTblClassStudents)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rsTblClass_Students_tblEmployee");
            });

            modelBuilder.Entity<RsTblCoursesProcedure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rsTblCourses_Procedures");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Prid).HasColumnName("PRID");
            });

            modelBuilder.Entity<RsTblCoursesSkillsKnowledge>(entity =>
            {
                entity.HasKey(e => new { e.Skid, e.Corid })
                    .HasName("aaaaarsTblCourses_SkillsKnowledge_PK")
                    .IsClustered(false);

                entity.ToTable("rsTblCourses_SkillsKnowledge");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.Skid, "SKID");

                entity.HasIndex(e => e.Corid, "tblCoursesrsTblCourses_SkillsKnowledge");

                entity.HasIndex(e => e.Skid, "tblSkillsKnowledgersTblCourses_SkillsKnowledge");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.RsTblCoursesSkillsKnowledges)
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblCourses_SkillsKnowle_FK00");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.RsTblCoursesSkillsKnowledges)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblCourses_SkillsKnowle_FK01");
            });

            modelBuilder.Entity<RsTblCoursesTask>(entity =>
            {
                entity.HasKey(e => new { e.Tid, e.Corid })
                    .HasName("aaaaarsTblCourses_Tasks_PK")
                    .IsClustered(false);

                entity.ToTable("rsTblCourses_Tasks");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.Tid, "TID");

                entity.HasIndex(e => e.Corid, "tblCoursesrsTblCourses_Tasks");

                entity.HasIndex(e => e.Tid, "tblTasksrsTblCourses_Tasks");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.RsTblCoursesTasks)
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblCourses_Tasks_FK00");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.RsTblCoursesTasks)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblCourses_Tasks_FK01");
            });

            modelBuilder.Entity<RsTblEmployeesSummaryTask>(entity =>
            {
                entity.HasKey(e => new { e.Eid, e.TidSum });

                entity.ToTable("rsTblEmployees_SummaryTasks");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TidSum).HasColumnName("TID_Sum");

                entity.Property(e => e.RsId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("rsID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.RsTblEmployeesSummaryTasks)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rsTblEmployees_SummaryTasks_tblEmployee");
            });

            modelBuilder.Entity<RsTblEmployeesTask>(entity =>
            {
                entity.HasKey(e => new { e.Eid, e.Tid })
                    .HasName("aaaaarsTblEmployees_Tasks_PK")
                    .IsClustered(false);

                entity.ToTable("rsTblEmployees_Tasks");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.Tid, "TID");

                entity.HasIndex(e => e.RsId, "rsID");

                entity.HasIndex(e => e.Tid, "tblTasksrsTblEmployees_Tasks");

                entity.HasIndex(e => e.Eid, "{E0371271-7722-4057-A5D0-87B30DE06580}");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.RsId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("rsID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.RsTblEmployeesTasks)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblEmployees_Tasks_FK00");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.RsTblEmployeesTasks)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblEmployees_Tasks_FK01");
            });

            modelBuilder.Entity<RsTblPositionTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rsTblPosition_Training");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.Pid, "PID");

                entity.HasIndex(e => e.Corid, "tblCoursesrsTblPosition_Training");

                entity.HasIndex(e => e.Pid, "tblPositionsrsTblPosition_Training");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.InitialVersion).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Ptpid).HasColumnName("PTPID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Ttype)
                    .IsRequired()
                    .HasMaxLength(1)
                    .HasColumnName("TType");

                entity.Property(e => e.Tyear)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("TYear");

                entity.HasOne(d => d.Cor)
                    .WithMany()
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblPosition_Training_FK00");
            });

            modelBuilder.Entity<RsTblProceduresTask>(entity =>
            {
                entity.HasKey(e => new { e.Prid, e.Tid });

                entity.ToTable("rsTblProcedures_Tasks");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<RsTblReportingMgrEmployee>(entity =>
            {
                entity.HasKey(e => new { e.Rmid, e.Eid });

                entity.ToTable("rsTblReportingMgr_Employees");

                entity.Property(e => e.Rmid).HasColumnName("RMID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.HasOne(d => d.Rm)
                    .WithMany(p => p.RsTblReportingMgrEmployees)
                    .HasForeignKey(d => d.Rmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rsTblReportingMgr_Employees_tblReportingManagers");
            });

            modelBuilder.Entity<RsTblTasksSkillsKnowledge>(entity =>
            {
                entity.HasKey(e => new { e.Tid, e.Skid })
                    .HasName("aaaaarsTblTasks_SkillsKnowledge_PK")
                    .IsClustered(false);

                entity.ToTable("rsTblTasks_SkillsKnowledge");

                entity.HasIndex(e => e.Skid, "KSID");

                entity.HasIndex(e => e.Tid, "TID");

                entity.HasIndex(e => e.Skid, "tblSkillsKnowledgersTblTasks_SkillsKnowledge");

                entity.HasIndex(e => e.Tid, "{DCA6589C-D76F-448A-824B-432EC096E2B1}");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.RsTblTasksSkillsKnowledges)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblTasks_SkillsKnowledg_FK01");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.RsTblTasksSkillsKnowledges)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rsTblTasks_SkillsKnowledg_FK00");
            });

            modelBuilder.Entity<RsTblTestTestItem>(entity =>
            {
                entity.ToTable("rsTblTest_TestItem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.RsTblTestTestItems)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rsTblTest_TestItem_tblTests");

                entity.HasOne(d => d.TestItem)
                    .WithMany(p => p.RsTblTestTestItems)
                    .HasForeignKey(d => d.TestItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rsTblTest_TestItem_tblTestItems");
            });

            modelBuilder.Entity<RstblPositionsTask>(entity =>
            {
                entity.HasKey(e => new { e.Pid, e.Tid })
                    .HasName("aaaaarstblPositions_Tasks_PK")
                    .IsClustered(false);

                entity.ToTable("rstblPositions_Tasks");

                entity.HasIndex(e => e.Pid, "PID");

                entity.HasIndex(e => e.Tid, "TID");

                entity.HasIndex(e => e.Tpid, "TPID");

                entity.HasIndex(e => e.Tpid, "tblTrainingPhasesrstblPositions_Tasks");

                entity.HasIndex(e => e.Pid, "{37B4A1D0-16F9-4201-A270-A8D8F996C4D5}");

                entity.HasIndex(e => e.Tid, "{4378734F-19F0-496F-A8FA-7A3981A7431B}");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.AvgDifficulty).HasDefaultValueSql("((0))");

                entity.Property(e => e.AvgFrequency).HasDefaultValueSql("((0))");

                entity.Property(e => e.AvgImportance).HasDefaultValueSql("((0))");

                entity.Property(e => e.DifComments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImpactR6).HasDefaultValueSql("((0))");

                entity.Property(e => e.R6Reason)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("R6_reason")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RrReason)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("RR_reason")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StatusDefault)
                    .HasColumnName("Status_Default")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StatusFinal)
                    .HasColumnName("Status_Final")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tpid).HasColumnName("TPID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.RstblPositionsTasks)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rstblPositions_Tasks_FK00");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.RstblPositionsTasks)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rstblPositions_Tasks_FK01");
            });

            modelBuilder.Entity<ScormActivity>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId })
                    .HasName("ScormActivity_pkey");

                entity.ToTable("ScormActivity");

                entity.HasComment("Tracks the activity state data for a given Scorm Object during an attempt at a Registration. This data represents the Activity Tree state from SCORM sequencing and is separate from but related to the CMI data held in the ActivityRT tables.");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_SA_object_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_ScormActivity");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Identifier for this activity");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasComment("Is the learner currently actively experiencing the training?");

                entity.Property(e => e.ActivityAbsoluteDur)
                    .HasColumnName("activity_absolute_dur")
                    .HasDefaultValueSql("((-1))")
                    .HasComment("The total time that the learner had access to the activity.");

                entity.Property(e => e.ActivityAttemptCount)
                    .HasColumnName("activity_attempt_count")
                    .HasComment("The number of times this activity has been attempted by the learner.");

                entity.Property(e => e.ActivityExpDurReported)
                    .HasColumnName("activity_exp_dur_reported")
                    .HasComment("The total time that the learner actually experienced this activity as reported by the Course. ");

                entity.Property(e => e.ActivityExpDurTracked)
                    .HasColumnName("activity_exp_dur_tracked")
                    .HasComment("The total time that the learner actually experienced this activity as tracked by the system.");

                entity.Property(e => e.ActivityProgressStatus)
                    .HasColumnName("activity_progress_status")
                    .HasComment("Is the activity data (attempt count) known for this activity");

                entity.Property(e => e.ActivityStartTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("activity_start_timestamp_utc")
                    .HasComment("Indicates the first time this activity was entered");

                entity.Property(e => e.AiccSessionId)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("aicc_session_id")
                    .HasComment("Identifier of AICC Session associated with the activity");

                entity.Property(e => e.AttemptAbsoluteDur)
                    .HasColumnName("attempt_absolute_dur")
                    .HasComment("The total time that the learner had access to the activity during the current attempt.");

                entity.Property(e => e.AttemptCompletionAmount)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("attempt_completion_amount")
                    .HasComment("Amount of the training the learner has completed. Represented as a decimal from 0.0-1.0");

                entity.Property(e => e.AttemptCompletionAmountStat)
                    .HasColumnName("attempt_completion_amount_stat")
                    .HasComment("Represents whether the Attempt Completion Amount is known for the activity.");

                entity.Property(e => e.AttemptCompletionStatus)
                    .HasColumnName("attempt_completion_status")
                    .HasComment("Has the leaner completed the training?");

                entity.Property(e => e.AttemptExpDurReported)
                    .HasColumnName("attempt_exp_dur_reported")
                    .HasComment("The total time that the learner actually experienced this activity during the current attempt as reported by the Course.");

                entity.Property(e => e.AttemptExpDurTracked)
                    .HasColumnName("attempt_exp_dur_tracked")
                    .HasComment("The total time that the learner actually experienced this activity during the current attempt as tracked by the system.");

                entity.Property(e => e.AttemptProgressStatus)
                    .HasColumnName("attempt_progress_status")
                    .HasComment("Is the attempt data (completion amount and completion status) known for this activity?");

                entity.Property(e => e.AttemptStartTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("attempt_start_timestamp_utc")
                    .HasComment("Indicates the first time this activity was entered for the current attempt");

                entity.Property(e => e.AttemptedDuringThisAttempt)
                    .HasColumnName("attempted_during_this_attempt")
                    .HasComment("Tracks whether this activity was attempted in the current attempt in its parent");

                entity.Property(e => e.FirstCompletionTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("first_completion_timestamp_utc")
                    .HasComment("The timestamp when this activity was first completed");

                entity.Property(e => e.Included)
                    .HasColumnName("included")
                    .HasComment("Is this activity including in the set presented to the user. If this activity's parent only presents x of y \r\n\r\nchildren, all activities may not be included.");

                entity.Property(e => e.IsLatestAttempt)
                    .IsRequired()
                    .HasColumnName("is_latest_attempt")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Whether this is the latest attempt for the registration");

                entity.Property(e => e.Ordinal)
                    .HasColumnName("ordinal")
                    .HasComment("Number representing the order in which this activity is displayed relative to its siblings. Useful when the parent \r\n\r\nrandomizes the order of its children.");

                entity.Property(e => e.PrevAttemptCompletionStatus)
                    .HasColumnName("prev_attempt_completion_status")
                    .HasComment("The value of attempt completion status on the previous attempt of this activity");

                entity.Property(e => e.PrevAttemptProgressStatus)
                    .HasColumnName("prev_attempt_progress_status")
                    .HasComment("The value of attempt progress status on the previous attempt of this activity");

                entity.Property(e => e.RandomizedChildren)
                    .HasColumnName("randomized_children")
                    .HasComment("If this activity randomizes the order its children are presented, has this process already occurred?");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The ScormObject associated with this activity.");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("The registration this activity is associated with.");

                entity.Property(e => e.SelectedChildren)
                    .HasColumnName("selected_children")
                    .HasComment("If this activity randomly selects which children are to be presented, has this process already occurred?");

                entity.Property(e => e.Suspended)
                    .HasColumnName("suspended")
                    .HasComment("Is this activity currently suspended so the learner can resume a previous attempt?");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivity_ScormObject");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivity_ScormRegis_1");
            });

            modelBuilder.Entity<ScormActivityObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.ScormActivityObjectiveId })
                    .HasName("ScormActivityObjective_pkey");

                entity.ToTable("ScormActivityObjective");

                entity.HasComment("The current status of any objectives associated with this activity. Every activity will have a primary objective \r\n\r\nassociated with it that holds the score and satisfaction status for the activity as a whole.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity that this objective is associated with");

                entity.Property(e => e.ScormActivityObjectiveId)
                    .HasColumnName("scorm_activity_objective_id")
                    .HasComment("Index of this objective within the activity.");

                entity.Property(e => e.CompletionStatus)
                    .HasColumnName("completion_status")
                    .HasComment("Represents the current completion status of this objective (known vs unknown)");

                entity.Property(e => e.CompletionStatusValue)
                    .HasColumnName("completion_status_value")
                    .HasComment("Represents the current completion status of this objective (completed vs incomplete)");

                entity.Property(e => e.FirstObjNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("first_obj_normalized_measure")
                    .HasDefaultValueSql("((0))")
                    .HasComment("The first score record for this objective");

                entity.Property(e => e.FirstSuccessTimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("first_success_timestamp_utc")
                    .HasComment("The timestamp when this objective was first succesfully completed");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier")
                    .HasComment("Unique string identifying this objective.");

                entity.Property(e => e.ObjectiveMeasureStatus)
                    .HasColumnName("objective_measure_status")
                    .HasComment("Is the normalized measure for this objective known?");

                entity.Property(e => e.ObjectiveNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("objective_normalized_measure")
                    .HasComment("The score (normalized between -1.0 and 1.0) the learner achieved on this objective.");

                entity.Property(e => e.ObjectiveProgressStatus)
                    .HasColumnName("objective_progress_status")
                    .HasComment("Is the satisfied status for this objective known?");

                entity.Property(e => e.ObjectiveSatisfiedStatus)
                    .HasColumnName("objective_satisfied_status")
                    .HasComment("Has the learner satisfied this objective?");

                entity.Property(e => e.PrevObjMeasureStatus)
                    .HasColumnName("prev_obj_measure_status")
                    .HasComment("The value of objective measure status on the previous attempt of this activity");

                entity.Property(e => e.PrevObjNormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("prev_obj_normalized_measure")
                    .HasComment("The value of objective normalized measure on the previous attempt of this activity");

                entity.Property(e => e.PrevObjProgressStatus)
                    .HasColumnName("prev_obj_progress_status")
                    .HasComment("The value of objective progress status on the previous attempt of this activity");

                entity.Property(e => e.PrevObjSatisfiedStatus)
                    .HasColumnName("prev_obj_satisfied_status")
                    .HasComment("The value of objective statified status on the previous attempt of this activity");

                entity.Property(e => e.PrimaryObjective)
                    .HasColumnName("primary_objective")
                    .HasComment("Is this the primary objective for the Activity? If so, its values represent the values for the Activity as a \r\n\r\nwhole.");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure")
                    .HasComment("The amount of progress the learner has made towards completing the objective measured as a double from 0-1.");

                entity.Property(e => e.ProgressMeasureStatus)
                    .HasColumnName("progress_measure_status")
                    .HasComment("Represents whether the Progress Measure is known or unknown");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max")
                    .HasComment("The maximum score the learner could have achieved on this objective.");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min")
                    .HasComment("The minimum score the learner could have achieved on this objective.");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw")
                    .HasComment("The raw score (not normalized to any range) that the learner achieved on this objective.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivity)
                    .WithMany(p => p.ScormActivityObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityObjective_S_1");
            });

            modelBuilder.Entity<ScormActivityRt>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId })
                    .HasName("ScormActivityRT_pkey");

                entity.ToTable("ScormActivityRT");

                entity.HasComment("A single row for each SCO in a course for each ScormRegistration... this is where you see tangible pieces of data like scores and statuses for each SCO");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity associated with this set of run time data.");

                entity.Property(e => e.AudioCaptioning)
                    .HasColumnName("audio_captioning")
                    .HasComment("Represents the learner's preference regarding text displaying to supplement an audio presentation.");

                entity.Property(e => e.AudioLevel)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("audio_level")
                    .HasComment("Number representing the relative audio volume level that the learner prefers.");

                entity.Property(e => e.CompletionStatus)
                    .HasColumnName("completion_status")
                    .HasComment("Completion status tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Credit)
                    .HasColumnName("credit")
                    .HasComment("Credit tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.DeliverySpeed)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("delivery_speed")
                    .HasComment("The relative speed at which the learner prefers to experience the training.");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasComment("Entry tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ExitMode)
                    .HasColumnName("exit_mode")
                    .HasComment("Exit tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.LanguagePreference)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("language_preference")
                    .HasComment("The language in which the learner prefers to experience the training.");

                entity.Property(e => e.LessonMode)
                    .HasColumnName("lesson_mode")
                    .HasComment("Lesson Mode tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasComment("Bookmark stored by the SCO.");

                entity.Property(e => e.LocationNull)
                    .IsRequired()
                    .HasColumnName("location_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the location field actually NULL. Needed for Oracle which can't distinguish between NULL and empty \r\n\r\nstring.");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure")
                    .HasComment("Amount of the training the learner has completed. Represented as a decimal from 0.0-1.0");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max")
                    .HasComment("The maximum score the user could have achieved as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min")
                    .HasComment("The minimum score the user could have achieved as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw")
                    .HasComment("The score the user achieved as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreScaled)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("score_scaled")
                    .HasComment("The score the user achieved as reported by the SCO (normalized from -1.0 to 1.0).");

                entity.Property(e => e.SuccessStatus)
                    .HasColumnName("success_status")
                    .HasComment("Success status tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.SuspendData)
                    .HasColumnName("suspend_data")
                    .HasComment("Suspend data stored by the SCO for later use.");

                entity.Property(e => e.SuspendDataNull)
                    .IsRequired()
                    .HasColumnName("suspend_data_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the suspend data field actually NULL. Needed for Oracle which can't distinguish between NULL and \r\n\r\nempty string.");

                entity.Property(e => e.SuspendDataOverflow)
                    .HasColumnName("suspend_data_overflow")
                    .HasComment("Extra suspend data needed by the SCO that doesn't fit in the suspend data field. Needed for Oracle which can only \r\n\r\nstore 4000 characters whereas SCORM requires 4096 characters.");

                entity.Property(e => e.SuspendDataOverflowNull)
                    .IsRequired()
                    .HasColumnName("suspend_data_overflow_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the suspend data overflow field actually NULL. Needed for Oracle which can't distinguish between \r\n\r\nNULL and empty string.");

                entity.Property(e => e.TotalTime)
                    .HasColumnName("total_time")
                    .HasComment("The total time (in hundredths of a second) the learner spent experiencing the training as reported by the SCO.");

                entity.Property(e => e.TotalTimeTracked)
                    .HasColumnName("total_time_tracked")
                    .HasComment("The total time (in hundredths of a second) the learner spent experiencing the training as tracked by the SCP.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivity)
                    .WithOne(p => p.ScormActivityRt)
                    .HasForeignKey<ScormActivityRt>(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRT_ScormAct_1");
            });

            modelBuilder.Entity<ScormActivityRtcomment>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.CommentIndex, e.FromLms })
                    .HasName("ScormActivityRTComment_pkey");

                entity.ToTable("ScormActivityRTComment");

                entity.HasComment("The Comments (both from the learner and from the LMS) associated with this set of run time tracking data.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("The activity id for this comment");

                entity.Property(e => e.CommentIndex)
                    .HasColumnName("comment_index")
                    .HasComment("Index of this comment within the RT data");

                entity.Property(e => e.FromLms)
                    .HasColumnName("from_lms")
                    .HasComment("Is this a Comment From LMS, or a Comment From Learner? Really a bit field, but bits cannot be part of a primary \r\n\r\nkey.");

                entity.Property(e => e.CommentText)
                    .HasColumnName("comment_text")
                    .HasComment("The text of the comment");

                entity.Property(e => e.CommentTextNull)
                    .IsRequired()
                    .HasColumnName("comment_text_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the comment text field actually NULL. Needed for Oracle which can't distinguish between NULL and \r\n\r\nempty string.");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .HasComment("Identifier for the RT Comment");

                entity.Property(e => e.Language)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("language")
                    .HasComment("The language this comment is written in.");

                entity.Property(e => e.LanguageNull)
                    .IsRequired()
                    .HasColumnName("language_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the language field actually NULL. Needed for Oracle which can't distinguish between NULL and empty \r\n\r\nstring.");

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .HasColumnName("location")
                    .HasComment("The location this comment should be displayed in or came from within the content.");

                entity.Property(e => e.LocationNull)
                    .IsRequired()
                    .HasColumnName("location_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the location field actually NULL. Needed for Oracle which can't distinguish between NULL and empty \r\n\r\nstring.");

                entity.Property(e => e.TimestampNull)
                    .IsRequired()
                    .HasColumnName("timestamp_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the timestamp field actually NULL. Needed for Oracle which can't distinguish between NULL and empty \r\n\r\nstring.");

                entity.Property(e => e.TimestampText)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("timestamp_text")
                    .HasComment("The time the comment was created stored as provided by the SCO or LMS.");

                entity.Property(e => e.TimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp_utc")
                    .HasComment("The time the comment was created stored in UTC time.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtcomments)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTComment_S_1");
            });

            modelBuilder.Entity<ScormActivityRtintCorrectResp>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex, e.InteractionCorrectRespIndex })
                    .HasName("mActivityRTIntCorrectResp_pkey");

                entity.ToTable("ScormActivityRTIntCorrectResp");

                entity.HasComment("0 to many correct responses to a given interaction, as reported by the content");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity this interaction response is associated with.");

                entity.Property(e => e.InteractionIndex)
                    .HasColumnName("interaction_index")
                    .HasComment("Interaction within the activity that this correct response is associated with.");

                entity.Property(e => e.InteractionCorrectRespIndex)
                    .HasColumnName("interaction_correct_resp_index")
                    .HasComment("The index of this correct response within the interaction.");

                entity.Property(e => e.CorrectResponse)
                    .HasColumnName("correct_response")
                    .HasComment("The text of the correct response. Note, no _null field is needed because there won't be a row in this table if the \r\n\r\nvalue is null.");

                entity.Property(e => e.CorrectResponseOverflow)
                    .HasColumnName("correct_response_overflow")
                    .HasComment("Extra correct response data needed by the SCO that doesn't fit in the correct response field. Needed for Oracle \r\n\r\nwhich can only store 4000 characters whereas SCORM requires 4096 characters.\r\n");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithMany(p => p.ScormActivityRtintCorrectResps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntCorrec_1");
            });

            modelBuilder.Entity<ScormActivityRtintLearnerResp>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex })
                    .HasName("mActivityRTIntLearnerResp_pkey");

                entity.ToTable("ScormActivityRTIntLearnerResp");

                entity.HasComment("The learner's response to an interaction. This table has a one-to-one relationship to the \r\n\r\nScormActivityRTInteraction table but cannot be part of that table due to max row size limitations.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity this interaction response is associated with.");

                entity.Property(e => e.InteractionIndex)
                    .HasColumnName("interaction_index")
                    .HasComment("Interaction within the activity that this correct response is associated with.");

                entity.Property(e => e.LearnerResponse)
                    .HasColumnName("learner_response")
                    .HasComment("The learner's response to this interaction.");

                entity.Property(e => e.LearnerResponseNull)
                    .IsRequired()
                    .HasColumnName("learner_response_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the learner_response field actually NULL. Needed for Oracle which can't distinguish between NULL \r\n\r\nand empty string.");

                entity.Property(e => e.LearnerResponseOverflow)
                    .HasColumnName("learner_response_overflow")
                    .HasComment("Extra response data needed by the SCO that doesn't fit in the learner_response field. Needed for Oracle which can \r\n\r\nonly store 4000 characters wheras SCORM requires 4096 characters.");

                entity.Property(e => e.LearnerResponseOverflowNull)
                    .IsRequired()
                    .HasColumnName("learner_response_overflow_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the learner_response_overflow field actually NULL. Needed for Oracle which can't distinguish \r\n\r\nbetween NULL and empty string.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithOne(p => p.ScormActivityRtintLearnerResp)
                    .HasForeignKey<ScormActivityRtintLearnerResp>(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntLearne_1");
            });

            modelBuilder.Entity<ScormActivityRtintObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex, e.InteractionObjectiveIndex })
                    .HasName("ormActivityRTIntObjective_pkey");

                entity.ToTable("ScormActivityRTIntObjective");

                entity.HasComment("Learning objectives that are associated with this interaction. Generally the interaction is evaluating the mastery \r\n\r\nof these learning objectives. These objectives may or may not correspond to Activity Objectives or Run Time Objectives.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity this interaction response is associated with.");

                entity.Property(e => e.InteractionIndex)
                    .HasColumnName("interaction_index")
                    .HasComment("Interaction within the activity that this correct response is associated with.");

                entity.Property(e => e.InteractionObjectiveIndex)
                    .HasColumnName("interaction_objective_index")
                    .HasComment("he index of this objective within the interaction.");

                entity.Property(e => e.ObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_id")
                    .HasComment("The identifier objective being associated with this interaction. Note, no _null field is needed because there won't \r\n\r\nbe a row in this table if the value is null.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivityRtinteraction)
                    .WithMany(p => p.ScormActivityRtintObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId, d.InteractionIndex })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTIntObject_1");
            });

            modelBuilder.Entity<ScormActivityRtinteraction>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.InteractionIndex })
                    .HasName("cormActivityRTInteraction_pkey");

                entity.ToTable("ScormActivityRTInteraction");

                entity.HasComment("0 to many interactions associated with a given SCO (ScormActivityRT)");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity this interaction is associated with.");

                entity.Property(e => e.InteractionIndex)
                    .HasColumnName("interaction_index")
                    .HasComment("The index of the interaction within this activity.");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description")
                    .HasComment("A human readable description of the interaction. Only stored for SCORM 2004 content.");

                entity.Property(e => e.DescriptionNull)
                    .IsRequired()
                    .HasColumnName("description_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the description field actually NULL. Needed for Oracle which can't distinguish between NULL and \r\n\r\nempty string.");

                entity.Property(e => e.InteractionId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("interaction_id")
                    .HasComment("String identifying this interaction. Not necessarily unique, especially since an interaction may be attempted more \r\n\r\nthan once.");

                entity.Property(e => e.Latency)
                    .HasColumnName("latency")
                    .HasComment("The amount of time (in hundredths of a second) that the learner spent on this interaction, as reported by the SCO.");

                entity.Property(e => e.Result)
                    .HasColumnName("result")
                    .HasComment("The result of this interaction. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ResultNumeric)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("result_numeric")
                    .HasComment("If the result is numeric, this field contains the decimal representing the numeric value of the result.");

                entity.Property(e => e.TimestampNull)
                    .IsRequired()
                    .HasColumnName("timestamp_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the timestamp field actually NULL. Needed for Oracle which can't distinguish between NULL and empty \r\n\r\nstring.");

                entity.Property(e => e.TimestampText)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("timestamp_text")
                    .HasComment("The time the interaction was created, stored as provided by the SCO or LMS.");

                entity.Property(e => e.TimestampUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp_utc")
                    .HasComment("The time the interaction was created, stored in UTC time.");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("The type of the interaction. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.Weighting)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("weighting")
                    .HasComment("The weight of this interaction relative to the other interactions in this SCO.");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtinteractions)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTInteracti_1");
            });

            modelBuilder.Entity<ScormActivityRtobjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormActivityId, e.ObjectiveIndex })
                    .HasName("ScormActivityRTObjective_pkey");

                entity.ToTable("ScormActivityRTObjective");

                entity.HasComment("The run time status data for the learning objectives associated with this activity. These objectives may or may not \r\n\r\ncorrespond to the activity objectives.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormActivityId)
                    .HasColumnName("scorm_activity_id")
                    .HasComment("Activity these objectives are associated with.");

                entity.Property(e => e.ObjectiveIndex)
                    .HasColumnName("objective_index")
                    .HasComment("The index of this objective within the activity.");

                entity.Property(e => e.CompletionStatus)
                    .HasColumnName("completion_status")
                    .HasComment("Completion status tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Description)
                    .HasMaxLength(510)
                    .HasColumnName("description")
                    .HasComment("A human readable description of this learning objective. Not available in SCORM 1.2.");

                entity.Property(e => e.DescriptionNull)
                    .IsRequired()
                    .HasColumnName("description_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the description field actually NULL. Needed for Oracle which can't distinguish between NULL and \r\n\r\nempty string.");

                entity.Property(e => e.ObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_id")
                    .HasComment("The string uniquely identifying this objective.");

                entity.Property(e => e.ObjectiveIdNull)
                    .IsRequired()
                    .HasColumnName("objective_id_null")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Is the value in the objective_id field actually NULL. Needed for Oracle which can't distinguish between NULL and \r\n\r\nempty string.");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure")
                    .HasComment("Amount of the training the learner has completed towards this objective. Represented as a decimal from 0.0-1.0");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max")
                    .HasComment("The maximum score the user could have achieved on this objective as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min")
                    .HasComment("The minimum score the user could have achieved on this objective as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw")
                    .HasComment("The score the user achieved on this objective as reported by the SCO (range unbounded).");

                entity.Property(e => e.ScoreScaled)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("score_scaled")
                    .HasComment("The score the user achieved on this objective as reported by the SCO (normalized from -1.0 to 1.0).");

                entity.Property(e => e.SuccessStatus)
                    .HasColumnName("success_status")
                    .HasComment("Success status tracking data. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormActivityRt)
                    .WithMany(p => p.ScormActivityRtobjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormActivityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormActivityRTObjective_1");
            });

            modelBuilder.Entity<ScormAiccSession>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.AiccSessionId })
                    .HasName("ScormAiccSession_pkey");

                entity.ToTable("ScormAiccSession");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_ScormAiccSession");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.AiccSessionId)
                    .HasColumnName("aicc_session_id")
                    .HasComment("Identifier of the AICC session");

                entity.Property(e => e.ExternalConfiguration)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_configuration")
                    .HasComment("Identifier of learner this tag is associated with");

                entity.Property(e => e.ExternalRegistrationId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_registration_id")
                    .HasComment("External config of this session");

                entity.Property(e => e.IsTracking)
                    .HasColumnName("is_tracking")
                    .HasComment("Whether this session is tracking");

                entity.Property(e => e.LaunchHistoryId)
                    .HasColumnName("launch_history_id")
                    .HasComment("Identifier of the acssociated launch history record");

                entity.Property(e => e.LegacyAiccSessionId)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("legacy_aicc_session_id");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Identifier of SCORM registration this session is associated with");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormAiccSessions)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormAiccSession_ScormRe_1");
            });

            modelBuilder.Entity<ScormEngineDbUpdate>(entity =>
            {
                entity.HasKey(e => e.UpdateId)
                    .IsClustered(false);

                entity.Property(e => e.UpdateId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("update_id")
                    .HasComment("The value identifying the update applied.");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");
            });

            modelBuilder.Entity<ScormLaunchHistory>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.LaunchHistoryId })
                    .HasName("ScormLaunchHistory_pkey");

                entity.ToTable("ScormLaunchHistory");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId, e.LaunchTime }, "IX_ScormLaunchHistory");

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDt }, "IX_ScormLaunchHistory_update");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.LaunchHistoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("launch_history_id")
                    .HasComment("Unique identifier for this launch.");

                entity.Property(e => e.Completion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("completion")
                    .HasComment("Description of completion status when training exited.");

                entity.Property(e => e.ExitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("exit_time")
                    .HasComment("The time learner exited this registration.");

                entity.Property(e => e.ExitTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("exit_time_utc");

                entity.Property(e => e.ExperiencedDurationTracked)
                    .HasColumnName("experienced_duration_tracked")
                    .HasComment("The total time (in hundredths of a second) the learner spent experiencing the training as tracked by the Scorm Engine when training exited.");

                entity.Property(e => e.HistoryLog)
                    .HasColumnName("history_log")
                    .HasComment("Cumlative history log for this launch.");

                entity.Property(e => e.LastRuntimeUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_runtime_update");

                entity.Property(e => e.LastRuntimeUpdateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("last_runtime_update_utc");

                entity.Property(e => e.LaunchTime)
                    .HasColumnType("datetime")
                    .HasColumnName("launch_time")
                    .HasComment("The time learner launched this registration.");

                entity.Property(e => e.LaunchTimeUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("launch_time_utc");

                entity.Property(e => e.MeasureStatus)
                    .HasColumnName("measure_status")
                    .HasComment("Is the normalized measure for this training known?");

                entity.Property(e => e.NormalizedMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("normalized_measure")
                    .HasComment("The score (normalized between -1.0 and 1.0) the learner achieved on this training when training exited.");

                entity.Property(e => e.Satisfaction)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("satisfaction")
                    .HasComment("Description of satisfaction status when training exited.");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Registration that this launch data is applicable to.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record.");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated.");

                entity.Property(e => e.UpdateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt_utc");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormLaunchHistories)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormLaunchHistory_Scorm_1");
            });

            modelBuilder.Entity<ScormMetadatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormMetadataId })
                    .HasName("ScormMetadata_pkey");

                entity.HasComment("Metadata for each ScormObject, as provided in the manifest");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormObjectId }, "IX_SMD_object_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId }, "IX_SMD_package_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormMetadataId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_metadata_id")
                    .HasComment("Primary key identifying this set of metadata");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasComment("Description of the learning object  being described by the metadata");

                entity.Property(e => e.Duration)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("duration")
                    .HasComment("The time a continuous learning object takes when played at intended speed.  This is usesful for sounds, movies, simulations, etc.");

                entity.Property(e => e.FileHref)
                    .HasMaxLength(1000)
                    .HasColumnName("file_href")
                    .HasComment("Comma-separated list of file paths for a particular SCORM resource.");

                entity.Property(e => e.Identifier)
                    .HasMaxLength(1000)
                    .HasColumnName("identifier")
                    .HasComment("Globally unique label that identifies the SCORM Content Model Component");

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .HasComment("Comma-delimited list of common keywords or phrases that describe the learning object");

                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("language_code")
                    .HasComment("Primary language used within the learning object to communicate to the user");

                entity.Property(e => e.MetadataIndex)
                    .HasColumnName("metadata_index")
                    .HasComment("Original ordering of metadata in the manifest.");

                entity.Property(e => e.MetadataXml)
                    .HasColumnName("metadata_xml")
                    .HasComment("The metadata xml associated through the SCORM Manifest, if available");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("Object within the package the metadata is attached to.  If MIN value (null) the metadata describes the package as a whole.");

                entity.Property(e => e.ScormPackageId)
                    .HasColumnName("scorm_package_id")
                    .HasComment("Package the metadata is attached to");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .HasColumnName("title")
                    .HasComment("Name given to the learning object");

                entity.Property(e => e.TypicalLearningTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typical_learning_time")
                    .HasComment("Approximate typical time it takes a user to work with or through the learning object for the intended audience.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .HasColumnName("version")
                    .HasComment("The edition of the SCORM Content Model Component used by this learning object.");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormMetadata)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .HasConstraintName("FK_ScormMetadata_ScormObject");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormMetadata)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormMetadata_ScormPackage");
            });

            modelBuilder.Entity<ScormObject>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObject_pkey");

                entity.ToTable("ScormObject");

                entity.HasComment("Holds the individual parts of a SCORM course, such as SCOs, Aggregations and Assets. Each \"item\" from a manifest is \r\n\r\nrepresented as SCORM object.");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId }, "IX_SO_package_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_object_id")
                    .HasComment("Identifier for the object.");

                entity.Property(e => e.CompletedByMeasure)
                    .HasColumnName("completed_by_measure")
                    .HasComment("Determines whether or not the completionThreshold should be used to determine completion of thie LearningObject.");

                entity.Property(e => e.CompletionProgressWeight)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("completion_progress_weight")
                    .HasDefaultValueSql("((1.0))")
                    .HasComment("The weight of this LearningObject CompletionThreshold to use for completion rollup.");

                entity.Property(e => e.CompletionThreshold)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("completion_threshold")
                    .HasComment("The amount of the course that the learner must experience before completion is achieved. Represented as a decimal \r\n\r\nfrom 0.0-1.0");

                entity.Property(e => e.DataFromLms)
                    .HasColumnName("data_from_lms")
                    .HasComment("Data that the SCO expects the LMS to make available through the run time environment");

                entity.Property(e => e.FileList)
                    .HasColumnName("file_list")
                    .HasComment("Comma-delimited list of all file hrefs associated with this learning object");

                entity.Property(e => e.Href)
                    .HasMaxLength(2000)
                    .HasColumnName("href")
                    .HasComment("The URL to launch the object relative to the root of the package. This field should only be populated for launchable \r\n\r\nobjects such as SCOs.");

                entity.Property(e => e.MasteryScore)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("mastery_score")
                    .HasComment("The passing score for this SCO");

                entity.Property(e => e.MaxTimeAllowed)
                    .HasColumnName("max_time_allowed")
                    .HasComment("The maximum time the user is allowed to experience this training.");

                entity.Property(e => e.Parameters)
                    .HasMaxLength(1000)
                    .HasColumnName("parameters")
                    .HasComment("The querystring parameters that should be passed to this object when launching it.");

                entity.Property(e => e.PersistState)
                    .HasColumnName("persist_state")
                    .HasComment("Determines whether or not the activity's state should be persisted between attempts (deprecated).");

                entity.Property(e => e.Prerequisites)
                    .HasMaxLength(200)
                    .HasColumnName("prerequisites")
                    .HasComment("The prerequisites for this object (note these are not enforced due to problems with the specification).");

                entity.Property(e => e.ScormObjectTypeId)
                    .HasColumnName("scorm_object_type_id")
                    .HasComment("The type of the object.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ScormPackageId)
                    .HasColumnName("scorm_package_id")
                    .HasComment("The package this object is associated with.");

                entity.Property(e => e.TimeLimitAction)
                    .HasColumnName("time_limit_action")
                    .HasComment("What should the content do if the time limit has been exceeded? Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title")
                    .HasComment("The title of the object.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.Visible)
                    .HasColumnName("visible")
                    .HasComment("Should this object be visible to the user?");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormObjects)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObject_ScormPackage");
            });

            modelBuilder.Entity<ScormObjectHierarchy>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ParentScormObjectId, e.ChildScormObjectId })
                    .HasName("ScormObjectHierarchy_pkey");

                entity.ToTable("ScormObjectHierarchy");

                entity.HasComment("Establishes hierarchical relationships between ScormObjects to form a tree structure. There should be one \r\n\r\nScormObject in each package that is not listed as a child in this table, that object is the root of the tree.");

                entity.HasIndex(e => new { e.EngineTenantId, e.ChildScormObjectId }, "IX_SOH_child_object_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ParentScormObjectId)
                    .HasColumnName("parent_scorm_object_id")
                    .HasComment("Parent ScormObject in the hierarchical relationship.");

                entity.Property(e => e.ChildScormObjectId)
                    .HasColumnName("child_scorm_object_id")
                    .HasComment("Child ScormObject in the hierarchical relationship.");

                entity.Property(e => e.Ordinal)
                    .HasColumnName("ordinal")
                    .HasComment("Relative order of the child within the parent with respect to its siblings.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectHierarchyScormObjects)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ChildScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectHierarchyChil_1");

                entity.HasOne(d => d.ScormObjectNavigation)
                    .WithMany(p => p.ScormObjectHierarchyScormObjectNavigations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ParentScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectHierarchyPare_1");
            });

            modelBuilder.Entity<ScormObjectIdentifier>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObjectIdentifiers_pkey");

                entity.HasComment("Contains alternate textual identifiers for the ScormObject. There is a one-to-one relationship between this table \r\n\r\nand the ScormObject table. It is not part of the ScormObject table because of row size constraints.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("ScormObject this row is associated with.");

                entity.Property(e => e.ExternalIdentifier)
                    .HasColumnName("external_identifier")
                    .HasComment("An external identifier that uniquely identifies this object in the external LMS. This field is useful for tracking \r\n\r\nSCOs that are reused across packages or that have multiple versions.");

                entity.Property(e => e.ItemIdentifier)
                    .HasColumnName("item_identifier")
                    .HasComment("The identifier listed in the SCORM manifest under the Item tag.");

                entity.Property(e => e.ResourceIdentifier)
                    .HasColumnName("resource_identifier")
                    .HasComment("The identifier listed in the SCORM manifest under the Resource tag.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObject)
                    .WithOne(p => p.ScormObjectIdentifier)
                    .HasForeignKey<ScormObjectIdentifier>(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectIdentifiers_S_1");
            });

            modelBuilder.Entity<ScormObjectSeqDatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId })
                    .HasName("ScormObjectSeqData_pkey");

                entity.HasComment("Holds the sequencing rules for SCORM 2004 content.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this sequencing data relates to.");

                entity.Property(e => e.CompletionSetByContent)
                    .HasColumnName("completion_set_by_content")
                    .HasComment("Specifies whether or not the SCO will set its own completion data.");

                entity.Property(e => e.ConstrainChoice)
                    .HasColumnName("constrain_choice")
                    .HasComment("Only allows activities that are logically next or previous from the current activity to be selected by a choice \r\n\r\nnavigation request.");

                entity.Property(e => e.ControlChoice)
                    .HasColumnName("control_choice")
                    .HasComment("Is the user allowed to make choice navigation requests?");

                entity.Property(e => e.ControlChoiceExit)
                    .HasColumnName("control_choice_exit")
                    .HasComment("Can the user exit this Scorm Object by making a choice navigation request?");

                entity.Property(e => e.ControlFlow)
                    .HasColumnName("control_flow")
                    .HasComment("Can the user navigation using previous and next commands?");

                entity.Property(e => e.ControlForwardOnly)
                    .HasColumnName("control_forward_only")
                    .HasComment("Should the user be forced to progress through the content by only moving forward and never moving backward?");

                entity.Property(e => e.HideAbandon)
                    .HasColumnName("hide_abandon")
                    .HasComment("Should the SCP hide the abandon navigation button.");

                entity.Property(e => e.HideAbandonAll)
                    .HasColumnName("hide_abandon_all")
                    .HasComment("Determines whether the abandon all navigation option is enabled.");

                entity.Property(e => e.HideContinue)
                    .HasColumnName("hide_continue")
                    .HasComment("Should the SCP hide the continue navigation button.");

                entity.Property(e => e.HideExit)
                    .HasColumnName("hide_exit")
                    .HasComment("Should the SCP hide the exit navigation button.");

                entity.Property(e => e.HideExitAll)
                    .HasColumnName("hide_exit_all")
                    .HasComment("Determines whether the exit all navigation option is enabled.");

                entity.Property(e => e.HidePrevious)
                    .HasColumnName("hide_previous")
                    .HasComment("Should the SCP hide the previous navigation button.");

                entity.Property(e => e.HideSuspendAll)
                    .HasColumnName("hide_suspend_all")
                    .HasComment("Determines whether the suspend all navigation option is enabled.");

                entity.Property(e => e.LimitCondAttemptControl)
                    .HasColumnName("limit_cond_attempt_control")
                    .HasComment("Is the attempt limit known for this object?");

                entity.Property(e => e.LimitCondAttemptDurControl)
                    .HasColumnName("limit_cond_attempt_dur_control")
                    .HasComment("Not implemented in the current version, only included for possible future support thus we just store whatever the \r\n\r\nmanifest specifies");

                entity.Property(e => e.LimitCondAttemptDurLimit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("limit_cond_attempt_dur_limit")
                    .HasComment("Not implemented in the current version, only included for possible future support thus we just store whatever the \r\n\r\nmanifest specifies");

                entity.Property(e => e.LimitCondAttemptLimit)
                    .HasColumnName("limit_cond_attempt_limit")
                    .HasComment("The maximum number of time the learner is allowed to attempt this object.");

                entity.Property(e => e.MeasureSatisfactionIfActive)
                    .HasColumnName("measure_satisfaction_if_active")
                    .HasComment("Determines if an activity's score should be included in roll-up even if the activity is still active and not \r\n\r\nnecessarily complete.");

                entity.Property(e => e.ObjectiveSetByContent)
                    .HasColumnName("objective_set_by_content")
                    .HasComment("Specifies whether or not the SCO will set its own success data.");

                entity.Property(e => e.PreventActivation)
                    .HasColumnName("prevent_activation")
                    .HasComment("Prevents activities from beginning by a choice navigation request.");

                entity.Property(e => e.RandomizationTiming)
                    .HasColumnName("randomization_timing")
                    .HasComment("Determines when an activity's children should be put in a random order. Stored as an integer representing the \r\n\r\nvocabulary.");

                entity.Property(e => e.RandomizeChildren)
                    .HasColumnName("randomize_children")
                    .HasComment("Determines whether an activity's children should be presented in a random order.");

                entity.Property(e => e.RequiredForCompleted)
                    .HasColumnName("required_for_completed")
                    .HasComment("Indicates when the activity's tracking information contributes to its parent's completion status. Stored as an \r\n\r\ninteger representing the vocabulary.");

                entity.Property(e => e.RequiredForIncomplete)
                    .HasColumnName("required_for_incomplete")
                    .HasComment("Indicates when the activity's tracking information contributes to its parent being incomplete. Stored as an integer \r\n\r\nrepresenting the vocabulary.");

                entity.Property(e => e.RequiredForNotSatisfied)
                    .HasColumnName("required_for_not_satisfied")
                    .HasComment("Indicates when the activity's tracking information contributes to its parent being not satisfied. Stored as an \r\n\r\ninteger representing the vocabulary.");

                entity.Property(e => e.RequiredForSatisfied)
                    .HasColumnName("required_for_satisfied")
                    .HasComment("Indicates when the activity's tracking information contributes to its parent's satisfied status. Stored as an \r\n\r\ninteger representing the vocabulary.");

                entity.Property(e => e.RollupObjMeasureWeight)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("rollup_obj_measure_weight")
                    .HasComment("When rolling up a score, what weight should be given to this score relative to the other objects.");

                entity.Property(e => e.RollupObjectiveSatisfied)
                    .HasColumnName("rollup_objective_satisfied")
                    .HasComment("Determines if the satisfaction/success data from the children should be rollup to the parent.");

                entity.Property(e => e.RollupProgressCompletion)
                    .HasColumnName("rollup_progress_completion")
                    .HasComment("Determines if the progress/completion data from the children should be rollup to the parent.");

                entity.Property(e => e.SelectionCount)
                    .HasColumnName("selection_count")
                    .HasComment("The number of this activity's children that should be included in this attempt.");

                entity.Property(e => e.SelectionCountStatus)
                    .HasColumnName("selection_count_status")
                    .HasComment("Is the selection count known for this activity?");

                entity.Property(e => e.SelectionTiming)
                    .HasColumnName("selection_timing")
                    .HasComment("Determines when an activity should select which children should be included in this delivery. Stored as an integer \r\n\r\nrepresenting the vocabulary.");

                entity.Property(e => e.Tracked)
                    .HasColumnName("tracked")
                    .HasComment("Determines whether an activity's data should be tracked and used for sequencing and rollup.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.UseCurrentAttemptObjInfo)
                    .HasColumnName("use_current_attempt_obj_info")
                    .HasComment("Determines whether or not success status data from previous attempts on the object should be considered in \r\n\r\nsequencing evaluations, or if only data from the current attempt should be considered.");

                entity.Property(e => e.UseCurrentAttemptProgInfo)
                    .HasColumnName("use_current_attempt_prog_info")
                    .HasComment("Determines whether or not completion status data from previous attempts on the object should be considered in \r\n\r\nsequencing evaluations, or if only data from the current attempt should be considered.");

                entity.HasOne(d => d.ScormObject)
                    .WithOne(p => p.ScormObjectSeqDatum)
                    .HasForeignKey<ScormObjectSeqDatum>(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqData_Scorm_1");
            });

            modelBuilder.Entity<ScormObjectSeqObjective>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjectSeqObjectiveId })
                    .HasName("ScormObjectSeqObjective_pkey");

                entity.ToTable("ScormObjectSeqObjective");

                entity.HasComment("Represents objectives defined in the sequencing rules that are used for sequencing and rollup evaluation.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this sequencing data relates to.");

                entity.Property(e => e.ScormObjectSeqObjectiveId)
                    .HasColumnName("scorm_object_seq_objective_id")
                    .HasComment("The index of the objective within the ScormObject.");

                entity.Property(e => e.MinNormalizedMeasure)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("min_normalized_measure")
                    .HasComment("The passing score for the objective, normalized values between -1.0 and 1.0");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier")
                    .HasComment("A unique identifier representing the objective. This may or may not correspond to runtime objectives and will always \r\n\r\nresult in the creation of a new ActivityObjective at runtime");

                entity.Property(e => e.PrimaryObjective)
                    .HasColumnName("primary_objective")
                    .HasComment("Is this the primary objective for the ScormObject? If so, then the satisfaction of this objective corresponds to the \r\n\r\nsatisfaction status of the ScormObject as a whole.");

                entity.Property(e => e.SatisfiedByMeasure)
                    .HasColumnName("satisfied_by_measure")
                    .HasComment("Indicates that the score of the objective is the only mechanism that may be used to determine the satisfaction of \r\n\r\nthe objective.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqObjectives)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqObjective_1");
            });

            modelBuilder.Entity<ScormObjectSeqObjectiveMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjectSeqObjectiveId, e.ScormObjectSeqObjMapId })
                    .HasName("cormObjectSeqObjectiveMap_pkey");

                entity.ToTable("ScormObjectSeqObjectiveMap");

                entity.HasComment("This table is a mechanism for mapping objectives associated with a ScormObject with global learning objectives.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this sequencing data relates to.");

                entity.Property(e => e.ScormObjectSeqObjectiveId)
                    .HasColumnName("scorm_object_seq_objective_id")
                    .HasComment("The index of the objective within the ScormObject.");

                entity.Property(e => e.ScormObjectSeqObjMapId)
                    .HasColumnName("scorm_object_seq_obj_map_id")
                    .HasComment("The index of the map within the Scorm Object Sequencing Objective.");

                entity.Property(e => e.ReadCompletionStatus)
                    .HasColumnName("read_completion_status")
                    .HasComment("The value of the SCORM ReadCompletionStatus sequencing data (ADL Extension)");

                entity.Property(e => e.ReadNormalizedMeasure)
                    .HasColumnName("read_normalized_measure")
                    .HasComment("Should this objective read the score from the global objective?");

                entity.Property(e => e.ReadProgressMeasure)
                    .HasColumnName("read_progress_measure")
                    .HasComment("The value of the SCORM ReadProgressMeasure sequencing data (ADL Extension)");

                entity.Property(e => e.ReadSatisfiedStatus)
                    .HasColumnName("read_satisfied_status")
                    .HasComment("Should this objective read the satisfied status from the global objective?");

                entity.Property(e => e.ReadScoreMax)
                    .HasColumnName("read_score_max")
                    .HasComment("The value of the SCORM ReadMaxScore sequencing data (ADL Extension)");

                entity.Property(e => e.ReadScoreMin)
                    .HasColumnName("read_score_min")
                    .HasComment("The value of the SCORM ReadMinScore sequencing data (ADL Extension)");

                entity.Property(e => e.ReadScoreRaw)
                    .HasColumnName("read_score_raw")
                    .HasComment("The value of the SCORM ReadRawScore sequencing data (ADL Extension)");

                entity.Property(e => e.TargetObjectiveId)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("target_objective_id")
                    .HasComment("The unique identifier of the global objective this local objective should be mapped to.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.WriteCompletionStatus)
                    .HasColumnName("write_completion_status")
                    .HasComment("The value of the SCORM WriteCompletionStatus sequencing data (ADL Extension)");

                entity.Property(e => e.WriteNormalizedMeasure)
                    .HasColumnName("write_normalized_measure")
                    .HasComment("Should this objective write the score to the global objective?");

                entity.Property(e => e.WriteProgressMeasure)
                    .HasColumnName("write_progress_measure")
                    .HasComment("The value of the SCORM WriteProgressMeasure sequencing data (ADL Extension)");

                entity.Property(e => e.WriteSatisfiedStatus)
                    .HasColumnName("write_satisfied_status")
                    .HasComment("Should this objective write the satisfied status to the global objective?");

                entity.Property(e => e.WriteScoreMax)
                    .HasColumnName("write_score_max")
                    .HasComment("The value of the SCORM WriteMaxScore sequencing data (ADL Extension)");

                entity.Property(e => e.WriteScoreMin)
                    .HasColumnName("write_score_min")
                    .HasComment("The value of the SCORM WriteMinScore sequencing data (ADL Extension)");

                entity.Property(e => e.WriteScoreRaw)
                    .HasColumnName("write_score_raw")
                    .HasComment("The value of the SCORM WriteRawScore sequencing data (ADL Extension)");

                entity.HasOne(d => d.ScormObjectSeqObjective)
                    .WithMany(p => p.ScormObjectSeqObjectiveMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.ScormObjectSeqObjectiveId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqObjectiveM_1");
            });

            modelBuilder.Entity<ScormObjectSeqRollupRule>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.RollupRuleId })
                    .HasName("ScormObjectSeqRollupRule_pkey");

                entity.ToTable("ScormObjectSeqRollupRule");

                entity.HasComment("Represents an instruction used to roll up the state of an activity's children to that activity.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this rule relates to.");

                entity.Property(e => e.RollupRuleId)
                    .HasColumnName("rollup_rule_id")
                    .HasComment("The index of this rule within the ScormObject");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasComment("The new state of the activity if the rollup rule evaluates to true. Stored as an integer representing the \r\n\r\nvocabulary.");

                entity.Property(e => e.ChildActivitySet)
                    .HasColumnName("child_activity_set")
                    .HasComment("The set of children that contribute their data to the rollup. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ConditionCombination)
                    .HasColumnName("condition_combination")
                    .HasComment("Operator used to combine the conditions for this rule. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.MinimumCount)
                    .HasColumnName("minimum_count")
                    .HasComment("The minimum number of children that must be included when the atLeastCount child activity set is used.");

                entity.Property(e => e.MinimumPercent)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("minimum_percent")
                    .HasComment("The minimum percent of the children that must be included when the atLeastPercent child activity set is used.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqRollupRules)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRollupRule_1");
            });

            modelBuilder.Entity<ScormObjectSeqRollupRuleCond>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.RollupRuleId, e.RollupRuleConditionId })
                    .HasName("rmObjectSeqRollupRuleCond_pkey");

                entity.ToTable("ScormObjectSeqRollupRuleCond");

                entity.HasComment("Conditions used to evaluate whether or not a roll up rule should be processed.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("Scorm object associated with this rule.");

                entity.Property(e => e.RollupRuleId)
                    .HasColumnName("rollup_rule_id")
                    .HasComment("Rule this condition is associated with.");

                entity.Property(e => e.RollupRuleConditionId)
                    .HasColumnName("rollup_rule_condition_id")
                    .HasComment("Index of this condition within the rule.");

                entity.Property(e => e.ConditionOperator)
                    .HasColumnName("condition_operator")
                    .HasComment("Can negate the result of the explicit evaluation. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.RuleCondition)
                    .HasColumnName("rule_condition")
                    .HasComment("The condition to evaluate to determine if this rule is true. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObjectSeqRollupRule)
                    .WithMany(p => p.ScormObjectSeqRollupRuleConds)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.RollupRuleId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRollupRule_2");
            });

            modelBuilder.Entity<ScormObjectSeqRule>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.SeqRuleId })
                    .HasName("ScormObjectSeqRule_pkey");

                entity.ToTable("ScormObjectSeqRule");

                entity.HasComment("Represents a rule that is used to determine the order in which a learner experiences the activities.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this rule relates to.");

                entity.Property(e => e.SeqRuleId)
                    .HasColumnName("seq_rule_id")
                    .HasComment("The index of this rule within the ScormObject");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasComment("The sequencing action that should be performed if this rule evaluates to true.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ConditionCombination)
                    .HasColumnName("condition_combination")
                    .HasComment("Operator used to combine the conditions for this rule. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.RuleType)
                    .HasColumnName("rule_type")
                    .HasComment("The place in the sequencing process where this rule is evaluated. Stored as an integer representing the \r\n\r\nvocabulary.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObjectSeqDatum)
                    .WithMany(p => p.ScormObjectSeqRules)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRule_Scorm_1");
            });

            modelBuilder.Entity<ScormObjectSeqRuleCond>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.SeqRuleId, e.SeqRuleConditionId })
                    .HasName("ScormObjectSeqRuleCond_pkey");

                entity.ToTable("ScormObjectSeqRuleCond");

                entity.HasComment("Represents a condition that must be satisfied in order for a sequencing rule to be executed.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("The Scorm Object this rule relates to.");

                entity.Property(e => e.SeqRuleId)
                    .HasColumnName("seq_rule_id")
                    .HasComment("The index of this rule within the ScormObject");

                entity.Property(e => e.SeqRuleConditionId)
                    .HasColumnName("seq_rule_condition_id")
                    .HasComment("The index of this condition within the sequencing rule.");

                entity.Property(e => e.ConditionOperator)
                    .HasColumnName("condition_operator")
                    .HasComment("Can negate the result of the explicit evaluation. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.MeasureThreshold)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("measure_threshold")
                    .HasComment("The minimum score needed to satisfy this rule condition.");

                entity.Property(e => e.ReferencedObjective)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("referenced_objective")
                    .HasComment("Specifies a sequencing objective that this rule extracts state data from for use in evaluation.");

                entity.Property(e => e.RuleCondition)
                    .HasColumnName("rule_condition")
                    .HasComment("The condition to evaluate to determine if this rule is true. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObjectSeqRule)
                    .WithMany(p => p.ScormObjectSeqRuleConds)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId, d.SeqRuleId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSeqRuleCond_S_1");
            });

            modelBuilder.Entity<ScormObjectSharedDataMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.ScormObjSharedDataMapId })
                    .HasName("ScormObjectSharedDataMap_pkey");

                entity.ToTable("ScormObjectSharedDataMap");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("SCORM Object that this shared data map is applicable to.");

                entity.Property(e => e.ScormObjSharedDataMapId)
                    .HasColumnName("scorm_obj_shared_data_map_id")
                    .HasComment("Index of the shared data map within the learning object.");

                entity.Property(e => e.ReadSharedData)
                    .HasColumnName("read_shared_data")
                    .HasComment("Does the learning object have read access to the value?");

                entity.Property(e => e.TargetSharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("target_shared_data_id")
                    .HasComment("Unique identifier of the shared data target.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.WriteSharedData)
                    .HasColumnName("write_shared_data")
                    .HasComment("Does the learning object have write access to the value?");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectSharedDataMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSharedDataMap_1");
            });

            modelBuilder.Entity<ScormObjectSspbucket>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormObjectId, e.BucketIndex })
                    .HasName("ScormObjectSSPBucket_pkey");

                entity.ToTable("ScormObjectSSPBucket");

                entity.HasComment("Sharable State Persistence Bucket for a SCORM Object");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormObjectId)
                    .HasColumnName("scorm_object_id")
                    .HasComment("Scorm object that has this SSP bucket defined on it");

                entity.Property(e => e.BucketIndex)
                    .HasColumnName("bucket_index")
                    .HasComment("Incrementing integer representing this bucket's index in the collection");

                entity.Property(e => e.BucketIdentifier)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_identifier")
                    .HasComment("Unique URI identifying bucket");

                entity.Property(e => e.BucketType)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_type")
                    .HasComment("Optional string that the bucket can specifiy to categorize what type of data it holds");

                entity.Property(e => e.Persistence)
                    .HasColumnName("persistence")
                    .HasComment("Duration and scope in which this bucket is persisted and visible (course, learner or session).  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Reducible)
                    .HasColumnName("reducible")
                    .HasComment("Will the bucket accept anything less than the requested number of bytes");

                entity.Property(e => e.SizeMin)
                    .HasColumnName("size_min")
                    .HasComment("Minimum number of bytes this bucket requested be allocated for storage");

                entity.Property(e => e.SizeRequested)
                    .HasColumnName("size_requested")
                    .HasComment("Maximum number of bytes this bucket requested be allocated for storage");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormObject)
                    .WithMany(p => p.ScormObjectSspbuckets)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormObjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormObjectSSPBucket_Sco_1");
            });

            modelBuilder.Entity<ScormObjectStore>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ObjectKeySha1 })
                    .HasName("ScormObjectStore_pkey");

                entity.ToTable("ScormObjectStore");

                entity.HasIndex(e => new { e.EngineTenantId, e.Expiry }, "IX_SOS_e");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ObjectKeySha1)
                    .HasMaxLength(20)
                    .HasColumnName("object_key_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ObjectKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("object_key");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("object_type");

                entity.Property(e => e.ObjectValue).HasColumnName("object_value");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ScormPackage>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormPackageId })
                    .HasName("ScormPackage_pkey");

                entity.ToTable("ScormPackage");

                entity.HasComment("Represents a SCORM package. This table is part of the integration layer and will change for each implementation. \r\n\r\nThis table will often have a 1-to-1 correspondence with a table in the external LMS that stores the concept or course, lesson, task, etc.");

                entity.HasIndex(e => new { e.EngineTenantId, e.CorId, e.VersionId }, "ExternalPackageId")
                    .IsUnique();

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDt, e.ScormPackageId }, "IX_SP_console_ordering");

                entity.HasIndex(e => new { e.EngineTenantId, e.InvariantTitle }, "IX_SP_invariant_title");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormPackageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_package_id")
                    .HasComment("Unique identifier for the package.");

                entity.Property(e => e.ConnectorContentId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("connector_content_id");

                entity.Property(e => e.ContentConnectorId)
                    .HasMaxLength(16)
                    .HasColumnName("content_connector_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CorId)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("cor_id");

                entity.Property(e => e.DisplayTitle)
                    .HasMaxLength(200)
                    .HasColumnName("display_title");

                entity.Property(e => e.InvariantTitle)
                    .HasMaxLength(200)
                    .HasColumnName("invariant_title");

                entity.Property(e => e.LearningStandardId)
                    .HasColumnName("learning_standard_id")
                    .HasComment("The learning standard used by this package (SCORM 1.2, AICC, etc). Stored as an integer representing the \r\n\r\nvocabulary.");

                entity.Property(e => e.ObjectivesGlobalToSystem)
                    .HasColumnName("objectives_global_to_system")
                    .HasComment("Determines whether the sequencing objectives defined in this package that have maps should be exposed to the rest of \r\n\r\nthe system or just contained within this package.");

                entity.Property(e => e.SharedDataGlobalToSystem)
                    .IsRequired()
                    .HasColumnName("shared_data_global_to_system")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Specifies whether global objective data for this package should be exposed to the entire system.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.VersionId)
                    .HasColumnName("version_id")
                    .HasComment("An optional incrementing id for retakes of the same package");

                entity.Property(e => e.WebPath)
                    .HasMaxLength(500)
                    .HasColumnName("web_path")
                    .HasComment("The web path to the course");
            });

            modelBuilder.Entity<ScormPackagePropertiesPreset>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.PresetId })
                    .HasName("mPackagePropertiesPresets_pkey");

                entity.HasComment("Persisted collections of ScormPackage properties");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.PresetId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("preset_id")
                    .HasComment("primary key of the preset");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("created_by")
                    .HasComment("the owner of the preset.  could be 'system' or an individual user");

                entity.Property(e => e.PropertyXml)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("property_xml")
                    .HasComment("the full xml of the properties as scorm engine metadata");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title")
                    .HasComment("title given to the package properties preset");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ScormPackageProperty>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormPackageId })
                    .HasName("ScormPackageProperties_pkey");

                entity.HasComment("Contains a variety of settings that determine how the SCP should deliver the package. These values are all \r\n\r\nproprietary to the SCP and are designed to increase compatibility.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormPackageId)
                    .HasColumnName("scorm_package_id")
                    .HasComment("The package these properties apply to.");

                entity.Property(e => e.AllowCompleteStatusChange)
                    .HasColumnName("allow_complete_status_change")
                    .HasComment("Allow a once-complete status to become incomplete again");

                entity.Property(e => e.AlwaysFlowToFirstSco)
                    .HasColumnName("always_flow_to_first_sco")
                    .HasComment("Should the SCP always launch the first SCO when the course  is launched regardless of sequencing rules.");

                entity.Property(e => e.ApplyStatusToSuccess)
                    .HasColumnName("apply_status_to_success")
                    .HasComment("Apply the rudimentary rollup status rules also to the success status");

                entity.Property(e => e.CaptureHistory)
                    .HasColumnName("capture_history")
                    .HasComment("Specifies if the course should return attempt information to the server");

                entity.Property(e => e.CaptureHistoryDetailed)
                    .HasColumnName("capture_history_detailed")
                    .HasComment("Specifies if the course should return detailed attempt information to the server");

                entity.Property(e => e.CommCommitFrequency)
                    .HasColumnName("comm_commit_frequency")
                    .HasComment("How often to send commit runtime data updates (in seconds)");

                entity.Property(e => e.CommMaxFailedSubmissions)
                    .HasColumnName("comm_max_failed_submissions")
                    .HasComment("Maximum number of retries when updating runtime data before declaring failure");

                entity.Property(e => e.ComplStatOfFailedSucStat)
                    .HasColumnName("compl_stat_of_failed_suc_stat")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Specifies the Completion Status value to apply in the case of a Failed Success Status.  Only applied if set to \"completed\" or \"incomplete\"");

                entity.Property(e => e.CourseStructureStartsOpen)
                    .HasColumnName("course_structure_starts_open")
                    .HasComment("If the course structure is displayed, should it default to being open or closed?");

                entity.Property(e => e.CourseStructureWidth)
                    .HasColumnName("course_structure_width")
                    .HasComment("If the course structure is displayed, how many pixels should be allocated for it?");

                entity.Property(e => e.DebugControlAudit)
                    .HasColumnName("debug_control_audit")
                    .HasComment("Record control audit messages in the debug log");

                entity.Property(e => e.DebugControlDetailed)
                    .HasColumnName("debug_control_detailed")
                    .HasComment("Record control details in the debug log");

                entity.Property(e => e.DebugIncludeTimestamps)
                    .HasColumnName("debug_include_timestamps")
                    .HasComment("Record timestamps in the debug log");

                entity.Property(e => e.DebugLookaheadAudit)
                    .HasColumnName("debug_lookahead_audit")
                    .HasComment("Record look-ahead audit messages in the debug log");

                entity.Property(e => e.DebugLookaheadDetailed)
                    .HasColumnName("debug_lookahead_detailed")
                    .HasComment("Record look-ahead details in the debug log");

                entity.Property(e => e.DebugRteAudit)
                    .HasColumnName("debug_rte_audit")
                    .HasComment("Record runtime audit messages in the debug log");

                entity.Property(e => e.DebugRteDetailed)
                    .HasColumnName("debug_rte_detailed")
                    .HasComment("Record runtime details in the debug log");

                entity.Property(e => e.DebugSequencingAudit)
                    .HasColumnName("debug_sequencing_audit")
                    .HasComment("Record sequencing audit messages in the debug log");

                entity.Property(e => e.DebugSequencingDetailed)
                    .HasColumnName("debug_sequencing_detailed")
                    .HasComment("Record sequencing details in the debug log");

                entity.Property(e => e.DebugSequencingSimple)
                    .HasColumnName("debug_sequencing_simple")
                    .HasComment("Capture sequencing debugging information in a more human readable fashion than the very technical Audit or Detailed level");

                entity.Property(e => e.DesiredFullscreen)
                    .HasColumnName("desired_fullscreen")
                    .HasComment("If true, indicates that the SCO would like to be launched as a full screen window.");

                entity.Property(e => e.DesiredHeight)
                    .HasColumnName("desired_height")
                    .HasComment("If greater than 0, the number of vertical pixels the SCOs in this package would like to have available during \r\n\r\ndelivery.");

                entity.Property(e => e.DesiredWidth)
                    .HasColumnName("desired_width")
                    .HasComment("If greater than 0, the number of horizontal pixels the SCOs in this package would like to have available during \r\n\r\ndelivery.");

                entity.Property(e => e.EnableChoiceNav)
                    .HasColumnName("enable_choice_nav")
                    .HasComment("Should the SCP allow the learner to navigate by selecting an item from the table of contents?");

                entity.Property(e => e.EnableFlowNav)
                    .HasColumnName("enable_flow_nav")
                    .HasComment("Should the SCP allow the user to navigate using previous and next commands?");

                entity.Property(e => e.FinalNotSatLogoutAction)
                    .HasColumnName("final_not_sat_logout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits \r\n\r\nwith an exit type of logout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalNotSatNormalAction)
                    .HasColumnName("final_not_sat_normal_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits \r\n\r\nwith an exit type of normal. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalNotSatSuspendAction)
                    .HasColumnName("final_not_sat_suspend_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits \r\n\r\nwith an exit type of suspend. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalNotSatTimeoutAction)
                    .HasColumnName("final_not_sat_timeout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has not been satisfied exits \r\n\r\nwith an exit type of timeout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalSatLogoutAction)
                    .HasColumnName("final_sat_logout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits \r\n\r\nwith an exit type of logout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalSatNormalAction)
                    .HasColumnName("final_sat_normal_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits \r\n\r\nwith an exit type of normal. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalSatSuspendAction)
                    .HasColumnName("final_sat_suspend_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits \r\n\r\nwith an exit type of suspend. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinalSatTimeoutAction)
                    .HasColumnName("final_sat_timeout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO at the end of the course that has been satisfied exits \r\n\r\nwith an exit type of timeout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.FinishCausesImmediateCommit)
                    .HasColumnName("finish_causes_immediate_commit")
                    .HasComment("Will force the SCP to immediately persist data to the server when the SCO calls Finish/Terminate rather than waiting \r\n\r\nfor the next periodic commit. Useful for SCOs that only save data on unload and require the player to be launched in a new window.");

                entity.Property(e => e.FirstScoIsPretest)
                    .HasColumnName("first_sco_is_pretest")
                    .HasComment("This parameter indicates that if the first SCO achieves a lesson status of passed, then the rest of the SCOs in the course will be marked complete.");

                entity.Property(e => e.ForceDisableRootChoice)
                    .HasColumnName("force_disable_root_choice")
                    .HasComment("Disables the Root menu item Choice option.  This is to prevent new attempts being initiated on the course.");

                entity.Property(e => e.ForceObjComplSetByContent)
                    .HasColumnName("force_obj_compl_set_by_content")
                    .HasComment("Override the manifest settings for \"Objective Set By Content\" and \"Completion Set By Content\" with true values.");

                entity.Property(e => e.IeCompatibilityMode)
                    .HasColumnName("ie_compatibility_mode")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IntNotSatLogoutAction)
                    .HasColumnName("int_not_sat_logout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course \r\n\r\nexits with an exit type of logout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntNotSatNormalAction)
                    .HasColumnName("int_not_sat_normal_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO in the middle of a course that has not been satisfied \r\n\r\nexits with an exit type of normal. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntNotSatSuspendAction)
                    .HasColumnName("int_not_sat_suspend_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course \r\n\r\nexits with an exit type of suspend. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntNotSatTimeoutAction)
                    .HasColumnName("int_not_sat_timeout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has not been satisfied in the middle of a course  \r\n\r\nexits with an exit type of timeout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntSatLogoutAction)
                    .HasColumnName("int_sat_logout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits \r\n\r\nwith an exit type of logout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntSatNormalAction)
                    .HasColumnName("int_sat_normal_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits \r\n\r\nwith an exit type of normal. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntSatSuspendAction)
                    .HasColumnName("int_sat_suspend_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits \r\n\r\nwith an exit type of suspend. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.IntSatTimeoutAction)
                    .HasColumnName("int_sat_timeout_action")
                    .HasComment("The navigation behavior the SCP should display when a SCO that has been satisfied in the middle of a course exits \r\n\r\nwith an exit type of timeout. Stored as an integer representing the vocabulary.");

                entity.Property(e => e.InvalidMenuItemAction)
                    .HasColumnName("invalid_menu_item_action")
                    .HasDefaultValueSql("((2))")
                    .HasComment("Defines how to handle menu item links that won't succeed (show, hide or disable).  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.InvokeRollupAtSuspendall)
                    .HasColumnName("invoke_rollup_at_suspendall")
                    .HasComment("Initiates rollup when SuspendAll is invoked.");

                entity.Property(e => e.IsAvailableOffline).HasColumnName("is_available_offline");

                entity.Property(e => e.LaunchComplRegsAsNoCredit)
                    .HasColumnName("launch_compl_regs_as_no_credit")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Once complete, relaunched registrations launch as no credit");

                entity.Property(e => e.LogoutCausesPlayerExit)
                    .HasColumnName("logout_causes_player_exit")
                    .HasComment("Should the SCP allow a cmi.exit request of logout to exit the entire player");

                entity.Property(e => e.LookaheadSequencerMode)
                    .HasColumnName("lookahead_sequencer_mode")
                    .HasDefaultValueSql("((2))")
                    .HasComment("Determines if or how lookahead sequencer is enabled client-side.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.NumberOfScoringObjects)
                    .HasColumnName("number_of_scoring_objects")
                    .HasComment("If the Score Rollup Mode is Fixed Average, this parameter indicates how many SCOs should be reporting a score.");

                entity.Property(e => e.OfflineSynchMode)
                    .HasColumnName("offline_synch_mode")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Defines the method of synchronization when merging offline data with lms data.  Applicable only when RSOP (Rustici Software Offline Player) is enabled.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.PlayerLaunchType)
                    .HasColumnName("player_launch_type")
                    .HasComment("How should the SCP player itself be launched (frameset or popup window)? Stored as an integer representing the \r\n\r\nvocabulary.");

                entity.Property(e => e.PreventRightClick)
                    .HasColumnName("prevent_right_click")
                    .HasComment("Should the SCP prevent users from right clicking in its navigation frames?");

                entity.Property(e => e.PreventWindowResize)
                    .HasColumnName("prevent_window_resize")
                    .HasComment("Should the SCP prevent users from resizing its window?");

                entity.Property(e => e.RequiredFullscreen)
                    .HasColumnName("required_fullscreen")
                    .HasComment("If true, then the SCOs in this package require that they occupy the entire screen during delivery.");

                entity.Property(e => e.RequiredHeight)
                    .HasColumnName("required_height")
                    .HasComment("If greater than 0, the then SCOs in this package require this many pixels of screen width to play correctly. If this \r\n\r\nwidth is not available, the SCO may not be delivered correctly.");

                entity.Property(e => e.RequiredWidth)
                    .HasColumnName("required_width")
                    .HasComment("If greater than 0, the then SCOs in this package require this many pixels of screen height to play correctly. If \r\n\r\nthis height is not available, the SCO may not be delivered correctly.");

                entity.Property(e => e.ResetRtTiming)
                    .HasColumnName("reset_rt_timing")
                    .HasComment("Should the SCP always persist runtime data when the exit type is suspend, or should this be left up to the sequencer?  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ReturnToLmsAction)
                    .HasColumnName("return_to_lms_action")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Defines what happens when a user clicks \"Return To Lms\"");

                entity.Property(e => e.RollupEmptySetToUnknown).HasColumnName("rollup_empty_set_to_unknown");

                entity.Property(e => e.RollupRuntimeAtScoUnload)
                    .HasColumnName("rollup_runtime_at_sco_unload")
                    .HasComment("Initiates rollup and transfer of runtime data at ScoUnload for all SCOs.");

                entity.Property(e => e.SatisfiedCausesCompletion)
                    .HasColumnName("satisfied_causes_completion")
                    .HasComment("Determines whether or not activity Satisfaction will set completion");

                entity.Property(e => e.ScaleRawScore)
                    .HasColumnName("scale_raw_score")
                    .HasComment("For SCORM 2004, if SCO sets a raw score but not a scaled score, determines if the raw score should count as the normative score for the SCO");

                entity.Property(e => e.ScoLaunchType)
                    .HasColumnName("sco_launch_type")
                    .HasComment("How should the SCP launch individual SCOs (frameset or popup window)? Stored as an integer representing the \r\n\r\nvocabulary.\r\n");

                entity.Property(e => e.ScoreOverridesStatus)
                    .HasColumnName("score_overrides_status")
                    .HasComment("Should the score override the status?");

                entity.Property(e => e.ScoreRollupMode)
                    .HasColumnName("score_rollup_mode")
                    .HasComment("Determines how scores are rolled up to the course level.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.ShowCloseItem)
                    .HasColumnName("show_close_item")
                    .HasComment("Should the SCP show the Close Item button?");

                entity.Property(e => e.ShowCourseStructure)
                    .HasColumnName("show_course_structure")
                    .HasComment("Should the SCP show the course tree structure during delivery?");

                entity.Property(e => e.ShowFinishButton)
                    .HasColumnName("show_finish_button")
                    .HasComment("Should the SCP show the Return to LMS button?");

                entity.Property(e => e.ShowHelp)
                    .HasColumnName("show_help")
                    .HasComment("Should the SCP show the help button during delivery?");

                entity.Property(e => e.ShowNavBar)
                    .HasColumnName("show_nav_bar")
                    .HasComment("Should the SCP show the navigation bar containing the navigation buttons?");

                entity.Property(e => e.ShowProgressBar)
                    .HasColumnName("show_progress_bar")
                    .HasComment("Should the SCP show the progress bar during course delivery?");

                entity.Property(e => e.ShowTitlebar)
                    .HasColumnName("show_titlebar")
                    .HasComment("Should the SCP show the title bar?");

                entity.Property(e => e.StatusDisplay)
                    .HasColumnName("status_display")
                    .HasComment("How the SCP should graphically represent the current status of individual scorm objects during delivery. Stored as \r\n\r\nan integer representing the vocabulary.");

                entity.Property(e => e.StatusRollupMode)
                    .HasColumnName("status_rollup_mode")
                    .HasComment("Determines how completion status is rolled up to the course level.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.StudentPrefsGlobalToCourse)
                    .HasColumnName("student_prefs_global_to_course")
                    .HasComment("When a student makes a preference (audio volume, etc) the preference should be applied to all SCOs");

                entity.Property(e => e.SuspendDataMaxLength)
                    .HasColumnName("suspend_data_max_length")
                    .HasDefaultValueSql("((64000))")
                    .HasComment("Maximum length of suspend data to save");

                entity.Property(e => e.ThresholdScoreForCompletion)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("threshold_score_for_completion")
                    .HasComment("If the Status Rollup Mode is Complete When Threshold Score is Met, this parameter indicates what the threshold score for completion is. This value is a decimal between 0-1.");

                entity.Property(e => e.TimeLimit)
                    .HasColumnName("time_limit")
                    .HasComment("Maximum length of time a user can spend taking a course");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.UseMeasureProgressBar)
                    .HasColumnName("use_measure_progress_bar")
                    .HasComment("Specifies whether the interface should determine progress bar progress using Measure Rollup or individual SCO completion.");

                entity.Property(e => e.UseQuickLookaheadSeq)
                    .IsRequired()
                    .HasColumnName("use_quick_lookahead_seq")
                    .HasDefaultValueSql("((1))")
                    .HasComment("In SCORM 2004 4th Edition and later, determines whether or not to use the Quick Lookahead Sequencer");

                entity.Property(e => e.ValidateInteractionResponses)
                    .IsRequired()
                    .HasColumnName("validate_interaction_responses")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Determines whether the format of SCORM responses are validated client-side");

                entity.Property(e => e.WrapScoWindowWithApi)
                    .HasColumnName("wrap_sco_window_with_api")
                    .HasComment("Will put an API relay object in a frameset around a SCO that is launched in a new window. This is useful for SCOs \r\n\r\nthat incorrectly use the ADL API Finder algorithm from spawned windows.");

                entity.HasOne(d => d.ScormPackage)
                    .WithOne(p => p.ScormPackageProperty)
                    .HasForeignKey<ScormPackageProperty>(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormPackageProperties_S_1");
            });

            modelBuilder.Entity<ScormRegistration>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId })
                    .HasName("ScormRegistration_pkey");

                entity.ToTable("ScormRegistration");

                entity.HasComment("Represents a ScormRegistration. This table is in the integration layer and will change for each implementation. It will \r\n\r\noften have a 1-to-1 correspondence with a table in the external LMS that stores the concept or assignment, enrollment, attempt, etc.");

                entity.HasIndex(e => new { e.EngineTenantId, e.ClId, e.EId, e.InstanceId }, "ExternalRegistrationId")
                    .IsUnique();

                entity.HasIndex(e => new { e.EngineTenantId, e.GlobalObjectiveScope }, "IX_SR_gos");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormPackageId, e.UpdateDtUtc, e.ScormRegistrationId }, "IX_SR_package_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.SuspendedActivityId }, "IX_SR_suspended_activity_id");

                entity.HasIndex(e => new { e.EngineTenantId, e.UpdateDtUtc, e.ScormRegistrationId }, "IX_SR_update_dt_utc");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Identifier for this registration");

                entity.Property(e => e.ClId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cl_id");

                entity.Property(e => e.CompletedDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("completed_dt_utc");

                entity.Property(e => e.ConvertedToTincan).HasColumnName("converted_to_tincan");

                entity.Property(e => e.CreateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("create_dt_utc");

                entity.Property(e => e.CreatedForCredit).HasColumnName("created_for_credit");

                entity.Property(e => e.EId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("e_id");

                entity.Property(e => e.FirstaccessDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("firstaccess_dt_utc");

                entity.Property(e => e.GlobalObjectiveScope)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("global_objective_scope")
                    .HasComment("String representation of the external LMS's concept of a user. Needed for determining the scope of global \r\n\r\nobjectives. If more granular objective scoping is desired, this field can be any string that represents the scope.");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instance_id")
                    .HasComment("An optional incrementing id for retakes of the same package");

                entity.Property(e => e.RuntimeData).HasColumnName("runtime_data");

                entity.Property(e => e.ScormPackageId)
                    .HasColumnName("scorm_package_id")
                    .HasComment("The package to be delivered in this registration");

                entity.Property(e => e.SuspendedActivityId)
                    .HasColumnName("suspended_activity_id")
                    .HasComment("The activity the user last attempted in this registration (if it is paused).");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.Property(e => e.UpdateDtUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt_utc");

                entity.Property(e => e.UpdateSequence).HasColumnName("update_sequence");

                entity.HasOne(d => d.ScormPackage)
                    .WithMany(p => p.ScormRegistrations)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistration_ScormP_1");
            });

            modelBuilder.Entity<ScormRegistrationGlobalObj>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormRegistrationObjId })
                    .HasName("cormRegistrationGlobalObj_pkey");

                entity.ToTable("ScormRegistrationGlobalObj");

                entity.HasComment("Global objectives relevant to this registration. Any objective in the same scope that is read by this \r\n\r\nregistration's activities is included in this table. Note, there may be multiple instances of a single global obj to serve each reg that accesses it.");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Registration that this objective is applicable to.");

                entity.Property(e => e.ScormRegistrationObjId)
                    .HasColumnName("scorm_registration_obj_id")
                    .HasComment("Index of the objective within the registration.");

                entity.Property(e => e.CompletionStatus)
                    .HasColumnName("completion_status")
                    .HasComment("Represents the current completion status of this objective (known vs unknown)");

                entity.Property(e => e.CompletionStatusValue)
                    .HasColumnName("completion_status_value")
                    .HasComment("Represents the current completion status of this objective (completed vs incomplete)");

                entity.Property(e => e.ObjectiveIdentifier)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("objective_identifier")
                    .HasComment("Unique identifier of the global objective.");

                entity.Property(e => e.ObjectiveMeasureStatus)
                    .HasColumnName("objective_measure_status")
                    .HasComment("Is the normalized measure for this objective known?");

                entity.Property(e => e.ObjectiveNormalizedMeasure)
                    .HasColumnType("decimal(5, 4)")
                    .HasColumnName("objective_normalized_measure")
                    .HasComment("The score (normalized between -1.0 and 1.0) the learner achieved on this objective.");

                entity.Property(e => e.ObjectiveProgressStatus)
                    .HasColumnName("objective_progress_status")
                    .HasComment("Is the satisfied status for this objective known?");

                entity.Property(e => e.ObjectiveSatisfiedStatus)
                    .HasColumnName("objective_satisfied_status")
                    .HasComment("Has the learner satisfied this objective?");

                entity.Property(e => e.ProgressMeasure)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("progress_measure")
                    .HasComment("The amount of progress the learner has made towards completing the objective measured as a double from 0-1.");

                entity.Property(e => e.ProgressMeasureStatus)
                    .HasColumnName("progress_measure_status")
                    .HasComment("Represents whether the Progress Measure is known or unknown");

                entity.Property(e => e.ScoreMax)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_max")
                    .HasComment("The maximum score the learner could have achieved on this objective.");

                entity.Property(e => e.ScoreMin)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_min")
                    .HasComment("The minimum score the learner could have achieved on this objective.");

                entity.Property(e => e.ScoreRaw)
                    .HasColumnType("decimal(38, 7)")
                    .HasColumnName("score_raw")
                    .HasComment("The raw score (not normalized to any range) that the learner achieved on this objective.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationGlobalObjs)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationGlobalO_1");
            });

            modelBuilder.Entity<ScormRegistrationSharedDataVal>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormSharedDataValueId })
                    .HasName("RegistrationSharedDataVal_pkey");

                entity.ToTable("ScormRegistrationSharedDataVal");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "IX_SRSV_registration_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormSharedDataValueId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_shared_data_value_id")
                    .HasComment("Primary key of the actual data value row");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasComment("The actual data of the shared data item");

                entity.Property(e => e.GlobalObjectiveScope)
                    .HasMaxLength(100)
                    .HasColumnName("global_objective_scope")
                    .HasComment("If globally scoped, this is the unique global scope identifier.  Otherwise this value is null");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("If globally scoped, this value is null. Otherwise it is the SCORM Registration value associated with this data itemf");

                entity.Property(e => e.SharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("shared_data_id")
                    .HasComment("Unique identifier of the share data target");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSharedDataVals)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .HasConstraintName("FK_ScormRegistrationSDVal");
            });

            modelBuilder.Entity<ScormRegistrationSharedDatum>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.ScormRegistrationDataId })
                    .HasName("ormRegistrationSharedData_pkey");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormSharedDataValueId }, "IX_SRS_data_value_id");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Registration that this shared data item is applicable to.");

                entity.Property(e => e.ScormRegistrationDataId)
                    .HasColumnName("scorm_registration_data_id")
                    .HasComment("Index of the shared data within the registration.");

                entity.Property(e => e.ScormSharedDataValueId)
                    .HasColumnName("scorm_shared_data_value_id")
                    .HasComment("Primary key of the actual data value row");

                entity.Property(e => e.SharedDataId)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("shared_data_id")
                    .HasComment("Unique identifier of the shared data target.");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSharedData)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSharedD_1");

                entity.HasOne(d => d.ScormRegistrationSharedDataVal)
                    .WithMany(p => p.ScormRegistrationSharedData)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormSharedDataValueId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSharedD_2");
            });

            modelBuilder.Entity<ScormRegistrationSspbucket>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId, e.BucketIndex })
                    .HasName("cormRegistrationSSPBucket_pkey");

                entity.ToTable("ScormRegistrationSSPBucket");

                entity.HasComment("Sharable State Persistence Bucket for a SCORM Registration");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId)
                    .HasColumnName("scorm_registration_id")
                    .HasComment("Scorm registration that has this runtime bucket assocaiated with it");

                entity.Property(e => e.BucketIndex)
                    .HasColumnName("bucket_index")
                    .HasComment("Incrementing integer representing this bucket's index in the collection");

                entity.Property(e => e.AllocationSuccess)
                    .HasColumnName("allocation_success")
                    .HasComment("Did the LMS choose to allocate the requested amount of storage, the minimum amount of storage or no storage.  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.BucketIdentifier)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_identifier")
                    .HasComment("Unique URI identifying bucket");

                entity.Property(e => e.BucketType)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("bucket_type")
                    .HasComment("Optional string that the bucket can specifiy to categorize what type of data it holds");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasComment("The actual data stored in the bucket");

                entity.Property(e => e.LocalActivityId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("local_activity_id")
                    .HasComment("If the bucket is scoped just to the session, that means it is local to a SCO. This field is the item identifier of that SCO.");

                entity.Property(e => e.Persistence)
                    .HasColumnName("persistence")
                    .HasComment("Duration and scope in which this bucket is persisted and visible (course, learner or session).  Stored as an integer representing the vocabulary.");

                entity.Property(e => e.Reducible)
                    .HasColumnName("reducible")
                    .HasComment("Will the bucket accept anything less than the requested number of bytes");

                entity.Property(e => e.SizeMin)
                    .HasColumnName("size_min")
                    .HasComment("Minimum number of bytes this bucket requested be allocated for storage");

                entity.Property(e => e.SizeRequested)
                    .HasColumnName("size_requested")
                    .HasComment("Maximum number of bytes this bucket requested be allocated for storage");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())")
                    .HasComment("The user or process that last updated this record");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("The time this record was last updated");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationSspbuckets)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScormRegistrationSSPBuck_1");
            });

            modelBuilder.Entity<ScormRegistrationStatementMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.MapId })
                    .HasName("mRegistrationStatementMap_pkey");

                entity.ToTable("ScormRegistrationStatementMap");

                entity.HasIndex(e => new { e.EngineTenantId, e.ScormRegistrationId }, "UIX_Registration_Stmt_Map_Reg");

                entity.HasIndex(e => new { e.EngineTenantId, e.StatementId }, "UIX_Registration_Stmt_Map_Stmt")
                    .IsUnique();

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.MapId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("map_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.StatementId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithMany(p => p.ScormRegistrationStatementMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xapi_Statement_Map_Reg");

                entity.HasOne(d => d.TinCanStatementIndex)
                    .WithOne(p => p.ScormRegistrationStatementMap)
                    .HasForeignKey<ScormRegistrationStatementMap>(d => new { d.EngineTenantId, d.StatementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Xapi_Statement_Map_Stmt");
            });

            modelBuilder.Entity<ServerPath>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ServerPath1).HasColumnName("ServerPath");

                entity.Property(e => e.ShortName).HasMaxLength(10);
            });

            modelBuilder.Entity<SysTblCopyTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sysTblCopyTables");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SysTblDbscriptsRun>(entity =>
            {
                entity.HasKey(e => e.Scrid)
                    .HasName("PK__sysTblDB__C6DD0E78101B19B5");

                entity.ToTable("sysTblDBScriptsRun");

                entity.Property(e => e.Scrid).HasColumnName("SCRID");

                entity.Property(e => e.DateRun).HasColumnType("datetime");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RunBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScriptVersion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysTblDbversionsAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sysTblDBVersionsAll");

                entity.Property(e => e.Dbversion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DBVersion");
            });

            modelBuilder.Entity<SysTblDbversionsLoaded>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sysTblDBVersionsLoaded");

                entity.Property(e => e.Dbversion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("DBVersion");
            });

            modelBuilder.Entity<SysTblMainMenu>(entity =>
            {
                entity.HasKey(e => e.FormName)
                    .HasName("aaaaasysTblMainMenu_PK")
                    .IsClustered(false);

                entity.ToTable("sysTblMainMenu");

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.Property(e => e.FormDescription).HasMaxLength(50);

                entity.Property(e => e.Section).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SysTblMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("sysTblMenu");

                entity.Property(e => e.FormDescription)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FormName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Section).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SysTblReport>(entity =>
            {
                entity.HasKey(e => e.ReportName)
                    .HasName("aaaaasysTblReports_PK")
                    .IsClustered(false);

                entity.ToTable("sysTblReports");

                entity.Property(e => e.ReportName).HasMaxLength(50);

                entity.Property(e => e.ReportDescription).HasMaxLength(100);

                entity.Property(e => e.Section).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<SysTblSetting>(entity =>
            {
                entity.HasKey(e => e.ItemName)
                    .HasName("aaaaasysTblSettings_PK")
                    .IsClustered(false);

                entity.ToTable("sysTblSettings");

                entity.Property(e => e.ItemName).HasMaxLength(50);

                entity.Property(e => e.ItemValue)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SysTblSubMenu>(entity =>
            {
                entity.HasKey(e => e.FormName)
                    .HasName("aaaaasysTblSubMenu_PK")
                    .IsClustered(false);

                entity.ToTable("sysTblSubMenu");

                entity.Property(e => e.FormName).HasMaxLength(50);

                entity.Property(e => e.FormDescription).HasMaxLength(50);

                entity.Property(e => e.Section).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblActionTaken>(entity =>
            {
                entity.HasKey(e => e.Atid);

                entity.ToTable("tblActionTaken");

                entity.Property(e => e.Atid).HasColumnName("ATID");

                entity.Property(e => e.Atdate)
                    .HasMaxLength(50)
                    .HasColumnName("ATDate");

                entity.Property(e => e.Atdesc)
                    .HasMaxLength(255)
                    .HasColumnName("ATDesc");

                entity.Property(e => e.Atdetails)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("ATDetails");

                entity.Property(e => e.AtparentId).HasColumnName("ATParentID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblAddlCertsInfo>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaatblAddlCertsInfo_PK")
                    .IsClustered(false);

                entity.ToTable("tblAddlCertsInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddlCertExpDate).HasColumnType("datetime");

                entity.Property(e => e.AddlCertIssueDate).HasColumnType("datetime");

                entity.Property(e => e.AddlCertNum).HasMaxLength(50);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TrainingTypeId).HasColumnName("TrainingTypeID");
            });

            modelBuilder.Entity<TblAlert>(entity =>
            {
                entity.ToTable("tblAlerts");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DismissedOn).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblAlerts)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblAlerts__EID__04D07943");
            });

            modelBuilder.Entity<TblAuditMenuLevel>(entity =>
            {
                entity.HasKey(e => e.Amlid);

                entity.ToTable("tblAuditMenuLevels");

                entity.Property(e => e.Amlid).HasColumnName("AMLID");

                entity.Property(e => e.Amid).HasColumnName("AMID");

                entity.Property(e => e.Amldesc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AMLDesc");
            });

            modelBuilder.Entity<TblAuditReportsInsert>(entity =>
            {
                entity.HasKey(e => e.Arid);

                entity.ToTable("tblAuditReportsInsert");

                entity.Property(e => e.Arid).HasColumnName("ARID");

                entity.Property(e => e.CriteriaForm)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.ReportTitle)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCarryOverHour>(entity =>
            {
                entity.HasKey(e => new { e.Eid, e.NerccertIssueDate })
                    .HasName("aaaaatblCarryOverHours_PK")
                    .IsClustered(false);

                entity.ToTable("tblCarryOverHours");

                entity.HasIndex(e => e.Eid, "EID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.Co4)
                    .HasColumnName("CO_4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Co5)
                    .HasColumnName("CO_5")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CoComments)
                    .HasMaxLength(255)
                    .HasColumnName("CO_Comments");

                entity.Property(e => e.CoOpTopics)
                    .HasColumnName("CO_OpTopics")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CoSimulation)
                    .HasColumnName("CO_Simulation")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CoStandards)
                    .HasColumnName("CO_Standards")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CoTotalCeh)
                    .HasColumnName("CO_TotalCEH")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dru)
                    .HasColumnType("datetime")
                    .HasColumnName("DRU")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ruby)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RUBy")
                    .HasDefaultValueSql("('System')");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.Cid);

                entity.ToTable("tblCategories");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cdesc)
                    .HasMaxLength(255)
                    .HasColumnName("CDesc");

                entity.Property(e => e.Cnum)
                    .HasColumnName("CNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CsubNum)
                    .HasColumnName("CSubNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblCertificationHistory>(entity =>
            {
                entity.HasKey(e => e.Chid)
                    .HasName("aaaaatblCertificationHistory_PK")
                    .IsClustered(false);

                entity.ToTable("tblCertificationHistory");

                entity.HasIndex(e => e.Chid, "CHID");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.ChNerccertNum, "NERCCertNum");

                entity.HasIndex(e => e.Eid, "tblEmployeetblCertificationHistory");

                entity.Property(e => e.Chid).HasColumnName("CHID");

                entity.Property(e => e.ChDra)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_DRA")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))");

                entity.Property(e => e.ChIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_IssueDate");

                entity.Property(e => e.ChNerccertArea).HasColumnName("CH_NERCCertArea");

                entity.Property(e => e.ChNerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_NERCCertExpDate");

                entity.Property(e => e.ChNerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_NERCCertIssueDate");

                entity.Property(e => e.ChNerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("CH_NERCCertNum");

                entity.Property(e => e.ChRaby)
                    .HasMaxLength(50)
                    .HasColumnName("CH_RABy");

                entity.Property(e => e.Eid)
                    .HasColumnName("EID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblCertificationHistories)
                    .HasForeignKey(d => d.Eid)
                    .HasConstraintName("tblCertificationHistory_FK00");
            });

            modelBuilder.Entity<TblClass>(entity =>
            {
                entity.HasKey(e => e.Clid);

                entity.ToTable("tblClasses");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clnote1)
                    .HasMaxLength(50)
                    .HasColumnName("CLNote1");

                entity.Property(e => e.Clnote2)
                    .HasMaxLength(50)
                    .HasColumnName("CLNote2");

                entity.Property(e => e.ClstartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLStartDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.EndAmPm).HasDefaultValueSql("((1))");

                entity.Property(e => e.EndTimeStr)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Lcid).HasColumnName("LCID");

                entity.Property(e => e.ProctorId).HasColumnName("ProctorID");

                entity.Property(e => e.SelfReg).HasDefaultValueSql("((0))");

                entity.Property(e => e.SelfRegEndDate).HasColumnType("date");

                entity.Property(e => e.SelfRegOpen).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartAmPm).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTimeStr)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TotalSeats).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblClasses)
                    .HasForeignKey(d => d.Corid)
                    .HasConstraintName("FK_tblClasses_tblCourses");

                entity.HasOne(d => d.In)
                    .WithMany(p => p.TblClasses)
                    .HasForeignKey(d => d.Inid)
                    .HasConstraintName("FK_tblClasses_lktblInstructors");

                entity.HasOne(d => d.Lc)
                    .WithMany(p => p.TblClasses)
                    .HasForeignKey(d => d.Lcid)
                    .HasConstraintName("FK_tblClasses_lktblLocations");
            });

            modelBuilder.Entity<TblClassNotificationEmployee>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PK_tblClassNotificationEmployees_1");

                entity.ToTable("tblClassNotificationEmployees");

                entity.Property(e => e.Eid)
                    .ValueGeneratedNever()
                    .HasColumnName("EID");

                entity.HasOne(d => d.EidNavigation)
                    .WithOne(p => p.TblClassNotificationEmployee)
                    .HasForeignKey<TblClassNotificationEmployee>(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblClassNotificationEmployees_tblEmployee");
            });

            modelBuilder.Entity<TblClassNotificationHistory>(entity =>
            {
                entity.HasKey(e => e.Cnhid);

                entity.ToTable("tblClassNotificationHistory");

                entity.Property(e => e.Cnhid).HasColumnName("CNHID");

                entity.Property(e => e.Cndate)
                    .HasColumnType("datetime")
                    .HasColumnName("CNDate");

                entity.Property(e => e.Cnsender)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CNSender");
            });

            modelBuilder.Entity<TblClassNotificationSetting>(entity =>
            {
                entity.HasKey(e => e.Cnid);

                entity.ToTable("tblClassNotificationSettings");

                entity.Property(e => e.Cnid).HasColumnName("CNID");

                entity.Property(e => e.MsgText)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Class schedule is attached')");

                entity.Property(e => e.Ndow).HasColumnName("NDOW");

                entity.Property(e => e.Nenable).HasColumnName("NEnable");

                entity.Property(e => e.Nend)
                    .HasColumnType("datetime")
                    .HasColumnName("NEnd");

                entity.Property(e => e.Ninterval).HasColumnName("NInterval");

                entity.Property(e => e.Nrange)
                    .HasColumnName("NRange")
                    .HasDefaultValueSql("((60))");

                entity.Property(e => e.Nstart)
                    .HasColumnType("datetime")
                    .HasColumnName("NStart");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblClassTest>(entity =>
            {
                entity.HasKey(e => new { e.Clid, e.TestId, e.Sequence });

                entity.ToTable("tblClass_Tests");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Cl)
                    .WithMany(p => p.TblClassTests)
                    .HasForeignKey(d => d.Clid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblClass_Tests_tblClasses");
            });

            modelBuilder.Entity<TblClassTestOverride>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblClass_TestOverrides");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.EmpsettingId).HasColumnName("EMPSettingID");

                entity.Property(e => e.Units)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblClassesDropReg>(entity =>
            {
                entity.ToTable("tblClassesDropReg");

                entity.HasIndex(e => new { e.Eid, e.Clid, e.Corid }, "uc_DropReg")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.ConfirmDate).HasColumnType("date");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.RegDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblClassesSelfReg>(entity =>
            {
                entity.HasKey(e => e.Srid);

                entity.ToTable("tblClassesSelfReg");

                entity.Property(e => e.Srid).HasColumnName("SRID");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.ConfirmDate).HasColumnType("date");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.IsRemoved).HasDefaultValueSql("((0))");

                entity.Property(e => e.RegDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblCoSk>(entity =>
            {
                entity.HasKey(e => e.Coskid);

                entity.ToTable("tblCO_SK");

                entity.HasIndex(e => new { e.Coid, e.Skid }, "IX_tblCO_SK")
                    .IsUnique();

                entity.Property(e => e.Coskid).HasColumnName("COSKID");

                entity.Property(e => e.Coid).HasColumnName("COID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.TblCoSks)
                    .HasForeignKey(d => d.Coid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCO_SK_tblContentObjects");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblCoSks)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCO_SK_tblSkillsKnowledge");
            });

            modelBuilder.Entity<TblContentObject>(entity =>
            {
                entity.HasKey(e => e.Coid);

                entity.ToTable("tblContentObjects");

                entity.Property(e => e.Coid).HasColumnName("COID");

                entity.Property(e => e.CodateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("CODateStamp")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Codesc)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("CODesc");

                entity.Property(e => e.Coname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COName");

                entity.Property(e => e.Conum).HasColumnName("CONum");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ShelfId).HasColumnName("ShelfID");

                entity.Property(e => e.SourceFile)
                    .IsRequired()
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Shelf)
                    .WithMany(p => p.TblContentObjects)
                    .HasForeignKey(d => d.ShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblContentObjects_tblCOShelf");
            });

            modelBuilder.Entity<TblContentObjectPresentation>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("tblContentObjectPresentations");

                entity.HasIndex(e => e.Title, "IX_tblContentObjectPresentations")
                    .IsUnique();

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblContentObjectPresentations)
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblContentObjectPresentations_tblCourses");
            });

            modelBuilder.Entity<TblCopresentationCo>(entity =>
            {
                entity.HasKey(e => new { e.Pid, e.Coid });

                entity.ToTable("tblCOPresentation_CO");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Coid).HasColumnName("COID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.TblCopresentationCos)
                    .HasForeignKey(d => d.Coid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCOPresentation_CO_tblContentObjects");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.TblCopresentationCos)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCOPresentation_CO_tblContentObjectPresentations");
            });

            modelBuilder.Entity<TblCorack>(entity =>
            {
                entity.HasKey(e => e.RackId);

                entity.ToTable("tblCORack");

                entity.HasIndex(e => new { e.RoomId, e.RackNumber }, "IX_tblCORack")
                    .IsUnique();

                entity.Property(e => e.RackId).HasColumnName("RackID");

                entity.Property(e => e.RackDesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TblCoracks)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCORack_tblCORoom");
            });

            modelBuilder.Entity<TblCoroom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("tblCORoom");

                entity.HasIndex(e => new { e.RoomLtr, e.RoomNumber }, "IX_tblCORoom")
                    .IsUnique();

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomDesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RoomLtr).HasMaxLength(5);
            });

            modelBuilder.Entity<TblCoshelf>(entity =>
            {
                entity.HasKey(e => e.ShelfId);

                entity.ToTable("tblCOShelf");

                entity.HasIndex(e => new { e.RackId, e.ShelfNumber }, "IX_tblCOShelf")
                    .IsUnique();

                entity.Property(e => e.ShelfId).HasColumnName("ShelfID");

                entity.Property(e => e.RackId).HasColumnName("RackID");

                entity.Property(e => e.ShelfDesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rack)
                    .WithMany(p => p.TblCoshelves)
                    .HasForeignKey(d => d.RackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblCOShelf_tblCORack");
            });

            modelBuilder.Entity<TblCourse>(entity =>
            {
                entity.HasKey(e => e.Corid);

                entity.ToTable("tblCourses");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.ActEoptotal).HasColumnName("Act_EOPTotal");

                entity.Property(e => e.ActMayBeUsedAsPdh).HasColumnName("Act_MayBeUsedAsPDH");

                entity.Property(e => e.ActMayBeUsedByCso).HasColumnName("Act_MayBeUsedByCSO");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.ActSimulationTotal).HasColumnName("Act_SimulationTotal");

                entity.Property(e => e.AssessmentMethodId).HasColumnName("AssessmentMethodID");

                entity.Property(e => e.CatNercstandards).HasColumnName("Cat_NERCStandards");

                entity.Property(e => e.CatOperatingTopics).HasColumnName("Cat_OperatingTopics");

                entity.Property(e => e.CatProfRelated).HasColumnName("Cat_ProfRelated");

                entity.Property(e => e.Ceh10).HasColumnName("CEH_10");

                entity.Property(e => e.Ceh11).HasColumnName("CEH_11");

                entity.Property(e => e.Ceh12).HasColumnName("CEH_12");

                entity.Property(e => e.Ceh13).HasColumnName("CEH_13");

                entity.Property(e => e.Ceh14).HasColumnName("CEH_14");

                entity.Property(e => e.Ceh15).HasColumnName("CEH_15");

                entity.Property(e => e.Ceh16).HasColumnName("CEH_16");

                entity.Property(e => e.Ceh17).HasColumnName("CEH_17");

                entity.Property(e => e.Ceh18).HasColumnName("CEH_18");

                entity.Property(e => e.Ceh19).HasColumnName("CEH_19");

                entity.Property(e => e.Ceh20).HasColumnName("CEH_20");

                entity.Property(e => e.Ceh4).HasColumnName("CEH_4");

                entity.Property(e => e.Ceh5).HasColumnName("CEH_5");

                entity.Property(e => e.Ceh6).HasColumnName("CEH_6");

                entity.Property(e => e.Ceh7).HasColumnName("CEH_7");

                entity.Property(e => e.Ceh8).HasColumnName("CEH_8");

                entity.Property(e => e.Ceh9).HasColumnName("CEH_9");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.CehApprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_ApprovalDate");

                entity.Property(e => e.CehBio)
                    .HasColumnName("CEH_BIO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehBito)
                    .HasColumnName("CEH_BITO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehNerc)
                    .HasColumnName("CEH_NERC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehProf)
                    .HasColumnName("CEH_Prof")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehRc)
                    .HasColumnName("CEH_RC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehReg)
                    .HasColumnName("CEH_Reg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehTo)
                    .HasColumnName("CEH_TO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehappDataFee).HasColumnName("CEHAppData_Fee");

                entity.Property(e => e.CehappDataType)
                    .HasMaxLength(50)
                    .HasColumnName("CEHAppData_Type");

                entity.Property(e => e.Content)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.CorEmpAllowRetake).HasColumnName("corEMP_AllowRetake");

                entity.Property(e => e.CorEmpAutoReleaseRetake).HasColumnName("corEMP_AutoReleaseRetake");

                entity.Property(e => e.CorEmpRetakeCount)
                    .HasColumnName("corEMP_RetakeCount")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CorEmpShowAnswers).HasColumnName("corEMP_ShowAnswers");

                entity.Property(e => e.CorEmpShowCorrect).HasColumnName("corEMP_ShowCorrect");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.CorexpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CORExpDate");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CourseProcedures)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

                entity.Property(e => e.DeliveryTeam)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyOpsHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.EopsPer002).HasColumnName("EOPsPER002");

                entity.Property(e => e.EvaluationMethod).IsUnicode(false);

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.IsOptional).HasDefaultValueSql("((0))");

                entity.Property(e => e.LerningActivityMultiDeliv).HasColumnName("LerningActivity_MultiDeliv");

                entity.Property(e => e.LerningActivityPublic).HasColumnName("LerningActivity_Public");

                entity.Property(e => e.LerningActivitySelfStudy).HasColumnName("LerningActivity_SelfStudy");

                entity.Property(e => e.LerningActivitySelfStudyNa).HasColumnName("LerningActivity_SelfStudy_NA");

                entity.Property(e => e.NotNercrelated).HasColumnName("NotNERCRelated");

                entity.Property(e => e.NotOnNercreport).HasColumnName("NotOnNERCReport");

                entity.Property(e => e.Note1)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Note2)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OptionalComments).HasMaxLength(500);

                entity.Property(e => e.Other).HasDefaultValueSql("((0))");

                entity.Property(e => e.Prerequisites)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Reg2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SelfReg).HasDefaultValueSql("((0))");

                entity.Property(e => e.SimHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.TargetAudience10).HasColumnName("TargetAudience_10");

                entity.Property(e => e.TargetAudience9).HasColumnName("TargetAudience_9");

                entity.Property(e => e.TargetAudienceBio).HasColumnName("TargetAudience_BIO");

                entity.Property(e => e.TargetAudienceCrs).HasColumnName("TargetAudience_CRS");

                entity.Property(e => e.TargetAudienceGo).HasColumnName("TargetAudience_GO");

                entity.Property(e => e.TargetAudienceMo).HasColumnName("TargetAudience_MO");

                entity.Property(e => e.TargetAudienceOpe).HasColumnName("TargetAudience_OPE");

                entity.Property(e => e.TargetAudienceOther).HasColumnName("TargetAudience_Other");

                entity.Property(e => e.TargetAudienceOtherSpecify)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TargetAudience_OtherSpecify");

                entity.Property(e => e.TargetAudienceRc).HasColumnName("TargetAudience_RC");

                entity.Property(e => e.TargetAudienceTo).HasColumnName("TargetAudience_TO");

                entity.Property(e => e.Topics11).HasColumnName("Topics_11");

                entity.Property(e => e.Topics12).HasColumnName("Topics_12");

                entity.Property(e => e.TopicsBc).HasColumnName("Topics_BC");

                entity.Property(e => e.TopicsEo).HasColumnName("Topics_EO");

                entity.Property(e => e.TopicsIpso).HasColumnName("Topics_IPSO");

                entity.Property(e => e.TopicsMo).HasColumnName("Topics_MO");

                entity.Property(e => e.TopicsOa).HasColumnName("Topics_OA");

                entity.Property(e => e.TopicsPp).HasColumnName("Topics_PP");

                entity.Property(e => e.TopicsPsr).HasColumnName("Topics_PSR");

                entity.Property(e => e.TopicsPtee).HasColumnName("Topics_PTEE");

                entity.Property(e => e.TopicsSp).HasColumnName("Topics_SP");

                entity.Property(e => e.TopicsTools).HasColumnName("Topics_Tools");

                entity.Property(e => e.TotalCeh)
                    .HasColumnName("TotalCEH")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.TrainingPlan).IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Type10).HasColumnName("Type_10");

                entity.Property(e => e.Type11).HasColumnName("Type_11");

                entity.Property(e => e.TypeClassroom).HasColumnName("Type_Classroom");

                entity.Property(e => e.TypeComputerBased).HasColumnName("Type_ComputerBased");

                entity.Property(e => e.TypeConference).HasColumnName("Type_Conference");

                entity.Property(e => e.TypeInternetBased).HasColumnName("Type_InternetBased");

                entity.Property(e => e.TypeOjttraining).HasColumnName("Type_OJTTraining");

                entity.Property(e => e.TypeOther).HasColumnName("Type_Other");

                entity.Property(e => e.TypeOtherSpecify)
                    .HasMaxLength(50)
                    .HasColumnName("Type_OtherSpecify");

                entity.Property(e => e.TypeOtsimulation).HasColumnName("Type_OTSimulation");

                entity.Property(e => e.TypeSelfStudy).HasColumnName("Type_SelfStudy");

                entity.Property(e => e.TypeWorkshop).HasColumnName("Type_Workshop");

                entity.Property(e => e.VerifAndDocOfCehhours)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("VerifAndDocOfCEHHours");

                entity.HasOne(d => d.Su)
                    .WithMany(p => p.TblCourses)
                    .HasForeignKey(d => d.Suid)
                    .HasConstraintName("FK_tblCourses_lktblSupplier");
            });

            modelBuilder.Entity<TblCourseEvaluationTrainingIssue>(entity =>
            {
                entity.ToTable("tblCourseEvaluation_TrainingIssues");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblCourseEvaluationTrainingIssues)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblCourseEvaluation_TrainingIssues_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblCourseSegment>(entity =>
            {
                entity.HasKey(e => e.Csid)
                    .HasName("aaaaatblCourseSegments_PK")
                    .IsClustered(false);

                entity.ToTable("tblCourseSegments");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.Csid, "LACID");

                entity.HasIndex(e => e.Num1, "Num1");

                entity.HasIndex(e => e.Num2, "Num2");

                entity.HasIndex(e => e.Corid, "tblCoursestblCourseSegments");

                entity.Property(e => e.Csid).HasColumnName("CSID");

                entity.Property(e => e.Chk1).HasColumnName("chk1");

                entity.Property(e => e.Chk2).HasColumnName("chk2");

                entity.Property(e => e.ChkEop).HasColumnName("chkEOP");

                entity.Property(e => e.ChkOper).HasColumnName("chkOPER");

                entity.Property(e => e.ChkProf).HasColumnName("chkPROF");

                entity.Property(e => e.ChkSim).HasColumnName("chkSIM");

                entity.Property(e => e.ChkStds).HasColumnName("chkSTDS");

                entity.Property(e => e.Content)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Corid)
                    .HasColumnName("CORID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Eop)
                    .HasColumnName("EOP")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Num1).HasDefaultValueSql("((0))");

                entity.Property(e => e.Num2).HasDefaultValueSql("((0))");

                entity.Property(e => e.Oper)
                    .HasColumnName("OPER")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Prof)
                    .HasColumnName("PROF")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SegProcedures)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SegProceduresNerc)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SegmentNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SegmentTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sim)
                    .HasColumnName("SIM")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Stds)
                    .HasColumnName("STDS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblCourseSegments)
                    .HasForeignKey(d => d.Corid)
                    .HasConstraintName("tblCourseSegments_FK00");
            });

            modelBuilder.Entity<TblCourseSegmentDetail>(entity =>
            {
                entity.HasKey(e => e.Csid);

                entity.ToTable("tblCourseSegmentDetails");

                entity.Property(e => e.Csid)
                    .ValueGeneratedNever()
                    .HasColumnName("CSID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.CriticalDecisionMaking).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryCbt).HasColumnName("DeliveryCBT");

                entity.Property(e => e.DeliveryGroupExercise).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryOjt).HasColumnName("DeliveryOJT");

                entity.Property(e => e.DeliveryOther).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveryOtherText)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FieldVisit).HasDefaultValueSql("((0))");

                entity.Property(e => e.InstructorId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("InstructorID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ojt)
                    .HasColumnName("OJT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PerformanceDemo).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProblemSolving).HasDefaultValueSql("((0))");

                entity.Property(e => e.Simulation).HasDefaultValueSql("((0))");

                entity.Property(e => e.SituationalAwareness).HasDefaultValueSql("((0))");

                entity.Property(e => e.TestQuizElectronic).HasDefaultValueSql("((0))");

                entity.Property(e => e.TestQuizWritten).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblCourseSegmentLearningObjective>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblCourseSegment_LearningObjectives");

                entity.Property(e => e.Csid).HasColumnName("CSID");

                entity.Property(e => e.ObjId).HasColumnName("ObjID");

                entity.Property(e => e.ObjType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCoversheet>(entity =>
            {
                entity.HasKey(e => e.Cvid)
                    .HasName("PK__tblCover__A04CFC430FE70E86");

                entity.ToTable("tblCoversheets");

                entity.HasIndex(e => e.Cvnum, "UQ__tblCover__DE5CB426AD92D47B")
                    .IsUnique();

                entity.Property(e => e.Cvid).HasColumnName("CVID");

                entity.Property(e => e.Cvinactive)
                    .HasColumnName("CVInactive")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cvinstructions)
                    .IsUnicode(false)
                    .HasColumnName("CVInstructions");

                entity.Property(e => e.Cvnum).HasColumnName("CVNum");

                entity.Property(e => e.Cvtitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CVTitle");

                entity.Property(e => e.CvtypeId).HasColumnName("CVTypeID");
            });

            modelBuilder.Entity<TblDifproject>(entity =>
            {
                entity.HasKey(e => e.Difprjid);

                entity.ToTable("tblDIFProjects");

                entity.Property(e => e.Difprjid).HasColumnName("DIFPRJID");

                entity.Property(e => e.Difprjtitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DIFPRJTitle");

                entity.Property(e => e.Difstatus).HasColumnName("DIFStatus");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.HistoricalOnly).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pid)
                    .HasColumnName("PID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDifsurveyEmplSummary>(entity =>
            {
                entity.HasKey(e => e.Difsid);

                entity.ToTable("tblDIFSurveyEmplSummary");

                entity.Property(e => e.Difsid).HasColumnName("DIFSID");

                entity.Property(e => e.CompletedDate).HasColumnType("date");

                entity.Property(e => e.Difprjid).HasColumnName("DIFPRJID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PosComments).IsUnicode(false);

                entity.Property(e => e.ReleasedDate).HasColumnType("date");

                entity.Property(e => e.ReleasedToEmp).HasDefaultValueSql("((0))");

                entity.Property(e => e.SurveyStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDifsurveyEmployee>(entity =>
            {
                entity.HasKey(e => e.Difid);

                entity.ToTable("tblDIFSurveyEmployee");

                entity.Property(e => e.Difid).HasColumnName("DIFID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.Difficulty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Difprjid).HasColumnName("DIFPRJID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Frequency).HasDefaultValueSql("((0))");

                entity.Property(e => e.Importance).HasDefaultValueSql("((0))");

                entity.Property(e => e.Na)
                    .HasColumnName("NA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.HasOne(d => d.Difprj)
                    .WithMany(p => p.TblDifsurveyEmployees)
                    .HasForeignKey(d => d.Difprjid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDIFSurveyEmployee_tblDIFProjects");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblDifsurveyEmployees)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDIFSurveyEmployee_tblEmployee");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.TblDifsurveyEmployees)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDIFSurveyEmployee_tblPositions");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblDifsurveyEmployees)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDIFSurveyEmployee_tblTasks");
            });

            modelBuilder.Entity<TblDifsurveyPosition>(entity =>
            {
                entity.HasKey(e => e.Difpid);

                entity.ToTable("tblDIFSurveyPosition");

                entity.Property(e => e.Difpid).HasColumnName("DIFPID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.Difprjid).HasColumnName("DIFPRJID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.StatusDefault).HasColumnName("Status_Default");

                entity.Property(e => e.StatusFinal).HasColumnName("Status_Final");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<TblDocumentLink>(entity =>
            {
                entity.HasKey(e => new { e.LinkedDocId, e.TypeId, e.LinkItemId });

                entity.ToTable("tblDocumentLinks");

                entity.Property(e => e.LinkedDocId).HasColumnName("LinkedDocID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.LinkItemId).HasColumnName("LinkItemID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.Property(e => e.Ldlid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LDLID");
            });

            modelBuilder.Entity<TblDocumentLinkType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("tblDocumentLinkTypes");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeDesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDocumentType>(entity =>
            {
                entity.HasKey(e => e.Dtid);

                entity.ToTable("tblDocumentTypes");

                entity.Property(e => e.Dtid)
                    .ValueGeneratedNever()
                    .HasColumnName("DTID");

                entity.Property(e => e.Dtdesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DTDesc");

                entity.Property(e => e.DtsortOrder).HasColumnName("DTSortOrder");
            });

            modelBuilder.Entity<TblDutyArea>(entity =>
            {
                entity.HasKey(e => e.Daid)
                    .HasName("aaaaatblDutyAreas_PK")
                    .IsClustered(false);

                entity.ToTable("tblDutyAreas");

                entity.HasIndex(e => e.Danum, "DANum");

                entity.HasIndex(e => e.DasubNum, "DASubNum");

                entity.HasIndex(e => new { e.Daletter, e.Danum, e.DasubNum }, "uc_DANum")
                    .IsUnique();

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum)
                    .HasColumnName("DANum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DasubNum)
                    .HasColumnName("DASubNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SkillRelated).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblElecImportClmnMap>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblElecImportClmnMap");

                entity.Property(e => e.DefValStr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Impid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("IMPID");

                entity.Property(e => e.ImportFileTypeId).HasColumnName("ImportFileTypeID");

                entity.Property(e => e.Req)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder).HasColumnName("sortOrder");
            });

            modelBuilder.Entity<TblEmpLogin>(entity =>
            {
                entity.ToTable("tblEmpLogin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LoginDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblEmpTestInstance>(entity =>
            {
                entity.ToTable("tblEmpTestInstance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.Etid).HasColumnName("ETID");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.TestId).HasColumnName("TestID");
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("aaaaatblEmployee_PK")
                    .IsClustered(false);

                entity.ToTable("tblEmployee");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.Enum, "ENum");

                entity.HasIndex(e => e.NerccertNum, "NERCCertNum");

                entity.HasIndex(e => e.Oid, "OID");

                entity.HasIndex(e => e.Pid, "PID");

                entity.HasIndex(e => e.RegCertNum, "RegCertNum");

                entity.HasIndex(e => e.RegCertNum2, "RegCertNum2");

                entity.HasIndex(e => e.Oid, "lktblOrganizationstblEmployee");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EIdptext).HasColumnName("E_IDPText");

                entity.Property(e => e.Ecity)
                    .HasMaxLength(50)
                    .HasColumnName("ECity");

                entity.Property(e => e.Eemail)
                    .HasMaxLength(50)
                    .HasColumnName("EEmail");

                entity.Property(e => e.EexpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EExpirationDate");

                entity.Property(e => e.EfirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("EFirstName");

                entity.Property(e => e.EhireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EHireDate");

                entity.Property(e => e.ElastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ELastName");

                entity.Property(e => e.EmiddleInitial)
                    .HasMaxLength(50)
                    .HasColumnName("EMiddleInitial");

                entity.Property(e => e.EnotCertified)
                    .HasColumnName("ENotCertified")
                    .HasDefaultValueSql("((0))")
                    .HasComment("not certified");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Ephone)
                    .HasMaxLength(50)
                    .HasColumnName("EPhone");

                entity.Property(e => e.Estate)
                    .HasMaxLength(50)
                    .HasColumnName("EState");

                entity.Property(e => e.Estreet1)
                    .HasMaxLength(50)
                    .HasColumnName("EStreet1");

                entity.Property(e => e.Estreet2)
                    .HasMaxLength(50)
                    .HasColumnName("EStreet2");

                entity.Property(e => e.EwillNotBeRecertified)
                    .HasColumnName("EWillNotBeRecertified")
                    .HasDefaultValueSql("((0))")
                    .HasComment("will not be recertified");

                entity.Property(e => e.EworkLoc)
                    .HasMaxLength(50)
                    .HasColumnName("EWorkLoc");

                entity.Property(e => e.Ezip)
                    .HasMaxLength(50)
                    .HasColumnName("EZip");

                entity.Property(e => e.IsQtdadmin).HasColumnName("IsQTDAdmin");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertAreaExisting).HasColumnName("NERCCertArea_Existing");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Note1).HasMaxLength(255);

                entity.Property(e => e.Note2).HasMaxLength(255);

                entity.Property(e => e.Oid).HasColumnName("OID");

                entity.Property(e => e.Pid)
                    .HasColumnName("PID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PosEndDate).HasColumnType("datetime");

                entity.Property(e => e.PosQualDate).HasColumnType("datetime");

                entity.Property(e => e.PosStartDate).HasColumnType("datetime");

                entity.Property(e => e.Qtsmanager).HasColumnName("QTSManager");

                entity.Property(e => e.QualDateVerified).HasDefaultValueSql("((0))");

                entity.Property(e => e.RegCertExpDate).HasColumnType("datetime");

                entity.Property(e => e.RegCertExpDate2).HasColumnType("datetime");

                entity.Property(e => e.RegCertIssueDate).HasColumnType("datetime");

                entity.Property(e => e.RegCertIssueDate2).HasColumnType("datetime");

                entity.Property(e => e.RegCertNum).HasMaxLength(50);

                entity.Property(e => e.RegCertNum2).HasMaxLength(50);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.RegCertType2).HasMaxLength(50);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Retired).HasDefaultValueSql("((0))");

                entity.Property(e => e.SosuserId)
                    .HasColumnName("SOSUserID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TrProgId).HasColumnName("TrProgID");

                entity.Property(e => e.Trainee).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.OidNavigation)
                    .WithMany(p => p.TblEmployees)
                    .HasForeignKey(d => d.Oid)
                    .HasConstraintName("tblEmployee_FK00");
            });

            modelBuilder.Entity<TblEmployeeAdditionalPosition>(entity =>
            {
                entity.ToTable("tblEmployee_AdditionalPositions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.QualDateVerified).HasDefaultValueSql("((0))");

                entity.Property(e => e.QualificationDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Trainee).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEmployeeGroup>(entity =>
            {
                entity.HasKey(e => e.Egid);

                entity.ToTable("tblEmployee_Groups");

                entity.HasIndex(e => e.Egid, "IX_tblEmployee_Groups")
                    .IsUnique();

                entity.Property(e => e.Egid).HasColumnName("EGID");

                entity.Property(e => e.Egdesc)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("EGDesc");
            });

            modelBuilder.Entity<TblEmployeeGroupEmployee>(entity =>
            {
                entity.HasKey(e => new { e.Egid, e.Empid });

                entity.ToTable("tblEmployeeGroup_Employees");

                entity.Property(e => e.Egid).HasColumnName("EGID");

                entity.Property(e => e.Empid).HasColumnName("EMPID");

                entity.HasOne(d => d.Eg)
                    .WithMany(p => p.TblEmployeeGroupEmployees)
                    .HasForeignKey(d => d.Egid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblEmployeeGroup_Employees_tblEmployee_Groups");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.TblEmployeeGroupEmployees)
                    .HasForeignKey(d => d.Empid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblEmployeeGroup_Employees_tblEmployee");
            });

            modelBuilder.Entity<TblEmployeePositionHistory>(entity =>
            {
                entity.HasKey(e => e.Phid);

                entity.ToTable("tblEmployee_PositionHistory");

                entity.Property(e => e.Phid).HasColumnName("PHID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.QualificationDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Trainee).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEmployeeStatusRecord>(entity =>
            {
                entity.ToTable("tblEmployee_StatusRecords");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChangedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("Changed_On")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Comments).HasMaxLength(500);

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Effective_Date");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.NewStatus).HasColumnName("New_Status");

                entity.Property(e => e.PrevStatus).HasColumnName("Prev_Status");

                entity.Property(e => e.SaveHistory).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEmployeeTest>(entity =>
            {
                entity.HasKey(e => e.Etid);

                entity.ToTable("tblEmployee_Test");

                entity.Property(e => e.Etid).HasColumnName("ETID");

                entity.Property(e => e.AddedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateComplete).HasColumnType("datetime");

                entity.Property(e => e.DisclaimerSigned).HasDefaultValueSql("((0))");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.ExpOverride).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPreTest).HasDefaultValueSql("((0))");

                entity.Property(e => e.Restart).HasDefaultValueSql("((0))");

                entity.Property(e => e.Retake).HasDefaultValueSql("((0))");

                entity.Property(e => e.TestComplete).HasDefaultValueSql("((0))");

                entity.Property(e => e.TestGrade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestInterrupt).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblEmployeeTestResponse>(entity =>
            {
                entity.HasKey(e => e.Etrid);

                entity.ToTable("tblEmployee_TestResponse");

                entity.Property(e => e.Etrid).HasColumnName("ETRID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Etid).HasColumnName("ETID");

                entity.Property(e => e.Response).HasMaxLength(500);

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");
            });

            modelBuilder.Entity<TblEmpsetting>(entity =>
            {
                entity.HasKey(e => e.EmpsettingId);

                entity.ToTable("tblEMPSettings");

                entity.Property(e => e.EmpsettingId)
                    .ValueGeneratedNever()
                    .HasColumnName("EMPSettingID");

                entity.Property(e => e.EmpsettingDesc)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("EMPSettingDesc");

                entity.Property(e => e.EmpsettingUnit)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("EMPSettingUnit");

                entity.Property(e => e.EmpsettingValue)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EMPSettingValue");
            });

            modelBuilder.Entity<TblEmpuser>(entity =>
            {
                entity.HasKey(e => e.Uname)
                    .HasName("PK_Users")
                    .IsClustered(false);

                entity.ToTable("tblEMPUsers");

                entity.Property(e => e.Uname)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("uname");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblErrorLog>(entity =>
            {
                entity.HasKey(e => e.ErrorLogId);

                entity.ToTable("tblErrorLog");

                entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");

                entity.Property(e => e.ErrorDate).HasColumnType("datetime");

                entity.Property(e => e.ErrorDesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorNum)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblExtImportHistory>(entity =>
            {
                entity.HasKey(e => e.Impid)
                    .HasName("PK__tblExtIm__F248F39799F88F57");

                entity.ToTable("tblExtImportHistory");

                entity.Property(e => e.Impid).HasColumnName("IMPID");

                entity.Property(e => e.Extpid).HasColumnName("EXTPID");

                entity.Property(e => e.ImportDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ImportStatus)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblFavoriteReport>(entity =>
            {
                entity.HasKey(e => new { e.ReportTitle, e.Eid });

                entity.ToTable("tblFavoriteReports");

                entity.Property(e => e.ReportTitle)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.CriteriaForm)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblForm>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("tblForms");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fdescription)
                    .HasMaxLength(255)
                    .HasColumnName("FDescription");

                entity.Property(e => e.Fexpired).HasColumnName("FExpired");

                entity.Property(e => e.Finstructions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("FInstructions");

                entity.Property(e => e.Fname)
                    .HasMaxLength(255)
                    .HasColumnName("FName");

                entity.Property(e => e.Fversion).HasColumnName("FVersion");

                entity.Property(e => e.Rsid).HasColumnName("RSID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Rs)
                    .WithMany(p => p.TblForms)
                    .HasForeignKey(d => d.Rsid)
                    .HasConstraintName("FK_tblForms_lktblRatingScales");
            });

            modelBuilder.Entity<TblFormQuestion>(entity =>
            {
                entity.HasKey(e => e.Fqid)
                    .HasName("aaaaatblFormQuestions_PK")
                    .IsClustered(false);

                entity.ToTable("tblFormQuestions");

                entity.HasIndex(e => e.Fid, "FID");

                entity.HasIndex(e => e.Fqnum, "FQNum");

                entity.HasIndex(e => e.Fsid, "FSID");

                entity.HasIndex(e => e.Fid, "{29E3D284-0402-4AE4-ABDA-EB261142FDF1}");

                entity.Property(e => e.Fqid).HasColumnName("FQID");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Fqdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("FQDesc");

                entity.Property(e => e.Fqinactive).HasColumnName("FQInactive");

                entity.Property(e => e.Fqnum)
                    .HasColumnName("FQNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Fsid)
                    .HasColumnName("FSID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.TblFormQuestions)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("tblFormQuestions_FK00");
            });

            modelBuilder.Entity<TblFormSection>(entity =>
            {
                entity.HasKey(e => e.Fsid)
                    .HasName("aaaaatblFormSections_PK")
                    .IsClustered(false);

                entity.ToTable("tblFormSections");

                entity.HasIndex(e => e.Fid, "FID");

                entity.HasIndex(e => e.Fsnum, "FQNum");

                entity.HasIndex(e => e.Fid, "{A37989A0-0F0C-49B0-A715-39B9B7EAF934}");

                entity.Property(e => e.Fsid).HasColumnName("FSID");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Fsdesc)
                    .HasMaxLength(255)
                    .HasColumnName("FSDesc");

                entity.Property(e => e.Fsexpired).HasColumnName("FSExpired");

                entity.Property(e => e.Fsnum)
                    .HasColumnName("FSNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.TblFormSections)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("tblFormSections_FK00");
            });

            modelBuilder.Entity<TblGapProject>(entity =>
            {
                entity.HasKey(e => e.Prjid);

                entity.ToTable("tblGapProjects");

                entity.Property(e => e.Prjid).HasColumnName("PRJID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Prjdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PRJDate");

                entity.Property(e => e.Prjtitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRJTitle");

                entity.Property(e => e.Rsid).HasColumnName("RSID");

                entity.Property(e => e.SurveyDetails)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rs)
                    .WithMany(p => p.TblGapProjects)
                    .HasForeignKey(d => d.Rsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGapProjects_lktblRatingScales");
            });

            modelBuilder.Entity<TblGapRating>(entity =>
            {
                entity.HasKey(e => e.Gapid);

                entity.ToTable("tblGapRatings");

                entity.Property(e => e.Gapid).HasColumnName("GAPID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Prjid).HasColumnName("PRJID");

                entity.Property(e => e.Raby)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("RABy")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblGapRatings)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGapRatings_tblEmployee");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.TblGapRatings)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGapRatings_tblPositions");

                entity.HasOne(d => d.Prj)
                    .WithMany(p => p.TblGapRatings)
                    .HasForeignKey(d => d.Prjid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGapRatings_tblGapProjects");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblGapRatings)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGapRatings_tblTasks");
            });

            modelBuilder.Entity<TblGapstatus>(entity =>
            {
                entity.HasKey(e => e.Gapsid);

                entity.ToTable("tblGAPStatus");

                entity.Property(e => e.Gapsid).HasColumnName("GAPSID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Prjid).HasColumnName("PRJID");

                entity.Property(e => e.ReleasedToEmp).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblGapstatuses)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGAPStatus_tblEmployee");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.TblGapstatuses)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGAPStatus_tblPositions");

                entity.HasOne(d => d.Prj)
                    .WithMany(p => p.TblGapstatuses)
                    .HasForeignKey(d => d.Prjid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGAPStatus_tblGapProjects");
            });

            modelBuilder.Entity<TblGroupSchedule>(entity =>
            {
                entity.HasKey(e => e.Gsid);

                entity.ToTable("tblGroupSchedule");

                entity.Property(e => e.Gsid).HasColumnName("GSID");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dru)
                    .HasColumnType("datetime")
                    .HasColumnName("DRU");

                entity.Property(e => e.Egid).HasColumnName("EGID");

                entity.Property(e => e.GroupTitle)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ilagid).HasColumnName("ILAGID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblGroupScheduleEmployee>(entity =>
            {
                entity.HasKey(e => new { e.Gsid, e.Eid });

                entity.ToTable("tblGroupSchedule_Employees");

                entity.Property(e => e.Gsid).HasColumnName("GSID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.HasOne(d => d.Gs)
                    .WithMany(p => p.TblGroupScheduleEmployees)
                    .HasForeignKey(d => d.Gsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGroupSchedule_Employees_tblGroupSchedule");
            });

            modelBuilder.Entity<TblGroupScheduleIla>(entity =>
            {
                entity.HasKey(e => new { e.Gsid, e.Corid });

                entity.ToTable("tblGroupSchedule_ILA");

                entity.Property(e => e.Gsid).HasColumnName("GSID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.ClassDate).HasColumnType("datetime");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.LocId).HasColumnName("LocID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Gs)
                    .WithMany(p => p.TblGroupScheduleIlas)
                    .HasForeignKey(d => d.Gsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGroupSchedule_ILA_tblGroupSchedule");
            });

            modelBuilder.Entity<TblIdp>(entity =>
            {
                entity.HasKey(e => e.Idpid)
                    .HasName("aaaaatblIDPs_PK")
                    .IsClustered(false);

                entity.ToTable("tblIDPs");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.DefClid, "DefCLID");

                entity.HasIndex(e => e.Idpid, "IDPID");

                entity.HasIndex(e => e.Eid, "PID");

                entity.HasIndex(e => e.Pid, "PID1");

                entity.HasIndex(e => e.Corid, "tblCoursestblIDPs");

                entity.HasIndex(e => e.Eid, "tblEmployeetblIDPs");

                entity.Property(e => e.Idpid).HasColumnName("IDPID");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.CompInstructor).HasMaxLength(50);

                entity.Property(e => e.CompLoc).HasMaxLength(50);

                entity.Property(e => e.Corid)
                    .HasColumnName("CORID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DefClid)
                    .HasColumnName("DefCLID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1)))");

                entity.Property(e => e.Eid)
                    .HasColumnName("EID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Pid)
                    .HasColumnName("PID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReqCompDate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Ttype)
                    .HasMaxLength(1)
                    .HasColumnName("TType");

                entity.Property(e => e.Tyear)
                    .HasMaxLength(50)
                    .HasColumnName("TYear");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblIdps)
                    .HasForeignKey(d => d.Corid)
                    .HasConstraintName("tblIDPs_FK00");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblIdps)
                    .HasForeignKey(d => d.Eid)
                    .HasConstraintName("tblIDPs_FK01");
            });

            modelBuilder.Entity<TblIdpreleaseEmpSummary>(entity =>
            {
                entity.HasKey(e => e.RelId);

                entity.ToTable("tblIDPRelease_EmpSummary");

                entity.Property(e => e.RelId).HasColumnName("RelID");

                entity.Property(e => e.CompletedDate).HasColumnType("date");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EmpIdpresponse).HasColumnName("EMP_IDPResponse");

                entity.Property(e => e.Empcomments).HasColumnName("EMPComments");

                entity.Property(e => e.IdpTitle)
                    .HasMaxLength(500)
                    .HasColumnName("IDP_Title");

                entity.Property(e => e.Idpid).HasColumnName("IDPID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.ReleasedBy).HasMaxLength(300);

                entity.Property(e => e.ReleasedDate).HasColumnType("date");

                entity.Property(e => e.ReleasedToEmp).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblIdpreview>(entity =>
            {
                entity.HasKey(e => e.Idpid);

                entity.ToTable("tblIDPReview");

                entity.Property(e => e.Idpid).HasColumnName("IDPId");

                entity.Property(e => e.IdpCreatedBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("IDP_CreatedBy");

                entity.Property(e => e.IdpCreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDP_CreatedDate");

                entity.Property(e => e.IdpEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDP_EndDate");

                entity.Property(e => e.IdpReleaseText).HasColumnName("IDP_ReleaseText");

                entity.Property(e => e.IdpReleaseTextPlain).HasColumnName("IDP_ReleaseText_Plain");

                entity.Property(e => e.IdpStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("IDP_StartDate");

                entity.Property(e => e.IdpStatus).HasColumnName("IDP_Status");

                entity.Property(e => e.IdpTitle)
                    .HasMaxLength(500)
                    .HasColumnName("IDP_Title");
            });

            modelBuilder.Entity<TblIlaContentObject>(entity =>
            {
                entity.HasKey(e => e.Coilaid)
                    .HasName("PK_tblILA_COID2");

                entity.ToTable("tblILA_ContentObject");

                entity.Property(e => e.Coilaid).HasColumnName("COILAID");

                entity.Property(e => e.Coid).HasColumnName("COID");

                entity.Property(e => e.Corid).HasColumnName("CORID");
            });

            modelBuilder.Entity<TblIlaDetail>(entity =>
            {
                entity.HasKey(e => e.Corid);

                entity.ToTable("tblILA_Details");

                entity.Property(e => e.Corid)
                    .ValueGeneratedNever()
                    .HasColumnName("CORID");

                entity.Property(e => e.AttVerComments)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AttendanceVerificationId).HasColumnName("AttendanceVerificationID");

                entity.Property(e => e.AttendanceVerificationOther)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Avidentification).HasColumnName("AVIdentification");

                entity.Property(e => e.Avother).HasColumnName("AVOther");

                entity.Property(e => e.Avroster).HasColumnName("AVRoster");

                entity.Property(e => e.AvsignInSheet).HasColumnName("AVSignInSheet");

                entity.Property(e => e.CourseComments)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FormVersion)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InstructionalDeliveryTeamOtherText)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.InstructionalDeliveryTeamTrainer).HasDefaultValueSql("((0))");

                entity.Property(e => e.LearningAssessJustf)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ojt).HasColumnName("OJT");

                entity.Property(e => e.OtherTypeAssmtText)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderFollowsSat).HasColumnName("ProviderFollowsSAT");

                entity.Property(e => e.StartDate1).HasColumnType("datetime");

                entity.Property(e => e.StartDate2).HasColumnType("datetime");

                entity.Property(e => e.StartDate3).HasColumnType("datetime");

                entity.Property(e => e.StartDate4).HasColumnType("datetime");

                entity.Property(e => e.StartDate5).HasColumnType("datetime");

                entity.Property(e => e.StartDate6).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblIlaGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblILA_Groups");

                entity.Property(e => e.Ilagdesc)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("ILAGDesc");

                entity.Property(e => e.Ilagid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ILAGID");
            });

            modelBuilder.Entity<TblIlaNercstandard>(entity =>
            {
                entity.HasKey(e => new { e.Nsid, e.Corid });

                entity.ToTable("tblILA_NERCStandards");

                entity.Property(e => e.Nsid).HasColumnName("NSID");

                entity.Property(e => e.Corid).HasColumnName("CORID");
            });

            modelBuilder.Entity<TblIlaSimulation>(entity =>
            {
                entity.HasKey(e => e.IlasimId);

                entity.ToTable("tblILA_Simulation");

                entity.Property(e => e.IlasimId).HasColumnName("ILASIM_ID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.DocumentationProcedures)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Generation)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorPrep)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Interchange)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.LoadingConditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.NetworkConfiguration)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasColumnType("text");

                entity.Property(e => e.Other)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.RolePlays)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ScenarioDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.ScenarioTitle)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.ValidityChecks)
                    .HasMaxLength(5000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblIlaSimulationCriterion>(entity =>
            {
                entity.HasKey(e => e.Ilascid);

                entity.ToTable("tblILA_Simulation_Criteria");

                entity.Property(e => e.Ilascid).HasColumnName("ILASCID");

                entity.Property(e => e.CriteriaDesc)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.IlasimId).HasColumnName("ILASimID");

                entity.Property(e => e.PosId).HasColumnName("PosID");
            });

            modelBuilder.Entity<TblIlaSimulationEventGroup>(entity =>
            {
                entity.HasKey(e => e.Egid);

                entity.ToTable("tblILA_Simulation_EventGroups");

                entity.Property(e => e.Egid).HasColumnName("EGID");

                entity.Property(e => e.Egdesc)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("EGDesc");

                entity.Property(e => e.Egnum).HasColumnName("EGNum");

                entity.Property(e => e.Ilasimid).HasColumnName("ILASIMID");
            });

            modelBuilder.Entity<TblIlaSimulationObjective>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblILA_Simulation_Objectives");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Ilasimid).HasColumnName("ILASIMID");

                entity.Property(e => e.ObjectiveId).HasColumnName("ObjectiveID");

                entity.Property(e => e.ObjectiveType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.SktaskId).HasColumnName("SKTaskID");
            });

            modelBuilder.Entity<TblIlaSimulationPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblILA_Simulation_Positions");

                entity.Property(e => e.Ilasimid).HasColumnName("ILASIMID");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.Rsid).HasColumnName("RSID");
            });

            modelBuilder.Entity<TblIlaSimulationScript>(entity =>
            {
                entity.HasKey(e => e.ScriptId)
                    .HasName("PK_tblILASimulation_Script");

                entity.ToTable("tblILA_Simulation_Script");

                entity.Property(e => e.ScriptId).HasColumnName("ScriptID");

                entity.Property(e => e.Event)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ilascid).HasColumnName("ILASCID");

                entity.Property(e => e.IlasimId).HasColumnName("ILASimID");

                entity.Property(e => e.Initiator)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InitiatorPosId).HasColumnName("InitiatorPosID");

                entity.HasOne(d => d.Ilasc)
                    .WithMany(p => p.TblIlaSimulationScripts)
                    .HasForeignKey(d => d.Ilascid)
                    .HasConstraintName("FK_tblILASimulation_Script_tblILA_Simulation_Criteria");

                entity.HasOne(d => d.Ilasim)
                    .WithMany(p => p.TblIlaSimulationScripts)
                    .HasForeignKey(d => d.IlasimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblILASimulation_Script_tblILA_Simulation");
            });

            modelBuilder.Entity<TblIlaTest>(entity =>
            {
                entity.HasKey(e => new { e.Corid, e.TestId });

                entity.ToTable("tblILA_Test");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.TestId).HasColumnName("TestID");
            });

            modelBuilder.Entity<TblIlaTrainingTopic>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblILA_TrainingTopics");

                entity.HasIndex(e => e.Corid, "UQ__tblILA_T__F7D2E36178D8B6B4")
                    .IsUnique();

                entity.Property(e => e.Agc).HasColumnName("agc");

                entity.Property(e => e.AppropriateCommunications).HasColumnName("appropriate_communications");

                entity.Property(e => e.BalancingResources).HasColumnName("balancing_resources");

                entity.Property(e => e.Blackstart).HasColumnName("blackstart");

                entity.Property(e => e.Busses).HasColumnName("busses");

                entity.Property(e => e.Capacitance).HasColumnName("capacitance");

                entity.Property(e => e.CommSystems).HasColumnName("comm_systems");

                entity.Property(e => e.CommunicationsLoss).HasColumnName("communications_loss");

                entity.Property(e => e.CompanyPolicies).HasColumnName("company_policies");

                entity.Property(e => e.ContingencyAnalysis).HasColumnName("contingency_analysis");

                entity.Property(e => e.ContingencyProblems).HasColumnName("contingency_problems");

                entity.Property(e => e.ContingencyReserves).HasColumnName("contingency_reserves");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.CyberSecurity).HasColumnName("cyber_security");

                entity.Property(e => e.DemandSide).HasColumnName("demand_side");

                entity.Property(e => e.ETag).HasColumnName("e_tag");

                entity.Property(e => e.ElectricalCircuits).HasColumnName("electrical_circuits");

                entity.Property(e => e.Emergencies).HasColumnName("emergencies");

                entity.Property(e => e.EmsLoss).HasColumnName("ems_loss");

                entity.Property(e => e.EnergyAccounting).HasColumnName("energy_accounting");

                entity.Property(e => e.EnergyAccountingApp).HasColumnName("energy_accounting_app");

                entity.Property(e => e.FacilitiesLoss).HasColumnName("facilities_loss");

                entity.Property(e => e.FacilityRestorationPriorities).HasColumnName("facility_restoration_priorities");

                entity.Property(e => e.Forecasting).HasColumnName("forecasting");

                entity.Property(e => e.FrequencyControl).HasColumnName("frequency_control");

                entity.Property(e => e.GenerationLoss).HasColumnName("generation_loss");

                entity.Property(e => e.Generators).HasColumnName("generators");

                entity.Property(e => e.Impedance).HasColumnName("impedance");

                entity.Property(e => e.InadvertentEnergy).HasColumnName("inadvertent_energy");

                entity.Property(e => e.Inductance).HasColumnName("inductance");

                entity.Property(e => e.Interchange).HasColumnName("interchange");

                entity.Property(e => e.Introduction).HasColumnName("introduction");

                entity.Property(e => e.IslandingAndSynchronizing).HasColumnName("islanding_and_synchronizing");

                entity.Property(e => e.IsoRto).HasColumnName("iso_rto");

                entity.Property(e => e.KirchhoffsLaw).HasColumnName("kirchhoffs_law");

                entity.Property(e => e.LineLoadingRelief).HasColumnName("line_loading_relief");

                entity.Property(e => e.LoadShedding).HasColumnName("load_shedding");

                entity.Property(e => e.Magnetism).HasColumnName("magnetism");

                entity.Property(e => e.MarketApplications).HasColumnName("market_applications");

                entity.Property(e => e.Naesb).HasColumnName("naesb");

                entity.Property(e => e.Oasis).HasColumnName("oasis");

                entity.Property(e => e.OhmsLaw).HasColumnName("ohms_law");

                entity.Property(e => e.OperatingReserves).HasColumnName("operating_reserves");

                entity.Property(e => e.Outage).HasColumnName("outage");

                entity.Property(e => e.PerUnit).HasColumnName("per_unit");

                entity.Property(e => e.PowerFlow).HasColumnName("power_flow");

                entity.Property(e => e.PowerPlants).HasColumnName("power_plants");

                entity.Property(e => e.PowerSystemFaults).HasColumnName("power_system_faults");

                entity.Property(e => e.PrimaryControlCenterLoss).HasColumnName("primary_control_center_loss");

                entity.Property(e => e.ProperCommunications).HasColumnName("proper_communications");

                entity.Property(e => e.Protection).HasColumnName("protection");

                entity.Property(e => e.PvCurves).HasColumnName("pv_curves");

                entity.Property(e => e.Pythagorean).HasColumnName("pythagorean");

                entity.Property(e => e.Ratios).HasColumnName("ratios");

                entity.Property(e => e.RealReactivePower).HasColumnName("real_reactive_power");

                entity.Property(e => e.Regional).HasColumnName("regional");

                entity.Property(e => e.Relays).HasColumnName("relays");

                entity.Property(e => e.ReliabilityStandards).HasColumnName("reliability_standards");

                entity.Property(e => e.RestorationPhilosophies).HasColumnName("restoration_philosophies");

                entity.Property(e => e.Scada).HasColumnName("scada");

                entity.Property(e => e.Stability).HasColumnName("stability");

                entity.Property(e => e.StabilityAngleVoltage).HasColumnName("stability_angle_voltage");

                entity.Property(e => e.StandardsOfConduct).HasColumnName("standards_of_conduct");

                entity.Property(e => e.StateEstimator).HasColumnName("state_estimator");

                entity.Property(e => e.Substations).HasColumnName("substations");

                entity.Property(e => e.SyncronizingEquipment).HasColumnName("syncronizing_equipment");

                entity.Property(e => e.Tariffs).HasColumnName("tariffs");

                entity.Property(e => e.TelemetryProblems).HasColumnName("telemetry_problems");

                entity.Property(e => e.TimeError).HasColumnName("time_error");

                entity.Property(e => e.TransactionScheduleing).HasColumnName("transaction_scheduleing");

                entity.Property(e => e.Transformers).HasColumnName("transformers");

                entity.Property(e => e.TransformersPrinciple).HasColumnName("transformers_principle");

                entity.Property(e => e.Transmission).HasColumnName("transmission");

                entity.Property(e => e.TransmissionLoss).HasColumnName("transmission_loss");

                entity.Property(e => e.TransmissionPrinciple).HasColumnName("transmission_principle");

                entity.Property(e => e.Trigonometry).HasColumnName("trigonometry");

                entity.Property(e => e.UnderVoltage).HasColumnName("under_voltage");

                entity.Property(e => e.VoiceAndDataComms).HasColumnName("voice_and_data_comms");

                entity.Property(e => e.VoltageControl).HasColumnName("voltage_control");

                entity.HasOne(d => d.Cor)
                    .WithOne()
                    .HasForeignKey<TblIlaTrainingTopic>(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblILA_Tr__CORID__4AF81212");
            });

            modelBuilder.Entity<TblIlagroupIla>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblILAGroup_ILAs");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Ilagid).HasColumnName("ILAGID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.LocId).HasColumnName("LocID");
            });

            modelBuilder.Entity<TblIlaresource>(entity =>
            {
                entity.HasKey(e => e.Ilarid)
                    .HasName("aaaaatblILAResources_PK")
                    .IsClustered(false);

                entity.ToTable("tblILAResources");

                entity.HasIndex(e => e.Corid, "CORID");

                entity.HasIndex(e => e.Ilarid, "ILARID");

                entity.HasIndex(e => e.Corid, "tblCoursestblILAResources");

                entity.Property(e => e.Ilarid).HasColumnName("ILARID");

                entity.Property(e => e.Corid)
                    .HasColumnName("CORID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ilarchapter)
                    .HasMaxLength(50)
                    .HasColumnName("ILARChapter");

                entity.Property(e => e.Ilarcomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("ILARComments");

                entity.Property(e => e.Ilarhyperlink)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("ILARHyperlink");

                entity.Property(e => e.IlarhyperlinkText)
                    .HasMaxLength(255)
                    .HasColumnName("ILARHyperlinkText");

                entity.Property(e => e.Ilarnum).HasColumnName("ILARNum");

                entity.Property(e => e.Ilarsection)
                    .HasMaxLength(50)
                    .HasColumnName("ILARSection");

                entity.Property(e => e.Ilartitle)
                    .HasMaxLength(50)
                    .HasColumnName("ILARTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblIlaresources)
                    .HasForeignKey(d => d.Corid)
                    .HasConstraintName("tblILAResources_FK00");
            });

            modelBuilder.Entity<TblImage>(entity =>
            {
                entity.HasKey(e => e.Imid)
                    .HasName("PK_tblImmages");

                entity.ToTable("tblImages");

                entity.Property(e => e.Imid)
                    .HasColumnName("IMID")
                    .HasComment("");

                entity.Property(e => e.Imbody)
                    .HasColumnType("image")
                    .HasColumnName("IMBody");

                entity.Property(e => e.Imdesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMDesc");

                entity.Property(e => e.ImparentId)
                    .HasColumnName("IMParentID")
                    .HasComment("0 for IMType=0, SUID for IMType=1");

                entity.Property(e => e.Imtype)
                    .HasColumnName("IMType")
                    .HasComment("0-Reports Logo, 1-Provider Rep. Signature by SUID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblImageSize>(entity =>
            {
                entity.HasKey(e => e.ImageSizeId);

                entity.ToTable("tblImageSize");

                entity.Property(e => e.ImageSizeId)
                    .ValueGeneratedNever()
                    .HasColumnName("ImageSizeID");

                entity.Property(e => e.ImageType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblInstructorsAdministrator>(entity =>
            {
                entity.ToTable("tblInstructors_Administrators");

                entity.HasOne(d => d.Instuctor)
                    .WithMany(p => p.TblInstructorsAdministrators)
                    .HasForeignKey(d => d.InstuctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblInstructors_Administrators_lktblInstructors");
            });

            modelBuilder.Entity<TblLabelReplacementText>(entity =>
            {
                entity.HasKey(e => e.ReplacementLabelId);

                entity.ToTable("tblLabelReplacementText");

                entity.Property(e => e.ReplacementLabelId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReplacementLabelID");

                entity.Property(e => e.DefaultText)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ReplacementText)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLinkedDocument>(entity =>
            {
                entity.HasKey(e => e.Ldid);

                entity.ToTable("tblLinkedDocuments");

                entity.Property(e => e.Ldid).HasColumnName("LDID");

                entity.Property(e => e.Ldcomment)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("LDComment");

                entity.Property(e => e.LddateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("LDDateStamp")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LddocDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LDDocDate");

                entity.Property(e => e.LdfileName)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("LDFileName");

                entity.Property(e => e.Ldreference).HasColumnName("LDReference");

                entity.Property(e => e.Ldtype).HasColumnName("LDType");

                entity.HasOne(d => d.LdtypeNavigation)
                    .WithMany(p => p.TblLinkedDocuments)
                    .HasForeignKey(d => d.Ldtype)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLinkedDocuments_tblDocumentTypes");
            });

            modelBuilder.Entity<TblMenuItem>(entity =>
            {
                entity.HasKey(e => e.MuId);

                entity.ToTable("tblMenuItems");

                entity.Property(e => e.MuId).HasColumnName("MuID");

                entity.Property(e => e.MuActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MuForm)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MuLabel)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MuParentId).HasColumnName("MuParentID");

                entity.Property(e => e.MuQtsmgrAccess).HasColumnName("MuQTSMgrAccess");
            });

            modelBuilder.Entity<TblNercstandard>(entity =>
            {
                entity.HasKey(e => e.Nsid);

                entity.ToTable("tblNERCStandards");

                entity.HasIndex(e => e.Nsname, "IX_tblNERCStandards")
                    .IsUnique();

                entity.Property(e => e.Nsid).HasColumnName("NSID");

                entity.Property(e => e.Nsname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NSName");

                entity.Property(e => e.UserDefined)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblNercstandardsCategory>(entity =>
            {
                entity.HasKey(e => e.Nscid);

                entity.ToTable("tblNERCStandardsCategories");

                entity.HasIndex(e => e.Nscdesc, "IX_tblNERCStandardsCategories")
                    .IsUnique();

                entity.Property(e => e.Nscid)
                    .ValueGeneratedNever()
                    .HasColumnName("NSCID");

                entity.Property(e => e.Nscdesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NSCDesc");
            });

            modelBuilder.Entity<TblObjectivesUserAdd>(entity =>
            {
                entity.HasKey(e => e.ObjId)
                    .HasName("aaaaatblObjectives_UserAdd_PK")
                    .IsClustered(false);

                entity.ToTable("tblObjectives_UserAdd");

                entity.Property(e => e.ObjId).HasColumnName("ObjID");

                entity.Property(e => e.ObjCorid).HasColumnName("Obj_CORID");

                entity.Property(e => e.ObjDateAdded)
                    .HasColumnType("datetime")
                    .HasColumnName("Obj_DateAdded")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ObjIsAdded)
                    .HasColumnName("Obj_IsAdded")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ObjText)
                    .IsRequired()
                    .HasColumnName("Obj_Text");

                entity.Property(e => e.ObjType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Obj_Type");

                entity.Property(e => e.Sequence).HasDefaultValueSql("((999))");
            });

            modelBuilder.Entity<TblOjtevaluator>(entity =>
            {
                entity.HasKey(e => e.Tqid);

                entity.ToTable("tblOJTEvaluators");

                entity.HasIndex(e => new { e.EvalEid, e.Ojthid }, "idx_Evaluator");

                entity.Property(e => e.Tqid).HasColumnName("TQID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalEid).HasColumnName("EVAL_EID");

                entity.Property(e => e.EvalMethod)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HasSigned)
                    .HasColumnName("hasSigned")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ojthid).HasColumnName("OJTHID");

                entity.Property(e => e.SignedDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblOjtevaluatorEidNavigations)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTEvalu__EID__4DD47EBD");

                entity.HasOne(d => d.EvalE)
                    .WithMany(p => p.TblOjtevaluatorEvalEs)
                    .HasForeignKey(d => d.EvalEid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTEva__EVAL___4EC8A2F6");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblOjtevaluators)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTEvalu__TID__4FBCC72F");
            });

            modelBuilder.Entity<TblOjthistory>(entity =>
            {
                entity.HasKey(e => e.Ojthid)
                    .HasName("aaaaatblOJTHistory_PK")
                    .IsClustered(false);

                entity.ToTable("tblOJTHistory");

                entity.HasIndex(e => e.Ojthid, "OJTHID");

                entity.HasIndex(e => e.ParentId, "ParentID");

                entity.Property(e => e.Ojthid).HasColumnName("OJTHID");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.EvalEid).HasColumnName("EVAL_EID");

                entity.Property(e => e.EvalId).HasColumnName("EValID");

                entity.Property(e => e.EvalMethod).HasMaxLength(255);

                entity.Property(e => e.Evaluator).HasMaxLength(2000);

                entity.Property(e => e.HasTraineeSigned).HasColumnName("hasTraineeSigned");

                entity.Property(e => e.ObservPeriod).HasMaxLength(50);

                entity.Property(e => e.OnlineSubmissionDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tnum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TNum");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblOjthistoryQuestion>(entity =>
            {
                entity.HasKey(e => e.Tqqid);

                entity.ToTable("tblOJTHistoryQuestions");

                entity.HasIndex(e => e.Ojthid, "idx_OJTID2");

                entity.Property(e => e.Tqqid).HasColumnName("TQQID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalEid).HasColumnName("EVAL_EID");

                entity.Property(e => e.Ojthid).HasColumnName("OJTHID");

                entity.Property(e => e.Tqanswer)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TQAnswer");

                entity.Property(e => e.Tqnumber).HasColumnName("TQNumber");

                entity.Property(e => e.Tqquestion)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TQQuestion");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblOjthistoryQuestionEidNavigations)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHisto__EID__50B0EB68");

                entity.HasOne(d => d.EvalE)
                    .WithMany(p => p.TblOjthistoryQuestionEvalEs)
                    .HasForeignKey(d => d.EvalEid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHis__EVAL___51A50FA1");

                entity.HasOne(d => d.Ojth)
                    .WithMany(p => p.TblOjthistoryQuestions)
                    .HasForeignKey(d => d.Ojthid)
                    .HasConstraintName("FK__tblOJTHis__OJTHI__529933DA");
            });

            modelBuilder.Entity<TblOjthistoryStep>(entity =>
            {
                entity.HasKey(e => e.Tqsid);

                entity.ToTable("tblOJTHistorySteps");

                entity.HasIndex(e => e.Ojthid, "idx_OJTID1");

                entity.Property(e => e.Tqsid).HasColumnName("TQSID");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalEid).HasColumnName("EVAL_EID");

                entity.Property(e => e.IsQualified).HasColumnName("isQualified");

                entity.Property(e => e.Ojthid).HasColumnName("OJTHID");

                entity.Property(e => e.StepDesc)
                    .IsRequired()
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.StepEvalDate).HasColumnType("date");

                entity.Property(e => e.StepNum)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TidStep).HasColumnName("TID_Step");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblOjthistoryStepEidNavigations)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHisto__EID__538D5813");

                entity.HasOne(d => d.EvalE)
                    .WithMany(p => p.TblOjthistoryStepEvalEs)
                    .HasForeignKey(d => d.EvalEid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHis__EVAL___54817C4C");

                entity.HasOne(d => d.Ojth)
                    .WithMany(p => p.TblOjthistorySteps)
                    .HasForeignKey(d => d.Ojthid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHis__OJTHI__5575A085");

                entity.HasOne(d => d.TidStepNavigation)
                    .WithMany(p => p.TblOjthistorySteps)
                    .HasForeignKey(d => d.TidStep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblOJTHis__TID_S__5669C4BE");
            });

            modelBuilder.Entity<TblOjtinstruction>(entity =>
            {
                entity.HasKey(e => e.Iid);

                entity.ToTable("tblOJTInstructions");

                entity.Property(e => e.Iid).HasColumnName("IID");

                entity.Property(e => e.Instruction).IsUnicode(false);
            });

            modelBuilder.Entity<TblOjtreportSelectedSetting>(entity =>
            {
                entity.HasKey(e => e.SettingsId);

                entity.ToTable("tblOJTReportSelectedSettings");

                entity.HasIndex(e => new { e.Raby, e.Title }, "IX_tblOJTReportSelectedSettings")
                    .IsUnique();

                entity.Property(e => e.SettingsId).HasColumnName("SettingsID");

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Raby)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("RABy")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.SettingsDesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblOjtreportSettingsDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblOJTReportSettingsDetail");

                entity.Property(e => e.ControlName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SettingsId).HasColumnName("SettingsID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Settings)
                    .WithMany()
                    .HasForeignKey(d => d.SettingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOJTReportSettingsDetail_tblOJTReportSelectedSettings");
            });

            modelBuilder.Entity<TblOnlineClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblOnlineClasses");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.DateCompleted).HasColumnType("datetime");

                entity.Property(e => e.DatePaused).HasColumnType("datetime");

                entity.Property(e => e.DateRescheduled).HasColumnType("datetime");

                entity.Property(e => e.DateStarted).HasColumnType("datetime");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.ScheduledDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPerspectiveCourse>(entity =>
            {
                entity.ToTable("tblPerspectiveCourse");

                entity.Property(e => e.ConvertedDate).HasColumnType("datetime");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CourseNumber)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfAnalysis).HasColumnType("datetime");

                entity.Property(e => e.GoalsResponse).IsUnicode(false);

                entity.Property(e => e.GoalsResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("GoalsResponse_LastUpdated");

                entity.Property(e => e.GoalsResponseLastUpdatedBy).HasColumnName("GoalsResponse_LastUpdatedBy");

                entity.Property(e => e.KnowledgeResponse).IsUnicode(false);

                entity.Property(e => e.KnowledgeResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("KnowledgeResponse_LastUpdated");

                entity.Property(e => e.KnowledgeResponseLastUpdatedBy).HasColumnName("KnowledgeResponse_LastUpdatedBy");

                entity.Property(e => e.LearningMetricResponse).IsUnicode(false);

                entity.Property(e => e.LearningMetricResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("LearningMetricResponse_LastUpdated");

                entity.Property(e => e.LearningMetricResponseLastUpdatedBy).HasColumnName("LearningMetricResponse_LastUpdatedBy");

                entity.Property(e => e.MotivationResponse).IsUnicode(false);

                entity.Property(e => e.MotivationResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("MotivationResponse_LastUpdated");

                entity.Property(e => e.MotivationResponseLastUpdatedBy).HasColumnName("MotivationResponse_LastUpdatedBy");

                entity.Property(e => e.NtrComments).IsUnicode(false);

                entity.Property(e => e.PerceptionResponse).IsUnicode(false);

                entity.Property(e => e.PerceptionResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerceptionResponse_LastUpdated");

                entity.Property(e => e.PerceptionResponseLastUpdatedBy).HasColumnName("PerceptionResponse_LastUpdatedBy");

                entity.Property(e => e.PerformanceMetricResponse).IsUnicode(false);

                entity.Property(e => e.PerformanceMetricResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerformanceMetricResponse_LastUpdated");

                entity.Property(e => e.PerformanceMetricResponseLastUpdatedBy).HasColumnName("PerformanceMetricResponse_LastUpdatedBy");

                entity.Property(e => e.PerformanceObjectivesResponse).IsUnicode(false);

                entity.Property(e => e.PerformanceObjectivesResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerformanceObjectivesResponse_LastUpdated");

                entity.Property(e => e.PerformanceObjectivesResponseLastUpdatedBy).HasColumnName("PerformanceObjectivesResponse_LastUpdatedBy");

                entity.Property(e => e.ProblemStatementResponse).IsUnicode(false);

                entity.Property(e => e.ProblemStatementResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ProblemStatementResponse_LastUpdated");

                entity.Property(e => e.ProblemStatementResponseLastUpdatedBy).HasColumnName("ProblemStatementResponse_LastUpdatedBy");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResultsResponse).IsUnicode(false);

                entity.Property(e => e.ResultsResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ResultsResponse_LastUpdated");

                entity.Property(e => e.ResultsResponseLastUpdatedBy).HasColumnName("ResultsResponse_LastUpdatedBy");

                entity.Property(e => e.ReviewerNotes).IsUnicode(false);

                entity.Property(e => e.TblCourseCorId).HasColumnName("tblCourseCorID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.TblPerspectiveCourseInstructors)
                    .HasForeignKey(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblPerspe__Instr__4B97FBE7");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.TblPerspectiveCourses)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblPerspe__Provi__4C8C2020");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.TblPerspectiveCourseReviewers)
                    .HasForeignKey(d => d.ReviewerId)
                    .HasConstraintName("FK__tblPerspe__Revie__4D804459");

                entity.HasOne(d => d.TblCourseCor)
                    .WithMany(p => p.TblPerspectiveCourses)
                    .HasForeignKey(d => d.TblCourseCorId)
                    .HasConstraintName("FK__tblPerspe__tblCo__4E746892");
            });

            modelBuilder.Entity<TblPerspectiveCourseArchive>(entity =>
            {
                entity.ToTable("tblPerspectiveCourse_Archives");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblPerspectiveCourseArchives)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblPerspectiveCourse_Archives_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblPerspectiveCourseSnapshot>(entity =>
            {
                entity.ToTable("tblPerspectiveCourse_Snapshot");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.GoalsResponse).IsUnicode(false);

                entity.Property(e => e.GoalsResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("GoalsResponse_LastUpdated");

                entity.Property(e => e.GoalsResponseLastUpdatedBy).HasColumnName("GoalsResponse_LastUpdatedBy");

                entity.Property(e => e.KnowledgeResponse).IsUnicode(false);

                entity.Property(e => e.KnowledgeResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("KnowledgeResponse_LastUpdated");

                entity.Property(e => e.KnowledgeResponseLastUpdatedBy).HasColumnName("KnowledgeResponse_LastUpdatedBy");

                entity.Property(e => e.LearningMetricResponse).IsUnicode(false);

                entity.Property(e => e.LearningMetricResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("LearningMetricResponse_LastUpdated");

                entity.Property(e => e.LearningMetricResponseLastUpdatedBy).HasColumnName("LearningMetricResponse_LastUpdatedBy");

                entity.Property(e => e.MotivationResponse).IsUnicode(false);

                entity.Property(e => e.MotivationResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("MotivationResponse_LastUpdated");

                entity.Property(e => e.MotivationResponseLastUpdatedBy).HasColumnName("MotivationResponse_LastUpdatedBy");

                entity.Property(e => e.PerceptionResponse).IsUnicode(false);

                entity.Property(e => e.PerceptionResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerceptionResponse_LastUpdated");

                entity.Property(e => e.PerceptionResponseLastUpdatedBy).HasColumnName("PerceptionResponse_LastUpdatedBy");

                entity.Property(e => e.PerformanceMetricResponse).IsUnicode(false);

                entity.Property(e => e.PerformanceMetricResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerformanceMetricResponse_LastUpdated");

                entity.Property(e => e.PerformanceMetricResponseLastUpdatedBy).HasColumnName("PerformanceMetricResponse_LastUpdatedBy");

                entity.Property(e => e.PerformanceObjectivesResponse).IsUnicode(false);

                entity.Property(e => e.PerformanceObjectivesResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("PerformanceObjectivesResponse_LastUpdated");

                entity.Property(e => e.PerformanceObjectivesResponseLastUpdatedBy).HasColumnName("PerformanceObjectivesResponse_LastUpdatedBy");

                entity.Property(e => e.PerspectiveCourseId).HasColumnName("PerspectiveCourseID");

                entity.Property(e => e.ProblemStatementResponse).IsUnicode(false);

                entity.Property(e => e.ProblemStatementResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ProblemStatementResponse_LastUpdated");

                entity.Property(e => e.ProblemStatementResponseLastUpdatedBy).HasColumnName("ProblemStatementResponse_LastUpdatedBy");

                entity.Property(e => e.ResultsResponse).IsUnicode(false);

                entity.Property(e => e.ResultsResponseLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ResultsResponse_LastUpdated");

                entity.Property(e => e.ResultsResponseLastUpdatedBy).HasColumnName("ResultsResponse_LastUpdatedBy");

                entity.HasOne(d => d.PerspectiveCourse)
                    .WithMany(p => p.TblPerspectiveCourseSnapshots)
                    .HasForeignKey(d => d.PerspectiveCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblPerspe__Persp__5150D53D");
            });

            modelBuilder.Entity<TblPhase>(entity =>
            {
                entity.HasKey(e => e.CoursePhaseId);

                entity.ToTable("tblPhases");

                entity.Property(e => e.CoursePhaseId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblPosTaskAnnualReview>(entity =>
            {
                entity.HasKey(e => e.Tharid);

                entity.ToTable("tblPosTaskAnnualReview");

                entity.Property(e => e.Tharid).HasColumnName("THARID");

                entity.Property(e => e.Arnotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("ARNotes");

                entity.Property(e => e.ArposId).HasColumnName("ARPosID");

                entity.Property(e => e.ArrevBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ARRevBy");

                entity.Property(e => e.ArrevDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ARRevDate");

                entity.Property(e => e.Arsig)
                    .HasColumnType("image")
                    .HasColumnName("ARSig");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Arpos)
                    .WithMany(p => p.TblPosTaskAnnualReviews)
                    .HasForeignKey(d => d.ArposId)
                    .HasConstraintName("FK_tblPosTaskAnnualReview_tblPositions");
            });

            modelBuilder.Entity<TblPosition>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("tblPositions");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pdescription)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PDescription");

                entity.Property(e => e.Pnum).HasColumnName("PNum");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblPositionTasksHistory>(entity =>
            {
                entity.HasKey(e => e.Pthid);

                entity.ToTable("tblPositionTasksHistory");

                entity.Property(e => e.Pthid).HasColumnName("PTHID");

                entity.Property(e => e.Baseline).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChangeDateStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pthcritical).HasColumnName("PTHCritical");

                entity.Property(e => e.Pthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PTHDate");

                entity.Property(e => e.Pthnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PTHNote");

                entity.Property(e => e.PthrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("PTHRevisedBy");

                entity.Property(e => e.Pthtype)
                    .HasMaxLength(50)
                    .HasColumnName("PTHType");

                entity.Property(e => e.Thid).HasColumnName("THID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblPositionTrainingProgram>(entity =>
            {
                entity.HasKey(e => e.Ptpid);

                entity.ToTable("tblPosition_TrainingProgram");

                entity.HasIndex(e => new { e.Pid, e.ProgramType, e.Tpdate, e.Revision }, "IX_tblPosition_TrainingProgram")
                    .IsUnique();

                entity.Property(e => e.Ptpid).HasColumnName("PTPID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.ProgramTitle)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramType)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Revision).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Tpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TPDate");

                entity.Property(e => e.TpendDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TPEndDate");
            });

            modelBuilder.Entity<TblProcReleaseEmpSummary>(entity =>
            {
                entity.HasKey(e => e.RelId);

                entity.ToTable("tblProcRelease_EmpSummary");

                entity.Property(e => e.RelId).HasColumnName("RelID");

                entity.Property(e => e.CompletedDate).HasColumnType("date");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EmployeeProcResponse).HasColumnName("employee_procResponse");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PosComments).IsUnicode(false);

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.ProcEmpstatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ProcEMPStatus");

                entity.Property(e => e.ProcEndDateAtRel)
                    .HasColumnType("date")
                    .HasColumnName("Proc_EndDate_AtRel");

                entity.Property(e => e.ProcStartDateAtRel)
                    .HasColumnType("date")
                    .HasColumnName("Proc_StartDate_AtRel");

                entity.Property(e => e.ReleasedDate).HasColumnType("date");

                entity.Property(e => e.ReleasedToEmp).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblProcedure>(entity =>
            {
                entity.HasKey(e => e.Prid);

                entity.ToTable("tblProcedures");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Iaid).HasColumnName("IAID");

                entity.Property(e => e.PrEmpReleaseText)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PR_EmpReleaseText");

                entity.Property(e => e.PrEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PR_EndDate");

                entity.Property(e => e.PrOnlineStatus)
                    .HasColumnName("PR_OnlineStatus")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PrStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PR_StartDate");

                entity.Property(e => e.Prinactive).HasColumnName("PRInactive");

                entity.Property(e => e.Prnum).HasColumnName("PRNum");

                entity.Property(e => e.PrrevDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PRRevDate");

                entity.Property(e => e.PrrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("PRRevisedBy");

                entity.Property(e => e.Prrevision).HasColumnName("PRRevision");

                entity.Property(e => e.Prseries)
                    .HasMaxLength(50)
                    .HasColumnName("PRSeries");

                entity.Property(e => e.Prtitle)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblProcedureResource>(entity =>
            {
                entity.HasKey(e => e.Prrid);

                entity.ToTable("tblProcedureResources");

                entity.Property(e => e.Prrid).HasColumnName("PRRID");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Prrchapter)
                    .HasMaxLength(50)
                    .HasColumnName("PRRChapter");

                entity.Property(e => e.Prrcomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRRComments");

                entity.Property(e => e.Prrhyperlink)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRRHyperlink");

                entity.Property(e => e.PrrhyperlinkText)
                    .HasMaxLength(255)
                    .HasColumnName("PRRHyperlinkText");

                entity.Property(e => e.Prrnum).HasColumnName("PRRNum");

                entity.Property(e => e.Prrsection)
                    .HasMaxLength(50)
                    .HasColumnName("PRRSection");

                entity.Property(e => e.Prrtitle)
                    .HasMaxLength(50)
                    .HasColumnName("PRRTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblProceduresHistory>(entity =>
            {
                entity.HasKey(e => e.Prhid);

                entity.ToTable("tblProceduresHistory");

                entity.Property(e => e.Prhid).HasColumnName("PRHID");

                entity.Property(e => e.Iaid).HasColumnName("IAID");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Prnum).HasColumnName("PRNum");

                entity.Property(e => e.PrrevDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PRRevDate");

                entity.Property(e => e.PrrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("PRRevisedBy");

                entity.Property(e => e.Prrevision).HasColumnName("PRRevision");

                entity.Property(e => e.Prseries)
                    .HasMaxLength(50)
                    .HasColumnName("PRSeries");

                entity.Property(e => e.Prtitle)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblProceduresTasksHistory>(entity =>
            {
                entity.HasKey(e => e.Prthid);

                entity.ToTable("tblProceduresTasksHistory");

                entity.Property(e => e.Prthid).HasColumnName("PRTHID");

                entity.Property(e => e.Prhid).HasColumnName("PRHID");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblProctor>(entity =>
            {
                entity.HasKey(e => e.ProctorId);

                entity.ToTable("tblProctors");

                entity.Property(e => e.ProctorId).HasColumnName("ProctorID");

                entity.Property(e => e.ProctorName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblQtsmgrOrganization>(entity =>
            {
                entity.HasKey(e => new { e.Eid, e.OrgId });

                entity.ToTable("tblQTSMgr_Organization");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.OrgId).HasColumnName("OrgID");

                entity.Property(e => e.AddedBy)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.TblQtsmgrOrganizations)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblQTSMgr_Organization_tblEmployee");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.TblQtsmgrOrganizations)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblQTSMgr_Organization_lktblOrganizations");
            });

            modelBuilder.Entity<TblReportingManager>(entity =>
            {
                entity.HasKey(e => e.Rmid)
                    .HasName("PK__tblRepor__474689A835D11ED4");

                entity.ToTable("tblReportingManagers");

                entity.Property(e => e.Rmid).HasColumnName("RMID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsFixedLength(true);

                entity.Property(e => e.Ename)
                    .HasMaxLength(101)
                    .HasColumnName("EName")
                    .HasComputedColumnSql("(([FName]+' ')+[LName])", false);

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .HasColumnName("FName");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .HasColumnName("LName");

                entity.Property(e => e.RmEid).HasColumnName("RM_EID");
            });

            modelBuilder.Entity<TblReportsField>(entity =>
            {
                entity.HasKey(e => new { e.ReportAcronum, e.FieldName });

                entity.ToTable("tblReportsFields");

                entity.Property(e => e.ReportAcronum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FieldName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldText).IsUnicode(false);
            });

            modelBuilder.Entity<TblRsaw>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW");

                entity.Property(e => e.ApplicableFunction).HasMaxLength(20);

                entity.Property(e => e.ComplianceAssessmentDate).HasColumnType("datetime");

                entity.Property(e => e.ComplianceEnforcementAuthority).HasMaxLength(255);

                entity.Property(e => e.ComplianceMonitoringMethod).HasMaxLength(255);

                entity.Property(e => e.NamesOfAuditors)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Ncrnumber)
                    .HasMaxLength(255)
                    .HasColumnName("NCRNumber");

                entity.Property(e => e.R111response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R111Response");

                entity.Property(e => e.R11response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R11Response");

                entity.Property(e => e.R12response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R12Response");

                entity.Property(e => e.R13response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R13Response");

                entity.Property(e => e.R14question1).HasColumnName("R14Question1");

                entity.Property(e => e.R14question2).HasColumnName("R14Question2");

                entity.Property(e => e.R14response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R14Response");

                entity.Property(e => e.R1response)
                    .IsUnicode(false)
                    .HasColumnName("R1Response");

                entity.Property(e => e.R21question1).HasColumnName("R21Question1");

                entity.Property(e => e.R21response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R21Response");

                entity.Property(e => e.R2response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R2Response");

                entity.Property(e => e.R31question1).HasColumnName("R31Question1");

                entity.Property(e => e.R31question2).HasColumnName("R31Question2");

                entity.Property(e => e.R31response1)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R31Response1");

                entity.Property(e => e.R31response2)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R31Response2");

                entity.Property(e => e.R3response)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("R3Response");

                entity.Property(e => e.RegisteredEntity).HasMaxLength(255);

                entity.Property(e => e.Rsawid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsaw2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_2");

                entity.Property(e => e.ApplicableFunction).HasMaxLength(20);

                entity.Property(e => e.ComplianceAssessmentDate).HasColumnType("datetime");

                entity.Property(e => e.ComplianceEnforcementAuthority).HasMaxLength(255);

                entity.Property(e => e.ComplianceMonitoringMethod).HasMaxLength(255);

                entity.Property(e => e.NamesOfAuditors)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Ncrnumber)
                    .HasMaxLength(255)
                    .HasColumnName("NCRNumber");

                entity.Property(e => e.R1response)
                    .IsUnicode(false)
                    .HasColumnName("R1Response");

                entity.Property(e => e.R2response)
                    .IsUnicode(false)
                    .HasColumnName("R2Response");

                entity.Property(e => e.R3question).HasColumnName("R3Question");

                entity.Property(e => e.R3response1)
                    .IsUnicode(false)
                    .HasColumnName("R3Response_1");

                entity.Property(e => e.R3response2)
                    .IsUnicode(false)
                    .HasColumnName("R3Response_2");

                entity.Property(e => e.R4question1).HasColumnName("R4Question_1");

                entity.Property(e => e.R4question2).HasColumnName("R4Question_2");

                entity.Property(e => e.R4response1)
                    .IsUnicode(false)
                    .HasColumnName("R4Response_1");

                entity.Property(e => e.R4response2)
                    .IsUnicode(false)
                    .HasColumnName("R4Response_2");

                entity.Property(e => e.R4response3)
                    .IsUnicode(false)
                    .HasColumnName("R4Response_3");

                entity.Property(e => e.R5response)
                    .IsUnicode(false)
                    .HasColumnName("R5Response");

                entity.Property(e => e.R6response)
                    .IsUnicode(false)
                    .HasColumnName("R6Response");

                entity.Property(e => e.RegisteredEntity).HasMaxLength(255);

                entity.Property(e => e.Rsawid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsaw2Change>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_2_Changes");

                entity.Property(e => e.RsawcdateIdentified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWCDateIdentified");

                entity.Property(e => e.RsawcdateImplimented)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWCDateImplimented");

                entity.Property(e => e.Rsawcdescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWCDescription");

                entity.Property(e => e.Rsawcid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWCID");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsaw2Evidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_2_Evidence");

                entity.Property(e => e.Rsawedate)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWEDate");

                entity.Property(e => e.Rsawedescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWEDescription");

                entity.Property(e => e.RsawedocSection)
                    .HasMaxLength(10)
                    .HasColumnName("RSAWEDocSection");

                entity.Property(e => e.RsawefileName)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWEFileName");

                entity.Property(e => e.Rsaweid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWEID");

                entity.Property(e => e.Rsawepage).HasColumnName("RSAWEPage");

                entity.Property(e => e.Rsawerevision)
                    .HasMaxLength(20)
                    .HasColumnName("RSAWERevision");

                entity.Property(e => e.Rsawesection)
                    .HasMaxLength(20)
                    .HasColumnName("RSAWESection");

                entity.Property(e => e.RsawesectionTitle)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWESectionTitle");

                entity.Property(e => e.Rsawetitle)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWETitle");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsaw2Expert>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_2_Experts");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Rsawxid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWXID");

                entity.Property(e => e.Rsawxname)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXName");

                entity.Property(e => e.Rsawxorganization)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXOrganization");

                entity.Property(e => e.Rsawxrequirement)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXRequirement");

                entity.Property(e => e.Rsawxtitle)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsaw2Modified>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_2_Modified");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.RsawmdateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWMDateModified");

                entity.Property(e => e.RsawmdateVerified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWMDateVerified");

                entity.Property(e => e.Rsawmdescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWMDescription");

                entity.Property(e => e.Rsawmid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWMID");

                entity.Property(e => e.Rsawmmethod)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWMMethod");

                entity.Property(e => e.Rsawmoperator)
                    .HasMaxLength(50)
                    .HasColumnName("RSAWMOperator");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsawChange>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_Changes");

                entity.Property(e => e.RsawcdateIdentified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWCDateIdentified");

                entity.Property(e => e.RsawcdateImplimented)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWCDateImplimented");

                entity.Property(e => e.Rsawcdescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWCDescription");

                entity.Property(e => e.Rsawcid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWCID");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsawEvidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_Evidence");

                entity.Property(e => e.Rsawedate)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWEDate");

                entity.Property(e => e.Rsawedescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWEDescription");

                entity.Property(e => e.RsawedocSection)
                    .HasMaxLength(10)
                    .HasColumnName("RSAWEDocSection");

                entity.Property(e => e.RsawefileName)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWEFileName");

                entity.Property(e => e.Rsaweid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWEID");

                entity.Property(e => e.Rsawepage).HasColumnName("RSAWEPage");

                entity.Property(e => e.Rsawerevision)
                    .HasMaxLength(20)
                    .HasColumnName("RSAWERevision");

                entity.Property(e => e.Rsawesection)
                    .HasMaxLength(20)
                    .HasColumnName("RSAWESection");

                entity.Property(e => e.RsawesectionTitle)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWESectionTitle");

                entity.Property(e => e.Rsawetitle)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("RSAWETitle");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsawExpert>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_Experts");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.Rsawxid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWXID");

                entity.Property(e => e.Rsawxname)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXName");

                entity.Property(e => e.Rsawxorganization)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXOrganization");

                entity.Property(e => e.Rsawxrequirement)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXRequirement");

                entity.Property(e => e.Rsawxtitle)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWXTitle");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblRsawModified>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRSAW_Modified");

                entity.Property(e => e.Rsawid).HasColumnName("RSAWID");

                entity.Property(e => e.RsawmdateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWMDateModified");

                entity.Property(e => e.RsawmdateVerified)
                    .HasColumnType("datetime")
                    .HasColumnName("RSAWMDateVerified");

                entity.Property(e => e.Rsawmdescription)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWMDescription");

                entity.Property(e => e.Rsawmid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RSAWMID");

                entity.Property(e => e.Rsawmmethod)
                    .HasMaxLength(255)
                    .HasColumnName("RSAWMMethod");

                entity.Property(e => e.Rsawmoperator)
                    .HasMaxLength(50)
                    .HasColumnName("RSAWMOperator");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblSafetyHazard>(entity =>
            {
                entity.HasKey(e => e.Shzid)
                    .HasName("PK__tblSafet__F5E09673D025F4BF");

                entity.ToTable("tblSafetyHazards");

                entity.HasIndex(e => e.Shztitle, "UQ__tblSafet__9AB9154A02FC0BC7")
                    .IsUnique();

                entity.HasIndex(e => new { e.Hzcid, e.Shznum }, "uc_SHZNum")
                    .IsUnique();

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.Property(e => e.Hzcid).HasColumnName("HZCID");

                entity.Property(e => e.Inactive).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ppe)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("PPE")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Shzdesc)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("SHZDesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Shznum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SHZNum");

                entity.Property(e => e.Shztitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SHZTitle");

                entity.HasOne(d => d.Hzc)
                    .WithMany(p => p.TblSafetyHazards)
                    .HasForeignKey(d => d.Hzcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__HZCID__60E75331");
            });

            modelBuilder.Entity<TblSafetyHazardAbatement>(entity =>
            {
                entity.HasKey(e => e.Shzabid)
                    .HasName("PK__tblSafet__9DCED66ECB818F83");

                entity.ToTable("tblSafetyHazardAbatements");

                entity.HasIndex(e => new { e.Shzid, e.Anum }, "uc_SHZABID")
                    .IsUnique();

                entity.Property(e => e.Shzabid).HasColumnName("SHZABID");

                entity.Property(e => e.Anum).HasColumnName("ANum");

                entity.Property(e => e.Shzabatement)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SHZAbatement");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSafetyHazardAbatements)
                    .HasForeignKey(d => d.Shzid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__SHZID__5B2E79DB");
            });

            modelBuilder.Entity<TblSafetyHazardCategory>(entity =>
            {
                entity.HasKey(e => e.Hzcid)
                    .HasName("PK__tblSafet__E80FD70B14C380E6");

                entity.ToTable("tblSafetyHazardCategories");

                entity.HasIndex(e => e.Hzcategory, "UQ__tblSafet__7A4420A8482F065B")
                    .IsUnique();

                entity.Property(e => e.Hzcid).HasColumnName("HZCID");

                entity.Property(e => e.Hzcategory)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("HZCategory");

                entity.Property(e => e.Hzpriority)
                    .HasColumnName("HZPriority")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblSafetyHazardControl>(entity =>
            {
                entity.HasKey(e => e.Shzctid)
                    .HasName("PK__tblSafet__F07C2329670452C0");

                entity.ToTable("tblSafetyHazardControls");

                entity.HasIndex(e => new { e.Shzid, e.Cnum }, "uc_SHZCTID")
                    .IsUnique();

                entity.Property(e => e.Shzctid).HasColumnName("SHZCTID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.Shzcontrol)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SHZControl");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSafetyHazardControls)
                    .HasForeignKey(d => d.Shzid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__SHZID__5C229E14");
            });

            modelBuilder.Entity<TblSafetyHazardEo>(entity =>
            {
                entity.HasKey(e => e.Shzskid)
                    .HasName("PK__tblSafet__359C86AB4F363B72");

                entity.ToTable("tblSafetyHazardEOs");

                entity.HasIndex(e => new { e.Shzid, e.Skid }, "uc_SHZSKID")
                    .IsUnique();

                entity.Property(e => e.Shzskid).HasColumnName("SHZSKID");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSafetyHazardEos)
                    .HasForeignKey(d => d.Shzid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__SHZID__5D16C24D");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblSafetyHazardEos)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafetyH__SKID__5E0AE686");
            });

            modelBuilder.Entity<TblSafetyHazardIla>(entity =>
            {
                entity.HasKey(e => e.Shzcorid)
                    .HasName("PK__tblSafet__A5DFE8917F4E1F82");

                entity.ToTable("tblSafetyHazardILAs");

                entity.HasIndex(e => new { e.Shzid, e.Corid }, "uc_SHZCORID")
                    .IsUnique();

                entity.Property(e => e.Shzcorid).HasColumnName("SHZCORID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblSafetyHazardIlas)
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__CORID__5EFF0ABF");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSafetyHazardIlas)
                    .HasForeignKey(d => d.Shzid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__SHZID__5FF32EF8");
            });

            modelBuilder.Entity<TblSafetyHazardTask>(entity =>
            {
                entity.HasKey(e => e.Shztid)
                    .HasName("PK__tblSafet__FC70E0CBEAC08029");

                entity.ToTable("tblSafetyHazardTasks");

                entity.HasIndex(e => new { e.Shzid, e.Tid }, "uc_SHZTID")
                    .IsUnique();

                entity.Property(e => e.Shztid).HasColumnName("SHZTID");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSafetyHazardTasks)
                    .HasForeignKey(d => d.Shzid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafety__SHZID__61DB776A");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblSafetyHazardTasks)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSafetyHa__TID__62CF9BA3");
            });

            modelBuilder.Entity<TblSaveQuery>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblSaveQuery");

                entity.Property(e => e.GroupBy)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.QueryDate).HasColumnType("datetime");

                entity.Property(e => e.QueryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Query_ID");

                entity.Property(e => e.QueryId1).HasColumnName("QueryID");

                entity.Property(e => e.QueryName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.QueryText)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.TabId).HasColumnName("Tab_ID");
            });

            modelBuilder.Entity<TblSaveQueryCriterion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblSaveQuery_Criteria");

                entity.Property(e => e.CriteriaText)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CriteriaVal)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FkQueryId).HasColumnName("fk_QueryID");
            });

            modelBuilder.Entity<TblSaveQuerySelectedField>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblSaveQuery_SelectedFields");

                entity.Property(e => e.FieldText)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FieldVal)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FkQueryId).HasColumnName("fk_QueryID");
            });

            modelBuilder.Entity<TblSkProcedure>(entity =>
            {
                entity.HasKey(e => new { e.Skid, e.ProcId });

                entity.ToTable("tblSK_Procedures");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.ProcId).HasColumnName("ProcID");

                entity.HasOne(d => d.Proc)
                    .WithMany(p => p.TblSkProcedures)
                    .HasForeignKey(d => d.ProcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSK_Procedures_tblProcedures");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblSkProcedures)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSK_Procedures_tblSkillsKnowledge");
            });

            modelBuilder.Entity<TblSkillsKnowledge>(entity =>
            {
                entity.HasKey(e => e.Skid)
                    .HasName("aaaaatblSkillsKnowledge_PK")
                    .IsClustered(false);

                entity.ToTable("tblSkillsKnowledge");

                entity.HasIndex(e => e.Cid, "CID");

                entity.HasIndex(e => e.Skid, "SKID");

                entity.HasIndex(e => e.Sknum, "SKNum");

                entity.HasIndex(e => e.SksubNum, "SKSubNum");

                entity.HasIndex(e => e.Cid, "tblCategoriestblSkillsKnowledge");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Cid)
                    .HasColumnName("CID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Sknum)
                    .HasColumnName("SKNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SksubNum)
                    .HasColumnName("SKSubNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.TblSkillsKnowledges)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("tblSkillsKnowledge_FK00");
            });

            modelBuilder.Entity<TblSmudCourseDesignReviewer>(entity =>
            {
                entity.ToTable("tblSMUD_CourseDesignReviewers");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudCourseDesignReviewers)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUD_CourseDesignReviewers_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudCourseDevelopReviewer>(entity =>
            {
                entity.ToTable("tblSMUD_CourseDevelopReviewers");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudCourseDevelopReviewers)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUD_CourseDevelopReviewers_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudCourseImplementReviewer>(entity =>
            {
                entity.ToTable("tblSMUD_CourseImplementReviewers");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudCourseImplementReviewers)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUD_CourseImplementReviewers_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudDesignDefaultView>(entity =>
            {
                entity.ToTable("tblSMUD_DesignDefaultView");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSmudTrainingTopic>(entity =>
            {
                entity.HasKey(e => e.Ttid);

                entity.ToTable("tblSMUD_TrainingTopics");

                entity.Property(e => e.Ttid).HasColumnName("TTID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Tthid).HasColumnName("TTHID");

                entity.HasOne(d => d.Tth)
                    .WithMany(p => p.TblSmudTrainingTopics)
                    .HasForeignKey(d => d.Tthid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSMUD_TrainingTopics_tblSMUD_TrainingTopicsHeading");
            });

            modelBuilder.Entity<TblSmudTrainingTopicsHeading>(entity =>
            {
                entity.HasKey(e => e.Tthid);

                entity.ToTable("tblSMUD_TrainingTopicsHeading");

                entity.Property(e => e.Tthid)
                    .ValueGeneratedNever()
                    .HasColumnName("TTHID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSmudcourseDesign>(entity =>
            {
                entity.HasKey(e => e.CourseDetailId)
                    .HasName("PK_tblSmudCourse_Design");

                entity.ToTable("tblSMUDCourse_Design");

                entity.Property(e => e.ApplicationDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.CourseDetailsStatusLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EvaluationMethodResponseLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IlaapplicationStatus).HasColumnName("ILAApplicationStatus");

                entity.Property(e => e.IlaapplicationStatusLastUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ILAApplicationStatusLastUpdated");

                entity.Property(e => e.IlastatusLastUpdatedBy).HasColumnName("ILAStatusLastUpdatedBy");

                entity.Property(e => e.InstructorComments).IsUnicode(false);

                entity.Property(e => e.IsSelfRegistrationEmp).HasColumnName("IsSelfRegistrationEMP");

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.ObjectivesAndSegmentsLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.PilotDataApplicable)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrerequistiesStatusLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ProceduresStatusLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ResourcesStatusLastUpdated).HasColumnType("datetime");

                entity.Property(e => e.ReviewerComments).IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalCehs).HasColumnName("TotalCEHs");

                entity.Property(e => e.TrainingPlanStatusLastUpdated).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesigns)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSmudCourse_Design_tblSmudCourse_Design");
            });

            modelBuilder.Entity<TblSmudcourseDesignDelieveryMethod>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_DelieveryMethods");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Mid).HasColumnName("MID");

                entity.HasOne(d => d.MidNavigation)
                    .WithMany(p => p.TblSmudcourseDesignDelieveryMethods)
                    .HasForeignKey(d => d.Mid)
                    .HasConstraintName("FK_tblSMUDCourseDesign_DelieveryMethods_lkTblILAMethods");
            });

            modelBuilder.Entity<TblSmudcourseDesignEnablingObjective>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_EnablingObjectives");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignEnablingObjectives)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_EnablingObjectives_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseDesignNerc>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_NERC");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Nsid).HasColumnName("NSID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignNercs)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_NERC_tblPerspectiveCourse");

                entity.HasOne(d => d.Ns)
                    .WithMany(p => p.TblSmudcourseDesignNercs)
                    .HasForeignKey(d => d.Nsid)
                    .HasConstraintName("FK_tblSMUDCourseDesign_NERC_tblNERCStandards");
            });

            modelBuilder.Entity<TblSmudcourseDesignPrerequistie>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_Prerequistie");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignPrerequisties)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_Prerequistie_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseDesignProcedure>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_Procedures");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignProcedures)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSmudCourseDesign_Procedures_tblPerspectiveCourse");

                entity.HasOne(d => d.Proccedure)
                    .WithMany(p => p.TblSmudcourseDesignProcedures)
                    .HasForeignKey(d => d.ProccedureId)
                    .HasConstraintName("FK_tblSmudCourseDesign_Procedures_tblProcedures");
            });

            modelBuilder.Entity<TblSmudcourseDesignResource>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_Resources");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignResources)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_Resources_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseDesignSafetyHazard>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_SafetyHazards");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignSafetyHazards)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSmudCourseDesign_SafetyHazards_tblPerspectiveCourse");

                entity.HasOne(d => d.Shz)
                    .WithMany(p => p.TblSmudcourseDesignSafetyHazards)
                    .HasForeignKey(d => d.Shzid)
                    .HasConstraintName("FK_tblSmudCourseDesign_SafetyHazards_tblSafetyHazards");
            });

            modelBuilder.Entity<TblSmudcourseDesignSegment>(entity =>
            {
                entity.HasKey(e => e.SegmentId);

                entity.ToTable("tblSMUDCourseDesign_Segments");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignSegments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_Segments_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseDesignTargetAudience>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_TargetAudience");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.TblSmudcourseDesignTargetAudiences)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_TargetAudience_tblTargetAudience");
            });

            modelBuilder.Entity<TblSmudcourseDesignTask>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_Tasks");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignTasks)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_Tasks_tblSMUDCourseDesign_Tasks");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TblSmudcourseDesignTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_Tasks_tblTasks");
            });

            modelBuilder.Entity<TblSmudcourseDesignTrainingTopic>(entity =>
            {
                entity.ToTable("tblSMUDCourseDesign_TrainingTopics");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDesignTrainingTopics)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseDesign_TrainingTopics_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseDevelop>(entity =>
            {
                entity.HasKey(e => e.CourseDevelopId);

                entity.ToTable("tblSMUDCourse_Develop");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseDevelops)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourse_Develop_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseEvaluation>(entity =>
            {
                entity.ToTable("tblSMUDCourseEvaluation");

                entity.Property(e => e.Behaviour30DemailSent).HasColumnName("Behaviour30DEmailSent");

                entity.Property(e => e.Behaviour60DemailSent).HasColumnName("Behaviour60DEmailSent");

                entity.Property(e => e.Behaviour90DemailSent).HasColumnName("Behaviour90DEmailSent");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EvaluationCompletionDate).HasColumnType("datetime");

                entity.Property(e => e.Level1StatusLastUpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Level2StatusLastUpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Level3StatusLastUpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Level4EvaluationCompletionDate).HasColumnType("datetime");

                entity.Property(e => e.Level4StatusLastUpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Results30DemailSent).HasColumnName("Results30DEmailSent");

                entity.Property(e => e.Results60DemailSent).HasColumnName("Results60DEmailSent");

                entity.Property(e => e.Results90DemailSent).HasColumnName("Results90DEmailSent");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseEvaluations)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSMUDCourseEvaluation_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseEvaluationDefaultView>(entity =>
            {
                entity.ToTable("tblSMUDCourseEvaluation_DefaultView");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DefaultView).HasMaxLength(50);

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblSmudcourseEvaluationTestAnalysis>(entity =>
            {
                entity.ToTable("tblSMUDCourseEvaluation_TestAnalysis");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedAt");

                entity.Property(e => e.ModifiedBy).HasColumnName("modifiedBy");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.PassItemdifficulty).HasColumnName("passItemdifficulty");

                entity.Property(e => e.PassItemdiscrimination).HasColumnName("passItemdiscrimination");

                entity.Property(e => e.Passitemdistractors).HasColumnName("passitemdistractors");

                entity.Property(e => e.TestItemId).HasColumnName("testItemId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseEvaluationTestAnalyses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseEvaluation_TestAnalysis_tblPerspectiveCourse");

                entity.HasOne(d => d.TestItem)
                    .WithMany(p => p.TblSmudcourseEvaluationTestAnalyses)
                    .HasForeignKey(d => d.TestItemId)
                    .HasConstraintName("FK_tblSMUDCourseEvaluation_TestAnalysis_tblTestItems");
            });

            modelBuilder.Entity<TblSmudcourseImplement>(entity =>
            {
                entity.HasKey(e => e.CourseImplementId);

                entity.ToTable("tblSMUDCourse_Implement");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ModifiedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseImplements)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourse_Implement_tblPerspectiveCourse");
            });

            modelBuilder.Entity<TblSmudcourseImplementClassSchedule>(entity =>
            {
                entity.HasKey(e => e.CourseScheduleId);

                entity.ToTable("tblSMUDCourseImplement_ClassSchedule");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSmudcourseImplementClassSchedules)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_tblPerspectiveCourse");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.TblSmudcourseImplementClassSchedules)
                    .HasForeignKey(d => d.EvaluationId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_tblForms");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.TblSmudcourseImplementClassSchedules)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_lktblInstructors");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TblSmudcourseImplementClassSchedules)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_lktblLocations");

                entity.HasOne(d => d.RetakeTest)
                    .WithMany(p => p.TblSmudcourseImplementClassScheduleRetakeTests)
                    .HasForeignKey(d => d.RetakeTestId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_tblTests1");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TblSmudcourseImplementClassScheduleTests)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_tblSMUDCourseImplement_ClassSchedule_tblTests");
            });

            modelBuilder.Entity<TblSmudsegmentsLinkObjective>(entity =>
            {
                entity.ToTable("tblSMUDSegments_LinkObjectives");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSmudsegmentsNercStandard>(entity =>
            {
                entity.ToTable("tblSMUDSegments_NercStandards");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblStudentEvaluationOverride>(entity =>
            {
                entity.HasKey(e => new { e.Clid, e.Fid, e.Fqid })
                    .HasName("aaaaatblStudentEvaluationOverrides_PK")
                    .IsClustered(false);

                entity.ToTable("tblStudentEvaluationOverrides");

                entity.HasIndex(e => e.Clid, "CLID");

                entity.HasIndex(e => e.Fid, "FID");

                entity.HasIndex(e => e.Fqid, "FQID");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqid).HasColumnName("FQID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OComments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("O_Comments");

                entity.Property(e => e.ORatingAverage)
                    .HasColumnName("O_RatingAverage")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ORatingHigh)
                    .HasColumnName("O_RatingHigh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ORatingLow)
                    .HasColumnName("O_RatingLow")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingAverage).HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingHigh).HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingLow).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblStudentForm>(entity =>
            {
                entity.HasKey(e => e.Sfid)
                    .HasName("aaaaatblStudentForms_PK")
                    .IsClustered(false);

                entity.ToTable("tblStudentForms");

                entity.HasIndex(e => e.Clid, "CLID");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.Fid, "FID");

                entity.HasIndex(e => e.RecordId, "RecordID");

                entity.HasIndex(e => e.Sfid, "SFID");

                entity.HasIndex(e => e.SelfPacedCorid, "SelfPacedCORID");

                entity.HasIndex(e => e.Fid, "{E4BD069C-AF56-430E-AC8F-AB19D72ED74F}");

                entity.Property(e => e.Sfid).HasColumnName("SFID");

                entity.Property(e => e.Clid)
                    .HasColumnName("CLID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EevaluationMethod)
                    .HasMaxLength(250)
                    .HasColumnName("EEvaluationMethod");

                entity.Property(e => e.Eevaluator)
                    .HasMaxLength(150)
                    .HasColumnName("EEvaluator");

                entity.Property(e => e.Eid)
                    .HasColumnName("EID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireOverride).HasDefaultValueSql("((0))");

                entity.Property(e => e.Fid)
                    .HasColumnName("FID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SelfPacedCorid)
                    .HasColumnName("SelfPacedCORID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sfcomplete).HasColumnName("SFComplete");

                entity.Property(e => e.Students).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.FidNavigation)
                    .WithMany(p => p.TblStudentForms)
                    .HasForeignKey(d => d.Fid)
                    .HasConstraintName("tblStudentForms_FK00");
            });

            modelBuilder.Entity<TblStudentFormsAnswer>(entity =>
            {
                entity.HasKey(e => e.Sfaid)
                    .HasName("aaaaatblStudentForms_Answers_PK")
                    .IsClustered(false);

                entity.ToTable("tblStudentForms_Answers");

                entity.HasIndex(e => e.Fqaid, "FQAID");

                entity.HasIndex(e => e.Fqid, "FQID");

                entity.HasIndex(e => e.Sfid, "SFID");

                entity.HasIndex(e => e.Fqid, "tblFormQuestionstblStudentForms_Answers");

                entity.HasIndex(e => e.Sfid, "{35FDE6AC-E57F-485E-9D91-AF00133A2F18}");

                entity.Property(e => e.Sfaid).HasColumnName("SFAID");

                entity.Property(e => e.Fqaid)
                    .HasColumnName("FQAID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Fqid)
                    .HasColumnName("FQID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sfacomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("SFAComments");

                entity.Property(e => e.Sfascore)
                    .HasColumnName("SFAScore")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SfascoreOverride)
                    .HasColumnName("SFAScore_Override")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Sfid)
                    .HasColumnName("SFID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Fq)
                    .WithMany(p => p.TblStudentFormsAnswers)
                    .HasForeignKey(d => d.Fqid)
                    .HasConstraintName("tblStudentForms_Answers_FK01");

                entity.HasOne(d => d.Sf)
                    .WithMany(p => p.TblStudentFormsAnswers)
                    .HasForeignKey(d => d.Sfid)
                    .HasConstraintName("tblStudentForms_Answers_FK00");
            });

            modelBuilder.Entity<TblSupportingDoc>(entity =>
            {
                entity.HasKey(e => e.Sdid);

                entity.ToTable("tblSupportingDocs");

                entity.Property(e => e.Sdid).HasColumnName("SDID");

                entity.Property(e => e.Hyperlink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MainId).HasColumnName("MainID");

                entity.Property(e => e.Sdnum).HasColumnName("SDNum");

                entity.Property(e => e.Sdtable)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SDTable");

                entity.Property(e => e.SupportingDocs)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTargetAudience>(entity =>
            {
                entity.HasKey(e => e.TargetAudienceId);

                entity.ToTable("tblTargetAudience");
            });

            modelBuilder.Entity<TblTask>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("aaaaatblTasks_PK")
                    .IsClustered(false);

                entity.ToTable("tblTasks");

                entity.HasIndex(e => e.Daid, "DAID");

                entity.HasIndex(e => e.Tnum, "TNum");

                entity.HasIndex(e => e.TsubNum, "TSubNum");

                entity.HasIndex(e => new { e.Daid, e.Tnum, e.TsubNum }, "uc_TNum")
                    .IsUnique();

                entity.HasIndex(e => e.Daid, "{CB957B40-E286-4C01-B243-9752DC0CBC9F}");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Daid)
                    .HasColumnName("DAID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Inactive).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tabbrev)
                    .HasMaxLength(255)
                    .HasColumnName("TAbbrev");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tnum)
                    .HasColumnName("TNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum)
                    .HasColumnName("TSubNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");

                entity.HasOne(d => d.Da)
                    .WithMany(p => p.TblTasks)
                    .HasForeignKey(d => d.Daid)
                    .HasConstraintName("tblTasks_FK00");
            });

            modelBuilder.Entity<TblTaskAuditChangeType>(entity =>
            {
                entity.HasKey(e => e.Tact);

                entity.ToTable("tblTaskAuditChangeType");

                entity.Property(e => e.Tact)
                    .ValueGeneratedNever()
                    .HasColumnName("TACT");

                entity.Property(e => e.TaskAuditChangeType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTaskIntroductionType>(entity =>
            {
                entity.HasKey(e => e.IntroTypeId);

                entity.ToTable("tblTaskIntroductionTypes");

                entity.Property(e => e.IntroTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("IntroTypeID");

                entity.Property(e => e.IntroTypeText)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTaskLinkage>(entity =>
            {
                entity.HasKey(e => e.Lid)
                    .HasName("PK__tblTaskL__C65557210F8FB58A");

                entity.ToTable("tblTaskLinkages");

                entity.Property(e => e.Lid).HasColumnName("LID");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PidImpacted).HasColumnName("PID_Impacted");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.TidImpacted).HasColumnName("TID_Impacted");

                entity.HasOne(d => d.RstblPositionsTask)
                    .WithMany(p => p.TblTaskLinkages)
                    .HasForeignKey(d => new { d.Pid, d.Tid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rstblPositions_Tasks_tblTaskLinkages");
            });

            modelBuilder.Entity<TblTaskSubStep>(entity =>
            {
                entity.HasKey(e => e.Tssid);

                entity.ToTable("tblTask_SubSteps");

                entity.Property(e => e.Tssid).HasColumnName("TSSID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tssdesc)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TSSDesc");

                entity.Property(e => e.Tssnum).HasColumnName("TSSNum");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblTaskSubSteps)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTask_SubSteps_tblTasks");
            });

            modelBuilder.Entity<TblTaskToolList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTaskToolList");

                entity.Property(e => e.TaskTool)
                    .IsRequired()
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Ttid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TTID");
            });

            modelBuilder.Entity<TblTasksAuditHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTasks_AuditHistory");

                entity.Property(e => e.ChangeDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeEntity)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PosId).HasColumnName("PosID");

                entity.Property(e => e.ProcId).HasColumnName("ProcID");

                entity.Property(e => e.Raby)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("RABy")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Tabbrev)
                    .HasMaxLength(255)
                    .HasColumnName("TAbbrev");

                entity.Property(e => e.Tahid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TAHID");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Thid).HasColumnName("THID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<TblTasksHistory>(entity =>
            {
                entity.HasKey(e => e.Thid);

                entity.ToTable("tblTasksHistory");

                entity.Property(e => e.Thid).HasColumnName("THID");

                entity.Property(e => e.Baseline).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChangeDateStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Inactive)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Thconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THConditions");

                entity.Property(e => e.Thcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THCriteria");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THNote");

                entity.Property(e => e.Thnum)
                    .HasMaxLength(50)
                    .HasColumnName("THNum");

                entity.Property(e => e.ThposHistory).HasColumnName("THPosHistory");

                entity.Property(e => e.ThprocList)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THProcList");

                entity.Property(e => e.Threferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THReferences");

                entity.Property(e => e.ThrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("THRevisedBy");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Thtools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THTools");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTasksIntroduction>(entity =>
            {
                entity.HasKey(e => e.Tiid);

                entity.ToTable("tblTasks_Introduction");

                entity.Property(e => e.Tiid).HasColumnName("TIID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblTasksIntroductions)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTasks_Introduction_tblTasks");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TblTasksIntroductions)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_tblTasks_Introduction_tblTaskIntroductionTypes");
            });

            modelBuilder.Entity<TblTasksQuestion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTasks_Questions");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tqanswer)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TQAnswer");

                entity.Property(e => e.Tqid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TQID");

                entity.Property(e => e.Tqnumber).HasColumnName("TQNumber");

                entity.Property(e => e.Tqquestion)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TQQuestion");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTasks_Questions_tblTasks");
            });

            modelBuilder.Entity<TblTasksReleasedQual>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTasksReleasedQual");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Qid).HasColumnName("QID");

                entity.Property(e => e.Qstatus).HasColumnName("QStatus");

                entity.Property(e => e.QualDate).HasColumnType("datetime");

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Trqid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TRQID");
            });

            modelBuilder.Entity<TblTasksSkillAssessment>(entity =>
            {
                entity.HasKey(e => e.Tsid)
                    .HasName("PK__tblTasks__82BB99BC757F3404");

                entity.ToTable("tblTasks_SkillAssessment");

                entity.Property(e => e.Tsid).HasColumnName("TSID");

                entity.Property(e => e.Descr)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<TblTaxonomy>(entity =>
            {
                entity.HasKey(e => e.TaxonomyLevelId)
                    .HasName("PK_tblTaxonomy");

                entity.ToTable("tblTaxonomies");

                entity.Property(e => e.TaxonomyLevelId)
                    .ValueGeneratedNever()
                    .HasColumnName("TaxonomyLevelID");

                entity.Property(e => e.TaxonomyLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTchistory>(entity =>
            {
                entity.HasKey(e => e.Tchid);

                entity.ToTable("tblTCHistory");

                entity.Property(e => e.Tchid).HasColumnName("TCHID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.HasTraineeSigned).HasColumnName("hasTraineeSigned");

                entity.Property(e => e.Lanid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LANID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tnum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TNum");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTdtimage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK_tblTDSImages");

                entity.ToTable("tblTDTImages");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTdtrandomReview>(entity =>
            {
                entity.HasKey(e => e.RandomReviewId);

                entity.ToTable("tblTDTRandomReview");

                entity.Property(e => e.RandomReviewId).HasColumnName("RandomReviewID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblTdtrandomReviews)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTDTRandomReview_tblSkillsKnowledge");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TblTdtrandomReviews)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTDTRandomReview_tblTests");
            });

            modelBuilder.Entity<TblTdtrandomReviewDetail>(entity =>
            {
                entity.HasKey(e => e.RandomReviewDetailId);

                entity.ToTable("tblTDTRandomReviewDetails");

                entity.Property(e => e.RandomReviewDetailId).HasColumnName("RandomReviewDetailID");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TblTdtrandomReviewDetails)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTDTRandomReviewDetails_tblTests");

                entity.HasOne(d => d.TestItem)
                    .WithMany(p => p.TblTdtrandomReviewDetails)
                    .HasForeignKey(d => d.TestItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTDTRandomReviewDetails_tblTestItems");
            });

            modelBuilder.Entity<TblTempCat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTempCat");

                entity.HasIndex(e => e.Id, "CourseId");

                entity.HasIndex(e => e.Num, "Num");

                entity.HasIndex(e => e.SubNum, "SubNum");

                entity.Property(e => e.Id).HasDefaultValueSql("((0))");

                entity.Property(e => e.Num).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubNum).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTempDelinquency>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("aaaaatblTempDelinquency_PK")
                    .IsClustered(false);

                entity.ToTable("tblTempDelinquency");

                entity.HasIndex(e => e.Eid, "EID");

                entity.HasIndex(e => e.NerccertNum, "NERCCertNum");

                entity.HasIndex(e => e.RegCertNum, "RegCertNum");

                entity.Property(e => e.Eid)
                    .ValueGeneratedNever()
                    .HasColumnName("EID");

                entity.Property(e => e.CehBio)
                    .HasColumnName("CEH_BIO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehBito)
                    .HasColumnName("CEH_BITO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehNerc)
                    .HasColumnName("CEH_NERC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehProf)
                    .HasColumnName("CEH_Prof")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehRc)
                    .HasColumnName("CEH_RC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehReg)
                    .HasColumnName("CEH_Reg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CehTo)
                    .HasColumnName("CEH_TO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.Clyear)
                    .HasColumnName("CLYear")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CredReqHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmResp).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmergencyOpsHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.Employee).HasMaxLength(255);

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NerccertArea)
                    .HasColumnName("NERCCertArea")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercpolicy)
                    .HasColumnName("NERCPolicy")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Oname)
                    .HasMaxLength(255)
                    .HasColumnName("OName");

                entity.Property(e => e.Other).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(255)
                    .HasColumnName("PDesc");

                entity.Property(e => e.RegCertIssueDate).HasColumnType("datetime");

                entity.Property(e => e.RegCertNum).HasMaxLength(50);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.RegReq).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReqCertExpDate).HasColumnType("datetime");

                entity.Property(e => e.SimHours).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalCeh)
                    .HasColumnName("TotalCEH")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalReqCehs)
                    .HasColumnName("TotalReqCEHs")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpsizeTs)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("upsize_ts");
            });

            modelBuilder.Entity<TblTempDuty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTempDuty");

                entity.HasIndex(e => e.Id, "CourseId");

                entity.HasIndex(e => e.Num, "Num");

                entity.HasIndex(e => e.SubNum, "SubNum");

                entity.Property(e => e.Id).HasDefaultValueSql("((0))");

                entity.Property(e => e.Letter).HasMaxLength(1);

                entity.Property(e => e.Num).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubNum).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTempEvaluationDatum>(entity =>
            {
                entity.HasKey(e => e.Fqid)
                    .HasName("aaaaatblTempEvaluationData_PK")
                    .IsClustered(false);

                entity.ToTable("tblTempEvaluationData");

                entity.HasIndex(e => e.Fqid, "FQID");

                entity.Property(e => e.Fqid)
                    .ValueGeneratedNever()
                    .HasColumnName("FQID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OComments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("O_Comments");

                entity.Property(e => e.ORatingAverage)
                    .HasColumnName("O_RatingAverage")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ORatingHigh)
                    .HasColumnName("O_RatingHigh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ORatingLow)
                    .HasColumnName("O_RatingLow")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingAverage).HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingHigh).HasDefaultValueSql("((0))");

                entity.Property(e => e.RatingLow).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpsizeTs)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("upsize_ts");
            });

            modelBuilder.Entity<TblTempList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTempList");

                entity.Property(e => e.CurrentUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Letter).HasMaxLength(1);

                entity.Property(e => e.Num).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubNum).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasMaxLength(7000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTempOjthistory>(entity =>
            {
                entity.HasKey(e => e.Ojthid)
                    .HasName("aaaaatblTempOJTHistory_PK")
                    .IsClustered(false);

                entity.ToTable("tblTempOJTHistory");

                entity.HasIndex(e => e.Ojthid, "OJTHID");

                entity.HasIndex(e => e.ParentId, "ParentID");

                entity.Property(e => e.Ojthid)
                    .ValueGeneratedNever()
                    .HasColumnName("OJTHID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.EvalMethod).HasMaxLength(255);

                entity.Property(e => e.Evaluator).HasMaxLength(50);

                entity.Property(e => e.ObservPeriod).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.UpsizeTs)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("upsize_ts");
            });

            modelBuilder.Entity<TblTest>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("tblTests");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.TestStatusId).HasColumnName("TestStatusID");

                entity.Property(e => e.TestTitle)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblTests)
                    .HasForeignKey(d => d.Corid)
                    .HasConstraintName("FK_tblTests_tblCourses");

                entity.HasOne(d => d.TestStatus)
                    .WithMany(p => p.TblTests)
                    .HasForeignKey(d => d.TestStatusId)
                    .HasConstraintName("FK_tblTests_tblTestStatus");
            });

            modelBuilder.Entity<TblTestItem>(entity =>
            {
                entity.HasKey(e => e.TestItemId);

                entity.ToTable("tblTestItems");

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.ImageSizeId).HasColumnName("ImageSizeID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.TaxonomyId).HasColumnName("TaxonomyID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.TblTestItems)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_tblTestItems_tblTDTImages");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblTestItems)
                    .HasForeignKey(d => d.Skid)
                    .HasConstraintName("FK_tblTestItems_tblSkillsKnowledge");

                entity.HasOne(d => d.Taxonomy)
                    .WithMany(p => p.TblTestItems)
                    .HasForeignKey(d => d.TaxonomyId)
                    .HasConstraintName("FK_tblTestItems_tblTaxonomy");

                entity.HasOne(d => d.TestItemTypeNavigation)
                    .WithMany(p => p.TblTestItems)
                    .HasForeignKey(d => d.TestItemType)
                    .HasConstraintName("FK_tblTestItems_tblTestItemTypes");
            });

            modelBuilder.Entity<TblTestItemType>(entity =>
            {
                entity.HasKey(e => e.TestItemTypeId)
                    .HasName("PK_tblTestItemType");

                entity.ToTable("tblTestItemTypes");

                entity.Property(e => e.TestItemTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("TestItemTypeID");

                entity.Property(e => e.TestItemType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTestRecallHistory>(entity =>
            {
                entity.HasKey(e => e.RecallId);

                entity.ToTable("tblTest_RecallHistory");

                entity.Property(e => e.RecallId).HasColumnName("RecallID");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.DateComplete).HasColumnType("datetime");

                entity.Property(e => e.DateRecalled)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateReleased).HasColumnType("datetime");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Etid).HasColumnName("ETID");

                entity.Property(e => e.RecalledBy)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ReleasedBy)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TestGrade)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TestId).HasColumnName("TestID");
            });

            modelBuilder.Entity<TblTestStatus>(entity =>
            {
                entity.HasKey(e => e.TestStatusId)
                    .HasName("PK_tblTestStatus");

                entity.ToTable("tblTestStatuses");

                entity.Property(e => e.TestStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TestStatusID");

                entity.Property(e => e.TestStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<TblTestitemDistractor>(entity =>
            {
                entity.HasKey(e => e.TestitemDistractorId);

                entity.ToTable("tblTestitemDistractors");

                entity.Property(e => e.TestitemDistractorId).HasColumnName("TestitemDistractorID");

                entity.Property(e => e.DistractorDetails).IsUnicode(false);

                entity.Property(e => e.ImageSizeId).HasColumnName("ImageSizeID");

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.TestItem)
                    .WithMany(p => p.TblTestitemDistractors)
                    .HasForeignKey(d => d.TestItemId)
                    .HasConstraintName("FK_tblTestitemDistractors_tblTestItems");
            });

            modelBuilder.Entity<TblTmcompHistoryByEmpSelectedTempList>(entity =>
            {
                entity.ToTable("tbl_TMCompHistoryByEmpSelectedTempList");

                entity.HasIndex(e => e.Eid, "EID2_Unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");
            });

            modelBuilder.Entity<TblTmcompHistorySelectedTempList>(entity =>
            {
                entity.ToTable("tbl_TMCompHistorySelectedTempList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");

                entity.Property(e => e.Tmid).HasColumnName("TMID");
            });

            modelBuilder.Entity<TblTmcompletionHistoryByEmpTempList>(entity =>
            {
                entity.ToTable("tblTMCompletionHistoryByEmpTempList");

                entity.HasIndex(e => e.Eid, "EID_Unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");
            });

            modelBuilder.Entity<TblTmcompletionHistoryTempList>(entity =>
            {
                entity.ToTable("tblTMCompletionHistoryTempList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");

                entity.Property(e => e.Tmid).HasColumnName("TMID");
            });

            modelBuilder.Entity<TblTmselectedTempList>(entity =>
            {
                entity.ToTable("tbl_TMSelectedTempList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");

                entity.Property(e => e.Tmid).HasColumnName("TMID");
            });

            modelBuilder.Entity<TblTmtempList>(entity =>
            {
                entity.ToTable("tbl_TMTempList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentUser).HasMaxLength(50);

                entity.Property(e => e.Modifiedtime)
                    .HasColumnType("datetime")
                    .HasColumnName("modifiedtime");

                entity.Property(e => e.Tmid).HasColumnName("TMID");
            });

            modelBuilder.Entity<TblTrainingEvalIssue>(entity =>
            {
                entity.HasKey(e => e.IssueId);

                entity.ToTable("tblTrainingEvalIssue");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Action)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeInProcedurePid).HasColumnName("ChangeInProcedurePID");

                entity.Property(e => e.ChangeInToolsDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InstFeedbackText)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.IssueIdcounter).HasColumnName("IssueIDCounter");

                entity.Property(e => e.IssueIdmonth).HasColumnName("IssueIDMonth");

                entity.Property(e => e.IssueIdnum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IssueIDNum");

                entity.Property(e => e.IssueIdyear).HasColumnName("IssueIDYear");

                entity.Property(e => e.IssueStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.LearningActivityCorid).HasColumnName("LearningActivityCORID");

                entity.Property(e => e.OjttaskQualification).HasColumnName("OJTTaskQualification");

                entity.Property(e => e.OjttaskQualificationTask).HasColumnName("OJTTaskQualificationTask");

                entity.Property(e => e.OtherDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.PerformanceProblemDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Problem)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramTypePosId).HasColumnName("ProgramTypePosID");

                entity.Property(e => e.ProgramTypeStartDate).HasColumnType("datetime");

                entity.Property(e => e.Raby)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("RABy")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Summary)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.TrProgVersionId)
                    .HasColumnName("TrProgVersionID")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ChangeInProcedureP)
                    .WithMany(p => p.TblTrainingEvalIssues)
                    .HasForeignKey(d => d.ChangeInProcedurePid)
                    .HasConstraintName("FK_tblTrainingEvalIssue_tblProcedures");
            });

            modelBuilder.Entity<TblTrainingEvalIssueDeliverable>(entity =>
            {
                entity.HasKey(e => e.DeliverableId);

                entity.ToTable("tblTrainingEvalIssue_Deliverables");

                entity.Property(e => e.DeliverableId).HasColumnName("DeliverableID");

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.DateAssigned).HasColumnType("datetime");

                entity.Property(e => e.DateDue).HasColumnType("datetime");

                entity.Property(e => e.DateReviewed).HasColumnType("datetime");

                entity.Property(e => e.DeliverableDesc)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblTrainingEvalIssueTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblTrainingEvalIssue_Tasks");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");
            });

            modelBuilder.Entity<TblTrainingIssueAnnualReview>(entity =>
            {
                entity.HasKey(e => e.Arid);

                entity.ToTable("tblTrainingIssue_AnnualReview");

                entity.Property(e => e.Arid).HasColumnName("ARID");

                entity.Property(e => e.Background)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Conclusions)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Design)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Dra)
                    .HasColumnType("datetime")
                    .HasColumnName("DRA")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EvalEndDate).HasColumnType("datetime");

                entity.Property(e => e.EvalStartDate).HasColumnType("datetime");

                entity.Property(e => e.EvalTraineeLrn)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Evaluation)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.EvaluatorNames)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Implementation)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Materials)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Purpose)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Raby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("RABy")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.SignatureName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Summary)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.SupportingDocs)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.TypeStartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblTrainingIssueSupportingDoc>(entity =>
            {
                entity.HasKey(e => e.Arsdid);

                entity.ToTable("tblTrainingIssue_SupportingDocs");

                entity.Property(e => e.Arsdid).HasColumnName("ARSDID");

                entity.Property(e => e.Arid).HasColumnName("ARID");

                entity.Property(e => e.Arsdnum).HasColumnName("ARSDNum");

                entity.Property(e => e.Hyperlink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SupportingDocs)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.HasOne(d => d.Ar)
                    .WithMany(p => p.TblTrainingIssueSupportingDocs)
                    .HasForeignKey(d => d.Arid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingIssue_SupportingDocs_tblTrainingIssue_AnnualReview");
            });

            modelBuilder.Entity<TblTrainingModule>(entity =>
            {
                entity.HasKey(e => e.Tmid);

                entity.ToTable("tblTrainingModule");

                entity.HasIndex(e => e.Tmnumber, "IX_tblTrainingModule")
                    .IsUnique();

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Tmactive).HasColumnName("TMActive");

                entity.Property(e => e.Tmdesc).HasColumnName("TMDesc");

                entity.Property(e => e.Tmname)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TMName");

                entity.Property(e => e.Tmnumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMNumber");

                entity.Property(e => e.Tmprereq)
                    .IsUnicode(false)
                    .HasColumnName("TMPrereq");

                entity.Property(e => e.Tmprocedure).HasColumnName("TMProcedure");

                entity.Property(e => e.Tmresource).HasColumnName("TMResource");
            });

            modelBuilder.Entity<TblTrainingModuleEoObjective>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Eoid });

                entity.ToTable("tblTrainingModuleEO_Objective");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Eoid).HasColumnName("EOID");
            });

            modelBuilder.Entity<TblTrainingModuleIla>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Corid });

                entity.ToTable("tblTrainingModule_ILA");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.HasOne(d => d.Cor)
                    .WithMany(p => p.TblTrainingModuleIlas)
                    .HasForeignKey(d => d.Corid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_ILA_tblCourses");

                entity.HasOne(d => d.Tm)
                    .WithMany(p => p.TblTrainingModuleIlas)
                    .HasForeignKey(d => d.Tmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_ILA_tblTrainingModule_ILA");
            });

            modelBuilder.Entity<TblTrainingModuleIlaObjective>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Corid, e.ObId, e.ObjType });

                entity.ToTable("tblTrainingModule_ILA_Objectives");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.ObId).HasColumnName("ObID");

                entity.Property(e => e.ObjType)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.TblTrainingModuleIla)
                    .WithMany(p => p.TblTrainingModuleIlaObjectives)
                    .HasForeignKey(d => new { d.Tmid, d.Corid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_ILA_Objectives_tblTrainingModule_ILA");
            });

            modelBuilder.Entity<TblTrainingModuleProcedure>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Prid });

                entity.ToTable("tblTrainingModule_Procedure");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Prid).HasColumnName("PRID");
            });

            modelBuilder.Entity<TblTrainingModuleProcedureNote>(entity =>
            {
                entity.HasKey(e => e.Tmid);

                entity.ToTable("tblTrainingModule_ProcedureNotes");

                entity.Property(e => e.Tmid)
                    .ValueGeneratedNever()
                    .HasColumnName("TMID");

                entity.Property(e => e.Tmnotes).HasColumnName("TMNotes");
            });

            modelBuilder.Entity<TblTrainingModuleResource>(entity =>
            {
                entity.HasKey(e => e.Tmrid);

                entity.ToTable("tblTrainingModule_Resource");

                entity.Property(e => e.Tmrid).HasColumnName("TMRID");

                entity.Property(e => e.TmfileName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TMFileName");

                entity.Property(e => e.TmfilePath)
                    .IsRequired()
                    .HasMaxLength(750)
                    .IsUnicode(false)
                    .HasColumnName("TMFilePath");

                entity.Property(e => e.Tmid).HasColumnName("TMID");
            });

            modelBuilder.Entity<TblTrainingModuleSk>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Skid });

                entity.ToTable("tblTrainingModule_SK");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.HasOne(d => d.Sk)
                    .WithMany(p => p.TblTrainingModuleSks)
                    .HasForeignKey(d => d.Skid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_SK_tblSkillsKnowledge");

                entity.HasOne(d => d.Tm)
                    .WithMany(p => p.TblTrainingModuleSks)
                    .HasForeignKey(d => d.Tmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_SK_tblTrainingModule");
            });

            modelBuilder.Entity<TblTrainingModuleTask>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Tid })
                    .HasName("PK_tblTrainingModule_TID");

                entity.ToTable("tblTrainingModule_Tasks");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.HasOne(d => d.TidNavigation)
                    .WithMany(p => p.TblTrainingModuleTasks)
                    .HasForeignKey(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_Tasks_tblTasks");

                entity.HasOne(d => d.Tm)
                    .WithMany(p => p.TblTrainingModuleTasks)
                    .HasForeignKey(d => d.Tmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTrainingModule_Tasks_tblTrainingModule");
            });

            modelBuilder.Entity<TblTrainingModuleTasksObjective>(entity =>
            {
                entity.HasKey(e => new { e.Tmid, e.Obid })
                    .HasName("PK_tblTrainingModule_Objectives");

                entity.ToTable("tblTrainingModuleTasks_Objectives");

                entity.Property(e => e.Tmid).HasColumnName("TMID");

                entity.Property(e => e.Obid).HasColumnName("OBID");
            });

            modelBuilder.Entity<TblTrainingPhase>(entity =>
            {
                entity.HasKey(e => e.Tpid)
                    .HasName("aaaaatblTrainingPhases_PK")
                    .IsClustered(false);

                entity.ToTable("tblTrainingPhases");

                entity.HasIndex(e => e.Pid, "PID");

                entity.HasIndex(e => e.Tpid, "TPID");

                entity.HasIndex(e => e.Tpnum, "TPNum");

                entity.HasIndex(e => e.Pid, "tblPositionstblTrainingPhases");

                entity.Property(e => e.Tpid).HasColumnName("TPID");

                entity.Property(e => e.Pid)
                    .HasColumnName("PID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(255)
                    .HasColumnName("TPDesc");

                entity.Property(e => e.Tpletter)
                    .HasMaxLength(1)
                    .HasColumnName("TPLetter");

                entity.Property(e => e.Tpnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TPNote");

                entity.Property(e => e.Tpnum)
                    .HasColumnName("TPNum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpsizeTs)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("upsize_ts");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.TblTrainingPhases)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("tblTrainingPhases_FK00");
            });

            modelBuilder.Entity<TblUserActivityLog>(entity =>
            {
                entity.HasKey(e => e.Alid);

                entity.ToTable("tblUserActivityLog");

                entity.Property(e => e.Alid).HasColumnName("ALID");

                entity.Property(e => e.Alaction)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("ALAction");

                entity.Property(e => e.Aldate)
                    .HasColumnType("datetime")
                    .HasColumnName("ALDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AlformName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ALFormName");

                entity.Property(e => e.AlformText)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("ALFormText");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblWarningMsgHistory>(entity =>
            {
                entity.HasKey(e => e.Wmhid)
                    .HasName("PK__tblWarni__ECE5F106099DBF74");

                entity.ToTable("tblWarningMsgHistory");

                entity.Property(e => e.Wmhid).HasColumnName("WMHID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Wmdate)
                    .HasColumnType("datetime")
                    .HasColumnName("WMDate");
            });

            modelBuilder.Entity<TblWarningSetting>(entity =>
            {
                entity.HasKey(e => e.WarningId);

                entity.ToTable("tblWarningSettings");

                entity.Property(e => e.WarningId).HasColumnName("WarningID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Nfreq).HasColumnName("NFreq");

                entity.Property(e => e.Nlevel).HasColumnName("NLevel");

                entity.Property(e => e.Ntype)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Num).HasColumnName("num");
            });

            modelBuilder.Entity<TenantConnectorContent>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ConnectorId, e.ContentId })
                    .HasName("TenantConnectorContent_pkey");

                entity.ToTable("TenantConnectorContent");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ConnectorId)
                    .HasMaxLength(16)
                    .HasColumnName("connector_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ContentId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("content_id");

                entity.Property(e => e.ContentInformation)
                    .IsRequired()
                    .HasColumnName("content_information");

                entity.Property(e => e.ContentTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("content_title");

                entity.Property(e => e.ContentUpdated).HasColumnName("content_updated");

                entity.Property(e => e.SearchText)
                    .IsRequired()
                    .HasMaxLength(700)
                    .HasColumnName("search_text");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantCredential>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.CredentialId })
                    .HasName("TenantCredentials_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.CredentialId)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("credential_id");

                entity.Property(e => e.CredentialName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("credential_name");

                entity.Property(e => e.CredentialPermissions)
                    .IsUnicode(false)
                    .HasColumnName("credential_permissions");

                entity.Property(e => e.CredentialSecret)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("credential_secret");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantDbUpdate>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.UpdateId })
                    .HasName("TenantDbUpdates_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.UpdateId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("update_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantPluginConfiguration>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.PluginId })
                    .HasName("TenantPluginConfiguration_pkey");

                entity.ToTable("TenantPluginConfiguration");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.PluginId)
                    .HasMaxLength(16)
                    .HasColumnName("plugin_id")
                    .IsFixedLength(true);

                entity.Property(e => e.PluginClass)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("plugin_class");

                entity.Property(e => e.PluginEnabled)
                    .IsRequired()
                    .HasColumnName("plugin_enabled")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PluginInternalConfig)
                    .IsUnicode(false)
                    .HasColumnName("plugin_internal_config");

                entity.Property(e => e.PluginUserConfig)
                    .IsUnicode(false)
                    .HasColumnName("plugin_user_config");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantPluginObjStore>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.PluginId, e.ObjectKeySha1 })
                    .HasName("TenantPluginObjStore_pkey");

                entity.ToTable("TenantPluginObjStore");

                entity.HasIndex(e => new { e.EngineTenantId, e.PluginId, e.Expiry }, "IX_ten_plug_obj_expiry");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.PluginId)
                    .HasMaxLength(16)
                    .HasColumnName("plugin_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ObjectKeySha1)
                    .HasMaxLength(20)
                    .HasColumnName("object_key_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ObjectKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("object_key");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("object_type");

                entity.Property(e => e.ObjectValue).HasColumnName("object_value");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantProperty>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.PropertyLevel, e.PropertyScope, e.PropertyName })
                    .HasName("TenantProperties_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.PropertyLevel).HasColumnName("property_level");

                entity.Property(e => e.PropertyScope).HasColumnName("property_scope");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("property_name");

                entity.Property(e => e.PropertyValue)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("property_value");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TenantTinCanForwardingMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.PairId })
                    .HasName("TenantTinCanForwardingMap_pkey");

                entity.ToTable("TenantTinCanForwardingMap");

                entity.HasIndex(e => new { e.EngineTenantId, e.VisibleAfter }, "IX_T_Forwarding_CheckoutNext");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.PairId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pair_id");

                entity.Property(e => e.Attempts).HasColumnName("attempts");

                entity.Property(e => e.LastForwardedStatementDate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_forwarded_statement_date");

                entity.Property(e => e.LastUpdatedUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("last_updated_utc");

                entity.Property(e => e.MoreUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("more_url");

                entity.Property(e => e.NoStatementsAttempts).HasColumnName("no_statements_attempts");

                entity.Property(e => e.SourcePassword)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("source_password");

                entity.Property(e => e.SourceUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("source_url");

                entity.Property(e => e.SourceUsername)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("source_username");

                entity.Property(e => e.TargetPassword)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("target_password");

                entity.Property(e => e.TargetUrl)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("target_url");

                entity.Property(e => e.TargetUsername)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("target_username");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VisibleAfter).HasColumnName("visible_after");
            });

            modelBuilder.Entity<TinCanActivityProvider>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ProviderId })
                    .HasName("TinCanActivityProvider_pkey");

                entity.ToTable("TinCanActivityProvider");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ProviderId)
                    .HasMaxLength(100)
                    .HasColumnName("provider_id");

                entity.Property(e => e.AuthType)
                    .HasColumnName("auth_type")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Info)
                    .IsUnicode(false)
                    .HasColumnName("info");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasColumnName("is_enabled")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PublicKey)
                    .HasMaxLength(1000)
                    .HasColumnName("public_key");

                entity.Property(e => e.Secret)
                    .HasMaxLength(100)
                    .HasColumnName("secret");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TinCanActivityProviderMap>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ProviderId, e.ActivityIdSha1 })
                    .HasName("TinCanActivityProviderMap_pkey");

                entity.ToTable("TinCanActivityProviderMap");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ProviderId)
                    .HasMaxLength(100)
                    .HasColumnName("provider_id");

                entity.Property(e => e.ActivityIdSha1)
                    .HasMaxLength(20)
                    .HasColumnName("activity_id_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.ActivityId)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("activity_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TinCanActivityProvider)
                    .WithMany(p => p.TinCanActivityProviderMaps)
                    .HasForeignKey(d => new { d.EngineTenantId, d.ProviderId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanActivityProviderMap_2");
            });

            modelBuilder.Entity<TinCanActorProperty>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.KeyValHash })
                    .HasName("TinCanActorProperties_pkey");

                entity.HasIndex(e => new { e.EngineTenantId, e.ActorId, e.Key }, "IX_ActorProp_aakey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.KeyValHash)
                    .HasMaxLength(20)
                    .HasColumnName("key_val_hash")
                    .IsFixedLength(true);

                entity.Property(e => e.ActorId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("actor_id")
                    .IsFixedLength(true);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("key");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<TinCanAgent>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.StatementId, e.AgentId })
                    .HasName("TinCanAgent_pkey");

                entity.ToTable("TinCanAgent");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.StatementId)
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.AgentId)
                    .HasMaxLength(20)
                    .HasColumnName("agent_id")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TinCanStatementIndex)
                    .WithMany(p => p.TinCanAgents)
                    .HasForeignKey(d => new { d.EngineTenantId, d.StatementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanAgent_2");
            });

            modelBuilder.Entity<TinCanContentToken>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.TokenId })
                    .HasName("TinCanContentToken_pkey");

                entity.ToTable("TinCanContentToken");

                entity.HasIndex(e => e.Expiry, "IX_TinCanContentToken_Expiry");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.TokenId)
                    .HasMaxLength(16)
                    .HasColumnName("token_id")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ExternalConfiguration)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_configuration");

                entity.Property(e => e.ExternalId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_id");

                entity.Property(e => e.PreviewToken).HasColumnName("preview_token");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TinCanContextActivity>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.StatementId, e.CtxActivityIdSha1 })
                    .HasName("TinCanContextActivity_pkey");

                entity.ToTable("TinCanContextActivity");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.StatementId)
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CtxActivityIdSha1)
                    .HasMaxLength(20)
                    .HasColumnName("ctx_activity_id_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TinCanStatementIndex)
                    .WithMany(p => p.TinCanContextActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.StatementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanContextActivity_2");
            });

            modelBuilder.Entity<TinCanDocument>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.DocumentIdHash })
                    .HasName("TinCanDocuments_pkey");

                entity.HasIndex(e => new { e.EngineTenantId, e.DocumentCtxHash }, "IX_document_ctx");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.DocumentIdHash)
                    .HasMaxLength(20)
                    .HasColumnName("document_id_hash")
                    .IsFixedLength(true);

                entity.Property(e => e.ActivityId)
                    .HasMaxLength(1024)
                    .HasColumnName("activity_id");

                entity.Property(e => e.ActorId)
                    .HasMaxLength(20)
                    .HasColumnName("actor_id")
                    .IsFixedLength(true);

                entity.Property(e => e.AsserterId)
                    .HasMaxLength(82)
                    .IsUnicode(false)
                    .HasColumnName("asserter_id");

                entity.Property(e => e.AsserterJson)
                    .IsUnicode(false)
                    .HasColumnName("asserter_json");

                entity.Property(e => e.ContentHash)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("content_hash")
                    .IsFixedLength(true);

                entity.Property(e => e.ContentLength).HasColumnName("content_length");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("content_type");

                entity.Property(e => e.DocumentCtxHash)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("document_ctx_hash")
                    .IsFixedLength(true);

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("document_id");

                entity.Property(e => e.RegistrationId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("registration_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<TinCanLaunchToken>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.TokenId })
                    .HasName("TinCanLaunchToken_pkey");

                entity.ToTable("TinCanLaunchToken");

                entity.HasIndex(e => e.Expiry, "IX_TinCanLaunchToken_Expiry");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.TokenId)
                    .HasMaxLength(16)
                    .HasColumnName("token_id")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ExternalConfiguration)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_configuration");

                entity.Property(e => e.ExternalRegistrationId)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("external_registration_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TinCanObjectStore>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ObjectKeySha1 })
                    .HasName("TinCanObjectStore_pkey");

                entity.ToTable("TinCanObjectStore");

                entity.HasIndex(e => new { e.EngineTenantId, e.Expiry }, "IX_TCOS_e");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ObjectKeySha1)
                    .HasMaxLength(20)
                    .HasColumnName("object_key_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.Expiry)
                    .HasColumnType("datetime")
                    .HasColumnName("expiry");

                entity.Property(e => e.ObjectKey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("object_key");

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("object_type");

                entity.Property(e => e.ObjectValue).HasColumnName("object_value");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TinCanPackage>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormPackageId })
                    .HasName("TinCanPackage_pkey");

                entity.ToTable("TinCanPackage");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormPackageId).HasColumnName("scorm_package_id");

                entity.Property(e => e.TincanActivityId)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("tincan_activity_id");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormPackage)
                    .WithOne(p => p.TinCanPackage)
                    .HasForeignKey<TinCanPackage>(d => new { d.EngineTenantId, d.ScormPackageId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanPackage_1");
            });

            modelBuilder.Entity<TinCanPermission>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.OwnerId })
                    .HasName("TinCanPermissions_pkey");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.OwnerId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("owner_id");

                entity.Property(e => e.ActivityWrite)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("activity_write");

                entity.Property(e => e.ActorWrite)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("actor_write");

                entity.Property(e => e.DocumentRead)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("document_read");

                entity.Property(e => e.DocumentWrite)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("document_write");

                entity.Property(e => e.StatementRead)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("statement_read");

                entity.Property(e => e.StatementWrite)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("statement_write");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TinCanRegistration>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.ScormRegistrationId })
                    .HasName("TinCanRegistration_pkey");

                entity.ToTable("TinCanRegistration");

                entity.HasIndex(e => new { e.EngineTenantId, e.TincanRegistrationId }, "UIX_TinCanRegistration_ID")
                    .IsUnique();

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.ScormRegistrationId).HasColumnName("scorm_registration_id");

                entity.Property(e => e.Cmi5CourseSatisfied).HasColumnName("cmi5_course_satisfied");

                entity.Property(e => e.CompOfFailedSuccess)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("comp_of_failed_success");

                entity.Property(e => e.Completion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("completion");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(10, 7)")
                    .HasColumnName("score");

                entity.Property(e => e.ScoreIsKnown).HasColumnName("score_is_known");

                entity.Property(e => e.Success)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("success");

                entity.Property(e => e.TincanRegistrationId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("tincan_registration_id")
                    .IsFixedLength(true);

                entity.Property(e => e.TotalSecondsTracked).HasColumnName("total_seconds_tracked");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ScormRegistration)
                    .WithOne(p => p.TinCanRegistration)
                    .HasForeignKey<TinCanRegistration>(d => new { d.EngineTenantId, d.ScormRegistrationId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanRegistration_1");
            });

            modelBuilder.Entity<TinCanRelatedActivity>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.StatementId, e.RelatedActidSha1 })
                    .HasName("TinCanRelatedActivity_pkey");

                entity.ToTable("TinCanRelatedActivity");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.StatementId)
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.RelatedActidSha1)
                    .HasMaxLength(20)
                    .HasColumnName("related_actid_sha1")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TinCanStatementIndex)
                    .WithMany(p => p.TinCanRelatedActivities)
                    .HasForeignKey(d => new { d.EngineTenantId, d.StatementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanRelatedActivity_2");
            });

            modelBuilder.Entity<TinCanRelatedAgent>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.StatementId, e.RelatedAgentId })
                    .HasName("TinCanRelatedAgent_pkey");

                entity.ToTable("TinCanRelatedAgent");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.StatementId)
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.RelatedAgentId)
                    .HasMaxLength(20)
                    .HasColumnName("related_agent_id")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TinCanStatementIndex)
                    .WithMany(p => p.TinCanRelatedAgents)
                    .HasForeignKey(d => new { d.EngineTenantId, d.StatementId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TinCanRelatedAgent_2");
            });

            modelBuilder.Entity<TinCanStatementIndex>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.StatementId })
                    .HasName("TinCanStatementIndex_pkey");

                entity.ToTable("TinCanStatementIndex");

                entity.HasIndex(e => new { e.EngineTenantId, e.CtxRegistration }, "IX_StatementIdx_reg");

                entity.HasIndex(e => new { e.EngineTenantId, e.Stored, e.StatementId }, "IX_StatementIdx_sortstring");

                entity.HasIndex(e => new { e.EngineTenantId, e.TargetId }, "IX_StatementIdx_targetid");

                entity.HasIndex(e => new { e.EngineTenantId, e.Verb }, "IX_StatementIdx_verb");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.StatementId)
                    .HasMaxLength(16)
                    .HasColumnName("statement_id")
                    .IsFixedLength(true);

                entity.Property(e => e.ActorId)
                    .HasMaxLength(20)
                    .HasColumnName("actor_id")
                    .IsFixedLength(true);

                entity.Property(e => e.Authoritative).HasColumnName("authoritative");

                entity.Property(e => e.CtxInstructorId)
                    .HasMaxLength(20)
                    .HasColumnName("ctx_instructor_id")
                    .IsFixedLength(true);

                entity.Property(e => e.CtxRegistration)
                    .HasMaxLength(16)
                    .HasColumnName("ctx_registration")
                    .IsFixedLength(true);

                entity.Property(e => e.Stored).HasColumnName("stored");

                entity.Property(e => e.TargetId)
                    .HasMaxLength(20)
                    .HasColumnName("target_id")
                    .IsFixedLength(true);

                entity.Property(e => e.TargetType)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("target_type");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Verb)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("verb")
                    .IsFixedLength(true);

                entity.Property(e => e.Version).HasColumnName("version");

                entity.Property(e => e.VersionFamily).HasColumnName("version_family");

                entity.Property(e => e.Voided).HasColumnName("voided");
            });

            modelBuilder.Entity<TinCanTargetStatement>(entity =>
            {
                entity.HasKey(e => new { e.EngineTenantId, e.TargetId, e.TargetingId })
                    .HasName("TinCanTargetStatement_pkey");

                entity.ToTable("TinCanTargetStatement");

                entity.HasIndex(e => new { e.EngineTenantId, e.Rescanned }, "IX_TargetStatement_rescanned");

                entity.HasIndex(e => new { e.EngineTenantId, e.TargetingId }, "IX_TargetStatement_targeting");

                entity.Property(e => e.EngineTenantId).HasColumnName("engine_tenant_id");

                entity.Property(e => e.TargetId)
                    .HasMaxLength(16)
                    .HasColumnName("target_id")
                    .IsFixedLength(true);

                entity.Property(e => e.TargetingId)
                    .HasMaxLength(16)
                    .HasColumnName("targeting_id")
                    .IsFixedLength(true);

                entity.Property(e => e.IsVoiding).HasColumnName("is_voiding");

                entity.Property(e => e.Rescanned).HasColumnName("rescanned");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("update_by")
                    .HasDefaultValueSql("(user_name())");

                entity.Property(e => e.UpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("update_dt")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<VAllClassBasedRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAllClassBasedRecords");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Suid).HasColumnName("SUID");
            });

            modelBuilder.Entity<VAllSelfPacedRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vAllSelfPacedRecords");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Suid).HasColumnName("SUID");
            });

            modelBuilder.Entity<VQtsAdditionalCertsForIla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_AdditionalCertsForILA");

                entity.Property(e => e.Ceh).HasColumnName("CEH");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.TrainingType).HasMaxLength(50);

                entity.Property(e => e.TrainingTypeId).HasColumnName("TrainingTypeID");
            });

            modelBuilder.Entity<VQtsCertificate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Certificate");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CpCity)
                    .HasMaxLength(50)
                    .HasColumnName("CP_City");

                entity.Property(e => e.CpPhone)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Phone");

                entity.Property(e => e.CpState)
                    .HasMaxLength(50)
                    .HasColumnName("CP_State");

                entity.Property(e => e.CpStreetAddress)
                    .HasMaxLength(100)
                    .HasColumnName("CP_StreetAddress");

                entity.Property(e => e.CpZip)
                    .HasMaxLength(50)
                    .HasColumnName("CP_ZIP");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.PassFailed)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TprName)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Name");

                entity.Property(e => e.TprSignaturePath)
                    .HasMaxLength(255)
                    .HasColumnName("TPR_SignaturePath");
            });

            modelBuilder.Entity<VQtsCertificateAllCeh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_CertificateAllCEHs");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.ContactPerson).HasMaxLength(50);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CpCity)
                    .HasMaxLength(50)
                    .HasColumnName("CP_City");

                entity.Property(e => e.CpPhone)
                    .HasMaxLength(50)
                    .HasColumnName("CP_Phone");

                entity.Property(e => e.CpState)
                    .HasMaxLength(50)
                    .HasColumnName("CP_State");

                entity.Property(e => e.CpStreetAddress)
                    .HasMaxLength(100)
                    .HasColumnName("CP_StreetAddress");

                entity.Property(e => e.CpZip)
                    .HasMaxLength(50)
                    .HasColumnName("CP_ZIP");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Oname)
                    .HasMaxLength(150)
                    .HasColumnName("OName");

                entity.Property(e => e.PassFailed)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TprName)
                    .HasMaxLength(50)
                    .HasColumnName("TPR_Name");

                entity.Property(e => e.TprSignaturePath)
                    .HasMaxLength(255)
                    .HasColumnName("TPR_SignaturePath");
            });

            modelBuilder.Entity<VQtsClassBasedRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ClassBasedRecords");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Suid).HasColumnName("SUID");
            });

            modelBuilder.Entity<VQtsClassEvaluationComment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ClassEvaluationComments");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqid).HasColumnName("FQID");

                entity.Property(e => e.Sfacomments)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("SFAComments");
            });

            modelBuilder.Entity<VQtsClassEvaluationSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ClassEvaluationSummary");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");

                entity.Property(e => e.Fqid).HasColumnName("FQID");
            });

            modelBuilder.Entity<VQtsClassesAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ClassesAll");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsCoursesSkillsRelated>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Courses_SkillsRelated");

                entity.Property(e => e.Corid).HasColumnName("CORID");
            });

            modelBuilder.Entity<VQtsCoursesTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Courses_Tasks");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsCriticalTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_CriticalTasks");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsDutyArea>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_DutyAreas");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Num).HasMaxLength(289);

                entity.Property(e => e.SubNum).HasMaxLength(320);
            });

            modelBuilder.Entity<VQtsEmpPosChange>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmpPosChange");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsEmpPosChangeDesk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmpPosChange_Desks");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.PosEndDate).HasColumnType("datetime");

                entity.Property(e => e.PosStartDate).HasColumnType("datetime");

                entity.Property(e => e.QualificationDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsEmployeePositionTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeePositionTraining");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Dt)
                    .HasColumnType("datetime")
                    .HasColumnName("dt");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeePositionTrainingAllDataDesk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeePositionTrainingAllData_Desks");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeePositionTrainingAllDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeePositionTrainingAllData");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeePositionTrainingPreviou>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeePositionTrainingPrevious");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Dt)
                    .HasColumnType("datetime")
                    .HasColumnName("dt");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeeTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeeTraining");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeeTrainingDesk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EmployeeTraining_Desk");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.TaskNum).HasMaxLength(93);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsEmployeesDum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Employees_DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsEmployeesTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Employees_Tasks");

                entity.Property(e => e.Comments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.RsId).HasColumnName("rsID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsEnablingObjective>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EnablingObjective");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.SkillDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.SkillNum)
                    .HasMaxLength(123)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.TopicNum)
                    .HasMaxLength(92)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VQtsEobyPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EOByPosition");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.CurrentUser).HasMaxLength(128);

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pnum).HasColumnName("PNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.Sknumber)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("SKNumber");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");
            });

            modelBuilder.Entity<VQtsEobyTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_EOByTask");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skill)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("Skill#");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VQtsFullRecordByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_FullRecordByYear");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ProctorName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");
            });

            modelBuilder.Entity<VQtsIdpcompletion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_IDPCompletion");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Completed).HasColumnType("datetime");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Idpid).HasColumnName("IDPID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.InstLoc).HasMaxLength(306);

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.OptionalComments).HasMaxLength(500);

                entity.Property(e => e.Partial)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Planned)
                    .HasMaxLength(61)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.ReqCompDate).HasColumnType("datetime");

                entity.Property(e => e.Scheduled).HasColumnType("datetime");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.Tyear)
                    .HasMaxLength(50)
                    .HasColumnName("TYear");
            });

            modelBuilder.Entity<VQtsIdplatestRecordDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_IDPLatestRecordDetails");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsInitialPositionTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_InitialPositionTraining");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(102);

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.InitialVersion).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Position).HasMaxLength(203);
            });

            modelBuilder.Entity<VQtsLabelReplacement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LabelReplacements");

                entity.Property(e => e.TxOther)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TxReg)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TxReg2)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TxTotal)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VQtsLastOjtevaluation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LastOJTEvaluation");

                entity.Property(e => e.LastEvaluation).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<VQtsLatestCompleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestCompleted");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastCompleted).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsLatestCompletedGrade>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestCompletedGrade");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastCompleted).HasColumnType("datetime");

                entity.Property(e => e.Partial)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");
            });

            modelBuilder.Entity<VQtsLatestPlanned>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestPlanned");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LatestPlanned).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsLatestRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestRecords");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsLatestRecordDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestRecordDetails");

                entity.Property(e => e.CehAppDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CEH_AppDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Clyear).HasColumnName("CLYear");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.ReasonWo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ReasonWO");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsLatestScheduled>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestScheduled");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastScheduled).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsLatestWaved>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_LatestWaved");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.LastWaved).HasColumnType("datetime");
            });

            modelBuilder.Entity<VQtsMainCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MainCategories");

                entity.Property(e => e.Cdesc)
                    .HasMaxLength(255)
                    .HasColumnName("CDesc");

                entity.Property(e => e.Cid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.MainCategory).HasMaxLength(288);
            });

            modelBuilder.Entity<VQtsMainDuty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MainDuty");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.MainDuty).HasMaxLength(289);
            });

            modelBuilder.Entity<VQtsMainTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MainTasks");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.TaskNumber).HasMaxLength(93);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TCriteria");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsManagerClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ManagerClasses");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.MgrId).HasColumnName("MgrID");
            });

            modelBuilder.Entity<VQtsManagerCourse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ManagerCourses");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.MgrId).HasColumnName("MgrID");
            });

            modelBuilder.Entity<VQtsManagerEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ManagerEmployees");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.MgrId).HasColumnName("MgrID");
            });

            modelBuilder.Entity<VQtsManagerOrganization>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ManagerOrganizations");

                entity.Property(e => e.MgrId).HasColumnName("MgrID");

                entity.Property(e => e.OrgId).HasColumnName("OrgID");
            });

            modelBuilder.Entity<VQtsManagerPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ManagerPositions");

                entity.Property(e => e.MgrId).HasColumnName("MgrID");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<VQtsMaxCoursesTasksSequence>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxCourses_TasksSequence");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsMaxEvalDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxEvalDate");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsMaxInitialVersion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxInitialVersion");

                entity.Property(e => e.MaxVersion).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Ptpid).HasColumnName("PTPID");

                entity.Property(e => e.Tpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TPDate");
            });

            modelBuilder.Entity<VQtsMaxPosTaskAddDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxPosTaskAddDate");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsMaxPosTaskAnnualReview>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxPosTaskAnnualReview");

                entity.Property(e => e.Arnotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("ARNotes");

                entity.Property(e => e.ArposId).HasColumnName("ARPosID");

                entity.Property(e => e.ArrevBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ARRevBy");

                entity.Property(e => e.ArrevDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ARRevDate");

                entity.Property(e => e.Arsig)
                    .HasColumnType("image")
                    .HasColumnName("ARSig");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Tharid).HasColumnName("THARID");
            });

            modelBuilder.Entity<VQtsMaxPosTaskAnnualReviewDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxPosTaskAnnualReviewDate");

                entity.Property(e => e.ArposId).HasColumnName("ARPosID");

                entity.Property(e => e.MaxArdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MaxARDate");
            });

            modelBuilder.Entity<VQtsMaxTaskHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxTaskHistory");

                entity.Property(e => e.ChangeDateStamp).HasColumnType("datetime");

                entity.Property(e => e.Inactive)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Thconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THConditions");

                entity.Property(e => e.Thcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THCriteria");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thid).HasColumnName("THID");

                entity.Property(e => e.Thnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THNote");

                entity.Property(e => e.Thnum)
                    .HasMaxLength(50)
                    .HasColumnName("THNum");

                entity.Property(e => e.ThposHistory).HasColumnName("THPosHistory");

                entity.Property(e => e.ThprocList)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THProcList");

                entity.Property(e => e.Threferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THReferences");

                entity.Property(e => e.ThrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("THRevisedBy");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Thtools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THTools");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsMaxTaskHistoryDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxTaskHistoryDate");

                entity.Property(e => e.MaxThdate)
                    .HasColumnType("datetime")
                    .HasColumnName("MaxTHDate");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsMaxVersion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MaxVersion");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Ptpid).HasColumnName("PTPID");
            });

            modelBuilder.Entity<VQtsMinPosTaskAfterAdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_MinPosTaskAfterAdd");

                entity.Property(e => e.MinDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsNercrelatedTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_NERCRelatedTraining");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.SecondDate).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsNercreportedClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_NERCReportedClasses");

                entity.Property(e => e.Clid).HasColumnName("CLID");
            });

            modelBuilder.Entity<VQtsNewOrModifiedTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_NewOrModifiedTasks");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.SubDaid).HasColumnName("SubDAID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsOjtsortOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_OJTSortOrder");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<VQtsOjttask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_OJTTasks");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.DriverId).HasColumnName("DriverID");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.SubDaid).HasColumnName("SubDAID");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VQtsPosTaskAfterAdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_PosTaskAfterAdd");

                entity.Property(e => e.ChangeDateStamp).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pthcritical).HasColumnName("PTHCritical");

                entity.Property(e => e.Pthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PTHDate");

                entity.Property(e => e.Pthid).HasColumnName("PTHID");

                entity.Property(e => e.Pthnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PTHNote");

                entity.Property(e => e.PthrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("PTHRevisedBy");

                entity.Property(e => e.Pthtype)
                    .HasMaxLength(50)
                    .HasColumnName("PTHType");

                entity.Property(e => e.Thid).HasColumnName("THID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsPosTaskDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_PosTaskDate");

                entity.Property(e => e.Dt)
                    .HasColumnType("datetime")
                    .HasColumnName("dt");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsPositionTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_PositionTasks");

                entity.Property(e => e.Da)
                    .HasMaxLength(320)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Flag)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.MainDuty).HasMaxLength(289);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Task)
                    .HasMaxLength(94)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(255)
                    .HasColumnName("TPDesc");

                entity.Property(e => e.Tpid).HasColumnName("TPID");

                entity.Property(e => e.Tpnum).HasColumnName("TPNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsPositionTrainingI>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_PositionTrainingI");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.InitialVersion).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<VQtsPositionsTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Positions_Tasks");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.StatusDefault).HasColumnName("Status_Default");

                entity.Property(e => e.StatusFinal).HasColumnName("Status_Final");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tpid).HasColumnName("TPID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsProceduresHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_ProceduresHistory");

                entity.Property(e => e.Iaid).HasColumnName("IAID");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.PrrevDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PRRevDate");

                entity.Property(e => e.PrrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("PRRevisedBy");

                entity.Property(e => e.Prrevision).HasColumnName("PRRevision");

                entity.Property(e => e.Prseries)
                    .HasMaxLength(50)
                    .HasColumnName("PRSeries");

                entity.Property(e => e.Prtitle)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRTitle");
            });

            modelBuilder.Entity<VQtsProceduresTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Procedures_Tasks");

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsRptSstrainingGuideSkillsAndTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_rptSSTrainingGuideSkillsAndTasks");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Course)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.Dummysort).HasColumnName("dummysort");

                entity.Property(e => e.Letter).HasMaxLength(1);

                entity.Property(e => e.Level1).HasMaxLength(289);

                entity.Property(e => e.Level2).HasMaxLength(320);

                entity.Property(e => e.Level3Desc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("Level3_Desc");

                entity.Property(e => e.Level3Num)
                    .HasMaxLength(92)
                    .IsUnicode(false)
                    .HasColumnName("Level3_Num");

                entity.Property(e => e.Level4Desc).HasColumnName("Level4_Desc");

                entity.Property(e => e.Level4Num)
                    .HasMaxLength(123)
                    .HasColumnName("Level4_Num");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Objective)
                    .IsRequired()
                    .HasMaxLength(19)
                    .IsUnicode(false);

                entity.Property(e => e.So1).HasColumnName("SO_1");

                entity.Property(e => e.So2).HasColumnName("SO_2");

                entity.Property(e => e.So3).HasColumnName("SO_3");

                entity.Property(e => e.So4).HasColumnName("SO_4");
            });

            modelBuilder.Entity<VQtsScormpackage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SCORMPackages");

                entity.Property(e => e.CorId)
                    .HasMaxLength(150)
                    .HasColumnName("cor_id");

                entity.Property(e => e.LearningStandardId).HasColumnName("learning_standard_id");

                entity.Property(e => e.ScormPackageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("scorm_package_id");

                entity.Property(e => e.VersionId).HasColumnName("version_id");

                entity.Property(e => e.WebPath)
                    .HasMaxLength(500)
                    .HasColumnName("web_path");
            });

            modelBuilder.Entity<VQtsScrub>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Scrub");

                entity.Property(e => e.Scrub)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("scrub");
            });

            modelBuilder.Entity<VQtsShazByPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SHazByPosition");

                entity.Property(e => e.CurrentUser).HasMaxLength(128);

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pnum).HasColumnName("PNum");

                entity.Property(e => e.Shzid).HasColumnName("SHZID");

                entity.Property(e => e.Shznum)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SHZNum");

                entity.Property(e => e.Shztitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("SHZTitle");
            });

            modelBuilder.Entity<VQtsSkbyPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SKByPosition");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.CurrentUser).HasMaxLength(128);

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pnum).HasColumnName("PNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.Sknumber)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("SKNumber");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");
            });

            modelBuilder.Entity<VQtsSkbyTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SKByTask");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skill)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("Skill#");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VQtsSkillsKnowledge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SkillsKnowledge");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.SkillDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.SkillNum)
                    .HasMaxLength(123)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.TopicNum)
                    .HasMaxLength(92)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VQtsStep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Steps");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.StepDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.StepNumber).HasMaxLength(124);

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VQtsStudentFormsAverage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_StudentForms_Averages");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Fid).HasColumnName("FID");
            });

            modelBuilder.Entity<VQtsStudentFormsAvgByCourse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_StudentForms_Avg_ByCourse");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Fid).HasColumnName("FID");
            });

            modelBuilder.Entity<VQtsSubDuty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_SubDuty");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.SubDuty).HasMaxLength(320);
            });

            modelBuilder.Entity<VQtsTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Tasks");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Desc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.Num).HasMaxLength(93);

                entity.Property(e => e.SubDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.SubNum).HasMaxLength(124);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsTaskAnnualReview>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskAnnualReview");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pid2).HasColumnName("PID2");

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Thconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THConditions");

                entity.Property(e => e.Thcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THCriteria");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THNote");

                entity.Property(e => e.Thnum)
                    .HasMaxLength(50)
                    .HasColumnName("THNum");

                entity.Property(e => e.ThposHistory).HasColumnName("THPosHistory");

                entity.Property(e => e.ThprocList)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THProcList");

                entity.Property(e => e.Threferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THReferences");

                entity.Property(e => e.ThrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("THRevisedBy");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Thtools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THTools");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsTaskChangeAfterPositionAdd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskChangeAfterPositionAdd");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Thconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THConditions");

                entity.Property(e => e.Thcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THCriteria");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THNote");

                entity.Property(e => e.Thnum)
                    .HasMaxLength(50)
                    .HasColumnName("THNum");

                entity.Property(e => e.ThposHistory).HasColumnName("THPosHistory");

                entity.Property(e => e.ThprocList)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THProcList");

                entity.Property(e => e.Threferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THReferences");

                entity.Property(e => e.ThrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("THRevisedBy");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Thtools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THTools");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsTaskHistoryNotBaseline>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskHistoryNotBaseline");

                entity.Property(e => e.ReviewComment)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Thconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THConditions");

                entity.Property(e => e.Thcriteria)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THCriteria");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("THID");

                entity.Property(e => e.Thnote)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THNote");

                entity.Property(e => e.Thnum)
                    .HasMaxLength(50)
                    .HasColumnName("THNum");

                entity.Property(e => e.ThposHistory).HasColumnName("THPosHistory");

                entity.Property(e => e.ThprocList)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THProcList");

                entity.Property(e => e.Threferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THReferences");

                entity.Property(e => e.ThrevisedBy)
                    .HasMaxLength(50)
                    .HasColumnName("THRevisedBy");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Thtools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("THTools");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsTaskInactiveDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Task_Inactive_Dates");

                entity.Property(e => e.EndInactive).HasColumnType("datetime");

                entity.Property(e => e.StartInactive).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskMaxChangeDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskMaxChangeDate");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskMaxChgDateNoRr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskMaxChgDateNoRR");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskNumberChange>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskNumberChange");

                entity.Property(e => e.NewDate).HasColumnType("datetime");

                entity.Property(e => e.NewNumber).HasMaxLength(50);

                entity.Property(e => e.OldDate).HasColumnType("datetime");

                entity.Property(e => e.OldNumber).HasMaxLength(50);

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualRecStep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualRecSteps");

                entity.Property(e => e.Cnt).HasColumnName("cnt");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualification>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.QualDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsBySk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualificationsBySK");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Skill)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("Skill#");
            });

            modelBuilder.Entity<VQtsTaskQualificationsFinal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_Final");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.QualDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsFinalOjt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_FinalOJT");

                entity.Property(e => e.Cordesc)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsFinalUnion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_FinalUnion");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.QualDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsOjt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_OJT");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsOjtDesk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_OJT_Desks");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.EvalDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskQualificationsSk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQUalifications_SK");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Skill)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("Skill#");
            });

            modelBuilder.Entity<VQtsTaskQualificationsSk1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQUalificationsSK");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.MaxDate).HasColumnType("datetime");

                entity.Property(e => e.Skill)
                    .HasMaxLength(123)
                    .IsUnicode(false)
                    .HasColumnName("Skill#");
            });

            modelBuilder.Entity<VQtsTaskQualificationsUnion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskQualifications_Union");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.QualDate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskSkills");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.SkillNum)
                    .HasMaxLength(123)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskStatements");

                entity.Property(e => e.Thdate)
                    .HasColumnType("datetime")
                    .HasColumnName("THDate");

                entity.Property(e => e.Thid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("THID");

                entity.Property(e => e.Thstatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("THStatement");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTaskStatementChange>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TaskStatementChange");

                entity.Property(e => e.NewDate).HasColumnType("datetime");

                entity.Property(e => e.NewStatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.OldStatement)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.Olddate).HasColumnType("datetime");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTasksActive>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TasksActive");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Tabbrev)
                    .HasMaxLength(255)
                    .HasColumnName("TAbbrev");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsTasksAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TasksAll");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Desc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.Num).HasMaxLength(93);

                entity.Property(e => e.SubDesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.SubNum).HasMaxLength(124);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VQtsTasksIntroduction>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TasksIntroduction");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<VQtsTasksOnly>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TasksOnly");

                entity.Property(e => e.Da)
                    .HasMaxLength(320)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDuty).HasMaxLength(289);

                entity.Property(e => e.Task)
                    .HasMaxLength(93)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VQtsTasksOnlyIncludeInactive>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TasksOnlyIncludeInactive");

                entity.Property(e => e.Da)
                    .HasMaxLength(320)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDuty).HasMaxLength(289);

                entity.Property(e => e.Task)
                    .HasMaxLength(93)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VQtsTasksSkillsKnowledge>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Tasks_SkillsKnowledge");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VQtsTasksSkillsRelated>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Tasks_SkillsRelated");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VQtsTestDistractor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TestDistractors");

                entity.Property(e => e.Distractor).IsUnicode(false);

                entity.Property(e => e.DistractorDetails).IsUnicode(false);

                entity.Property(e => e.TestItemId).HasColumnName("TestItemID");

                entity.Property(e => e.Text)
                    .IsUnicode(false)
                    .HasColumnName("text");
            });

            modelBuilder.Entity<VQtsTopic>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_Topics");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.Topic)
                    .HasMaxLength(7595)
                    .IsUnicode(false);

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.TopicNum)
                    .HasMaxLength(92)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VQtsTotalCehCompleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_Completed");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<VQtsTotalCehCompletedSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_CompletedSUM");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsTotalCehPlanned>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_Planned");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<VQtsTotalCehPlannedSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_PlannedSUM");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsTotalCehScheduled>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_Scheduled");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<VQtsTotalCehScheduledSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEH_ScheduledSUM");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsTotalCehscCompleted>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEHSC_Completed");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<VQtsTotalCehscCompletedSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEHSC_CompletedSUM");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsTotalCehscScheduled>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEHSC_Scheduled");

                entity.Property(e => e.ClassDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Class_Date");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<VQtsTotalCehscScheduledSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TotalCEHSC_ScheduledSUM");

                entity.Property(e => e.Eid).HasColumnName("EID");
            });

            modelBuilder.Entity<VQtsTrainingSummaryByPosition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TrainingSummaryByPosition");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.CoTotalCeh).HasColumnName("CO_TotalCEH");

                entity.Property(e => e.CompDate).HasColumnType("datetime");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(156);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercpolicy).HasColumnName("NERCPolicy");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");

                entity.Property(e => e.Tyear).HasColumnName("TYear");
            });

            modelBuilder.Entity<VQtsTrainingSummaryByPositionExtra>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vQTS_TrainingSummaryByPosition_Extra");

                entity.Property(e => e.CehBio).HasColumnName("CEH_BIO");

                entity.Property(e => e.CehBito).HasColumnName("CEH_BITO");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CehProf).HasColumnName("CEH_Prof");

                entity.Property(e => e.CehRc).HasColumnName("CEH_RC");

                entity.Property(e => e.CehReg).HasColumnName("CEH_Reg");

                entity.Property(e => e.CehTo).HasColumnName("CEH_TO");

                entity.Property(e => e.CertAbbrev).HasMaxLength(50);

                entity.Property(e => e.CoTotalCeh).HasColumnName("CO_TotalCEH");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(156);

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercpolicy).HasColumnName("NERCPolicy");

                entity.Property(e => e.Position).HasMaxLength(203);

                entity.Property(e => e.RegCertType).HasMaxLength(50);

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");

                entity.Property(e => e.TotalCehfinal).HasColumnName("TotalCEHFinal");

                entity.Property(e => e.TotalReqCehs).HasColumnName("TotalReqCEHs");

                entity.Property(e => e.Tyear).HasColumnName("TYear");
            });

            modelBuilder.Entity<ViewQtsCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_Categories");

                entity.Property(e => e.Cdesc)
                    .HasMaxLength(255)
                    .HasColumnName("CDesc");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubDesc)
                    .HasMaxLength(255)
                    .HasColumnName("CSubDesc");

                entity.Property(e => e.CsubNum)
                    .HasMaxLength(61)
                    .IsUnicode(false)
                    .HasColumnName("CSubNum");
            });

            modelBuilder.Entity<ViewQtsDum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_DA");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum)
                    .HasMaxLength(61)
                    .IsUnicode(false)
                    .HasColumnName("DASubNum");

                entity.Property(e => e.DutyDesc).HasMaxLength(255);
            });

            modelBuilder.Entity<ViewQtsDynamicQuery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_Dynamic_Query");

                entity.Property(e => e.ActPartialCredits).HasColumnName("Act_PartialCredits");

                entity.Property(e => e.CehNerc).HasColumnName("CEH_NERC");

                entity.Property(e => e.CertDesc).HasMaxLength(50);

                entity.Property(e => e.CertDescExisting).HasMaxLength(50);

                entity.Property(e => e.ChNerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_NERCCertExpDate");

                entity.Property(e => e.ChNerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CH_NERCCertIssueDate");

                entity.Property(e => e.ChNerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("CH_NERCCertNum");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.EfirstName)
                    .HasMaxLength(50)
                    .HasColumnName("EFirstName");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.ElastName)
                    .HasMaxLength(50)
                    .HasColumnName("ELastName");

                entity.Property(e => e.EnotCertified).HasColumnName("ENotCertified");

                entity.Property(e => e.Enum)
                    .HasMaxLength(50)
                    .HasColumnName("ENum");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.NerccertArea).HasColumnName("NERCCertArea");

                entity.Property(e => e.NerccertAreaExisting).HasColumnName("NERCCertArea_Existing");

                entity.Property(e => e.NerccertExpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertExpDate");

                entity.Property(e => e.NerccertIssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NERCCertIssueDate");

                entity.Property(e => e.NerccertNum)
                    .HasMaxLength(50)
                    .HasColumnName("NERCCertNum");

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Nsname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NSName");

                entity.Property(e => e.Oname)
                    .HasMaxLength(150)
                    .HasColumnName("OName");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.TopicsEo).HasColumnName("Topics_EO");

                entity.Property(e => e.TotalCeh).HasColumnName("TotalCEH");
            });

            modelBuilder.Entity<ViewQtsDynamicQuery2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_Dynamic_Query2");

                entity.Property(e => e.Cdesc)
                    .HasMaxLength(255)
                    .HasColumnName("CDesc");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.CsubDesc)
                    .HasMaxLength(255)
                    .HasColumnName("CSubDesc");

                entity.Property(e => e.CsubNum)
                    .HasMaxLength(61)
                    .IsUnicode(false)
                    .HasColumnName("CSubNum");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum)
                    .HasMaxLength(61)
                    .IsUnicode(false)
                    .HasColumnName("DASubNum");

                entity.Property(e => e.DutyDesc).HasMaxLength(255);

                entity.Property(e => e.Nercid)
                    .HasMaxLength(50)
                    .HasColumnName("NERCID");

                entity.Property(e => e.Notes).HasColumnType("text");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Prtitle)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("PRTitle");

                entity.Property(e => e.Revision).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.ScenarioTitle)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.SkillNum)
                    .HasMaxLength(123)
                    .IsUnicode(false);

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.TaskNmbr).HasMaxLength(93);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.TopicNum)
                    .HasMaxLength(92)
                    .IsUnicode(false);

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");

                entity.Property(e => e.Tyear)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("TYear");
            });

            modelBuilder.Entity<ViewQtsSk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_SK");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewQtsSkillNum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_SkillNum");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.SkillNum)
                    .HasMaxLength(123)
                    .IsUnicode(false);

                entity.Property(e => e.TopicNum)
                    .HasMaxLength(92)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewQtsTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_Tasks");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.TaskNmbr).HasMaxLength(93);

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<ViewQtsTyear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_QTS_TYear");

                entity.Property(e => e.Tyear)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("TYear");
            });

            modelBuilder.Entity<VwCategoryTopicSk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCategoryTopicSK");

                entity.Property(e => e.CatDesc).HasMaxLength(255);

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.SubCatDesc).HasMaxLength(255);

                entity.Property(e => e.SubCid).HasColumnName("SubCID");

                entity.Property(e => e.TopicDesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false);

                entity.Property(e => e.TopicSkid).HasColumnName("TopicSKID");
            });

            modelBuilder.Entity<VwClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwClasses");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.EndTimeStr)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Inid).HasColumnName("INID");

                entity.Property(e => e.Inname)
                    .HasMaxLength(50)
                    .HasColumnName("INName");

                entity.Property(e => e.Lcdesc)
                    .HasMaxLength(255)
                    .HasColumnName("LCDesc");

                entity.Property(e => e.Lcid).HasColumnName("LCID");

                entity.Property(e => e.SelfRegEndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTimeStr)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwContentObject>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwContentObjects");

                entity.Property(e => e.CodateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("CODateStamp");

                entity.Property(e => e.Codesc)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("CODesc");

                entity.Property(e => e.Coid).HasColumnName("COID");

                entity.Property(e => e.Coname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COName");

                entity.Property(e => e.Conum).HasColumnName("CONum");

                entity.Property(e => e.Conumber)
                    .HasMaxLength(48)
                    .HasColumnName("CONumber");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RoomLtr).HasMaxLength(5);

                entity.Property(e => e.ShelfId).HasColumnName("ShelfID");

                entity.Property(e => e.SourceFile)
                    .IsRequired()
                    .HasMaxLength(2500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCourseSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCourseSkills");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Skill)
                    .HasMaxLength(23)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");
            });

            modelBuilder.Entity<VwDefaultTraining>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDefaultTraining");

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<VwDutyAreaTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDutyAreaTasks");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.DutyArea).HasMaxLength(11);

                entity.Property(e => e.SubDadesc)
                    .HasMaxLength(255)
                    .HasColumnName("subDADesc");

                entity.Property(e => e.SubDaid).HasColumnName("subDAID");

                entity.Property(e => e.SubDutyArea).HasMaxLength(22);

                entity.Property(e => e.Tabbrev)
                    .HasMaxLength(255)
                    .HasColumnName("TAbbrev");

                entity.Property(e => e.Task).HasMaxLength(33);

                entity.Property(e => e.TaskDaid).HasColumnName("taskDAID");

                entity.Property(e => e.Taskts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("taskts");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VwIlatask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwILATasks");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Da)
                    .HasMaxLength(270)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.MainDuty).HasMaxLength(264);

                entity.Property(e => e.Task)
                    .HasMaxLength(24)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Ts)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ts");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VwImportedDocument>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwImportedDocuments");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.Property(e => e.Dtdesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DTDesc");

                entity.Property(e => e.Dtid).HasColumnName("DTID");

                entity.Property(e => e.DtsortOrder).HasColumnName("DTSortOrder");

                entity.Property(e => e.Ldcomment)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("LDComment");

                entity.Property(e => e.LddateStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("LDDateStamp");

                entity.Property(e => e.LddocDate)
                    .HasColumnType("datetime")
                    .HasColumnName("LDDocDate");

                entity.Property(e => e.LdfileName)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("LDFileName");

                entity.Property(e => e.Ldid).HasColumnName("LDID");

                entity.Property(e => e.Ldlid).HasColumnName("LDLID");

                entity.Property(e => e.Ldreference).HasColumnName("LDReference");

                entity.Property(e => e.Ldtype).HasColumnName("LDType");

                entity.Property(e => e.LinkItemId).HasColumnName("LinkItemID");

                entity.Property(e => e.LinkedDocId).HasColumnName("LinkedDocID");

                entity.Property(e => e.TypeDesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<VwJobAnalysisSurvey>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwJobAnalysisSurvey");

                entity.Property(e => e.Da)
                    .HasMaxLength(280)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.Daso)
                    .HasMaxLength(11)
                    .HasColumnName("DASO");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.DifComments)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.StatusFinal).HasColumnName("Status_Final");

                entity.Property(e => e.TaskNum).HasMaxLength(33);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VwLinkedDocument>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwLinkedDocuments");

                entity.Property(e => e.Cldate)
                    .HasColumnType("datetime")
                    .HasColumnName("CLDate");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Cordesc)
                    .HasMaxLength(100)
                    .HasColumnName("CORDesc");

                entity.Property(e => e.Corid).HasColumnName("CORID");

                entity.Property(e => e.Cornum)
                    .HasMaxLength(150)
                    .HasColumnName("CORNum");

                entity.Property(e => e.Dadesc)
                    .HasMaxLength(255)
                    .HasColumnName("DADesc");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.Property(e => e.Dtdesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("DTDesc");

                entity.Property(e => e.Dtid).HasColumnName("DTID");

                entity.Property(e => e.DtsortOrder).HasColumnName("DTSortOrder");

                entity.Property(e => e.DutyArea).HasMaxLength(11);

                entity.Property(e => e.EfirstName)
                    .HasMaxLength(50)
                    .HasColumnName("EFirstName");

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.ElastName)
                    .HasMaxLength(50)
                    .HasColumnName("ELastName");

                entity.Property(e => e.LdfileName)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("LDFileName");

                entity.Property(e => e.Ldid).HasColumnName("LDID");

                entity.Property(e => e.LinkItemId).HasColumnName("LinkItemID");

                entity.Property(e => e.LinkedDocId).HasColumnName("LinkedDocID");

                entity.Property(e => e.Pabbrev)
                    .HasMaxLength(50)
                    .HasColumnName("PAbbrev");

                entity.Property(e => e.Pdesc)
                    .HasMaxLength(150)
                    .HasColumnName("PDesc");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Pnum).HasColumnName("PNum");

                entity.Property(e => e.SubDadesc)
                    .HasMaxLength(255)
                    .HasColumnName("subDADesc");

                entity.Property(e => e.SubDutyArea).HasMaxLength(22);

                entity.Property(e => e.Suid).HasColumnName("SUID");

                entity.Property(e => e.Suname)
                    .HasMaxLength(100)
                    .HasColumnName("SUName");

                entity.Property(e => e.Tabbrev)
                    .HasMaxLength(255)
                    .HasColumnName("TAbbrev");

                entity.Property(e => e.Task).HasMaxLength(33);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.TypeDesc)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");
            });

            modelBuilder.Entity<VwPositionsTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPositionsTasks");

                entity.Property(e => e.Da)
                    .HasMaxLength(84)
                    .IsUnicode(false)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.Flag)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.MainDuty).HasMaxLength(298);

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.R6Reason)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("R6_reason");

                entity.Property(e => e.Rr)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("RR");

                entity.Property(e => e.RrReason)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("RR_reason");

                entity.Property(e => e.Task)
                    .HasMaxLength(85)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tconditions)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TConditions");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(255)
                    .HasColumnName("TPDesc");

                entity.Property(e => e.Tpid).HasColumnName("TPID");

                entity.Property(e => e.Tpnum).HasColumnName("TPNum");

                entity.Property(e => e.Treferences)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TReferences");

                entity.Property(e => e.Tstandards)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TStandards");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");

                entity.Property(e => e.Ttools)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("TTools");
            });

            modelBuilder.Entity<VwProcedureTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwProcedureTasks");

                entity.Property(e => e.Da)
                    .HasMaxLength(84)
                    .IsUnicode(false)
                    .HasColumnName("DA");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDuty).HasMaxLength(298);

                entity.Property(e => e.Prid).HasColumnName("PRID");

                entity.Property(e => e.Task)
                    .HasMaxLength(84)
                    .HasColumnName("Task#");

                entity.Property(e => e.Task1)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("Task");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VwQtsMainDaSubDaTaskGap>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwQTS_MainDA_SubDA_TaskGAP");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.MainDaid).HasColumnName("MainDAID");

                entity.Property(e => e.MainDuty).HasMaxLength(269);

                entity.Property(e => e.SubDaid).HasColumnName("SubDAID");

                entity.Property(e => e.SubDuty).HasMaxLength(280);

                entity.Property(e => e.TaskDesc).HasMaxLength(4000);

                entity.Property(e => e.TaskNumber).HasMaxLength(33);

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");
            });

            modelBuilder.Entity<VwQtsMainDaSubDum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwQTS_MainDA_SubDA");

                entity.Property(e => e.MainDaid).HasColumnName("MainDAID");

                entity.Property(e => e.MainDuty).HasMaxLength(269);

                entity.Property(e => e.SubDaid).HasColumnName("SubDAID");

                entity.Property(e => e.SubDuty).HasMaxLength(280);
            });

            modelBuilder.Entity<VwStudent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwStudents");

                entity.Property(e => e.Clid).HasColumnName("CLID");

                entity.Property(e => e.CompGrade).HasMaxLength(50);

                entity.Property(e => e.Eid).HasColumnName("EID");

                entity.Property(e => e.Employee).HasMaxLength(162);

                entity.Property(e => e.PartialExtra).HasColumnName("Partial_Extra");

                entity.Property(e => e.PartialOther).HasColumnName("Partial_Other");

                entity.Property(e => e.PartialReg).HasColumnName("Partial_Reg");

                entity.Property(e => e.PartialReg2).HasColumnName("Partial_Reg2");

                entity.Property(e => e.PartialSim).HasColumnName("Partial_Sim");

                entity.Property(e => e.PartialStd).HasColumnName("Partial_std");

                entity.Property(e => e.PartialTotal).HasColumnName("Partial_Total");

                entity.Property(e => e.PartialTotalCehs).HasColumnName("Partial_TotalCEHs");

                entity.Property(e => e.Pid).HasColumnName("PID");
            });

            modelBuilder.Entity<VwTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTasks");

                entity.Property(e => e.Daid).HasColumnName("DAID");

                entity.Property(e => e.Daletter)
                    .HasMaxLength(1)
                    .HasColumnName("DALetter");

                entity.Property(e => e.Danum).HasColumnName("DANum");

                entity.Property(e => e.DasubNum).HasColumnName("DASubNum");

                entity.Property(e => e.StepNum).HasMaxLength(24);

                entity.Property(e => e.Task)
                    .HasMaxLength(18)
                    .HasColumnName("Task#");

                entity.Property(e => e.Tdesc)
                    .HasMaxLength(7000)
                    .IsUnicode(false)
                    .HasColumnName("TDesc");

                entity.Property(e => e.Tid).HasColumnName("TID");

                entity.Property(e => e.Tnum).HasColumnName("TNum");

                entity.Property(e => e.TsubNum).HasColumnName("TSubNum");
            });

            modelBuilder.Entity<VwTaskSkill>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTaskSkills");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Skill)
                    .HasMaxLength(23)
                    .IsUnicode(false);

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.SksubNum).HasColumnName("SKSubNum");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<VwTopic>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTopics");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.Property(e => e.Cnum).HasColumnName("CNum");

                entity.Property(e => e.CsubNum).HasColumnName("CSubNum");

                entity.Property(e => e.Skdesc)
                    .HasMaxLength(7500)
                    .IsUnicode(false)
                    .HasColumnName("SKDesc");

                entity.Property(e => e.Skid).HasColumnName("SKID");

                entity.Property(e => e.Sknum).HasColumnName("SKNum");

                entity.Property(e => e.Topic)
                    .HasMaxLength(17)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
