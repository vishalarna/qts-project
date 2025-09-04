using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_GeneralReviewerSpecs
{
    public class TaskListReview_GeneralReviewer_TaskListReviewIdRequiredSpec : ISpecification<TaskListReview_GeneralReviewer>
    {
        public bool IsSatisfiedBy(TaskListReview_GeneralReviewer entity, params object[] args)
        {
            return entity.TaskListReviewId > 0;
        }
    }
}
