using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RR_IssuingAuthority : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<RegulatoryRequirement> RegulatoryRequirements { get; set; } = new List<RegulatoryRequirement>();

        public virtual ICollection<RR_IssuingAuthority_StatusHistory> RR_IssuingAuthority_StatusHistories { get; set; } = new List<RR_IssuingAuthority_StatusHistory>();

        public RR_IssuingAuthority(string title, string description, string website, DateTime? effectiveDate, string notes)
        {
            Title = title;
            Description = description;
            Website = website;
            EffectiveDate = effectiveDate;
            Notes = notes;
        }

        public RR_IssuingAuthority()
        {
        }
    }
}
