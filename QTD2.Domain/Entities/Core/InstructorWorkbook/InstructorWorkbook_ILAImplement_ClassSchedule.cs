using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class InstructorWorkbook_ILAImplement_ClassSchedule : Common.Entity 
    {
        public int ILAId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? InstructorId { get; set; }
        public int? LocationId { get; set; }
        public bool? IsInstructorEnrolled { get; set; }
        public bool? IsTestLinked { get; set; }
        public bool? IsRetakeLinked { get; set; }
        public int? TestId { get; set; }
        public int? RetakeTestId { get; set; }
        public int? EvaluationId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual StudentEvaluationForm StudentEvaluationForm { get; set; }
        public virtual Location Location { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
        public virtual Test Test { get; set; }
    }
}
