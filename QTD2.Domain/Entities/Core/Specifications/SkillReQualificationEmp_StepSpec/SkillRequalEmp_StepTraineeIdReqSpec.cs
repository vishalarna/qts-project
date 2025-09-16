using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.SkillReQualificationEmp_StepSpec
{
    public class SkillRequalEmp_StepTraineeIdReqSpec : ISpecification<SkillReQualificationEmp_Step>
    {
        public bool IsSatisfiedBy(SkillReQualificationEmp_Step entity, params object[] args)
        {
            return entity.TraineeId > 0;
        }
    }
}
