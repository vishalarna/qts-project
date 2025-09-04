using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblClassStudent
    {
        public int Clid { get; set; }
        public int Eid { get; set; }
        public string CompGrade { get; set; }
        public DateTime? SecondDate { get; set; }
        public float? PartialStd { get; set; }
        public float? PartialSim { get; set; }
        public float? PartialTotalCehs { get; set; }
        public float? PartialExtra { get; set; }
        public byte[] Ts { get; set; }
        public float? PartialReg { get; set; }
        public float? PartialReg2 { get; set; }
        public float? PartialOther { get; set; }
        public float? PartialTotal { get; set; }
        public float? Score { get; set; }
        public string ReasonWo { get; set; }
        public bool? DisclaimerSigned { get; set; }

        public virtual TblClass Cl { get; set; }
        public virtual TblEmployee EidNavigation { get; set; }
    }
}
