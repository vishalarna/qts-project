using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RegRequirement_EO_Link
{
    public class RegRequirement_EO_LinkOptions
    {
        public int RegulatoryRequirementId { get; set; }

        public int[] EOIds { get; set; }
    }
}
