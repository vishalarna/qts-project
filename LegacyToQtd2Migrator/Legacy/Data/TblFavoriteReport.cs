using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblFavoriteReport
    {
        public string ReportTitle { get; set; }
        public int Eid { get; set; }
        public string Header { get; set; }
        public string CriteriaForm { get; set; }
    }
}
