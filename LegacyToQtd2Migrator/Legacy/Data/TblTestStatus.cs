using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTestStatus
    {
        public TblTestStatus()
        {
            TblTests = new HashSet<TblTest>();
        }

        public byte TestStatusId { get; set; }
        public string TestStatus { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblTest> TblTests { get; set; }
    }
}
