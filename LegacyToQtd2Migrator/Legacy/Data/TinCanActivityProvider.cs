using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanActivityProvider
    {
        public TinCanActivityProvider()
        {
            TinCanActivityProviderMaps = new HashSet<TinCanActivityProviderMap>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string Info { get; set; }
        public string Name { get; set; }
        public string ProviderId { get; set; }
        public string PublicKey { get; set; }
        public string Secret { get; set; }
        public bool? IsEnabled { get; set; }
        public int AuthType { get; set; }

        public virtual ICollection<TinCanActivityProviderMap> TinCanActivityProviderMaps { get; set; }
    }
}
