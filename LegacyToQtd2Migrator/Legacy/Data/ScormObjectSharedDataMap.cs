using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSharedDataMap
    {
        public int ScormObjectId { get; set; }
        public int ScormObjSharedDataMapId { get; set; }
        public string TargetSharedDataId { get; set; }
        public bool ReadSharedData { get; set; }
        public bool WriteSharedData { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
