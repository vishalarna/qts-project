using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblActionTaken
    {
        public int Atid { get; set; }
        public int? AtparentId { get; set; }
        public string Atdate { get; set; }
        public string Atdesc { get; set; }
        public string Atdetails { get; set; }
        public byte[] Ts { get; set; }
    }
}
