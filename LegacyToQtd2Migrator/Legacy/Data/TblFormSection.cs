using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblFormSection
    {
        public int Fsid { get; set; }
        public int? Fid { get; set; }
        public float? Fsnum { get; set; }
        public string Fsdesc { get; set; }
        public bool Fsexpired { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblForm FidNavigation { get; set; }
    }
}
