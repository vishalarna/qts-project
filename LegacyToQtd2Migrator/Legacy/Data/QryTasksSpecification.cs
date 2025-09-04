using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryTasksSpecification
    {
        public int Tid { get; set; }
        public int? Tid2 { get; set; }
        public string Num { get; set; }
        public string Tdesc { get; set; }
        public int? TsubNum { get; set; }
        public int? Tnum { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public string MainDesc { get; set; }
        public string MainNum { get; set; }
        public bool Selected { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public bool? Critical { get; set; }
        public string CurrentUser { get; set; }
    }
}
