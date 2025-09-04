using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProjectTemplatePath
{
    public decimal Id { get; set; }

    public decimal FkProject { get; set; }

    public decimal DataField { get; set; }

    public string FilePath { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
