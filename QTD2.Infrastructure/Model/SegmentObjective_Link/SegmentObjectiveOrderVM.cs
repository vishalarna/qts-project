using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SegmentObjective_Link
{
    public class SegmentObjectiveOrderVM
    {
        public int? SegmentObjectiveLinkId { get; set; } 
        public int ObjectiveId { get; set; }
        public string Type { get; set; } 
        public int Order { get; set; } 

        public SegmentObjectiveOrderVM() { }
    }
}
