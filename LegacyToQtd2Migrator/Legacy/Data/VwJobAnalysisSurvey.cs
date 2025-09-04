using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwJobAnalysisSurvey
    {
        public int Pid { get; set; }
        public string Pdesc { get; set; }
        public int? Daid { get; set; }
        public string Da { get; set; }
        public string TaskNum { get; set; }
        public string Tdesc { get; set; }
        public double? AvgDifficulty { get; set; }
        public double? AvgImportance { get; set; }
        public double? AvgFrequency { get; set; }
        public int? Tnum { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? TsubNum { get; set; }
        public string Daso { get; set; }
        public int? DasubNum { get; set; }
        public int? StatusFinal { get; set; }
        public string DifComments { get; set; }
        public int Tid { get; set; }
        public bool? Inactive { get; set; }
    }
}
