using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class RevisionLogProgram
{
    public decimal FkProgram { get; set; }

    public decimal FkProgramImpl { get; set; }

    public string AttributesChanged { get; set; }

    public virtual ProgramImpl FkProgramImplNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
