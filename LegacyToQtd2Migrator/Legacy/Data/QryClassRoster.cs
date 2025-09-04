using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryClassRoster
    {
        public string Name { get; set; }
        public string Enum { get; set; }
        public string Cornum { get; set; }
        public int Clid { get; set; }
        public int? Corid { get; set; }
        public DateTime? Cldate { get; set; }
        public string CompGrade { get; set; }
        public string Lcdesc { get; set; }
        public string Inname { get; set; }
        public string Nercid { get; set; }
        public string Cordesc { get; set; }
        public string Oname { get; set; }
        public string Pabbrev { get; set; }
        public int Eid { get; set; }
        public string NerccertNum { get; set; }
    }
}
