using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantPluginConfiguration
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] PluginId { get; set; }
        public string PluginClass { get; set; }
        public bool? PluginEnabled { get; set; }
        public string PluginInternalConfig { get; set; }
        public string PluginUserConfig { get; set; }
    }
}
