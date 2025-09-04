using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskStatementChange
    {
        public DateTime? Olddate { get; set; }
        public DateTime? NewDate { get; set; }
        public int? Tid { get; set; }
        public string NewStatement { get; set; }
        public string OldStatement { get; set; }
    }
}
