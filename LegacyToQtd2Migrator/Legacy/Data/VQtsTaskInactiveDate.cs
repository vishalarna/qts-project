using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskInactiveDate
    {
        public int? Tid { get; set; }
        public DateTime? StartInactive { get; set; }
        public DateTime EndInactive { get; set; }
    }
}
