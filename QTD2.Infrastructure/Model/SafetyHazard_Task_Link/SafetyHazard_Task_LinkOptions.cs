using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SafetyHazard_Task_Link
{
    public class SafetyHazard_Task_LinkOptions
    {
        public int SaftyHazardId { get; set; }

        public int[] TaskIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
