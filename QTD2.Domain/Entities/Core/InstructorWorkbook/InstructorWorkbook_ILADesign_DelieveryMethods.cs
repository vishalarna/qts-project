using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILADesign_DelieveryMethods : Common.Entity
    {
        public int ILAId { get; set; }
        public int MID { get; set; }
        public bool? OtherOption { get; set; }
        public string InstructorEmail { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }

        public InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }

    }
}

