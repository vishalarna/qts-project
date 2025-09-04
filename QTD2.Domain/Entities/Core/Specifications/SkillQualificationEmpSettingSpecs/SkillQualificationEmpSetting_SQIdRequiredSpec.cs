using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.SkillQualificationEmpSettingSpecs
{
    public class SkillQualificationEmpSetting_SQIdRequiredSpec : ISpecification<SkillQualificationEmpSetting>
    {
        public bool IsSatisfiedBy(SkillQualificationEmpSetting entity, params object[] args)
        {
            return entity.SkillQualificationId > 0;
        }
    }
}
