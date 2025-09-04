using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_SafetyHazard_Link
{
    public class RR_SafetyHazard_LinkOptions
    {
        public int RegulatoryRequirementId { get; set; }

        public int[] SafetyHazardIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
