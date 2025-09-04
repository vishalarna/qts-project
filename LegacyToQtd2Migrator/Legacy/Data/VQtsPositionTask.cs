using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsPositionTask
    {
        public int Tid { get; set; }
        public string Task { get; set; }
        public string Task1 { get; set; }
        public string Tdesc { get; set; }
        public int Pid { get; set; }
        public int? Daid { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string Da { get; set; }
        public string MainDuty { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Daletter { get; set; }
        public string Flag { get; set; }
        public int? Tpid { get; set; }
        public int? Tpnum { get; set; }
        public string Tpdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public bool Critical { get; set; }
        public bool? ImpactR6 { get; set; }
    }
}
