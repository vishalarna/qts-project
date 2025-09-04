using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearningEvent
{
    public decimal Id { get; set; }

    public decimal FkCatalog { get; set; }

    public string Title { get; set; }

    public DateTime? DateCreated { get; set; }

    public decimal? DaysNotify { get; set; }

    public decimal? FkCreatedBy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? TestType { get; set; }

    public decimal? EventType { get; set; }

    public decimal RegType { get; set; }

    public decimal? FkEval { get; set; }

    public decimal? FkInstructor { get; set; }

    public decimal? PassingScore { get; set; }

    public decimal WeightedCourse { get; set; }

    public decimal EnforcePrereqs { get; set; }

    public DateTime? CurrentCtrlDate { get; set; }

    public DateTime? NextCtrlDate { get; set; }

    public decimal Approved { get; set; }

    public decimal Urgent { get; set; }

    public decimal QuestionPoolType { get; set; }

    public decimal EvalRequired { get; set; }

    public decimal AllowSelfWithdrawal { get; set; }

    public virtual ICollection<EvalEvent> EvalEvents { get; set; } = new List<EvalEvent>();

    public virtual ICollection<EvalResponse> EvalResponses { get; set; } = new List<EvalResponse>();

    public virtual LsCatalog FkCatalogNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual Eval FkEvalNavigation { get; set; }

    public virtual Learner FkInstructorNavigation { get; set; }

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemains { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmains { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsExamEvent> LsExamEventFkLearningEventNavigations { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsExamEvent> LsExamEventFkLearningEventOriginalNavigations { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsHoldRelease> LsHoldReleases { get; set; } = new List<LsHoldRelease>();

    public virtual ICollection<LsLearningEventAttempt> LsLearningEventAttempts { get; set; } = new List<LsLearningEventAttempt>();

    public virtual ICollection<LsLearningEventLearner> LsLearningEventLearners { get; set; } = new List<LsLearningEventLearner>();

    public virtual ICollection<LsLearningEventProgram> LsLearningEventPrograms { get; set; } = new List<LsLearningEventProgram>();

    public virtual ICollection<LsLearningEventTrack> LsLearningEventTracks { get; set; } = new List<LsLearningEventTrack>();

    public virtual ICollection<LsObjExternalTrack> LsObjExternalTracks { get; set; } = new List<LsObjExternalTrack>();

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual ICollection<LsPaOjeRequest> LsPaOjeRequests { get; set; } = new List<LsPaOjeRequest>();

    public virtual ICollection<LsProgramCompletion> LsProgramCompletions { get; set; } = new List<LsProgramCompletion>();

    public virtual ICollection<LsSelectedExam> LsSelectedExams { get; set; } = new List<LsSelectedExam>();

    public virtual ICollection<LsTaskQualification> LsTaskQualifications { get; set; } = new List<LsTaskQualification>();

    public virtual LsExamType TestTypeNavigation { get; set; }

    public virtual ICollection<Learner> FkLearners { get; set; } = new List<Learner>();

    public virtual ICollection<LsOrg> FkOrgs { get; set; } = new List<LsOrg>();
}
