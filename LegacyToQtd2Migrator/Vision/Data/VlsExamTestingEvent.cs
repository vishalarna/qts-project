using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsExamTestingEvent
{
    public decimal FkExam { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal? SelectionOrder { get; set; }

    public decimal? Type { get; set; }

    public decimal? IsSubq { get; set; }

    public decimal ResponseType { get; set; }

    public decimal? Score { get; set; }

    public decimal? Points { get; set; }

    public string Topic { get; set; }

    public string Num1 { get; set; }

    public string Num2 { get; set; }

    public string Num3 { get; set; }

    public decimal? FkObjective { get; set; }

    public string Objective { get; set; }
}
