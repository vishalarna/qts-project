using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamQuestionEventMa
{
    public decimal FkExam { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal ItemSequence { get; set; }

    public decimal Response1 { get; set; }

    public decimal? Response2 { get; set; }

    public decimal? Response3 { get; set; }

    public decimal? Points { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkQuestionImpl { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual QuestionImpl FkQuestionImplNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
