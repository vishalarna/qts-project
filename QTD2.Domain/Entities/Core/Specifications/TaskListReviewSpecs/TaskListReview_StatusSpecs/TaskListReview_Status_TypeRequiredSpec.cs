using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.TaskListReview_StatusSpecs
{
    public class TaskListReview_Status_TypeRequiredSpec : ISpecification<TaskListReview_Status>
    {
        public bool IsSatisfiedBy(TaskListReview_Status entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Type);
        }
    }
}