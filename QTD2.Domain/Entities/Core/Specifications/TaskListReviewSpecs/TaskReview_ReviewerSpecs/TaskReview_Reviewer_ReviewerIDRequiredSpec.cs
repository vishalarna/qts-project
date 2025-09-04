using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskReview_ReviewerSpecs
{
   public class TaskReview_Reviewer_ReviewerIDRequiredSpec : ISpecification<TaskReview_Reviewer>
    {
        public bool IsSatisfiedBy(TaskReview_Reviewer entity, params object[] args)
        {
            return entity.ReviewerId > 0;
        }
    }
}