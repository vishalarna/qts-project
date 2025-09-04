using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QuestionStatus
{
    public decimal Id { get; set; }

    public virtual ICollection<QuestionImpl> QuestionImpls { get; set; } = new List<QuestionImpl>();

    public virtual ICollection<QuestionStatusImpl> QuestionStatusImpls { get; set; } = new List<QuestionStatusImpl>();
}
