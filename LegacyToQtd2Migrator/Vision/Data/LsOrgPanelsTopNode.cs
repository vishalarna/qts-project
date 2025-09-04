using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOrgPanelsTopNode
{
    public decimal FkStructure { get; set; }

    public decimal FkHierarchy { get; set; }

    public decimal FkProject { get; set; }

    public decimal Type { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual LsOrg FkStructureNavigation { get; set; }
}
