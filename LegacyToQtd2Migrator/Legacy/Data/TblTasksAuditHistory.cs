using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTasksAuditHistory
    {
        public int Tahid { get; set; }
        public int? Thid { get; set; }
        public int Tid { get; set; }
        public int? Daid { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Tabbrev { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public bool Critical { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public bool? Inactive { get; set; }
        public DateTime? Dra { get; set; }
        public string Raby { get; set; }
        public string ChangeEntity { get; set; }
        public int? ChangeType { get; set; }
        public string ChangeDesc { get; set; }
        public int? PosId { get; set; }
        public int? ProcId { get; set; }
        public bool? CriticalPrev { get; set; }
        public bool? CriticalNew { get; set; }
    }
}
