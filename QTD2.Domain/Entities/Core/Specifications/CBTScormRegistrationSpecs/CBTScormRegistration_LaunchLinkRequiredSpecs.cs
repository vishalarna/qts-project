using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.CBTScormRegistrationSpecs
{
 public  class CBTScormRegistration_LaunchLinkRequiredSpecs  : ISpecification<CBT_ScormRegistration>
    {
        public bool IsSatisfiedBy(CBT_ScormRegistration entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.LaunchLink);
        }
    }
}