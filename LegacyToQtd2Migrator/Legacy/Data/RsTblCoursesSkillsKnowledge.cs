using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblCoursesSkillsKnowledge
    {
        public int Skid { get; set; }
        public int Corid { get; set; }
        public byte[] Ts { get; set; }
        public int? Sequence { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblSkillsKnowledge Sk { get; set; }
    }
}
