using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsaw2Evidence
    {
        public int Rsaweid { get; set; }
        public int Rsawid { get; set; }
        public string RsawefileName { get; set; }
        public string Rsawetitle { get; set; }
        public string Rsawerevision { get; set; }
        public DateTime? Rsawedate { get; set; }
        public int? Rsawepage { get; set; }
        public string Rsawesection { get; set; }
        public string RsawesectionTitle { get; set; }
        public string Rsawedescription { get; set; }
        public string RsawedocSection { get; set; }
        public byte[] Ts { get; set; }
    }
}
