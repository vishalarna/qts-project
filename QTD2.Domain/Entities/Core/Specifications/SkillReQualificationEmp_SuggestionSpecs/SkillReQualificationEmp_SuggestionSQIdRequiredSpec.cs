using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_SuggestionSpecs
{
    public class SkillReQualificationEmp_SuggestionSQIdRequiredSpec : ISpecification<SkillReQualificationEmp_Suggestion>
    {
        public bool IsSatisfiedBy(SkillReQualificationEmp_Suggestion entity, params object[] args)
        {
            return entity.SkillQualificationId > 0;
        }
    }
}
