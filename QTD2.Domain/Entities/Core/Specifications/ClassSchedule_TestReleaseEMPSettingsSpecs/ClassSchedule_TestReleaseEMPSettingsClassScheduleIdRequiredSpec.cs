using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TestReleaseEMPSettingsSpecs
{
    public class ClassSchedule_TestReleaseEMPSettingsClassScheduleIdRequiredSpec : ISpecification<ClassSchedule_TestReleaseEMPSetting>
    {
        public bool IsSatisfiedBy(ClassSchedule_TestReleaseEMPSetting entity, params object[] args)
        {
            return entity.ClassScheduleId > 0;
        }
    }
}
