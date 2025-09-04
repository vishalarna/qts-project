using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_Segments_LinkObjectives:Common.Entity
    {
        public string Type { get; set; }
        public int? SegmentId { get; set; }
        public int? ObjectiveId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int Order { get; set; }
    }
}
