using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_OperationType_LinksSpecs
{
   public class ActionItem_OperationType_Links_OperationTypeIDRequiredSpec : ISpecification<ActionItem_OperationType_Links>
    {
        public bool IsSatisfiedBy(ActionItem_OperationType_Links entity, params object[] args)
        {
            return entity.OperationTypeId > 0;
        }
    }
}