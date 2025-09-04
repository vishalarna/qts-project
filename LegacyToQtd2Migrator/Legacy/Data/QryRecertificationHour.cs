using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryRecertificationHour
    {
        public int? Eid { get; set; }
        public double? CompNerc { get; set; }
        public double? CompSim { get; set; }
        public double? CompCeh { get; set; }
        public int? Nercpolicy { get; set; }
        public int? CredReqHours { get; set; }
        public int? TotalReqCehs { get; set; }
    }
}
