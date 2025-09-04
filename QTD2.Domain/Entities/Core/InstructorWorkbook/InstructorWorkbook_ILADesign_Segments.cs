using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public partial class InstructorWorkbook_ILADesign_Segments : Common.Entity
    {
        public int? SegmentTime { get; set; }
        public string SegmentText { get; set; }
        public string SegmentTitle { get; set; }
        public int? SegmentOrder { get; set; }
        public int ILAId { get; set; }
        public int? SegmentId { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
        public virtual Segment Segment { get; set; }
    }
}
