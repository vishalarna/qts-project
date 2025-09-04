using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class AnalysisLevel
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisImpl> AnalysisImpls { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisLevelImpl> AnalysisLevelImpls { get; set; } = new List<AnalysisLevelImpl>();
}
