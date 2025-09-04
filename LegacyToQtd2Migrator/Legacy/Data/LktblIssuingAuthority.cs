using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblIssuingAuthority
    {
        public int Iaid { get; set; }
        public string Iatitle { get; set; }
        public int? Ianum { get; set; }
        public string Iaaddress1 { get; set; }
        public string Iaaddress2 { get; set; }
        public string Iacity { get; set; }
        public string Iastate { get; set; }
        public string Iazip { get; set; }
        public string Iaphone { get; set; }
        public string Iafax { get; set; }
        public string Iaemail { get; set; }
        public string Iaperson { get; set; }
        public bool Iadefault { get; set; }
        public byte[] Ts { get; set; }
    }
}
