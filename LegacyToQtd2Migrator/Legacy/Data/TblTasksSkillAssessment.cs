using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTasksSkillAssessment
    {
        public int Tsid { get; set; }
        public int Tid { get; set; }
        public int TypeId { get; set; }
        public string Descr { get; set; }
    }
}
