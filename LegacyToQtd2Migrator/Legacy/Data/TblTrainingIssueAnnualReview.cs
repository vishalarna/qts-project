using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingIssueAnnualReview
    {
        public TblTrainingIssueAnnualReview()
        {
            TblTrainingIssueSupportingDocs = new HashSet<TblTrainingIssueSupportingDoc>();
        }

        public int Arid { get; set; }
        public int ProgramType { get; set; }
        public int PositionId { get; set; }
        public int? TypeYear { get; set; }
        public DateTime? TypeStartDate { get; set; }
        public string EvaluatorNames { get; set; }
        public DateTime? EvalStartDate { get; set; }
        public DateTime? EvalEndDate { get; set; }
        public string Summary { get; set; }
        public string Background { get; set; }
        public string Purpose { get; set; }
        public string Method { get; set; }
        public string Design { get; set; }
        public string Materials { get; set; }
        public string Implementation { get; set; }
        public string Evaluation { get; set; }
        public string Conclusions { get; set; }
        public DateTime? Dra { get; set; }
        public string Raby { get; set; }
        public byte[] Ts { get; set; }
        public string EvalTraineeLrn { get; set; }
        public int? InitVersion { get; set; }
        public string SupportingDocs { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string SignatureName { get; set; }
        public string SignatureTitle { get; set; }

        public virtual ICollection<TblTrainingIssueSupportingDoc> TblTrainingIssueSupportingDocs { get; set; }
    }
}
