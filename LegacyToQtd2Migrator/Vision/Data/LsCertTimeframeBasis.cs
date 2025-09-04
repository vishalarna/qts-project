using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertTimeframeBasis
{
    public decimal IdValue { get; set; }

    public string Title { get; set; }

    public virtual ICollection<LsCertRequirement> LsCertRequirements { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertification> LsCertifications { get; set; } = new List<LsCertification>();
}
