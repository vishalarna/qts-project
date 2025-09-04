using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSaveQueryCriterion
    {
        public Guid? FkQueryId { get; set; }
        public string CriteriaVal { get; set; }
        public string CriteriaText { get; set; }
    }
}
