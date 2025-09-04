using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamQuestionEvent
{
    public decimal FkExam { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal FkLearner { get; set; }

    public decimal Sequence { get; set; }

    public decimal ResponseType { get; set; }

    public decimal? Score { get; set; }

    public decimal? Points { get; set; }

    public decimal? QuestionType { get; set; }

    public decimal? IsSubquestion { get; set; }

    public decimal ReviewedQuestion { get; set; }

    public DateTime DateAdministered { get; set; }

    public decimal FkQuestionImpl { get; set; }

    public string InstructorComment { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual QuestionImpl FkQuestionImplNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
