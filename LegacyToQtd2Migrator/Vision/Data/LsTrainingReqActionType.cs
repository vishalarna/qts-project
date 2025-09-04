using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqActionType
{
    public decimal IdValue { get; set; }

    public string Type { get; set; }

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItems { get; set; } = new List<LsTrainingReqItem>();
}
