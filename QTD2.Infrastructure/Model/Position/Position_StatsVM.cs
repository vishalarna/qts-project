using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Position
{
    public class Position_StatsVM
    {
        public int PosActive { get; set; }

        public int PosInactive { get; set; }

        public int PosNotLinkedToTasks { get; set; }

        public int PosNotLinkedToSQs { get; set; }

        public int PosNotLinkedToEmployees { get; set; }
    }
}
