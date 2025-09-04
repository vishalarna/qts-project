using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogQuestion
{
    public decimal FkQuestion { get; set; }

    public decimal FkQuestionImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual QuestionImpl FkQuestionImplNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
