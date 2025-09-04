using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIdp
    {
        public int Idpid { get; set; }
        public int? Eid { get; set; }
        public string Tyear { get; set; }
        public int? Corid { get; set; }
        public int? Pid { get; set; }
        public DateTime? Dra { get; set; }
        public string Ttype { get; set; }
        public DateTime? CompDate { get; set; }
        public string CompLoc { get; set; }
        public string CompInstructor { get; set; }
        public string CompGrade { get; set; }
        public DateTime? ReqCompDate { get; set; }
        public int? DefClid { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblEmployee EidNavigation { get; set; }
    }
}
