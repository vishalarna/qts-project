using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_EO_Link
{
    public class RR_EO_LinkOptions
    {
        public int RegulatoryRequirementId { get; set; }

        public int[] EOIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
