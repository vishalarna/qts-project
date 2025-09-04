using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblEmpsetting
    {
        public int EmpsettingId { get; set; }
        public string EmpsettingDesc { get; set; }
        public string EmpsettingValue { get; set; }
        public string EmpsettingUnit { get; set; }
    }
}
