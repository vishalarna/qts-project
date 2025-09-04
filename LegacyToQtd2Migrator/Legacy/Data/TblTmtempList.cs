using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTmtempList
    {
        public int Id { get; set; }
        public int? Tmid { get; set; }
        public string TrainingModule { get; set; }
        public string CurrentUser { get; set; }
        public int? Selection { get; set; }
        public DateTime? Modifiedtime { get; set; }
    }
}
