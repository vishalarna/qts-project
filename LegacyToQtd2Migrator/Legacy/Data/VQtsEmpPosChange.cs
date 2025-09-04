using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsEmpPosChange
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Eid { get; set; }
        public int? Pid { get; set; }
    }
}
