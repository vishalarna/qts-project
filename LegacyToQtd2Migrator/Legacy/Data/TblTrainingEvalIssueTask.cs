using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingEvalIssueTask
    {
        public int IssueId { get; set; }
        public int TaskId { get; set; }
        public int DriverId { get; set; }
    }
}
