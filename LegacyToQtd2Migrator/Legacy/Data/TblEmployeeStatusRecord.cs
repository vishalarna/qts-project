using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmployeeStatusRecord
    {
        public int Id { get; set; }
        public int? Eid { get; set; }
        public bool? PrevStatus { get; set; }
        public bool? NewStatus { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Comments { get; set; }
        public bool? SaveHistory { get; set; }
        public DateTime? ChangedOn { get; set; }
    }
}
