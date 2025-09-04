using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Program
{
    public decimal Id { get; set; }

    public virtual ICollection<EvalEvent> EvalEvents { get; set; } = new List<EvalEvent>();

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual ICollection<ExamUnitOb> ExamUnitObs { get; set; } = new List<ExamUnitOb>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgFkExamUnitNavigations { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgFkParentUnitNavigations { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgFkUnitNavigations { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<LsCatalogLesson> LsCatalogLessons { get; set; } = new List<LsCatalogLesson>();

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemains { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmains { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsDoclinkTrack> LsDoclinkTracks { get; set; } = new List<LsDoclinkTrack>();

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsExamEvent> LsExamEvents { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsExternalCompletion> LsExternalCompletions { get; set; } = new List<LsExternalCompletion>();

    public virtual ICollection<LsHoldRelease> LsHoldReleases { get; set; } = new List<LsHoldRelease>();

    public virtual ICollection<LsLearningEventAttempt> LsLearningEventAttempts { get; set; } = new List<LsLearningEventAttempt>();

    public virtual ICollection<LsLearningEventProgram> LsLearningEventPrograms { get; set; } = new List<LsLearningEventProgram>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTracks { get; set; } = new List<LsLearningEventTrack>();

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual ICollection<LsOnlineExamQuestionMa> LsOnlineExamQuestionMas { get; set; } = new List<LsOnlineExamQuestionMa>();

    public virtual ICollection<LsOnlineExamQuestionMc> LsOnlineExamQuestionMcs { get; set; } = new List<LsOnlineExamQuestionMc>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestions { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsPaEvaluatorTrainer> LsPaEvaluatorTrainers { get; set; } = new List<LsPaEvaluatorTrainer>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequests { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsPreviewExam> LsPreviewExams { get; set; } = new List<LsPreviewExam>();

    public virtual ICollection<LsProgramCompletion> LsProgramCompletions { get; set; } = new List<LsProgramCompletion>();

    public virtual ICollection<LsQualCard> LsQualCards { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsSelectedExam> LsSelectedExams { get; set; } = new List<LsSelectedExam>();

    public virtual ICollection<LsTaskQualification> LsTaskQualifications { get; set; } = new List<LsTaskQualification>();

    public virtual ICollection<ProgramAlternateAudit> ProgramAlternateAuditFkPrimaryNavigations { get; set; } = new List<ProgramAlternateAudit>();

    public virtual ICollection<ProgramAlternateAudit> ProgramAlternateAuditFkProgramNavigations { get; set; } = new List<ProgramAlternateAudit>();

    public virtual ICollection<ProgramAlternate> ProgramAlternates { get; set; } = new List<ProgramAlternate>();

    public virtual ICollection<ProgramComment> ProgramComments { get; set; } = new List<ProgramComment>();

    public virtual ICollection<ProgramExamOnlyWeighting> ProgramExamOnlyWeightingFkProgramSourceNavigations { get; set; } = new List<ProgramExamOnlyWeighting>();

    public virtual ICollection<ProgramExamOnlyWeighting> ProgramExamOnlyWeightingFkProgramWeightedNavigations { get; set; } = new List<ProgramExamOnlyWeighting>();

    public virtual ICollection<ProgramHierarchy> ProgramHierarchyFkChildNavigations { get; set; } = new List<ProgramHierarchy>();

    public virtual ICollection<ProgramHierarchy> ProgramHierarchyFkParentNavigations { get; set; } = new List<ProgramHierarchy>();

    public virtual ICollection<ProgramHtml> ProgramHtmls { get; set; } = new List<ProgramHtml>();

    public virtual ICollection<ProgramImpl> ProgramImpls { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramObjectiveContent> ProgramObjectiveContents { get; set; } = new List<ProgramObjectiveContent>();

    public virtual ICollection<ProgramPrerequisite> ProgramPrerequisiteFkProgramPrerequisiteNavigations { get; set; } = new List<ProgramPrerequisite>();

    public virtual ICollection<ProgramPrerequisite> ProgramPrerequisiteFkProgramSourceNavigations { get; set; } = new List<ProgramPrerequisite>();

    public virtual ICollection<ProgramStaticExam> ProgramStaticExams { get; set; } = new List<ProgramStaticExam>();

    public virtual ICollection<QualCardImpl> QualCardImpls { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<RevisionLogProgram> RevisionLogPrograms { get; set; } = new List<RevisionLogProgram>();

    public virtual ICollection<Sequencing> Sequencings { get; set; } = new List<Sequencing>();

    public virtual ICollection<ExamOnlineProfile> FkExamOnlineProfiles { get; set; } = new List<ExamOnlineProfile>();
}
