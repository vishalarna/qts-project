using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillQualificationSpecs
{
    public class SkillQualificationEmployeeIdRequiredSpec : ISpecification<SkillQualification>
    {
        public bool IsSatisfiedBy(SkillQualification entity, params object[] args)
        {
            return entity.EmployeeId > 0;
        }
    }
}
