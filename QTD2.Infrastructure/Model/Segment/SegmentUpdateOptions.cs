using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Segment
{
    public class SegmentUpdateOptions
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public bool IsNercStandard { get; set; }
        public bool IsPartialCredit { get; set; }

        public bool IsNercOperatingTopics { get; set; }

        public bool IsNercSimulation { get; set; }

        public string Content { get; set; }

        public byte[] Uploads { get; set; }
    }
}
