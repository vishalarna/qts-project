using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class TimeType
{
    public decimal Id { get; set; }

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRuleFkTimetypeCarryoverBasisNavigations { get; set; } = new List<LsCertRequirementsRule>();

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRuleFkTimetypeCrserepeatBasisNavigations { get; set; } = new List<LsCertRequirementsRule>();

    public virtual ICollection<LsDateModifier> LsDateModifiers { get; set; } = new List<LsDateModifier>();

    public virtual ICollection<LsTimeToCompleteImpl> LsTimeToCompleteImpls { get; set; } = new List<LsTimeToCompleteImpl>();

    public virtual ICollection<TimeSpanImpl> TimeSpanImpls { get; set; } = new List<TimeSpanImpl>();

    public virtual ICollection<TimeTypeImpl> TimeTypeImpls { get; set; } = new List<TimeTypeImpl>();
}
