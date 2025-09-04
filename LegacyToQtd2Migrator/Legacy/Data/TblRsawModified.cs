using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsawModified
    {
        public int Rsawmid { get; set; }
        public int? Rsawid { get; set; }
        public DateTime? RsawmdateModified { get; set; }
        public DateTime? RsawmdateVerified { get; set; }
        public string Rsawmoperator { get; set; }
        public string Rsawmdescription { get; set; }
        public string Rsawmmethod { get; set; }
        public byte[] Ts { get; set; }
    }
}
