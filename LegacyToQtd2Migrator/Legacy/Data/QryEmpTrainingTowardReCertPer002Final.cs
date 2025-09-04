using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryEmpTrainingTowardReCertPer002Final
    {
        public string Name { get; set; }
        public string EmpNo { get; set; }
        public string CertType { get; set; }
        public double HrsCompleted { get; set; }
        public int Eid { get; set; }
        public bool? EwillNotBeRecertified { get; set; }
        public bool InActive { get; set; }
        public bool? EnotCertified { get; set; }
    }
}
