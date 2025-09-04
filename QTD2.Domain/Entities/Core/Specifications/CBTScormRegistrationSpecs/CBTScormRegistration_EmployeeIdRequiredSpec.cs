using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CBTScormRegistrationSpecs
{
   public class CBTScormRegistration_EmployeeIdRequiredSpec : ISpecification<CBT_ScormRegistration>
    {
        public bool IsSatisfiedBy(CBT_ScormRegistration entity, params object[] args)
        {
            return entity.ClassScheduleEmployeeId > 0;
        }
    }
}
