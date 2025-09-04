using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryTasksNoStep
    {
        public int Tid { get; set; }
        public string TfullNum { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tcriteria { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public int? Tnum { get; set; }
        public int Daid { get; set; }
        public int? TsubNum { get; set; }
    }
}
