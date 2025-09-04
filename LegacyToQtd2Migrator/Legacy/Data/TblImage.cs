using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblImage
    {
        public int Imid { get; set; }
        public int? Imtype { get; set; }
        public int? ImparentId { get; set; }
        public byte[] Imbody { get; set; }
        public string Imdesc { get; set; }
        public byte[] Ts { get; set; }
    }
}
