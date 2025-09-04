using System;

namespace QTD2.Infrastructure.Model.Procedure
{
    public class ProcedureUpdateOptions
    {
        public int IssuingAuthorityId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte[] ProceduresFileUpload { get; set; }

        public string ChangeNotes { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublished { get; set; }

        public bool isSignificant { get; set; }

        public string File { get; set; }

        public string Hyperlink { get; set; }

        public string FileName { get; set; }
    }
}
