using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryLatestCompletedGrade
    {
        public int Eid { get; set; }
        public int? Corid { get; set; }
        public DateTime? LastCompleted { get; set; }
        public string CompGrade { get; set; }
    }
}
