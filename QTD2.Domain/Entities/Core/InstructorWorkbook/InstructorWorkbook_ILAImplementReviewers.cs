using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class InstructorWorkbook_ILAImplementReviewers : Common.Entity
    {
        public int ILAId { get; set; }
        public string ReviewerIds { get; set; }

        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
