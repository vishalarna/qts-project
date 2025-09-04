using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.NotificationRecipietSpecs
{
    public class NotificationRecipietToPersonIdRequiredSpec : ISpecification<NotificationRecipiet>
    {
        public bool IsSatisfiedBy(NotificationRecipiet entity, params object[] args)
        {
            return entity.ToPersonId > 0;
        }
    }
}
