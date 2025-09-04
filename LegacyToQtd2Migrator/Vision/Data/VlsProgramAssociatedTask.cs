using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProgramAssociatedTask
{
    public decimal Id { get; set; }

    public decimal FkAnalysis { get; set; }

    public string TextAscii { get; set; }

    public decimal FkProgram { get; set; }
}
