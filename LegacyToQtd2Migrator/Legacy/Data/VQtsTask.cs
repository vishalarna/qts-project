using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTask
    {
        public int Tid { get; set; }
        public string Num { get; set; }
        public string SubNum { get; set; }
        public string SubDesc { get; set; }
        public string Desc { get; set; }
        public int Daid { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
    }
}
