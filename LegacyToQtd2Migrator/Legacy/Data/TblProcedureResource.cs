using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblProcedureResource
    {
        public int Prrid { get; set; }
        public int? Prid { get; set; }
        public int? Prrnum { get; set; }
        public string Prrtitle { get; set; }
        public string Prrsection { get; set; }
        public string Prrchapter { get; set; }
        public string Prrhyperlink { get; set; }
        public string PrrhyperlinkText { get; set; }
        public string Prrcomments { get; set; }
        public byte[] Ts { get; set; }
    }
}
