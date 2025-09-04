using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsNewOrModifiedTask
    {
        public int SubDaid { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public int? Tnum { get; set; }
        public string Dadesc { get; set; }
        public int TaskId { get; set; }
        public int Daid { get; set; }
        public string Tdesc { get; set; }
        public int DriverId { get; set; }
        public int IssueId { get; set; }
    }
}
