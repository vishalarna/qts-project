using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblTasksSkillsKnowledge
    {
        public int Tid { get; set; }
        public int Skid { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblSkillsKnowledge Sk { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
