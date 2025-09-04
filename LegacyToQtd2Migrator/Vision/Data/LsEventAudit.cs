using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsEventAudit
{
    public decimal Id { get; set; }

    public DateTime EventDate { get; set; }

    public decimal FkEventsToAudit { get; set; }

    public decimal FkCreatedBy { get; set; }

    public decimal? EntityIdModified { get; set; }

    public decimal Status { get; set; }

    public string Ip { get; set; }

    public decimal? Processed { get; set; }

    public string Comments { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual LsEventsToAudit FkEventsToAuditNavigation { get; set; }
}
