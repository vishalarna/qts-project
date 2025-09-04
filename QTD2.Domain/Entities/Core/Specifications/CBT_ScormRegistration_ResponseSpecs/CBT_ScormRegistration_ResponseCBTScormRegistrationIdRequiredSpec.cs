using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.CBT_ScormRegistration_ResponseSpecs
{
    public class CBT_ScormRegistration_ResponseCBTScormRegistrationIdRequiredSpec : ISpecification<CBT_ScormRegistration_Response>
    {
        public bool IsSatisfiedBy(CBT_ScormRegistration_Response entity, params object[] args)
        {
            return entity.CBTScormRegistrationId > 0;
        }
    }
}
