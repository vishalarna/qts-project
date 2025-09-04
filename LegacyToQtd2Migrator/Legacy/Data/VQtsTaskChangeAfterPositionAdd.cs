using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskChangeAfterPositionAdd
    {
        public DateTime? Thdate { get; set; }
        public int Tid { get; set; }
        public int Pid { get; set; }
        public string Thnum { get; set; }
        public string Thstatement { get; set; }
        public string Thconditions { get; set; }
        public string Thtools { get; set; }
        public string Threferences { get; set; }
        public string Thcriteria { get; set; }
        public string ThprocList { get; set; }
        public string ThrevisedBy { get; set; }
        public string Thnote { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public string Tstandards { get; set; }
        public string Pdesc { get; set; }
        public int? Tnum { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewComment { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public bool? RequalRequired { get; set; }
        public bool? ThposHistory { get; set; }
    }
}
