using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReviewSpecs
{
    public class TaskListReview_EndDateRequiredSpec : ISpecification<TaskListReview>
    {
        public bool IsSatisfiedBy(TaskListReview entity, params object[] args)
        {
            return entity.EndDate != System.DateTime.MinValue;
        }
    }
}
