using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VwCategoryTopicSk
    {
        public int Cid { get; set; }
        public int? Cnum { get; set; }
        public string CatDesc { get; set; }
        public int SubCid { get; set; }
        public int? SubNum { get; set; }
        public string SubCatDesc { get; set; }
        public int TopicSkid { get; set; }
        public int? TopicNum { get; set; }
        public string TopicDesc { get; set; }
        public int Skid { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public string Skdesc { get; set; }
    }
}
