using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyItemResultsArch
{
    public decimal FkSurveyitemid { get; set; }

    public decimal FkEvaluator { get; set; }

    public decimal Diff { get; set; }

    public decimal Imp { get; set; }

    public decimal Freq { get; set; }

    public string Comments { get; set; }
}
