using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProgramDirectTask
{
    public decimal Id { get; set; }

    public decimal FkAnalysis { get; set; }

    public string TextAscii { get; set; }

    public DateTime AnalysisImplDateCreated { get; set; }

    public DateTime AnalysisImplDateExpired { get; set; }

    public decimal FkProgram { get; set; }

    public DateTime SequencingDateCreated { get; set; }

    public DateTime SequencingDateExpired { get; set; }

    public DateTime AnalysisLevelImplDateCreated { get; set; }

    public DateTime AnalysisLevelImplDateExpired { get; set; }

    public DateTime DirectObjectiveDateCreated { get; set; }

    public DateTime DirectObjectiveDateExpired { get; set; }

    public int? ConsolidationDateCreated { get; set; }

    public int? ConsolidationDateExpired { get; set; }
}
