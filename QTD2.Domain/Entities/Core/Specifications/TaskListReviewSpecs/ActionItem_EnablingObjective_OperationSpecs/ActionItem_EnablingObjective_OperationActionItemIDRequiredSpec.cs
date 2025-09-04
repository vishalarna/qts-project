using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_EnablingObjective_OperationSpecs
{
    public class ActionItem_EnablingObjective_OperationActionItemIDRequiredSpec : ISpecification<ActionItem_EnablingObjective_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_EnablingObjective_Operation entity, params object[] args)
        {
            return entity.ActionItemId > 0;
        }
    }
}