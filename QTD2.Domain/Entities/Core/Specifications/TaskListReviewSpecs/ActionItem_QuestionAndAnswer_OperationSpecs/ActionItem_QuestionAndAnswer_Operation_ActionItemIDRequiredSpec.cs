using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TaskListReviewSpecs.ActionItem_QuestionAndAnswer_OperationSpecs
{
   public class ActionItem_QuestionAndAnswer_Operation_ActionItemIDRequiredSpec : ISpecification<ActionItem_QuestionAndAnswer_Operation>
    {
        public bool IsSatisfiedBy(ActionItem_QuestionAndAnswer_Operation entity, params object[] args)
        {
            return entity.ActionItemId > 0;
        }
    }
}