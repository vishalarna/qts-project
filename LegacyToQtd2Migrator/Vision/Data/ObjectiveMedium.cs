using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveMedium
{
    public decimal Id { get; set; }

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveMediaImpl> ObjectiveMediaImpls { get; set; } = new List<ObjectiveMediaImpl>();
}
