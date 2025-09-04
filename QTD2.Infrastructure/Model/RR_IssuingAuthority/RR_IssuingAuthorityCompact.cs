using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.RegulatoryRequirement;

namespace QTD2.Infrastructure.Model.RR_IssuingAuthority
{
    public class RR_IssuingAuthorityCompact
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }

        public bool Active { get; set; }

        public List<RegulatoryRequirementCompact> regulatoryRequirementCompacts { get; set; } = new List<RegulatoryRequirementCompact>();

        public RR_IssuingAuthorityCompact()
        {
        }

        public RR_IssuingAuthorityCompact(string description, string title, int id, bool active)
        {
            Description = description;
            Title = title;
            Id = id;
            Active = active;
        }
    }
}
