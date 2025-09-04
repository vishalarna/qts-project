using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsEmployeePositionTrainingPreviou
    {
        public int Eid { get; set; }
        public int Tid { get; set; }
        public bool Complete { get; set; }
        public string Comments { get; set; }
        public string TaskNum { get; set; }
        public string Tdesc { get; set; }
        public string Position { get; set; }
        public string Employee { get; set; }
        public int? Tnum { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public int Daid { get; set; }
        public bool Critical { get; set; }
        public int RsId { get; set; }
        public int? SortOrder { get; set; }
        public DateTime? Dt { get; set; }
        public int Pid { get; set; }
        public bool? Trainee { get; set; }
    }
}
