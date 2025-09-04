using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamUnitOb
{
    public decimal FkExam { get; set; }

    public decimal? FkExamUnit { get; set; }

    public decimal? FkParentUnit { get; set; }

    public decimal FkUnit { get; set; }

    public decimal Sequence { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal? FkUnitImpl { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Program FkParentUnitNavigation { get; set; }

    public virtual ObjectiveImpl FkUnitImplNavigation { get; set; }

    public virtual Objective FkUnitNavigation { get; set; }
}
