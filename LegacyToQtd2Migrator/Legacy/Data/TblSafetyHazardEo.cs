using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSafetyHazardEo
    {
        public int Shzskid { get; set; }
        public int Skid { get; set; }
        public int Shzid { get; set; }

        public virtual TblSafetyHazard Shz { get; set; }
        public virtual TblSkillsKnowledge Sk { get; set; }
    }
}
