using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveHtml
{
    public decimal FkObjective { get; set; }

    public byte[] Text { get; set; }

    public byte[] Comments { get; set; }

    public byte[] Condition { get; set; }

    public byte[] Standard { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }
}
