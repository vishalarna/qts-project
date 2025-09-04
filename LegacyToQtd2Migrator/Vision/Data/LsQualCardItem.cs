using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsQualCardItem
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkAnalysis { get; set; }

    public decimal Sequence { get; set; }

    public virtual Analysis FkAnalysisNavigation { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }
}
