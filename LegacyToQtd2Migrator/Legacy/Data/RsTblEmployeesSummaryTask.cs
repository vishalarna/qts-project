using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblEmployeesSummaryTask
    {
        public int RsId { get; set; }
        public int Eid { get; set; }
        public int TidSum { get; set; }
        public byte[] Ts { get; set; }
        public bool Complete { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
    }
}
