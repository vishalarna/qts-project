using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsDutyArea
    {
        public int Daid { get; set; }
        public string Num { get; set; }
        public string SubNum { get; set; }
        public int? Danum { get; set; }
        public int? DasubNum { get; set; }
        public string Dadesc { get; set; }
        public string Daletter { get; set; }
        public bool? SkillRelated { get; set; }
    }
}
