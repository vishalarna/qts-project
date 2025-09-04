using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSkProcedure
    {
        public int Skid { get; set; }
        public int ProcId { get; set; }

        public virtual TblProcedure Proc { get; set; }
        public virtual TblSkillsKnowledge Sk { get; set; }
    }
}
