using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamComment
{
    public decimal FkExam { get; set; }

    public string Comments { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }
}
