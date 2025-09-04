using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPracticeExamMa
{
    public string Vlskey { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal ItemSequence { get; set; }

    public decimal Response1 { get; set; }

    public decimal? Response2 { get; set; }

    public decimal? Response3 { get; set; }

    public decimal Points { get; set; }

    public DateTime DateCached { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
