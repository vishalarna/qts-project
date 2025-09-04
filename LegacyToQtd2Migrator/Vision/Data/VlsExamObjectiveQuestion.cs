using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsExamObjectiveQuestion
{
    public decimal FkExam { get; set; }

    public decimal? FkObjective { get; set; }

    public decimal FkQuestion { get; set; }

    public string Objective { get; set; }
}
