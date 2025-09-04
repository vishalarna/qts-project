using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramAlternate
{
    public decimal Id { get; set; }

    public decimal? FkProgram { get; set; }

    public string UserDefinedId { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual ICollection<ProgramAlternateInheritance> ProgramAlternateInheritanceFkAncestorNavigations { get; set; } = new List<ProgramAlternateInheritance>();

    public virtual ICollection<ProgramAlternateInheritance> ProgramAlternateInheritanceFkDescendentNavigations { get; set; } = new List<ProgramAlternateInheritance>();
}
