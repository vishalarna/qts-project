using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblMenuItem
    {
        public int MuId { get; set; }
        public string MuLabel { get; set; }
        public int MuMenu { get; set; }
        public bool? MuActive { get; set; }
        public int MuParentId { get; set; }
        public string MuForm { get; set; }
        public int? MuList { get; set; }
        public int MuSortOrder { get; set; }
        public int? MuLevel { get; set; }
        public int? MuAccessLevel { get; set; }
        public int? MuUserAccess { get; set; }
        public int? MuQtsmgrAccess { get; set; }
    }
}
