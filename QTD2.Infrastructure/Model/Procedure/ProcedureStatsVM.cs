using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure
{
    public class ProcedureStatsVM
    {
        public int IAActive { get; set; }

        public int IAInactive { get; set; }

        public int ProcActive { get; set; }

        public int ProcInactive { get; set; }

        public int ProcNotLinkedToTasks { get; set; }

        public int ProcNotLinkedToEOs { get; set; }

        public int ProcNotLinkedToSHs { get; set; }

        public int ProcNotLinkedToRRs { get; set; }

        public int ProcNotLinkedToILAs { get; set; }
    }
}
