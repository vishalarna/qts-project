using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QualCard
{
    public decimal Id { get; set; }

    public virtual ICollection<QualCardImpl> QualCardImpls { get; set; } = new List<QualCardImpl>();

    public virtual ICollection<QualCardItem> QualCardItems { get; set; } = new List<QualCardItem>();

    public virtual ICollection<QualCardPrerequisite> QualCardPrerequisites { get; set; } = new List<QualCardPrerequisite>();

    public virtual ICollection<RevisionLogQualCard> RevisionLogQualCards { get; set; } = new List<RevisionLogQualCard>();
}
