using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TenantCredential
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string CredentialId { get; set; }
        public string CredentialName { get; set; }
        public string CredentialSecret { get; set; }
        public string CredentialPermissions { get; set; }
    }
}
