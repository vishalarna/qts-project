using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsSubDuty
    {
        public int Daid { get; set; }
        public string SubDuty { get; set; }
        public string Daletter { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string Dadesc { get; set; }
        public bool? SkillRelated { get; set; }
    }
}
