using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QualCardPrerequisite
{
    public decimal FkQualCard { get; set; }

    public decimal FkPrerequisite { get; set; }

    public decimal PrerequisiteType { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual QualCard FkQualCardNavigation { get; set; }
}
