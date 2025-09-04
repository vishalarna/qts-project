using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_RegRequirement_Link
{
    public class ILA_RegRequirement_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] RegulatoryRequirementIds { get; set; }
    }
}
