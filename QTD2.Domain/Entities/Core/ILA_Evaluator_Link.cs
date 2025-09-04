using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Evaluator_Link : Common.Entity
    {
        public int ILAId { get; set; }

        public int EvaluatorId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Employee Evaluator { get; set; }

        public ILA_Evaluator_Link()
        {
        }

        public ILA_Evaluator_Link(int iLAId, int evaluatorId)
        {
            ILAId = iLAId;
            EvaluatorId = evaluatorId;
        }
    }
}
