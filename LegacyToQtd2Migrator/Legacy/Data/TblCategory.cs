using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblSkillsKnowledges = new HashSet<TblSkillsKnowledge>();
        }

        public int Cid { get; set; }
        public int? Cnum { get; set; }
        public int? CsubNum { get; set; }
        public string Cdesc { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblSkillsKnowledge> TblSkillsKnowledges { get; set; }
    }
}
