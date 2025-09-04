using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormRegistrationStatementMap
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int MapId { get; set; }
        public int ScormRegistrationId { get; set; }
        public byte[] StatementId { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
    }
}
