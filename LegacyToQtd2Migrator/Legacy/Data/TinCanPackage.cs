using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanPackage
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormPackageId { get; set; }
        public string TincanActivityId { get; set; }

        public virtual ScormPackage ScormPackage { get; set; }
    }
}
