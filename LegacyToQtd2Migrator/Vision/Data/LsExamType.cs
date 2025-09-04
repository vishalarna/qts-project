using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsExamType
{
    public decimal IdValue { get; set; }

    public string Type { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateClosed { get; set; }

    public virtual ICollection<LsExamEvent> LsExamEvents { get; set; } = new List<LsExamEvent>();

    public virtual ICollection<LsLearningEvent> LsLearningEvents { get; set; } = new List<LsLearningEvent>();
}
