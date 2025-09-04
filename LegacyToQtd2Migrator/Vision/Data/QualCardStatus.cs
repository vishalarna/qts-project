using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QualCardStatus
{
    public decimal Id { get; set; }

    public virtual ICollection<QualCardImpl> QualCardImpls { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<QualCardStatusImpl> QualCardStatusImpls { get; set; } = new List<QualCardStatusImpl>();
}
