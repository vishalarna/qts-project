using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsSkbyPosition
    {
        public int Skid { get; set; }
        public string CurrentUser { get; set; }
        public string Sknumber { get; set; }
        public string Skdesc { get; set; }
        public int? Cnum { get; set; }
        public int? CsubNum { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public int Pid { get; set; }
        public string Pabbrev { get; set; }
        public int? Pnum { get; set; }
        public bool Inactive { get; set; }
    }
}
