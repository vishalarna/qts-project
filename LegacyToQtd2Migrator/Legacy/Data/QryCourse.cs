using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryCourse
    {
        public int Corid { get; set; }
        public string Cornum { get; set; }
        public string Cordesc { get; set; }
        public string Suname { get; set; }
        public string Nercid { get; set; }
        public bool SelfPased { get; set; }
    }
}
