using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class CentralLock
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string CLockId { get; set; }
        public string LockerId { get; set; }
        public DateTime Expiry { get; set; }
    }
}
