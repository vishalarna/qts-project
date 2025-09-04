using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantPluginObjStore
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] PluginId { get; set; }
        public string ObjectKey { get; set; }
        public byte[] ObjectKeySha1 { get; set; }
        public byte[] ObjectValue { get; set; }
        public string ObjectType { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
