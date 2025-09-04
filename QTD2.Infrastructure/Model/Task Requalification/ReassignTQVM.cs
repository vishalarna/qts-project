using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class ReassignTQVM
    {
        public int[] TQIds { get; set; }

        public int EvaluatorId { get; set; }

        public int[] ReassignEvalIds { get; set; }
    }
}
