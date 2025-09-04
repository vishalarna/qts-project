using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveStatus
{
    public decimal Id { get; set; }

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveStatusImpl> ObjectiveStatusImpls { get; set; } = new List<ObjectiveStatusImpl>();
}
