using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.ActivityNotificationSpecs
{
    public class ActivityNotificationTitleRequiredSpec : ISpecification<ActivityNotification>
    {
        public bool IsSatisfiedBy(ActivityNotification entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Title);
        }
    }
}
