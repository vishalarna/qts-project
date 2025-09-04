using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanActivityProviderMap
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string ProviderId { get; set; }
        public byte[] ActivityIdSha1 { get; set; }
        public string ActivityId { get; set; }

        public virtual TinCanActivityProvider TinCanActivityProvider { get; set; }
    }
}
