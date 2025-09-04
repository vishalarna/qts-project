using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanPermission
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string OwnerId { get; set; }
        public string StatementRead { get; set; }
        public string StatementWrite { get; set; }
        public string ActivityWrite { get; set; }
        public string ActorWrite { get; set; }
        public string DocumentRead { get; set; }
        public string DocumentWrite { get; set; }
    }
}
