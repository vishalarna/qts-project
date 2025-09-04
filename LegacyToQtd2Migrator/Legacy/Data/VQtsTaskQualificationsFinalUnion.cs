using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsTaskQualificationsFinalUnion
    {
        public int Eid { get; set; }
        public int Tid { get; set; }
        public DateTime? QualDate { get; set; }
        public string Cordesc { get; set; }
        public string Cornum { get; set; }
    }
}
