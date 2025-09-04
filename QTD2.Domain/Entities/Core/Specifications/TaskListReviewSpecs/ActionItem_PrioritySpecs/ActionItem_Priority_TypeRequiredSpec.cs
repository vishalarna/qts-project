using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_PrioritySpecs
{
    public class ActionItem_Priority_TypeRequiredSpec : ISpecification<ActionItem_Priority>
    {
        public bool IsSatisfiedBy(ActionItem_Priority entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Type);
        }
    }
}