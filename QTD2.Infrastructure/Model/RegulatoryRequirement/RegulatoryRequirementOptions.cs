using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RegulatoryRequirementOptions
    {
        public int[] RegulatoryRequirementIds { get; set; }

        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public int RegulatoryRequirementId { get; set; }
    }
}
