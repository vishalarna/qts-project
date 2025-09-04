using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblPositionTrainingProgram
    {
        public int Ptpid { get; set; }
        public int Pid { get; set; }
        public decimal? Revision { get; set; }
        public DateTime? Tpdate { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramType { get; set; }
        public DateTime? TpendDate { get; set; }
    }
}
