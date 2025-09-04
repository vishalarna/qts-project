using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_Segments_NercStandards:Common.Entity
    {
        public int SegmentId { get; set; }
        public bool Standards { get; set; }
        public bool OperatingTopic { get; set; }
        public bool Simulation { get; set; }
        public bool ProfessionalCredit { get; set; }
    }
}
