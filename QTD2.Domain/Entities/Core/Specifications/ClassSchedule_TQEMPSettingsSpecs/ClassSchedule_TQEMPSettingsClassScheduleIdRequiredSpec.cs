using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_TQEMPSettingsSpecs
{
    public class ClassSchedule_TQEMPSettingsClassScheduleIdRequiredSpec : ISpecification<ClassSchedule_TQEMPSetting>
    {
        public bool IsSatisfiedBy(ClassSchedule_TQEMPSetting entity, params object[] args)
        {
            return entity.ClassScheduleId > 0;
        }
    }
}
