using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTrainingReqLink
{
    public decimal FkLsTrainingReqItem { get; set; }

    public decimal FkNode { get; set; }

    public decimal Type { get; set; }

    public decimal FkImpl { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual LsTrainingReqItem FkLsTrainingReqItemNavigation { get; set; }
}
