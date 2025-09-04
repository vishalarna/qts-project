using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblReportingMgrEmployee
    {
        public int Rmid { get; set; }
        public int Eid { get; set; }

        public virtual TblReportingManager Rm { get; set; }
    }
}
