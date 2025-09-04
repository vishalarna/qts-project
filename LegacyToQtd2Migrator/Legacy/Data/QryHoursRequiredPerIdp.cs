using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryHoursRequiredPerIdp
    {
        public int? Eid { get; set; }
        public string Tyear { get; set; }
        public double? SumOfTotalHours { get; set; }
        public double? SumOfTotalCeh { get; set; }
        public double? SumOfCehReg { get; set; }
        public double? SumOfOther { get; set; }
        public double? SumOfEmergencyOpsHours { get; set; }
        public double? SumOfSimHours { get; set; }
    }
}
