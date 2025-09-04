using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwQtsMainDaSubDaTaskGap
    {
        public int MainDaid { get; set; }
        public int SubDaid { get; set; }
        public string MainDuty { get; set; }
        public string SubDuty { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public int Tid { get; set; }
        public int? Tnum { get; set; }
        public string Tdesc { get; set; }
        public string TaskNumber { get; set; }
        public string TaskDesc { get; set; }
    }
}
