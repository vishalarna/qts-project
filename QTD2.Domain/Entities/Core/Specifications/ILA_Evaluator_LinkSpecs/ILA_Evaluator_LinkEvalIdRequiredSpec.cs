using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ILA_Evaluator_LinkSpecs
{
    public class ILA_Evaluator_LinkEvalIdRequiredSpec : ISpecification<ILA_Evaluator_Link>
    {
        public bool IsSatisfiedBy(ILA_Evaluator_Link entity, params object[] args)
        {
            return entity.EvaluatorId > 0;
        }
    }
}
