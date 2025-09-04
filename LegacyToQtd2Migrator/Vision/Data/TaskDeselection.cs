using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class TaskDeselection
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisImpl> AnalysisImpls { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<TaskDeselectionImpl> TaskDeselectionImpls { get; set; } = new List<TaskDeselectionImpl>();
}
