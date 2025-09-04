using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramStatus
{
    public decimal Id { get; set; }

    public virtual ICollection<ProgramImpl> ProgramImpls { get; set; } = new List<ProgramImpl>();

    public virtual ICollection<ProgramStatusImpl> ProgramStatusImpls { get; set; } = new List<ProgramStatusImpl>();
}
