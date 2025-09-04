using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTaskSubStep
    {
        public int Tssid { get; set; }
        public int Tid { get; set; }
        public int? Tssnum { get; set; }
        public string Tssdesc { get; set; }

        public virtual TblTask TidNavigation { get; set; }
    }
}
