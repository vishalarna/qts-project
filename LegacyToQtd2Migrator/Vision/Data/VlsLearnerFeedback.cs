using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsLearnerFeedback
{
    public DateTime Examdate { get; set; }

    public string Exam { get; set; }

    public decimal? FkExamStatus { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public decimal FkCompany { get; set; }

    public string Pfirst { get; set; }

    public string Plast { get; set; }

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
}
