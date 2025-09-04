using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILADesign_EnablingObjectives : Common.Entity
    {
        public int ILAId { get; set; }
        public int EnablingObjectiveId { get; set; }
        public string EnablingObjectivesDescription { get; set; }
        public int ILAObjectiveOrder { get; set; }
        public virtual EnablingObjective EnablingObjective { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
