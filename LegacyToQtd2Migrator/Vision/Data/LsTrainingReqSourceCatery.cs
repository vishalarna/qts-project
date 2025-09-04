using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqSourceCatery
{
    public decimal IdValue { get; set; }

    public string Category { get; set; }

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItems { get; set; } = new List<LsTrainingReqItem>();
}
