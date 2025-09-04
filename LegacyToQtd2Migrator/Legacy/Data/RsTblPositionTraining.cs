using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblPositionTraining
    {
        public int Pid { get; set; }
        public int Corid { get; set; }
        public string Tyear { get; set; }
        public string Ttype { get; set; }
        public byte[] Ts { get; set; }
        public int? InitialVersion { get; set; }
        public int? Ptpid { get; set; }

        public virtual TblCourse Cor { get; set; }
    }
}
