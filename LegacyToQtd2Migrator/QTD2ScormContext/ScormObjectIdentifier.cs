using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectIdentifier
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public string ItemIdentifier { get; set; }
        public string ResourceIdentifier { get; set; }
        public string ExternalIdentifier { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
