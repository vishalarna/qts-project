using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblEmployeesTask
    {
        public int Eid { get; set; }
        public int Tid { get; set; }
        public bool Complete { get; set; }
        public string Comments { get; set; }
        public int RsId { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
