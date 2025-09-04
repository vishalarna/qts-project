using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqStatus
{
    public decimal IdValue { get; set; }

    public string Status { get; set; }

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItems { get; set; } = new List<LsTrainingReqItem>();
}
