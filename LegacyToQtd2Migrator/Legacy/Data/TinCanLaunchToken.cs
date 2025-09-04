using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanLaunchToken
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] TokenId { get; set; }
        public string ExternalRegistrationId { get; set; }
        public string ExternalConfiguration { get; set; }
        public DateTime Expiry { get; set; }
    }
}
