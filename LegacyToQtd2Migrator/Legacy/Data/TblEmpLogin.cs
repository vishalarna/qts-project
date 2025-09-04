using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmpLogin
    {
        public int Id { get; set; }
        public int? Eid { get; set; }
        public DateTime? LoginDate { get; set; }
        public bool? LoginStatus { get; set; }
    }
}
