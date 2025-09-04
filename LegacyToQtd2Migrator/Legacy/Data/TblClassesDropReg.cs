using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassesDropReg
    {
        public int Id { get; set; }
        public int Eid { get; set; }
        public int? Clid { get; set; }
        public int Corid { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
    }
}
