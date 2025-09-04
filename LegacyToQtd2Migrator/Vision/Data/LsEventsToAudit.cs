using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsEventsToAudit
{
    public decimal AuditEventId { get; set; }

    public decimal? AuditEventParentId { get; set; }

    public string EntityType { get; set; }

    public string EventText { get; set; }

    public decimal? EventActive { get; set; }

    public virtual LsEventsToAudit AuditEventParent { get; set; }

    public virtual ICollection<LsEventsToAudit> InverseAuditEventParent { get; set; } = new List<LsEventsToAudit>();

    public virtual ICollection<LsEventAudit> LsEventAudits { get; set; } = new List<LsEventAudit>();
}
