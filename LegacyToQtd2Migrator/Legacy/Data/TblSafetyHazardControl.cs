using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardControl
    {
        public int Shzctid { get; set; }
        public int Shzid { get; set; }
        public int Cnum { get; set; }
        public string Shzcontrol { get; set; }

        public virtual TblSafetyHazard Shz { get; set; }
    }
}
