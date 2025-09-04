using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class TaskChangeImpact
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisImpl> AnalysisImpls { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<TaskChangeImpactImpl> TaskChangeImpactImpls { get; set; } = new List<TaskChangeImpactImpl>();
}
