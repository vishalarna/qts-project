using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblWarningSetting
    {
        public int WarningId { get; set; }
        public int Eid { get; set; }
        public int? WarningLevel { get; set; }
        public int? Num { get; set; }
        public string Ntype { get; set; }
        public int? Nfreq { get; set; }
        public int? Nlevel { get; set; }
    }
}
