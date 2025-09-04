using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblUserActivityLog
    {
        public int Alid { get; set; }
        public string Username { get; set; }
        public string AlformText { get; set; }
        public string AlformName { get; set; }
        public string Alaction { get; set; }
        public DateTime Aldate { get; set; }
    }
}
