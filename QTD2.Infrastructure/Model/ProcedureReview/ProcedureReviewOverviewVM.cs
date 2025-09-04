using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ProcedureReview
{
    public class ProcedureReviewOverviewVM
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsStarted { get; set; }
        public bool IsPublished { get; set; }
        public bool Active { get; set; }
        public string ProcedureNumber { get; set; }
        public string ProcedureTitle { get; set; }
        public string ProcedureReviewTitle { get; set; }
        public string IssuingAuthorityTitle { get; set; }
        public int IssuingAuthorityId { get; set; }

        public ProcedureReviewOverviewVM(int id, DateTime startDateTime, DateTime endDateTime, bool isStarted, bool isPublished, bool active, string procedureNumber, string procedureTitle, string procedureReviewTitle, string issuingAuthorityTitle,int issuingAuthorityId)
        {
            Id = id;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            IsStarted = isStarted;
            IsPublished = isPublished;
            Active = active;
            ProcedureNumber = procedureNumber;
            ProcedureTitle = procedureTitle;
            ProcedureReviewTitle = procedureReviewTitle;
            IssuingAuthorityTitle = issuingAuthorityTitle;
            IssuingAuthorityId = issuingAuthorityId;
        }

        public ProcedureReviewOverviewVM() { }
    }
}
