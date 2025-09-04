using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Label
{
    public decimal Id { get; set; }

    public decimal FkProject { get; set; }

    public decimal Type { get; set; }

    public string Text { get; set; }

    public DateTime DateModified { get; set; }

    public decimal FkModifiedBy { get; set; }

    public virtual Developer FkModifiedByNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
