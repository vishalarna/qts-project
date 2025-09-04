using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILADesign_TargetAudience : Common.Entity
    {
        public int ILAId { get; set; }
        public int TargetAudienceId { get; set; }
        public bool? OtherOption { get; set; }
        public string InstructorEmail { get; set; }
        public virtual NERCTargetAudience NERCTargetAudience { get; set; }

        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
