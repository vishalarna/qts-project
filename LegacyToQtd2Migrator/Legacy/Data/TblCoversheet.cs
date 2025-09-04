using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCoversheet
    {
        public int Cvid { get; set; }
        public int Cvnum { get; set; }
        public int CvtypeId { get; set; }
        public string Cvtitle { get; set; }
        public bool? Cvinactive { get; set; }
        public string Cvinstructions { get; set; }
    }
}
