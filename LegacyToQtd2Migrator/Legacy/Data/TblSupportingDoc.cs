using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSupportingDoc
    {
        public int Sdid { get; set; }
        public int MainId { get; set; }
        public string SupportingDocs { get; set; }
        public string Hyperlink { get; set; }
        public int? Sdnum { get; set; }
        public string Sdtable { get; set; }
        public byte[] Ts { get; set; }
    }
}
