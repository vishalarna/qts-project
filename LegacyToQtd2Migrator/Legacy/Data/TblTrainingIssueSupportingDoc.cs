using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingIssueSupportingDoc
    {
        public int Arsdid { get; set; }
        public int Arid { get; set; }
        public string SupportingDocs { get; set; }
        public string Hyperlink { get; set; }
        public int? Arsdnum { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblTrainingIssueAnnualReview Ar { get; set; }
    }
}
