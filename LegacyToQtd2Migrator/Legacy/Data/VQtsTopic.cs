using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTopic
    {
        public int Skid { get; set; }
        public string Topic { get; set; }
        public int? Sknum { get; set; }
        public int? Cid { get; set; }
        public string TopicNum { get; set; }
        public string TopicDesc { get; set; }
        public bool Inactive { get; set; }
    }
}
