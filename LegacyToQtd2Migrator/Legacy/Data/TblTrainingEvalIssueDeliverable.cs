using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingEvalIssueDeliverable
    {
        public int DeliverableId { get; set; }
        public string DeliverableDesc { get; set; }
        public DateTime? DateAssigned { get; set; }
        public int? Priority { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateReviewed { get; set; }
        public string Status { get; set; }
        public bool? Completed { get; set; }
        public string AssignedTo { get; set; }
        public int IssueId { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
