using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsShazByPosition
    {
        public string Shznum { get; set; }
        public string Shztitle { get; set; }
        public bool? Inactive { get; set; }
        public int Shzid { get; set; }
        public string CurrentUser { get; set; }
        public int Pid { get; set; }
        public string Pabbrev { get; set; }
        public int? Pnum { get; set; }
    }
}
