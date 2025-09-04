using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.RR_IssuingAuthority;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RegulatoryRequirementCompact
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }

        public bool active { get; set; }

        public int IssuingAuthorityId { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string Number { get; set; }

        public string HyperLink { get; set; }

        public string FileName { get; set; }

        public virtual RR_IssuingAuthorityCompact IssuingAuthorityCompact { get; set; }

        public RegulatoryRequirementCompact()
        {
        }

        public RegulatoryRequirementCompact(string description, string title, int id, int issuingAuthorityId, bool active, string revisionNumber, DateTime? effectiveDate, string number, string hyperLink, string fileName)
        {
            Description = description;
            Title = title;
            Id = id;
            IssuingAuthorityId = issuingAuthorityId;
            this.active = active;
            RevisionNumber = revisionNumber;
            EffectiveDate = effectiveDate;
            Number = number;
            HyperLink = hyperLink;
            FileName = fileName;
        }
    }
}
