using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryXmltranscript
    {
        public string Nercid { get; set; }
        public DateTime? Cldate { get; set; }
        public string NerccertNum { get; set; }
        public string Cornum { get; set; }
        public int? Suid { get; set; }
        public float CehNerc { get; set; }
        public float SimHours { get; set; }
        public float TotalCeh { get; set; }
        public int Clid { get; set; }
        public bool SelfPased { get; set; }
        public bool ActPartialCredits { get; set; }
    }
}
