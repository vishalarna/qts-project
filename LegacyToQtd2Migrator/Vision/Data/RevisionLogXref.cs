using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogXref
{
    public decimal FkXrefLib { get; set; }

    public decimal FkXrefLibImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual XrefLibImpl FkXrefLibImplNavigation { get; set; }

    public virtual XrefLib FkXrefLibNavigation { get; set; }
}
