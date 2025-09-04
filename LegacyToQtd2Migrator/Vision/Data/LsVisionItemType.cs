using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsVisionItemType
{
    public decimal IdValue { get; set; }

    public string Text { get; set; }

    public virtual ICollection<LsQualCardPrerequisite> LsQualCardPrerequisites { get; set; } = new List<LsQualCardPrerequisite>();
}
