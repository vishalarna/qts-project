using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILA_Develop : Common.Entity
    {
        public int? ILAId { get; set; }
        public bool? IsPracticeTeachRequired { get; set; }
        public bool? IsPracticeTeachCompleted { get; set; }
        public bool? IsInstructorCheckListRequired { get; set; }
        public bool? IsInstructorCheckListCompleted { get; set; }
        public bool? SubmitForApproval { get; set; }
        public string DevelopResult { get; set; }
        public string InstructorComments { get; set; }
        public string ReviewerComments { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
