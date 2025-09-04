using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanContentToken
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] TokenId { get; set; }
        public string ExternalId { get; set; }
        public string ExternalConfiguration { get; set; }
        public DateTime Expiry { get; set; }
        public bool PreviewToken { get; set; }
    }
}
