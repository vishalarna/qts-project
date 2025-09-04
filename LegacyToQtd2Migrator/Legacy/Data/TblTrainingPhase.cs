using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingPhase
    {
        public int Tpid { get; set; }
        public int? Pid { get; set; }
        public int? Tpnum { get; set; }
        public string Tpdesc { get; set; }
        public string Tpnote { get; set; }
        public string Tpletter { get; set; }
        public byte[] UpsizeTs { get; set; }

        public virtual TblPosition PidNavigation { get; set; }
    }
}
