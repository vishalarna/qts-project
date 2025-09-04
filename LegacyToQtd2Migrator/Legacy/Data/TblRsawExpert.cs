using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblRsawExpert
    {
        public int Rsawxid { get; set; }
        public int Rsawid { get; set; }
        public string Rsawxname { get; set; }
        public string Rsawxtitle { get; set; }
        public string Rsawxorganization { get; set; }
        public string Rsawxrequirement { get; set; }
        public byte[] Ts { get; set; }
    }
}
