using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTempOjthistory
    {
        public int Ojthid { get; set; }
        public int? ParentId { get; set; }
        public bool Complete { get; set; }
        public string Comments { get; set; }
        public DateTime? EvalDate { get; set; }
        public string Evaluator { get; set; }
        public string ObservPeriod { get; set; }
        public string EvalMethod { get; set; }
        public byte[] UpsizeTs { get; set; }
    }
}
