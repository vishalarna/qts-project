using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.ClassSchedule_SelfRegistrationSpecs
{
    public class ClassSchedule_SelfRegistrationOptionsClassScheduleIdRequiredSpec : ISpecification<ClassSchedule_SelfRegistrationOptions>
    {
        public bool IsSatisfiedBy(ClassSchedule_SelfRegistrationOptions entity, params object[] args)
        {
            return entity.ClassScheduleId > 0;
        }
    }
}
