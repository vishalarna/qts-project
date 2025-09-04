using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_TypeSpecs
{
    public class TaskListReview_TypeRequiredSpec : ISpecification<TaskListReview_Type>
    {
        public bool IsSatisfiedBy(TaskListReview_Type entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Type);
        }
    }
}
