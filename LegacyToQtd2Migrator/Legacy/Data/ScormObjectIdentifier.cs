using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectIdentifier
    {
        public int ScormObjectId { get; set; }
        public string ItemIdentifier { get; set; }
        public string ResourceIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
