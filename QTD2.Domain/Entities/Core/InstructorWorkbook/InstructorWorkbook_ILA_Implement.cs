using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
  public partial class InstructorWorkbook_ILA_Implement :Common.Entity
    {
        public int ILAId { get; set; }
        public bool? SubmitRoasterForReview { get; set; }
        public string ImplementResult { get; set; }
        public string InstructorComments { get; set; }
        public string ReviewerComments { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }

    }
}
