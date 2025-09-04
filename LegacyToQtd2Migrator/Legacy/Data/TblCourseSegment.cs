using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCourseSegment
    {
        public int Csid { get; set; }
        public int? Corid { get; set; }
        public bool ChkStds { get; set; }
        public bool ChkOper { get; set; }
        public bool ChkSim { get; set; }
        public bool ChkEop { get; set; }
        public bool ChkProf { get; set; }
        public double? Stds { get; set; }
        public double? Oper { get; set; }
        public double? Sim { get; set; }
        public double? Eop { get; set; }
        public double? Prof { get; set; }
        public double? Total { get; set; }
        public string Content { get; set; }
        public double? Num1 { get; set; }
        public double? Num2 { get; set; }
        public bool Chk1 { get; set; }
        public bool Chk2 { get; set; }
        public byte[] Ts { get; set; }
        public bool? PartialCredit { get; set; }
        public string SegmentTitle { get; set; }
        public string SegmentNumber { get; set; }
        public string SegProcedures { get; set; }
        public string SegProceduresNerc { get; set; }
        public int SegDisplayOrder { get; set; }

        public virtual TblCourse Cor { get; set; }
    }
}
