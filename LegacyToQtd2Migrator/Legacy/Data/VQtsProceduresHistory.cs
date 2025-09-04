using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class VQtsProceduresHistory
    {
        public int? Prid { get; set; }
        public int? Iaid { get; set; }
        public string Prseries { get; set; }
        public string Prtitle { get; set; }
        public float? Prrevision { get; set; }
        public DateTime? PrrevDate { get; set; }
        public string PrrevisedBy { get; set; }
    }
}
