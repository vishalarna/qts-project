using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Tool_OperationSpecs
{
    public class ActionItem_Tool_Operation_ActionItemIdRequiredSpec : ISpecification<ActionItem_Tool_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_Tool_Operation entity, params object[] args)
        {
            return entity.ActionItemId > 0;
        }
    }
}