using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProgramConsolidTask
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

    public int? DirectObjectiveDateCreated { get; set; }

    public int? DirectObjectiveDateExpired { get; set; }

    public DateTime ConsolidationDateCreated { get; set; }

    public DateTime ConsolidationDateExpired { get; set; }
}
