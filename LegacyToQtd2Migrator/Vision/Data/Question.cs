using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Question
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisQuestion> AnalysisQuestions { get; set; } = new List<AnalysisQuestion>();

    public virtual ICollection<ContentImpl> ContentImpls { get; set; } = new List<ContentImpl>();

    public virtual ICollection<ExamChoiceOrder> ExamChoiceOrders { get; set; } = new List<ExamChoiceOrder>();

    public virtual ICollection<ExamLearnerFeedback> ExamLearnerFeedbacks { get; set; } = new List<ExamLearnerFeedback>();

    public virtual ICollection<ExamOnlineTesting> ExamOnlineTestings { get; set; } = new List<ExamOnlineTesting>();

    public virtual ICollection<ExamQuestionEventFi> ExamQuestionEventFis { get; set; } = new List<ExamQuestionEventFi>();

    public virtual ICollection<ExamQuestionEventMa> ExamQuestionEventMas { get; set; } = new List<ExamQuestionEventMa>();

    public virtual ICollection<ExamQuestionEventMc> ExamQuestionEventMcs { get; set; } = new List<ExamQuestionEventMc>();

    public virtual ICollection<ExamQuestionEventSa> ExamQuestionEventSas { get; set; } = new List<ExamQuestionEventSa>();

    public virtual ICollection<ExamQuestionEventTf> ExamQuestionEventTfs { get; set; } = new List<ExamQuestionEventTf>();

    public virtual ICollection<ExamQuestionEvent> ExamQuestionEvents { get; set; } = new List<ExamQuestionEvent>();

    public virtual ICollection<ExamUnitQq> ExamUnitQqs { get; set; } = new List<ExamUnitQq>();

    public virtual ICollection<LsExamGenerator> LsExamGenerators { get; set; } = new List<LsExamGenerator>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestionFkQuestionNavigations { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestionFkQuestionParentNavigations { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsOnlineExamQuestionMa> LsOnlineExamQuestionMas { get; set; } = new List<LsOnlineExamQuestionMa>();

    public virtual ICollection<LsOnlineExamQuestionMc> LsOnlineExamQuestionMcs { get; set; } = new List<LsOnlineExamQuestionMc>();

    public virtual ICollection<LsPracticeExamCo> LsPracticeExamCos { get; set; } = new List<LsPracticeExamCo>();

    public virtual ICollection<LsPracticeExamMa> LsPracticeExamMas { get; set; } = new List<LsPracticeExamMa>();

    public virtual ICollection<LsPracticeExamTf> LsPracticeExamTfs { get; set; } = new List<LsPracticeExamTf>();

    public virtual ICollection<LsPracticeExam> LsPracticeExams { get; set; } = new List<LsPracticeExam>();

    public virtual ICollection<ObjectiveQuestion> ObjectiveQuestions { get; set; } = new List<ObjectiveQuestion>();

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual ICollection<QuestionE> QuestionEs { get; set; } = new List<QuestionE>();

    public virtual ICollection<QuestionEsHtml> QuestionEsHtmls { get; set; } = new List<QuestionEsHtml>();

    public virtual ICollection<QuestionExplanation> QuestionExplanations { get; set; } = new List<QuestionExplanation>();

    public virtual ICollection<QuestionFiHtml> QuestionFiHtmls { get; set; } = new List<QuestionFiHtml>();

    public virtual ICollection<QuestionFi> QuestionFis { get; set; } = new List<QuestionFi>();

    public virtual ICollection<QuestionHtml> QuestionHtmls { get; set; } = new List<QuestionHtml>();

    public virtual ICollection<QuestionImpl> QuestionImplFkQuestionNavigations { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionImpl> QuestionImplFkScenarioParentNavigations { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionMaChoiceHtml> QuestionMaChoiceHtmls { get; set; } = new List<QuestionMaChoiceHtml>();

    public virtual ICollection<QuestionMaChoice> QuestionMaChoices { get; set; } = new List<QuestionMaChoice>();

    public virtual ICollection<QuestionMaItemHtml> QuestionMaItemHtmls { get; set; } = new List<QuestionMaItemHtml>();

    public virtual ICollection<QuestionMaItem> QuestionMaItems { get; set; } = new List<QuestionMaItem>();

    public virtual ICollection<QuestionMcChoiceHtml> QuestionMcChoiceHtmls { get; set; } = new List<QuestionMcChoiceHtml>();

    public virtual ICollection<QuestionMcChoice> QuestionMcChoices { get; set; } = new List<QuestionMcChoice>();

    public virtual ICollection<QuestionMc> QuestionMcs { get; set; } = new List<QuestionMc>();

    public virtual ICollection<QuestionSaHtml> QuestionSaHtmls { get; set; } = new List<QuestionSaHtml>();

    public virtual ICollection<QuestionSa> QuestionSas { get; set; } = new List<QuestionSa>();

    public virtual ICollection<QuestionSc> QuestionScFkQuestionNavigations { get; set; } = new List<QuestionSc>();

    public virtual ICollection<QuestionSc> QuestionScFkSubQuestionNavigations { get; set; } = new List<QuestionSc>();

    public virtual ICollection<QuestionStem> QuestionStems { get; set; } = new List<QuestionStem>();

    public virtual ICollection<QuestionTf> QuestionTfs { get; set; } = new List<QuestionTf>();

    public virtual ICollection<RevisionLogQuestion> RevisionLogQuestions { get; set; } = new List<RevisionLogQuestion>();
}
