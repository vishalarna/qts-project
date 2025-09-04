using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormPackagePropertiesPreset
    {
        public int PresetId { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string PropertyXml { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
    }
}
