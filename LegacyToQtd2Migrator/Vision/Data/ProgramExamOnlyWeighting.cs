using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramExamOnlyWeighting
{
    public decimal FkProgramSource { get; set; }

    public decimal FkProgramWeighted { get; set; }

    public decimal Sequence { get; set; }

    public decimal Weighting { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Program FkProgramSourceNavigation { get; set; }

    public virtual Program FkProgramWeightedNavigation { get; set; }
}
