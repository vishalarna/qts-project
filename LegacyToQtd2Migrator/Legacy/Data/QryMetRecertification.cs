using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryMetRecertification
    {
        public int Eid { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public string MetRecert { get; set; }
    }
}
