using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveClass
{
    public decimal Id { get; set; }

    public virtual ICollection<ObjectiveClassImpl> ObjectiveClassImpls { get; set; } = new List<ObjectiveClassImpl>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();
}
