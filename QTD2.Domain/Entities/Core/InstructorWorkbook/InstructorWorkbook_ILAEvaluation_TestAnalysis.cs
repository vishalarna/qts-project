using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class InstructorWorkbook_ILAEvaluation_TestAnalysis : Common.Entity 
    {
        public int ILAId { get; set; }
        public int TestItemId { get; set; }
        public bool? PassItemDifficulty { get; set; }
        public bool? PassItemDiscrimination { get; set; }
        public bool? PassItemDistractors { get; set; }
        public string Notes { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
        public virtual TestItem TestItem { get; set; }
    }
}
