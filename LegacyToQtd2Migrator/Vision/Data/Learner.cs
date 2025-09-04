using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Learner
{
    public decimal Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public decimal CanBeProctor { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateLearnerExpired { get; set; }

    public decimal? FkCreatedBy { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal FkCompany { get; set; }

    public decimal FkAddress { get; set; }

    public string Userdefined1 { get; set; }

    public string Userdefined2 { get; set; }

    public string Userdefined3 { get; set; }

    public DateTime? DatePwordChanged { get; set; }

    public decimal ExpireNotify { get; set; }

    public decimal? IsEvaluator { get; set; }

    public decimal? IsTrainer { get; set; }

    public decimal? Supervisor { get; set; }

    public decimal MustBeProctored { get; set; }

    public string TelephoneNumber { get; set; }

    public decimal? TempPassword { get; set; }

    public string MiddleName { get; set; }

    public string Suffix { get; set; }

    public decimal FkSecurityCode { get; set; }

    public string IdpId { get; set; }

    public virtual ICollection<EvalEvent> EvalEvents { get; set; } = new List<EvalEvent>();

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual ICollection<ExamEvent> ExamEventFkLearnerNavigations { get; set; } = new List<ExamEvent>();

    public virtual ICollection<ExamEvent> ExamEventFkProctorNavigations { get; set; } = new List<ExamEvent>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbackFkLearnerNavigations { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbackFkProctorNavigations { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbackFkResolvedByLNavigations { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamOnlineTesting> ExamOnlineTestings { get; set; } = new List<ExamOnlineTesting>();

    public virtual ICollection<ExamQuestionEventFi> ExamQuestionEventFis { get; set; } = new List<ExamQuestionEventFi>();

    public virtual ICollection<ExamQuestionEventMa> ExamQuestionEventMas { get; set; } = new List<ExamQuestionEventMa>();

    public virtual ICollection<ExamQuestionEventMc> ExamQuestionEventMcs { get; set; } = new List<ExamQuestionEventMc>();

    public virtual ICollection<ExamQuestionEventSa> ExamQuestionEventSas { get; set; } = new List<ExamQuestionEventSa>();

    public virtual ICollection<ExamQuestionEventTf> ExamQuestionEventTfs { get; set; } = new List<ExamQuestionEventTf>();

    public virtual ICollection<ExamQuestionEvent> ExamQuestionEvents { get; set; } = new List<ExamQuestionEvent>();

    public virtual LsAddress FkAddressNavigation { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual Learner FkExpiredByNavigation { get; set; }

    public virtual LsSecurityCode FkSecurityCodeNavigation { get; set; }

    public virtual ICollection<Learner> InverseFkCreatedByNavigation { get; set; } = new List<Learner>();

    public virtual ICollection<Learner> InverseFkExpiredByNavigation { get; set; } = new List<Learner>();

    public virtual ICollection<Learner> InverseSupervisorNavigation { get; set; } = new List<Learner>();

    public virtual ICollection<LsCertJob> LsCertJobFkCreatedbyNavigations { get; set; } = new List<LsCertJob>();

    public virtual ICollection<LsCertJob> LsCertJobFkLastModifiedbyNavigations { get; set; } = new List<LsCertJob>();

    public virtual ICollection<LsCertLrnr> LsCertLrnrFkCreatedbyNavigations { get; set; } = new List<LsCertLrnr>();

    public virtual ICollection<LsCertLrnr> LsCertLrnrFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnr>();

    public virtual ICollection<LsCertLrnr> LsCertLrnrFkLearnerNavigations { get; set; } = new List<LsCertLrnr>();

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmntFkCreatedbyNavigations { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmntFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmntFkLearnerNavigations { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByruleFkCreatedbyNavigations { get; set; } = new List<LsCertLrnrRcrdByrule>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByruleFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnrRcrdByrule>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByruleFkLearnerNavigations { get; set; } = new List<LsCertLrnrRcrdByrule>();

    public virtual ICollection<LsCertLrnrRecordBycert> LsCertLrnrRecordBycertFkCreatedbyNavigations { get; set; } = new List<LsCertLrnrRecordBycert>();

    public virtual ICollection<LsCertLrnrRecordBycert> LsCertLrnrRecordBycertFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnrRecordBycert>();

    public virtual ICollection<LsCertLrnrRecordBycert> LsCertLrnrRecordBycertFkLearnerNavigations { get; set; } = new List<LsCertLrnrRecordBycert>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrseFkCreatedbyNavigations { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrseFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrseFkLearnerNavigations { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemainFkCreatedbyNavigations { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemainFkLastModifiedbyNavigations { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemainFkLearnerNavigations { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertManscreCrdit> LsCertManscreCrditFkCreatedbyNavigations { get; set; } = new List<LsCertManscreCrdit>();

    public virtual ICollection<LsCertManscreCrdit> LsCertManscreCrditFkLastModifiedbyNavigations { get; set; } = new List<LsCertManscreCrdit>();

    public virtual ICollection<LsCertManscreCrdit> LsCertManscreCrditFkLearnerNavigations { get; set; } = new List<LsCertManscreCrdit>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmainFkCreatedbyNavigations { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmainFkLastModifiedbyNavigations { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmainFkLearnerNavigations { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsCertRequirement> LsCertRequirementFkCreatedbyNavigations { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertRequirement> LsCertRequirementFkLastModifiedbyNavigations { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRuleFkCreatedbyNavigations { get; set; } = new List<LsCertRequirementsRule>();

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRuleFkLastModifiedbyNavigations { get; set; } = new List<LsCertRequirementsRule>();

    public virtual ICollection<LsCertification> LsCertificationFkCreatedbyNavigations { get; set; } = new List<LsCertification>();

    public virtual ICollection<LsCertification> LsCertificationFkModifiedbyNavigations { get; set; } = new List<LsCertification>();

    public virtual LsDeveloperToLearner LsDeveloperToLearner { get; set; }

    public virtual ICollection<LsDoclinkTrack> LsDoclinkTracks { get; set; } = new List<LsDoclinkTrack>();

    public virtual ICollection<LsDocument> LsDocumentFkCreatedByNavigations { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsDocument> LsDocumentFkLearnerNavigations { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsDocument> LsDocumentFkModifiedByNavigations { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsEventAudit> LsEventAudits { get; set; } = new List<LsEventAudit>();

    public virtual ICollection<LsExamEvent> LsExamEventFkLearnerNavigations { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsExamEvent> LsExamEventFkProctorNavigations { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsExamResult> LsExamResults { get; set; } = new List<LsExamResult>();

    public virtual ICollection<LsExternalCompletion> LsExternalCompletions { get; set; } = new List<LsExternalCompletion>();

    public virtual ICollection<LsHoldRelease> LsHoldReleases { get; set; } = new List<LsHoldRelease>();

    public virtual ICollection<LsLearnerLogin> LsLearnerLogins { get; set; } = new List<LsLearnerLogin>();

    public virtual ICollection<LsLearnerPositionHist> LsLearnerPositionHists { get; set; } = new List<LsLearnerPositionHist>();

    public virtual ICollection<LsLearnerPosition> LsLearnerPositions { get; set; } = new List<LsLearnerPosition>();

    public virtual ICollection<LsLearningEventAttempt> LsLearningEventAttempts { get; set; } = new List<LsLearningEventAttempt>();

    public virtual ICollection<LsLearningEvent> LsLearningEventFkCreatedByNavigations { get; set; } = new List<LsLearningEvent>();

    public virtual ICollection<LsLearningEvent> LsLearningEventFkInstructorNavigations { get; set; } = new List<LsLearningEvent>();

    public virtual ICollection<LsLearningEventLearner> LsLearningEventLearners { get; set; } = new List<LsLearningEventLearner>();

    public virtual ICollection<LsLearningEventProgram> LsLearningEventPrograms { get; set; } = new List<LsLearningEventProgram>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTrackFkInstructorNavigations { get; set; } = new List<LsLearningEventTrack>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTrackFkLearnerNavigations { get; set; } = new List<LsLearningEventTrack>();

    public virtual LsLoginAttempt LsLoginAttempt { get; set; }

    public virtual ICollection<LsObjExternalTrack> LsObjExternalTracks { get; set; } = new List<LsObjExternalTrack>();

    public virtual ICollection<LsObjectiveMastery> LsObjectiveMasteries { get; set; } = new List<LsObjectiveMastery>();

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual ICollection<LsOnlineExamQuestionMa> LsOnlineExamQuestionMas { get; set; } = new List<LsOnlineExamQuestionMa>();

    public virtual ICollection<LsOnlineExamQuestionMc> LsOnlineExamQuestionMcs { get; set; } = new List<LsOnlineExamQuestionMc>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestions { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsOrg> LsOrgs { get; set; } = new List<LsOrg>();

    public virtual ICollection<LsPaEvaluatorTrainer> LsPaEvaluatorTrainers { get; set; } = new List<LsPaEvaluatorTrainer>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequestFkEvaluatorNavigations { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequestFkLearnerNavigations { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsPasswordStorage> LsPasswordStorages { get; set; } = new List<LsPasswordStorage>();

    public virtual ICollection<LsPreviewExam> LsPreviewExams { get; set; } = new List<LsPreviewExam>();

    public virtual ICollection<LsProctorSession> LsProctorSessions { get; set; } = new List<LsProctorSession>();

    public virtual ICollection<LsProgramCompletion> LsProgramCompletions { get; set; } = new List<LsProgramCompletion>();

    public virtual ICollection<LsQualCardEvent> LsQualCardEvents { get; set; } = new List<LsQualCardEvent>();

    public virtual ICollection<LsQualCard> LsQualCardFkCreatedByNavigations { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsQualCard> LsQualCardFkModifiedByNavigations { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsQualCardRouteHistory> LsQualCardRouteHistoryFkLearnerNavigations { get; set; } = new List<LsQualCardRouteHistory>();

    public virtual ICollection<LsQualCardRouteHistory> LsQualCardRouteHistoryFkSupervisorNavigations { get; set; } = new List<LsQualCardRouteHistory>();

    public virtual ICollection<LsQualCardRoute> LsQualCardRoutes { get; set; } = new List<LsQualCardRoute>();

    public virtual ICollection<LsQualEvaluatorTrainer> LsQualEvaluatorTrainers { get; set; } = new List<LsQualEvaluatorTrainer>();

    public virtual ICollection<LsQualJobPosition> LsQualJobPositions { get; set; } = new List<LsQualJobPosition>();

    public virtual ICollection<LsSelectedExam> LsSelectedExams { get; set; } = new List<LsSelectedExam>();

    public virtual ICollection<LsSurveyEvent> LsSurveyEventFkChangedByNavigations { get; set; } = new List<LsSurveyEvent>();

    public virtual ICollection<LsSurveyEvent> LsSurveyEventFkCreatedByNavigations { get; set; } = new List<LsSurveyEvent>();

    public virtual ICollection<LsSurvey> LsSurveyFkChangedByNavigations { get; set; } = new List<LsSurvey>();

    public virtual ICollection<LsSurvey> LsSurveyFkCreatedByNavigations { get; set; } = new List<LsSurvey>();

    public virtual ICollection<LsSurveyItemResultsDif> LsSurveyItemResultsDifFkLearnerChangedbyNavigations { get; set; } = new List<LsSurveyItemResultsDif>();

    public virtual ICollection<LsSurveyItemResultsDif> LsSurveyItemResultsDifFkLearnerNavigations { get; set; } = new List<LsSurveyItemResultsDif>();

    public virtual ICollection<LsSurveyItem> LsSurveyItems { get; set; } = new List<LsSurveyItem>();

    public virtual ICollection<LsSurveyeventItem> LsSurveyeventItems { get; set; } = new List<LsSurveyeventItem>();

    public virtual ICollection<LsSurveyeventRespondent> LsSurveyeventRespondentFkChangedByNavigations { get; set; } = new List<LsSurveyeventRespondent>();

    public virtual ICollection<LsSurveyeventRespondent> LsSurveyeventRespondentFkLearnerNavigations { get; set; } = new List<LsSurveyeventRespondent>();

    public virtual ICollection<LsTaskQualStep> LsTaskQualSteps { get; set; } = new List<LsTaskQualStep>();

    public virtual ICollection<LsTaskQualification> LsTaskQualificationFkEvaluatorNavigations { get; set; } = new List<LsTaskQualification>();

    public virtual ICollection<LsTaskQualification> LsTaskQualificationFkLearnerNavigations { get; set; } = new List<LsTaskQualification>();

    public virtual ICollection<LsTaskQualification> LsTaskQualificationFkTrainerNavigations { get; set; } = new List<LsTaskQualification>();

    public virtual ICollection<LsTimeToCompleteImpl> LsTimeToCompleteImpls { get; set; } = new List<LsTimeToCompleteImpl>();

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItems { get; set; } = new List<LsTrainingReqItem>();

    public virtual ICollection<LsVLesson> LsVLessons { get; set; } = new List<LsVLesson>();

    public virtual Learner SupervisorNavigation { get; set; }

    public virtual ICollection<LsCompany> FkCompanies { get; set; } = new List<LsCompany>();

    public virtual ICollection<LsLearningEvent> FkLearningEvents { get; set; } = new List<LsLearningEvent>();
}
