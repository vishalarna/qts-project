using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_Step_OperationSpecs
{
    public class ActionItem_Step_Operation_OperationTypeIDRequiredSpec : ISpecification<ActionItem_Step_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_Step_Operation entity, params object[] args)
        {
            return entity.OperationTypeId > 0;
        }
    }
}