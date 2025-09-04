using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_ILADesign_Procedures :Common.Entity
    {
        public int ILAId { get; set; }
        public int ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
