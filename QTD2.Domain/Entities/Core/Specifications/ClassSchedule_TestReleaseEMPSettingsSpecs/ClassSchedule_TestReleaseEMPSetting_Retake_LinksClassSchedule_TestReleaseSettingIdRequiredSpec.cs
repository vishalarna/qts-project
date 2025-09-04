using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TestReleaseEMPSettingsSpecs
{
    public class ClassSchedule_TestReleaseEMPSetting_Retake_LinksClassSchedule_TestReleaseSettingIdRequiredSpec : ISpecification<ClassSchedule_TestReleaseEMPSetting_Retake_Link>
    {
        public bool IsSatisfiedBy(ClassSchedule_TestReleaseEMPSetting_Retake_Link entity, params object[] args)
        {
            return entity.ClassSchedule_TestReleaseSettingId > 0;
        }
    }
}