using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Exam
{
    public decimal Id { get; set; }

    public virtual ICollection<ExamChoiceOrder> ExamChoiceOrders { get; set; } = new List<ExamChoiceOrder>();

    public virtual ICollection<ExamComment> ExamComments { get; set; } = new List<ExamComment>();

    public virtual ICollection<ExamEvent> ExamEvents { get; set; } = new List<ExamEvent>();

    public virtual ICollection<ExamFilter> ExamFilters { get; set; } = new List<ExamFilter>();

    public virtual ICollection<ExamImpl> ExamImpls { get; set; } = new List<ExamImpl>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbacks { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamOnlineTesting> ExamOnlineTestings { get; set; } = new List<ExamOnlineTesting>();

    public virtual ICollection<ExamOwner> ExamOwners { get; set; } = new List<ExamOwner>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptions { get; set; } = new List<ExamPrintOption>();

    public virtual ICollection<ExamProfileByObjective> ExamProfileByObjectives { get; set; } = new List<ExamProfileByObjective>();

    public virtual ICollection<ExamQuestionEventFi> ExamQuestionEventFis { get; set; } = new List<ExamQuestionEventFi>();

    public virtual ICollection<ExamQuestionEventMa> ExamQuestionEventMas { get; set; } = new List<ExamQuestionEventMa>();

    public virtual ICollection<ExamQuestionEventMc> ExamQuestionEventMcs { get; set; } = new List<ExamQuestionEventMc>();

    public virtual ICollection<ExamQuestionEventSa> ExamQuestionEventSas { get; set; } = new List<ExamQuestionEventSa>();

    public virtual ICollection<ExamQuestionEventTf> ExamQuestionEventTfs { get; set; } = new List<ExamQuestionEventTf>();

    public virtual ICollection<ExamQuestionEvent> ExamQuestionEvents { get; set; } = new List<ExamQuestionEvent>();

    public virtual ICollection<ExamUnitOb> ExamUnitObs { get; set; } = new List<ExamUnitOb>();

    public virtual ICollection<ExamUnitPg> ExamUnitPgs { get; set; } = new List<ExamUnitPg>();

    public virtual ICollection<ExamUnitQq> ExamUnitQqs { get; set; } = new List<ExamUnitQq>();

    public virtual ICollection<LsExamEvent> LsExamEvents { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsOnlineExamQuestionMa> LsOnlineExamQuestionMas { get; set; } = new List<LsOnlineExamQuestionMa>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestions { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsPreviewExam> LsPreviewExams { get; set; } = new List<LsPreviewExam>();

    public virtual ICollection<LsSelectedExam> LsSelectedExams { get; set; } = new List<LsSelectedExam>();

    public virtual ICollection<ProgramStaticExam> ProgramStaticExams { get; set; } = new List<ProgramStaticExam>();

    public virtual ICollection<RevisionLogExam> RevisionLogExams { get; set; } = new List<RevisionLogExam>();
}
