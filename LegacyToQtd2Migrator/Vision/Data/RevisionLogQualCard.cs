using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogQualCard
{
    public decimal FkQualCard { get; set; }

    public decimal FkQualCardImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual QualCardImpl FkQualCardImplNavigation { get; set; }

    public virtual QualCard FkQualCardNavigation { get; set; }
}
