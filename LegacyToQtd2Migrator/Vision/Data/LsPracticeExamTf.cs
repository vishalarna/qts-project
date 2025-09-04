using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPracticeExamTf
{
    public string Vlskey { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal? Response { get; set; }

    public DateTime DateCached { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
