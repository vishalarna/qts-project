using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassTest
    {
        public int Clid { get; set; }
        public int TestId { get; set; }
        public int Sequence { get; set; }
        public bool? IsPreTest { get; set; }

        public virtual TblClass Cl { get; set; }
    }
}
