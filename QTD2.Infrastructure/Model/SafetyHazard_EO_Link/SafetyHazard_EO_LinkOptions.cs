using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SafetyHazard_EO_Link
{
    public class SafetyHazard_EO_LinkOptions
    {
        public int SafetyHazardId { get; set; }

        public int[] EOIDs { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
