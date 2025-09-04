using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCoSk
    {
        public int Coskid { get; set; }
        public int Coid { get; set; }
        public int Skid { get; set; }

        public virtual TblContentObject Co { get; set; }
        public virtual TblSkillsKnowledge Sk { get; set; }
    }
}
