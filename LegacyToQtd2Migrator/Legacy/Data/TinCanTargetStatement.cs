using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanTargetStatement
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] TargetId { get; set; }
        public byte[] TargetingId { get; set; }
        public bool? IsVoiding { get; set; }
        public bool Rescanned { get; set; }
    }
}
