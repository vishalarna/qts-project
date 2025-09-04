using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramAlternateInheritance
{
    public decimal Id { get; set; }

    public decimal FkAncestor { get; set; }

    public decimal FkDescendent { get; set; }

    public decimal Distance { get; set; }

    public virtual ProgramAlternate FkAncestorNavigation { get; set; }

    public virtual ProgramAlternate FkDescendentNavigation { get; set; }
}
