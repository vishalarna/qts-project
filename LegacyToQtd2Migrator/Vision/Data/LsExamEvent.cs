using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExamEvent
{
    public decimal FkExamType { get; set; }

    public decimal FkExam { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkProctor { get; set; }

    public DateTime? DateStarted { get; set; }

    public DateTime? DateCompleted { get; set; }

    public decimal FkExamOnlineProfile { get; set; }

    public decimal? FkExamResults { get; set; }

    public decimal FkLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal? ExamSequence { get; set; }

    public decimal? FkLearningEventOriginal { get; set; }

    public decimal ManualReentryEnabled { get; set; }

    public decimal? Reviewed { get; set; }

    public decimal? Acknowledged { get; set; }

    public decimal ExamAutoReentryAttempts { get; set; }

    public DateTime? ReviewedDateCompleted { get; set; }

    public DateTime? AcknowledgedDateCompleted { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual ExamOnlineProfile FkExamOnlineProfileNavigation { get; set; }

    public virtual LsExamResult FkExamResultsNavigation { get; set; }

    public virtual LsExamType FkExamTypeNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventOriginalNavigation { get; set; }

    public virtual Learner FkProctorNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
