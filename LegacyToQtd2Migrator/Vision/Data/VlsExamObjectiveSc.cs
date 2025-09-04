using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsExamObjectiveSc
{
    public decimal FkExam { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal? FkObjective { get; set; }

    public string Objective { get; set; }
}
