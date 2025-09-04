using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTchistory
    {
        public int Tchid { get; set; }
        public int? ParentId { get; set; }
        public bool Complete { get; set; }
        public string Comments { get; set; }
        public DateTime? EvalDate { get; set; }
        public byte[] Ts { get; set; }
        public string Tdesc { get; set; }
        public string Tnum { get; set; }
        public string Lanid { get; set; }
        public bool HasTraineeSigned { get; set; }
    }
}
