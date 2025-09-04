using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramTuType
{
    public decimal Id { get; set; }

    public virtual ICollection<ProgramImpl> ProgramImpls { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramTuTypeImpl> ProgramTuTypeImpls { get; set; } = new List<ProgramTuTypeImpl>();
}
