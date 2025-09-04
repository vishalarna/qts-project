using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCarryOverHour
    {
        public int Eid { get; set; }
        public DateTime NerccertIssueDate { get; set; }
        public float? CoStandards { get; set; }
        public float? CoOpTopics { get; set; }
        public float? CoSimulation { get; set; }
        public float? CoTotalCeh { get; set; }
        public float? Co4 { get; set; }
        public float? Co5 { get; set; }
        public string CoComments { get; set; }
        public DateTime? Dru { get; set; }
        public string Ruby { get; set; }
        public byte[] Ts { get; set; }
    }
}
