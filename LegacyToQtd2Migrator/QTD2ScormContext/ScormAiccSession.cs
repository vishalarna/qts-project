using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormAiccSession
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public Guid AiccSessionId { get; set; }
        public string LegacyAiccSessionId { get; set; }
        public int ScormRegistrationId { get; set; }
        public string ExternalRegistrationId { get; set; }
        public string ExternalConfiguration { get; set; }
        public bool IsTracking { get; set; }
        public int? LaunchHistoryId { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
    }
}
