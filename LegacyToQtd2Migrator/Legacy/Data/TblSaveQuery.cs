using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSaveQuery
    {
        public int QueryId { get; set; }
        public string QueryName { get; set; }
        public string QueryText { get; set; }
        public DateTime QueryDate { get; set; }
        public Guid? QueryId1 { get; set; }
        public string GroupBy { get; set; }
        public int? TabId { get; set; }
    }
}
