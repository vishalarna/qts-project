using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassNotificationSetting
    {
        public int Cnid { get; set; }
        public int? Ninterval { get; set; }
        public int? Ndow { get; set; }
        public DateTime? Nstart { get; set; }
        public DateTime? Nend { get; set; }
        public bool Nenable { get; set; }
        public byte[] Ts { get; set; }
        public int? Nrange { get; set; }
        public string MsgText { get; set; }
    }
}
