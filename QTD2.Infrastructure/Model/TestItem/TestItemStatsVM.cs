using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemStatsVM
    {
        public int Active { get; set; }

        public int Inactive { get; set; }

        public int NotLinkedTests { get; set; }

        public int NotLinkedEOs { get; set; }
    }
}
