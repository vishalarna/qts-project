using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.NotificationRecipietSpecs
{
    public class NotificationRecipietAttemptDateRequiredSpec : ISpecification<NotificationRecipiet>
    {
        public bool IsSatisfiedBy(NotificationRecipiet entity, params object[] args)
        {
            return entity.AttemptDate != null && entity.AttemptDate != default(DateTime);
        }
    }
}
