using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamLearnerFeedback
{
    public decimal Id { get; set; }

    public decimal? FkExam { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkProctor { get; set; }

    public string Feedback { get; set; }

    public decimal? Viewed { get; set; }

    public string Resolution { get; set; }

    public decimal? Sequence { get; set; }

    public decimal? Archive { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? FkResolvedByD { get; set; }

    public decimal? FkResolvedByL { get; set; }

    public DateTime? DateResolved { get; set; }

    public decimal? FkAssignedTo { get; set; }

    public decimal FkQuestionImpl { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Learner FkProctorNavigation { get; set; }

    public virtual QuestionImpl FkQuestionImplNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }

    public virtual Developer FkResolvedByDNavigation { get; set; }

    public virtual Learner FkResolvedByLNavigation { get; set; }
}
