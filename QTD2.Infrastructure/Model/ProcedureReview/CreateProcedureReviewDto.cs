using QTD2.Domain.Entities.Core;
using System;

namespace QTD2.Infrastructure.Model.ProcedureReview
{
    public class CreateProcedureReviewDto
    {
        public int ProcedureId { get; set; }
        public string ProcedureReviewTitle { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ProcedureReviewInstructions { get; set; }
        public bool IsEmployeeShowResponses { get; set; }
        public string ProcedureReviewAcknowledgement { get; set; }
        public ProcedureReviewExtensionType? ExtensionType { get; set; }
        public int? ExtensionAmount { get; set; }
    }
}
