using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class CoverSheet
{
    public decimal Id { get; set; }

    public virtual ICollection<CoverSheetHtml> CoverSheetHtmls { get; set; } = new List<CoverSheetHtml>();

    public virtual ICollection<CoverSheetImpl> CoverSheetImpls { get; set; } = new List<CoverSheetImpl>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptionFkAnskeyCoversheetNavigations { get; set; } = new List<ExamPrintOption>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptionFkAnssheetCoversheetNavigations { get; set; } = new List<ExamPrintOption>();

    public virtual ICollection<ExamPrintOption> ExamPrintOptionFkTestCoversheetNavigations { get; set; } = new List<ExamPrintOption>();
}
