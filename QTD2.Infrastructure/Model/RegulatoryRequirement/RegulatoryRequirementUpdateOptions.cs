using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RegulatoryRequirementUpdateOptions
    {
        public int IssuingAuthorityId { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RevisionNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public byte[] Uploads { get; set; }
    }
}
