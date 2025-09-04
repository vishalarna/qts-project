using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTaskLinkage
    {
        public int Lid { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public int PidImpacted { get; set; }
        public int TidImpacted { get; set; }

        public virtual RstblPositionsTask RstblPositionsTask { get; set; }
    }
}
