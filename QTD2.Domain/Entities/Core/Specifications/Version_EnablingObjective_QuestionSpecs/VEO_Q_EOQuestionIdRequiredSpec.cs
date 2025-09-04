using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.Version_EnablingObjective_QuestionSpecs
{
    public class VEO_Q_EOQuestionIdRequiredSpec : ISpecification<Version_EnablingObjective_Question>
    {
        public bool IsSatisfiedBy(Version_EnablingObjective_Question entity, params object[] args)
        {
            return entity.EOQuestionId > 0;
        }
    }
}
