using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsPositionsTask
    {
        public int Pid { get; set; }
        public int Tid { get; set; }
        public double? AvgDifficulty { get; set; }
        public double? AvgImportance { get; set; }
        public double? AvgFrequency { get; set; }
        public int? StatusDefault { get; set; }
        public int? StatusFinal { get; set; }
        public bool Critical { get; set; }
        public int? Tpid { get; set; }
        public byte[] Ts { get; set; }
    }
}
