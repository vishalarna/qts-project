using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamStatus
{
    public decimal Id { get; set; }

    public virtual ICollection<ExamImpl> ExamImpls { get; set; } = new List<ExamImpl>();

    public virtual ICollection<ExamStatusImpl> ExamStatusImpls { get; set; } = new List<ExamStatusImpl>();
}
