using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsSkillsKnowledge
    {
        public int Skid { get; set; }
        public string TopicNum { get; set; }
        public string TopicDesc { get; set; }
        public string SkillNum { get; set; }
        public string SkillDesc { get; set; }
        public int? Cid { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public bool Inactive { get; set; }
    }
}
