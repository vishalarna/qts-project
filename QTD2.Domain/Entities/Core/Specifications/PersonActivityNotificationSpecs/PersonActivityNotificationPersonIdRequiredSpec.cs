using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.PersonActivityNotificationSpecs
{
    public class PersonActivityNotificationPersonIdRequiredSpec : ISpecification<PersonActivityNotification>
    {
        public bool IsSatisfiedBy(PersonActivityNotification entity, params object[] args)
        {
            return entity.PersonId > 0;
        }
    }
}
