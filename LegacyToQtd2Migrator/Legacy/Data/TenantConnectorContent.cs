using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantConnectorContent
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string ContentId { get; set; }
        public long ContentUpdated { get; set; }
        public byte[] ConnectorId { get; set; }
        public string SearchText { get; set; }
        public string ContentTitle { get; set; }
        public string ContentInformation { get; set; }
    }
}
