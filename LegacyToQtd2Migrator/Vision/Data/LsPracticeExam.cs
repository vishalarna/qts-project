using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPracticeExam
{
    public string Vlskey { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal SelectionOrder { get; set; }

    public decimal Sequence { get; set; }

    public decimal ResponseType { get; set; }

    public decimal? QuestionType { get; set; }

    public decimal? QuestionPoints { get; set; }

    public decimal? IsSubq { get; set; }

    public decimal? QuestionScore { get; set; }

    public DateTime DateCached { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
