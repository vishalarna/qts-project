using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskNumberChange
    {
        public DateTime? OldDate { get; set; }
        public DateTime? NewDate { get; set; }
        public int? Tid { get; set; }
        public string NewNumber { get; set; }
        public string OldNumber { get; set; }
    }
}
