using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_RegulatoryRequirement : Common.Entity
    {
        public Version_RegulatoryRequirement(RegulatoryRequirement rr)
        {
            RegulatoryRequirementId = rr.Id;
            IssuingAuthorityId = rr.IssuingAuthorityId;
            Number = rr.Number;
            Title = rr.Title;
            Description = rr.Description;
            RevisionNumber = rr.RevisionNumber;
            EffectiveDate = rr.EffectiveDate;
            Uploads = rr.Uploads;
            HyperLink = rr.HyperLink;
        }

        public Version_RegulatoryRequirement()
        {
        }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public virtual ICollection<Version_Task_RR_Link> Version_Task_RR_Links { get; set; } = new List<Version_Task_RR_Link>();

        public virtual ICollection<Version_EnablingObjective_RRLink> Version_EnablingObjective_RRLinks { get; set; } = new List<Version_EnablingObjective_RRLink>();

        public int RegulatoryRequirementId { get; set; }

        public int IssuingAuthorityId { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public byte[] Uploads { get; set; }

        public string HyperLink { get; set; }
    }
}
