using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanAgent
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] StatementId { get; set; }
        public byte[] AgentId { get; set; }

        public virtual TinCanStatementIndex TinCanStatementIndex { get; set; }
    }
}
