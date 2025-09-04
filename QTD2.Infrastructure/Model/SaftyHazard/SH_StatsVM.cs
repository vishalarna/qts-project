using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SH_StatsVM
    {
        public int CatActive { get; set; }

        public int CatInactive { get; set; }

        public int SHActive { get; set; }

        public int SHInactive { get; set; }

        public int SHNotLinkedToTasks { get; set; }

        public int SHNotLinkedToEOs { get; set; }

        public int SHNotLinkedToProcs { get; set; }

        public int SHNotLinkedToRRs { get; set; }

        public int SHNotLinkedToILAs { get; set; }
    }
}
