using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public class InstructorWorkbook_ILAEvaluation_DefaultView : Common.Entity
    {
        public string DefaultView { get; set; }
        public int? UserId { get; set; }

    }
}
