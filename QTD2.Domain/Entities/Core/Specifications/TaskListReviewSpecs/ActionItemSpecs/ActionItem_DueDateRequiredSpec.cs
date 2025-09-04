using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItemSpecs
{
    public class ActionItem_DueDateRequiredSpec : ISpecification<ActionItem>
    {
        public bool IsSatisfiedBy(ActionItem entity, params object[] args)
        {
            return entity.DueDate != System.DateTime.MinValue;
        }
    }
}