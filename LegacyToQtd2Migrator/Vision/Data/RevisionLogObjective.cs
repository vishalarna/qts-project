using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogObjective
{
    public decimal FkObjective { get; set; }

    public decimal FkObjectiveImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual ObjectiveImpl FkObjectiveImplNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }
}
