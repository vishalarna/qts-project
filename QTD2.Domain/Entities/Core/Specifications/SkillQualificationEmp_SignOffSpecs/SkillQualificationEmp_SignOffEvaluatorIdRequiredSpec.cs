using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmp_SignOffSpecs
{
    public class SkillQualificationEmp_SignOffEvaluatorIdRequiredSpec : ISpecification<SkillQualificationEmp_SignOff>
    {
        public bool IsSatisfiedBy(SkillQualificationEmp_SignOff entity, params object[] args)
        {
            return entity.EvaluatorId > 0;
        }
    }
}
