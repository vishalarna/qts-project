using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModuleSk
    {
        public int Tmid { get; set; }
        public int Skid { get; set; }

        public virtual TblSkillsKnowledge Sk { get; set; }
        public virtual TblTrainingModule Tm { get; set; }
    }
}
