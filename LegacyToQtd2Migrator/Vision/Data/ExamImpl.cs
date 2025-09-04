using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamImpl
{
    public decimal Id { get; set; }

    public decimal FkExam { get; set; }

    public decimal FkProject { get; set; }

    public string Text { get; set; }

    public decimal? ExamUnitType { get; set; }

    public decimal IsBuilt { get; set; }

    public string Password { get; set; }

    public decimal? CanEditWithoutPword { get; set; }

    public decimal? CanViewWithoutPword { get; set; }

    public decimal? CanEditWithPword { get; set; }

    public decimal? CanViewWithPword { get; set; }

    public decimal ProfileApplied { get; set; }

    public decimal ProfileType { get; set; }

    public decimal? ProfileValue { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public decimal Archived { get; set; }

    public decimal IsVdmExam { get; set; }

    public decimal? FkExamStatus { get; set; }

    public string VersionComments { get; set; }

    public decimal? Disqualified { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public string UserDefinedId { get; set; }

    public string Salt { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual ExamStatus FkExamStatusNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual ICollection<RevisionLogExam> RevisionLogExams { get; set; } = new List<RevisionLogExam>();
}
