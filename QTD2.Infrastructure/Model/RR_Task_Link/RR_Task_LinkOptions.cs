using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_Task_Link
{
    public class RR_Task_LinkOptions
    {
        public int RegRequirementId { get; set; }

        public int[] TaskIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
