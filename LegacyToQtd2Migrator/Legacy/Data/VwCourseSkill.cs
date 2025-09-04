using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwCourseSkill
    {
        public int Skid { get; set; }
        public string Skill { get; set; }
        public string Skdesc { get; set; }
        public int Corid { get; set; }
        public int? Cnum { get; set; }
        public int? CsubNum { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public int Cid { get; set; }
        public int? Sequence { get; set; }
        public byte[] Ts { get; set; }
        public bool Inactive { get; set; }
    }
}
