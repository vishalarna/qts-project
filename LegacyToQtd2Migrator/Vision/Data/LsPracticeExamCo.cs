using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPracticeExamCo
{
    public string Vlskey { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal Sequence { get; set; }

    public decimal QuestionPosition { get; set; }

    public DateTime DateCached { get; set; }

    public decimal Selected { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
