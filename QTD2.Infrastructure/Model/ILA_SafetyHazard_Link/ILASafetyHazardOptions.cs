using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_SafetyHazard_Link
{
    public class ILASafetyHazardOptions
    {
        public int[] ILAIds { get; set; }

        public int SafetyHazardId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
