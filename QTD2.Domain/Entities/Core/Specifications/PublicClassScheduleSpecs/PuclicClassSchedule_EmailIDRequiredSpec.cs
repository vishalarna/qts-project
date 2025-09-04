using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.PublicClassScheduleSpecs
{
    public class PuclicClassSchedule_EmailIDRequiredSpec : ISpecification<PublicClassScheduleRequest>
    {
        public bool IsSatisfiedBy(PublicClassScheduleRequest entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.EmailAddress);
        }
    }
}
