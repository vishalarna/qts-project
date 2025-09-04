using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LkTblCertificationType
    {
        public int CertId { get; set; }
        public string CertDesc { get; set; }
        public string CertAbbrev { get; set; }
        public int? Nercpolicy { get; set; }
        public int? CredReqHours { get; set; }
        public int? TotalReqCehs { get; set; }
        public byte[] Ts { get; set; }
    }
}
