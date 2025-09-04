using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardAbatement
    {
        public int Shzabid { get; set; }
        public int Shzid { get; set; }
        public int Anum { get; set; }
        public string Shzabatement { get; set; }

        public virtual TblSafetyHazard Shz { get; set; }
    }
}
