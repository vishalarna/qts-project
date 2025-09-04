using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QuestionImpl
{
    public decimal Id { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal FkProject { get; set; }

    public string Topic { get; set; }

    public decimal Type { get; set; }

    public decimal FkScenarioParent { get; set; }

    public decimal? FkQuestionStatus { get; set; }

    public decimal? IsPractice { get; set; }

    public decimal? FkLockedBy { get; set; }

    public decimal? MustAppearOnTest { get; set; }

    public decimal? TimeToComplete { get; set; }

    public decimal? Difficulty { get; set; }

    public string UserDefinedId { get; set; }

    public string CrossReference { get; set; }

    public string UserField1 { get; set; }

    public string UserField2 { get; set; }

    public string UserField3 { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public string VersionComments { get; set; }

    public decimal? Disqualified { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbacks { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamQuestionEventFi> ExamQuestionEventFis { get; set; } = new List<ExamQuestionEventFi>();

    public virtual ICollection<ExamQuestionEventMa> ExamQuestionEventMas { get; set; } = new List<ExamQuestionEventMa>();

    public virtual ICollection<ExamQuestionEventMc> ExamQuestionEventMcs { get; set; } = new List<ExamQuestionEventMc>();

    public virtual ICollection<ExamQuestionEventSa> ExamQuestionEventSas { get; set; } = new List<ExamQuestionEventSa>();

    public virtual ICollection<ExamQuestionEventTf> ExamQuestionEventTfs { get; set; } = new List<ExamQuestionEventTf>();

    public virtual ICollection<ExamQuestionEvent> ExamQuestionEvents { get; set; } = new List<ExamQuestionEvent>();

    public virtual ICollection<ExamUnitQq> ExamUnitQqs { get; set; } = new List<ExamUnitQq>();

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Developer FkLockedByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }

    public virtual QuestionStatus FkQuestionStatusNavigation { get; set; }

    public virtual Question FkScenarioParentNavigation { get; set; }

    public virtual ICollection<LsExamGenerator> LsExamGenerators { get; set; } = new List<LsExamGenerator>();

    public virtual ICollection<RevisionLogQuestion> RevisionLogQuestions { get; set; } = new List<RevisionLogQuestion>();
}
