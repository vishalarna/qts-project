using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class Cmi5ObjectiveMap
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectObjectiveId { get; set; }
        public int ScormObjectId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual ScormObject ScormObjectNavigation { get; set; }
    }
}
