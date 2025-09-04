using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsLatestCompleted
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public DateTime? LastCompleted { get; set; }
    }
}
