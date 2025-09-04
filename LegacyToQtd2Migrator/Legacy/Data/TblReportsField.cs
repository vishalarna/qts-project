using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblReportsField
    {
        public string ReportAcronum { get; set; }
        public string FieldName { get; set; }
        public string FieldText { get; set; }
        public float? FieldNumber { get; set; }
    }
}
