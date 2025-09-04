using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SegmentObjective_Link
{
    public class SegmentObjective_LinkOptions
    {
        public int SegmentId { get; set; }

        public int[] TaskIds { get; set; }

        public int[] EnablingObjectiveIds { get; set; }

        public int[] CustomEOIds { get; set; }
    }
}
