using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class TimeSpan
{
    public decimal Id { get; set; }

    public virtual ICollection<AnalysisImpl> AnalysisImplFkAnalysisRequalNavigations { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<AnalysisImpl> AnalysisImplFkTaskTrainingTimeNavigations { get; set; } = new List<AnalysisImpl>();

    public virtual ICollection<TimeSpanImpl> TimeSpanImpls { get; set; } = new List<TimeSpanImpl>();
}
