using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTotalCehscScheduled
    {
        public int Eid { get; set; }
        public float? TotalCeh { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public DateTime? ClassDate { get; set; }
    }
}
