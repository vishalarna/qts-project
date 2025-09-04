using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassesSelfReg
    {
        public int Srid { get; set; }
        public int Eid { get; set; }
        public int? Clid { get; set; }
        public int Corid { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public int SelfRegStatus { get; set; }
        public bool? IsRemoved { get; set; }
    }
}
