using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogExam
{
    public decimal FkExam { get; set; }

    public decimal FkExamImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual ExamImpl FkExamImplNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }
}
