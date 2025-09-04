using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwDutyAreaTask
    {
        public int Daid { get; set; }
        public string DutyArea { get; set; }
        public string SubDutyArea { get; set; }
        public string Task { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public string Dadesc { get; set; }
        public int SubDaid { get; set; }
        public int? DasubNum { get; set; }
        public string SubDadesc { get; set; }
        public int Tid { get; set; }
        public int? TaskDaid { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Tabbrev { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public bool Critical { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public byte[] Taskts { get; set; }
    }
}
