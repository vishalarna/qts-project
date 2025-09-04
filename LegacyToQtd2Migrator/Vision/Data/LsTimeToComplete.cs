using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsTimeToComplete
{
    public decimal Id { get; set; }

    public virtual ICollection<LsCatalog> LsCatalogFrequencyNavigations { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsCatalog> LsCatalogRequalPeriodNavigations { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsCertRequirement> LsCertRequirements { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertification> LsCertificationFkTimetocomplGraceperiodNavigations { get; set; } = new List<LsCertification>();

    public virtual ICollection<LsCertification> LsCertificationFkTimetocomplValidityPeriodNavigations { get; set; } = new List<LsCertification>();

    public virtual ICollection<LsQualCard> LsQualCardFkLsTimeToCompleteNavigations { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsQualCard> LsQualCardFkRequalNavigations { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsTimeToCompleteImpl> LsTimeToCompleteImpls { get; set; } = new List<LsTimeToCompleteImpl>();

    public virtual ICollection<QualCardImpl> QualCardImpls { get; set; } = new List<QualCardImpl>();
}
