using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.EnablingObjective_QuestionSpecs
{
    public class EnablingObjective_Question_EOIdRequiredSpec : ISpecification<EnablingObjective_Question>
    {
        public bool IsSatisfiedBy(EnablingObjective_Question entity, params object[] args)
        {
            return entity.EnablingObjectiveId > 0;
        }
    }
}
