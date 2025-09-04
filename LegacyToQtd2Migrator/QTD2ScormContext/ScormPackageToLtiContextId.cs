using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormPackageToLtiContextId
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormPackageId { get; set; }
        public string ContextId { get; set; }
    }
}
