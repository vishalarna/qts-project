using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCardPrerequisite
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkPrerequisite { get; set; }

    public decimal Type { get; set; }

    public decimal? FkJobPosition { get; set; }

    public virtual LsOrg FkJobPositionNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual LsVisionItemType TypeNavigation { get; set; }
}
