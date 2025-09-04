using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblStudentFormsAnswer
    {
        public int Sfaid { get; set; }
        public int? Sfid { get; set; }
        public int? Fqid { get; set; }
        public float? Sfascore { get; set; }
        public string Sfacomments { get; set; }
        public int? Fqaid { get; set; }
        public float? SfascoreOverride { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblFormQuestion Fq { get; set; }
        public virtual TblStudentForm Sf { get; set; }
    }
}
