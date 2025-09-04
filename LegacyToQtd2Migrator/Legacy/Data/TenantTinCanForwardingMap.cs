using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantTinCanForwardingMap
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string PairId { get; set; }
        public string SourceUrl { get; set; }
        public string SourceUsername { get; set; }
        public string SourcePassword { get; set; }
        public string TargetUrl { get; set; }
        public string TargetUsername { get; set; }
        public string TargetPassword { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public DateTime? LastForwardedStatementDate { get; set; }
        public string MoreUrl { get; set; }
        public int Attempts { get; set; }
        public int NoStatementsAttempts { get; set; }
        public long VisibleAfter { get; set; }
    }
}
