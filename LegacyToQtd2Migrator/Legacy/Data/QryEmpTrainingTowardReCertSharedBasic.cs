using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryEmpTrainingTowardReCertSharedBasic
    {
        public int Eid { get; set; }
        public DateTime? CompDate { get; set; }
        public int? Tyear { get; set; }
        public int? NerccertArea { get; set; }
        public DateTime? NerccertIssueDate { get; set; }
        public DateTime? NerccertExpDate { get; set; }
        public int? Corid { get; set; }
    }
}
