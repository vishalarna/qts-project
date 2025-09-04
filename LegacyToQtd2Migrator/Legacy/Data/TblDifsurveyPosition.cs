using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblDifsurveyPosition
    {
        public int Difpid { get; set; }
        public int Difprjid { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public double? AvgDifficulty { get; set; }
        public double? AvgImportance { get; set; }
        public double? AvgFrequency { get; set; }
        public int? StatusDefault { get; set; }
        public int? StatusFinal { get; set; }
        public string Comments { get; set; }
        public int? CtEmp { get; set; }
    }
}
