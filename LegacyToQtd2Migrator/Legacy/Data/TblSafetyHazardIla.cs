using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardIla
    {
        public int Shzcorid { get; set; }
        public int Corid { get; set; }
        public int Shzid { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblSafetyHazard Shz { get; set; }
    }
}
