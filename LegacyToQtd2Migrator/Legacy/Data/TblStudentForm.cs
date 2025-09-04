using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblStudentForm
    {
        public TblStudentForm()
        {
            TblStudentFormsAnswers = new HashSet<TblStudentFormsAnswer>();
        }

        public int Sfid { get; set; }
        public int? Eid { get; set; }
        public int? Fid { get; set; }
        public int? Clid { get; set; }
        public bool Sfcomplete { get; set; }
        public int? RecordId { get; set; }
        public DateTime? EvalDate { get; set; }
        public int? SelfPacedCorid { get; set; }
        public int? Students { get; set; }
        public string EevaluationMethod { get; set; }
        public string Eevaluator { get; set; }
        public byte[] Ts { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ExpireOverride { get; set; }

        public virtual TblForm FidNavigation { get; set; }
        public virtual ICollection<TblStudentFormsAnswer> TblStudentFormsAnswers { get; set; }
    }
}
