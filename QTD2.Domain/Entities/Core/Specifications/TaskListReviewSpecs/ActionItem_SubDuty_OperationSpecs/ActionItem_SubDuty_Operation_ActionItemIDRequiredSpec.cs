using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_SubDuty_OperationSpecs
{
  public  class ActionItem_SubDuty_Operation_ActionItemIDRequiredSpec : ISpecification<ActionItem_SubDuty_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_SubDuty_Operation entity, params object[] args)
        {
            return entity.ActionItemId > 0;
        }
    }
}