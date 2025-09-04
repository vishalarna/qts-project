using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsMainTask
    {
        public int Tid { get; set; }
        public int? Daid { get; set; }
        public int? Tnum { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string TaskNumber { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public string Tcriteria { get; set; }
    }
}
