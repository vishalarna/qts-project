using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_EnablingObjective_Link
{
    public class ILA_EnablingObjective_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] EnablingObjectiveIds { get; set; }
        public bool? IsIncludeMetaEO { get; set; } = false;
    }
}
