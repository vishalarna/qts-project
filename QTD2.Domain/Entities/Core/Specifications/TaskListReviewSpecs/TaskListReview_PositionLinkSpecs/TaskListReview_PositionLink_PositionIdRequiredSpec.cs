using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_PositionLinkSpecs
{
    public class TaskListReview_PositionLink_PositionIdRequiredSpec : ISpecification<TaskListReview_PositionLink>
    {
        public bool IsSatisfiedBy(TaskListReview_PositionLink entity, params object[] args)
        {
            return entity.PositionId > 0;
        }
    }
}
