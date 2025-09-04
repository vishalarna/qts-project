using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantProperty
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte PropertyLevel { get; set; }
        public int PropertyScope { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
