using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamQuestionEventMc
{
    public decimal FkExam { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal FkLearner { get; set; }

    public decimal Sequence { get; set; }

    public decimal QuestionPosition { get; set; }

    public decimal FkQuestionImpl { get; set; }

    public decimal Selected { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual QuestionImpl FkQuestionImplNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
