using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Sequencing
{
    public decimal FkProgram { get; set; }

    public decimal FkObjective { get; set; }

    public decimal Sequence { get; set; }

    public decimal FkProject { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal Questions { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
