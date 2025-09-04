using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SaftyHazard_RR_Link
{
    public class SaftyHazard_RR_LinkOptions
    {
        public int[] RegulatoryRequirementIds { get; set; }

        public int SafetyHazardId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
