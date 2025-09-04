using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class Cmi5AuLaunchDatum
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public string LaunchMethod { get; set; }
        public string LaunchParameters { get; set; }
        public string MoveOn { get; set; }
        public decimal? MasteryScore { get; set; }
        public string EntitlementKey { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
