using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwClass
    {
        public int Clid { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Corid { get; set; }
        public DateTime? StartDate { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public int? Lcid { get; set; }
        public int? Inid { get; set; }
        public bool? SelfReg { get; set; }
        public DateTime? SelfRegEndDate { get; set; }
        public int? TotalSeats { get; set; }
        public bool? SelfRegOpen { get; set; }
        public int? StartAmPm { get; set; }
        public int? EndAmPm { get; set; }
        public string StartTimeStr { get; set; }
        public string EndTimeStr { get; set; }
    }
}
