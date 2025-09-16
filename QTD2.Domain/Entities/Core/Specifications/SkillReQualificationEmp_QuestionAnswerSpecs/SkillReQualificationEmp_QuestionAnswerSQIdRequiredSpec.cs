using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_QuestionAnswerSpecs
{
    public class SkillReQualificationEmp_QuestionAnswerSQIdRequiredSpec : ISpecification<SkillReQualificationEmp_QuestionAnswer>
    {
        public bool IsSatisfiedBy(SkillReQualificationEmp_QuestionAnswer entity, params object[] args)
        {
            return entity.SkillQualificationId > 0;
        }
    }
}
