using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public partial class InstructorWorkbook_ProspectiveILA_Reviewers : Common.Entity
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public int ProspectiveILAId { get; set; }

        public virtual Instructor Reviewer { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
