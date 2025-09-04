using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblWarningMsgHistory
    {
        public int Wmhid { get; set; }
        public int Eid { get; set; }
        public int WarningLevel { get; set; }
        public DateTime Wmdate { get; set; }
        public string Email { get; set; }
        public int? CertType { get; set; }
    }
}
