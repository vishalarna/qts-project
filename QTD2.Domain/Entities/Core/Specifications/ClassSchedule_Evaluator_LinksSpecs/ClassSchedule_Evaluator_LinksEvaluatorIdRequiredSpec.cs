using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_Evaluator_LinksSpecs
{
    public class ClassSchedule_Evaluator_LinksEvaluatorIdRequiredSpec : ISpecification<ClassSchedule_Evaluator_Link>
    {
        public bool IsSatisfiedBy(ClassSchedule_Evaluator_Link entity, params object[] args)
        {
            return entity.EvaluatorId > 0;
        }
    }
}
