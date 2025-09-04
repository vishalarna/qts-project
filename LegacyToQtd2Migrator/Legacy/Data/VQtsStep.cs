using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsStep
    {
        public int Tid { get; set; }
        public int? Daid { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string StepNumber { get; set; }
        public string StepDesc { get; set; }
    }
}
