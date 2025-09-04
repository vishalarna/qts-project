using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RR_StatsVM
    {
        public int IAActive { get; set; }

        public int IAInactive { get; set; }

        public int RRActive { get; set; }

        public int RRInactive { get; set; }

        public int RRNotLinkedToTasks { get; set; }

        public int RRNotLinkedToEOs { get; set; }

        public int RRNotLinkedToSHs { get; set; }

        public int RRNotLinkedToProcs { get; set; }

        public int RRNotLinkedToILAs { get; set; }
    }
}
