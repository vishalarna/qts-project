using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProHierarchy
{
    public decimal FkParent { get; set; }

    public decimal FkChild { get; set; }

    public decimal Sequence { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }
}
