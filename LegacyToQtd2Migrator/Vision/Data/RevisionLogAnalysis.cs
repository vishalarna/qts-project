using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogAnalysis
{
    public decimal FkAnalysis { get; set; }

    public decimal FkAnalysisImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual AnalysisImpl FkAnalysisImplNavigation { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }
}
