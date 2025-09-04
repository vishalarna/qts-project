using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsLatestScheduled
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public DateTime? LastScheduled { get; set; }
    }
}
