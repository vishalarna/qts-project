using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblFormQuestionsAnswer
    {
        public int Fqaid { get; set; }
        public string Fqadesc { get; set; }
        public float? Fqascore { get; set; }
        public byte[] Ts { get; set; }
    }
}
