using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class SysTblReport
    {
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public int? SortOrder { get; set; }
        public int? Section { get; set; }
        public bool Display { get; set; }
        public bool ShowToEmp { get; set; }
    }
}
