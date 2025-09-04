using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamOnlineTesting
{
    public decimal FkExam { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal? Sequence { get; set; }

    public decimal? SelectionOrder { get; set; }

    public decimal? Type { get; set; }

    public decimal? Flag { get; set; }

    public decimal? Points { get; set; }

    public decimal? IsSubq { get; set; }

    public DateTime? DateViewed { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
