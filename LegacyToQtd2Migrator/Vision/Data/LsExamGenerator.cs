using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExamGenerator
{
    public string Vlskey { get; set; }

    public decimal Question { get; set; }

    public decimal ParentUnit { get; set; }

    public decimal Type { get; set; }

    public decimal? Subq { get; set; }

    public decimal? Points { get; set; }

    public decimal? VisionSel { get; set; }

    public decimal? OltSel { get; set; }

    public decimal FkQuestionImplId { get; set; }

    public virtual QuestionImpl FkQuestionImpl { get; set; }

    public virtual Question QuestionNavigation { get; set; }
}
