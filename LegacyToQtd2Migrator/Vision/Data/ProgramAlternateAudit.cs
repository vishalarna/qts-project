using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramAlternateAudit
{
    public decimal Id { get; set; }

    public decimal FkPrimary { get; set; }

    public decimal? FkProgram { get; set; }

    public string UserDefinedId { get; set; }

    public decimal Action { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkDeveloper { get; set; }

    public virtual Developer FkDeveloperNavigation { get; set; }

    public virtual Program FkPrimaryNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
