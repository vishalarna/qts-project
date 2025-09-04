using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsEobyTask
    {
        public int Tid { get; set; }
        public string Skill { get; set; }
        public string Skdesc { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public int? TsubNum { get; set; }
        public int? Tnum { get; set; }
        public int? Cnum { get; set; }
        public int? CsubNum { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public string Daletter { get; set; }
        public bool? Inactive { get; set; }
    }
}
