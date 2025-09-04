using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblProcedure
    {
        public TblProcedure()
        {
            TblSkProcedures = new HashSet<TblSkProcedure>();
            TblSmudcourseDesignProcedures = new HashSet<TblSmudcourseDesignProcedure>();
            TblTrainingEvalIssues = new HashSet<TblTrainingEvalIssue>();
        }

        public int Prid { get; set; }
        public int? Iaid { get; set; }
        public string Prseries { get; set; }
        public int? Prnum { get; set; }
        public string Prtitle { get; set; }
        public float? Prrevision { get; set; }
        public DateTime? PrrevDate { get; set; }
        public bool Prinactive { get; set; }
        public string PrrevisedBy { get; set; }
        public byte[] Ts { get; set; }
        public DateTime? PrStartDate { get; set; }
        public DateTime? PrEndDate { get; set; }
        public string PrEmpReleaseText { get; set; }
        public bool? PrOnlineStatus { get; set; }

        public virtual ICollection<TblSkProcedure> TblSkProcedures { get; set; }
        public virtual ICollection<TblSmudcourseDesignProcedure> TblSmudcourseDesignProcedures { get; set; }
        public virtual ICollection<TblTrainingEvalIssue> TblTrainingEvalIssues { get; set; }
    }
}
