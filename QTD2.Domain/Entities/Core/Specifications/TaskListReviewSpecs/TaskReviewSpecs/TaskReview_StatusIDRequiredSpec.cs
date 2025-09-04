using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskReviewSpecs
{
   public class TaskReview_StatusIDRequiredSpec : ISpecification<TaskReview>
    {
        public bool IsSatisfiedBy(TaskReview entity, params object[] args)
        {
            return entity.StatusId > 0;
        }
    }
}
