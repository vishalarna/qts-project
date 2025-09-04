using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsaw2Change
    {
        public int Rsawcid { get; set; }
        public int? Rsawid { get; set; }
        public DateTime? RsawcdateIdentified { get; set; }
        public DateTime? RsawcdateImplimented { get; set; }
        public string Rsawcdescription { get; set; }
        public byte[] Ts { get; set; }
    }
}
