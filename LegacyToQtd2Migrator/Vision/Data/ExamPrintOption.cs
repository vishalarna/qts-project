using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamPrintOption
{
    public decimal FkExam { get; set; }

    public decimal? ObjsOnTest { get; set; }

    public decimal? ObjsOnAnskey { get; set; }

    public decimal? ObjsOnAnssheet { get; set; }

    public decimal? FirstQuesNum { get; set; }

    public decimal? OneQuesPerPage { get; set; }

    public decimal? IgnoreWhiteSpace { get; set; }

    public decimal? FkTestCoversheet { get; set; }

    public decimal? FkAnskeyCoversheet { get; set; }

    public decimal? FkAnssheetCoversheet { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual CoverSheet FkAnskeyCoversheetNavigation { get; set; }

    public virtual CoverSheet FkAnssheetCoversheetNavigation { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual CoverSheet FkTestCoversheetNavigation { get; set; }
}
