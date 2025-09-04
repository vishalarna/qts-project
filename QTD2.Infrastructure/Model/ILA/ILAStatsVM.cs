using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class ILAStatsVM
    {
        public int Providers { get; set; }

        public int Topics { get; set; }

        public int ActiveILAs { get; set; }

        public int DraftILAs { get; set; }

        public int PublishedILAs { get; set; }

        public int UnlinkedTopicILAs { get; set; }
    }
}
