using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_RegulatoryRequirement_OperationSpecs
{
     public class ActionItem_RegulatoryRequirement_Operation_OperationTypeIDRequiredSpec : ISpecification<ActionItem_RegulatoryRequirement_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_RegulatoryRequirement_Operation entity, params object[] args)
        {
            return entity.OperationTypeId > 0;
        }
    }
}