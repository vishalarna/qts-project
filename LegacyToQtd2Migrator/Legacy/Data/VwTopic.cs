using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwTopic
    {
        public int Skid { get; set; }
        public string Topic { get; set; }
        public string Skdesc { get; set; }
        public int? Cnum { get; set; }
        public int? CsubNum { get; set; }
        public int? Sknum { get; set; }
        public int? Cid { get; set; }
    }
}
