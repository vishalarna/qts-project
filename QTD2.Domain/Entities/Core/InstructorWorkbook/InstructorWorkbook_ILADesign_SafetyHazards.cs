using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILADesign_SafetyHazards : Common.Entity
    {
        public int ILAId { get; set; }
        public int SHZID { get; set; }

        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
        public virtual SaftyHazard SaftyHazard { get; set; }
    }
}
