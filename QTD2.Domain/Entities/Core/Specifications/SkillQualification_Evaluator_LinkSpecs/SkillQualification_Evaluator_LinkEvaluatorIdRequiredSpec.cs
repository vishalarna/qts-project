using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillQualification_Evaluator_LinkSpecs
{
    public class SkillQualification_Evaluator_LinkEvaluatorIdRequiredSpec : ISpecification<SkillQualification_Evaluator_Link>
    {
        public bool IsSatisfiedBy(SkillQualification_Evaluator_Link entity, params object[] args)
        {
            return entity.EvaluatorId > 0;
        }
    }
}
