using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ViewQtsCategory
    {
        public int Cid { get; set; }
        public int? Cnum { get; set; }
        public string CsubNum { get; set; }
        public string Cdesc { get; set; }
        public string CsubDesc { get; set; }
    }
}
