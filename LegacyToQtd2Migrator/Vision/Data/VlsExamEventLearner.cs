using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsExamEventLearner
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

    public string Text { get; set; }

    public DateTime ExamImplDateCreated { get; set; }

    public DateTime ExamImplDateExpired { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public decimal FkCompany { get; set; }

    public string Company { get; set; }

    public decimal ExamScore { get; set; }

    public decimal AdjScore { get; set; }

    public decimal? PassingScore { get; set; }
}
