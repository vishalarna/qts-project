using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualJobPosition
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkJobPosition { get; set; }

    public decimal Status { get; set; }

    public DateTime DateChanged { get; set; }

    public decimal LastModifiedBy { get; set; }

    public virtual LsOrg FkJobPositionNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual Learner LastModifiedByNavigation { get; set; }
}
