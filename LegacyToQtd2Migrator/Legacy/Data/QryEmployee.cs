using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryEmployee
    {
        public int Eid { get; set; }
        public string Employee { get; set; }
        public int? Pid { get; set; }
        public string Pdesc { get; set; }
    }
}
