using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveSetting
{
    public decimal Id { get; set; }

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveSettingImpl> ObjectiveSettingImpls { get; set; } = new List<ObjectiveSettingImpl>();
}
