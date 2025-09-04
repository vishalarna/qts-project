using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblFormQuestion
    {
        public TblFormQuestion()
        {
            TblStudentFormsAnswers = new HashSet<TblStudentFormsAnswer>();
        }

        public int Fqid { get; set; }
        public int? Fid { get; set; }
        public float? Fqnum { get; set; }
        public string Fqdesc { get; set; }
        public bool Fqinactive { get; set; }
        public int? Fsid { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblForm FidNavigation { get; set; }
        public virtual ICollection<TblStudentFormsAnswer> TblStudentFormsAnswers { get; set; }
    }
}
